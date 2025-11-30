using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Exceptions
{
    public class KlantenException : Exception
    {
        public List<string> Errors;


        public KlantenException()
        {
        }

        public KlantenException(string? message) : base(message)
        {
        }

        public KlantenException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
