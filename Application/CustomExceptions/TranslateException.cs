using System;

namespace Application.CustomExceptions
{
    public class TranslateException : Exception
    {
        public TranslateException(string message) : base(message)
        {

        }
    }
}
