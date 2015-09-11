using System;
using Telesharp.Common.Types;

namespace Telesharp.Common.Interfaces
{
    public interface ICommand
    {
        Message Prototype { get; set; }
        void Executed(Message message); 
        string HelpText { get; set; }
    }
}