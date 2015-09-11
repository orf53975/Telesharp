using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telesharp.Common;
using Telesharp.Common.BotTypes;
using Telesharp.Common.Interfaces;
using Telesharp.Common.Types;

namespace Telesharp
{
    public class Bot
    {
        private int _threads;

        public void Run()
        {
            if (Settings.InfoToConsole)
            {
                Console.WriteLine("Running bot...");
            }
            _botThread.Start();
        }

        public void RunSync()
        {
            if (Settings.InfoToConsole)
            {
                Console.WriteLine("Running bot...");
            }
            Work();
        }

        public event ParseMessageEventHandler OnParseMessage = delegate { };
        public event BeginInvokeEventHandler BegineInvokeCommand = delegate { };

        private void Work()
        {
            Me = Methods.GetMe();
            if (Me == null)
            {
                Console.WriteLine("I don't know who i'm...");
                Console.WriteLine("We can't continue");
            }
            while (true)
            {
                var updates = Methods.GetUpdates();
                if (updates == null) // If can't get updates
                {
                    continue;
                }
                foreach (var upd in updates)
                {
                    // Invoke event
                    OnParseMessage(this, new ParseMessageEventArgs(upd.Message));

                    // Search command
                    var command = FindCommand(upd.Message);
                    if (command == null) continue; // If command not found - continue

                    // Invoke event
                    var eventargs = new BeginInvokeEventArgs(command, upd.Message);
                    BegineInvokeCommand(this, eventargs);

                    // Check for refuse to execute
                    if (eventargs.Refuse) continue;

                    // Execute command
                    if (!Settings.ExecuteCommandsSync && !eventargs.RunSync) Execute(command, upd.Message);
                    else ExecuteSync(command, upd.Message);
                }
                // Wait to next update...
                Thread.Sleep(Settings.CheckUpdatesInterval);
            }
        }

        private ICommand FindCommand(Message message)
        {
            return Commands.FirstOrDefault(command => Comparer.Compare(command, message));
        }

        private void Execute(ICommand command, Message message)
        {
            if (Settings.MaximumThreadsForCommands > 0 && _threads >= Settings.MaximumThreadsForCommands)
            {
                while (_threads <= Settings.MaximumThreadsForCommands)
                {
                    // Wait, when we can go to execute command
                }
            }
            Task.Factory.StartNew(() =>
            {
                try
                {
                    _threads++;
                    command.Executed(message);
                }
                catch (Exception exc)
                {
                    if (Settings.ExceptionsToConsole) Console.WriteLine("Exception when execute command async: \n" + exc);
                }
                finally
                {
                    _threads--;
                }
            });
        }

        private void ExecuteSync(ICommand command, Message message)
        {
            try
            {
                // Execute command
                command.Executed(message);
            }
            catch (Exception exc)
            {
                if (Settings.ExceptionsToConsole) Console.WriteLine("Exception execute when command: \n" + exc);
            }
        }

        public void WaitToDie()
        {
            while (_botThread.IsAlive)
            {
                Thread.Sleep(1000);
            }
        }

        #region Constructors

        public Bot(string token) : this(new BotSettings(token))
        {
        }

        public Bot(BotSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("Settings can't be null");
            }
            if (settings.Token == null)
            {
                throw new NullReferenceException("No token in settings!");
            }
            Settings = settings;
            Methods = new TelegramBotMethods(this);
            _botThread = new Thread(Work);
            Commands = new List<ICommand>();

            Console.WriteLine("Please know: you're use a alpha version of Telesharp. It's not completed fully");
            Console.WriteLine("--> DaFri Nochiterov, 2015");
        }

        #endregion

        #region Variables

        private readonly bool _botRunAsSync = false;
        private readonly Thread _botThread;
        public List<ICommand> Commands { get; set; }
        public CommandComparer Comparer { get; set; }
        public BotSettings Settings { get; set; }
        public User Me { get; set; }
        public TelegramBotMethods Methods { get; set; }

        public bool BotAlive
        {
            get { return _botRunAsSync || _botThread.IsAlive; }
        }

        #endregion
    }
}