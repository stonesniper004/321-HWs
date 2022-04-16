using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadsheetEngine
{
    public interface ICommand
    {
        void Execute();
        void UnExecute();
        string Description();
    }
}
