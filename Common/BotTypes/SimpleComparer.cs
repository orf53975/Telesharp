using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Telesharp.Common.Interfaces;
using Telesharp.Common.Types;

namespace Telesharp.Common.BotTypes
{
    public class SimpleComparer : CommandComparer
    {
        public override bool Compare(ICommand command, Message message)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            switch (((SimpleComparerCommand) command).CompareMode)
            {
                case SimpleComparerCommand.Mode.IsSame:
                    if (PublicInstancePropertiesEqual(command.Prototype, message))
                    {
                        return true;
                    }
                    break;
                case SimpleComparerCommand.Mode.BeginsWithTextFromOriginalMessage:
                    if (message.Text == null)
                    {
                        return false;
                    }
                    if (message.Text.IndexOf(command.Prototype.Text, StringComparison.Ordinal) == 0)
                    {
                        return true;
                    }
                    break;
                case SimpleComparerCommand.Mode.ContainsTextFromOriginalMessage:
                    return message.Text != null && message.Text.Contains(command.Prototype.Text);
                default:
                    return false;
            }

            return false;
        }

        public static bool PublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                var type = typeof (T);
                var ignoreList = new List<string>(ignore);
                var unequalProperties = from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    where !ignoreList.Contains(pi.Name)
                    let selfValue = type.GetProperty(pi.Name).GetValue(self, null)
                    let toValue = type.GetProperty(pi.Name).GetValue(to, null)
                    where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                    select selfValue;
                return !unequalProperties.Any();
            }
            return self == to;
        }
    }

    public class SimpleComparerCommand : ICommand
    {
        public enum Mode
        {
            /// <summary>
            ///     Performs if property same with original message
            /// </summary>
            IsSame,

            /// <summary>
            ///     Performs if message for comparison contains text from original message
            /// </summary>
            ContainsTextFromOriginalMessage,

            /// <summary>
            ///     Performs if message for comparison text begins with text in original message
            /// </summary>
            BeginsWithTextFromOriginalMessage
        }

        public SimpleComparerCommand()
            : this(
                new Message(-1, "/on"), "Nothing information, what can help you", Mode.BeginsWithTextFromOriginalMessage
                )
        {
        }

        public SimpleComparerCommand(Message prototype, string helpText, Mode compareMode = Mode.IsSame)
        {
            Prototype = prototype;
            CompareMode = compareMode;
            HelpText = helpText;
        }

        // /// <summary>
        // /// Original message for compare with getted message
        // /// </summary>
        // public Message OriginalMessage { get; set; }

        /// <summary>
        ///     Compare mode
        /// </summary>
        [DefaultValue(2)]
        public Mode CompareMode { get; set; }

        /// <summary>
        ///     Helpful text
        /// </summary>
        [DefaultValue("Nothing information, what can help you")]
        public string HelpText { get; set; }

        /// <summary>
        ///     Prototype of message
        /// </summary>
        public Message Prototype { get; set; }

        public virtual void Executed(Message message)
        {
            throw new NotImplementedException();
        }
    }
}