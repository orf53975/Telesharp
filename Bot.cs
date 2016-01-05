using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telesharp.Common;
using Telesharp.Common.BotTypes;
using Telesharp.Common.Interfaces;
using Telesharp.Common.TelesharpTypes;
using Telesharp.Common.Types;

namespace Telesharp
{
	public class Bot
	{
		public void Start()
		{
			if (_botThread?.IsAlive != null && (bool) _botThread?.IsAlive)
				throw new InvalidOperationException("Bot already started!");
			if (_botRunAsSync) throw new InvalidOperationException("Bot already started sync!");
			if (Settings.InfoToConsole)
			{
				Telesharp.Logger.Log(LogType.Info, Settings.Name, "Running bot...");
			}
			if (_botThread == null) PrepareThread();
			_botThread?.Start();
		}

		public void StartSync()
		{
			if (_botRunAsSync) throw new InvalidOperationException("Bot already started!");
			if (_botThread?.IsAlive != null && (bool) _botThread?.IsAlive)
				throw new InvalidOperationException("Bot already started async!");
			if (Settings.InfoToConsole)
			{
				Telesharp.Logger.Log(LogType.Info, Settings.Name, "Running bot sync...");
			}
			_botRunAsSync = true;
			Work();
		}

		public void Stop()
		{
			if (!BotAlive) throw new InvalidOperationException("Bot already stopped!");
			if (_stop) throw new InvalidOperationException("Bot already stops!");
			_stop = true;
			Telesharp.Logger.Log(LogType.Info, "Stopping bot...");
		}

		public event ParseMessageEventHandler OnParseMessage = delegate { };

		public event BeginInvokeEventHandler BeginInvokeCommand = delegate { };

		private void Work()
		{
			if (Settings.GetProfile && (Me = Methods.GetMe()) == null)
			{
				Telesharp.Logger.Log(LogType.Error, Settings.Name, "Error, when get profile of bot");
				Telesharp.Logger.Log(LogType.Info, Settings.Name, "Exit");
				return;
			}
			_loaded = true;
			Telesharp.InvokeBotRunnedEvent(this, new BotRunnedEventArgs {Exception = null});
			while (!_stop)
			{
				var updates = Methods.GetUpdates();
				if (updates == null) // If can't get updates
				{
					//Telesharp.Logger.Log(LogType.Warning, Settings.Name, "Can't get updates!");
					continue; // We just continue
				}
				for (var index = 0; index < updates.Length; index++)
				{
					var upd = updates[index];
					if (_stop) break;
					if (upd.Message == null) {
						if(upd.InlineQuery != null){
							GotInlineQuery(this, new GotInlineQueryEventArgs(upd.InlineQuery));
						}
					}
					// Invoke event
					OnParseMessage(this, new ParseMessageEventArgs(upd.Message));

					// Search command
					var command = FindCommand(upd.Message);
					if (command == null) continue; // If command not found - continue

					// Invoke event
					var eventargs = new BeginInvokeEventArgs(command, upd.Message);
					BeginInvokeCommand(this, eventargs);

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

		private void PrepareThread()
		{
			_botThread = new Thread(Work)
			{
				Name = Settings.Name + " Thread"
			};
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
					if (Settings.ExceptionsToConsole)
						Telesharp.Logger.Log(LogType.Error, Settings.Name, $"Exception, when execute command:\n{exc}");
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
				if (Settings.ExceptionsToConsole)
					Telesharp.Logger.Log(LogType.Error, Settings.Name, $"Exception, when execute command:\n{exc}");
			}
		}

		public void WaitToDie()
		{
			while (BotAlive)
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
			if (settings.Token == null)
			{
				throw new NullReferenceException("No token in settings!");
			}
			Settings = settings;
			Methods = new TelegramBotMethods(this);
			Commands = new List<ICommand>();
		}

		#endregion Constructors

		#region Variables

		private bool _loaded;
		private int _threads;
		private bool _stop;
		private bool _botRunAsSync;
		private Thread _botThread;
		public List<ICommand> Commands { get; set; }
		public CommandComparer Comparer { get; set; }
		public BotSettings Settings { get; set; }
		public User Me { get; set; }
		public TelegramBotMethods Methods { get; set; }

		public bool BotAlive => (_botRunAsSync && _loaded) || (_botThread != null && _botThread.IsAlive && _loaded);

		#endregion Variables
	}
}