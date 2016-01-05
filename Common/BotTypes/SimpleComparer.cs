using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telesharp.Common.Interfaces;
using Telesharp.Common.Types;

namespace Telesharp.Common.BotTypes
{
	/// <summary>
	///	 Simple comparer (yes, yes, yes. Very simple)
	/// </summary>
	public class SimpleComparer : CommandComparer
	{
		public override bool Compare(ICommand command, Message message)
		{
			if (command == null) throw new ArgumentNullException(nameof(command));
			switch (((SimpleComparerCommand) command).CompareMode)
			{
				case SimpleComparerCommand.Mode.IsSame:
					if (PublicInstancePropertiesEqual(command.Prototype, message))
						return true;
					break;
				case SimpleComparerCommand.Mode.BeginsWithTextFromOriginalMessage:
					if (message.Text == null) break;
					if (message.Text.StartsWith(command.Prototype.Text)) return true;
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
			if (self == null || to == null) return self == to;
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
	}
}