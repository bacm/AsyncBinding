using System;

namespace PrimeGeneratorWPF
{
    public class ValueReturnedEventArgs : EventArgs
    {
        public object Object { get; private set; }

        public ValueReturnedEventArgs(object obj)
        {
            Object = obj;
        }
    }
}