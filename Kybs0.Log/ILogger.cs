using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kybs0.Log
{
    public interface ILogger
    {
        string LogFolder { get; }
        void Info(string info);
        void Message(string message);
        void Error(string error);
        void Error(string error, Exception ex);
        void Error(Exception ex);
    } 
}
