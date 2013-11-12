using System;
using System.Collections.Generic;

namespace PrimeGeneratorWPF
{
    public class RunCommand : CancelableAsyncCommand
    {
        public event ValueReturnedEventHandler ValueReturned;

        protected override object OnExecute(object parameter)
        {
            foreach (var prime in Primes(Int32.MaxValue))
            {
                if (CancelationInProgress)
                {
                    return null;
                }

                if (ValueReturned != null)
                    ValueReturned(this, new ValueReturnedEventArgs(prime));
            }

            throw new NotImplementedException("cannot generate that much !!!");
        }

        // todo: not the best solution but its good for now
        // from http://www.alteridem.net/2007/08/22/the-yield-statement-in-c/
        public static IEnumerable<int> Primes(int max)
        {
            yield return 2;
            List<int> found = new List<int>();
            found.Add(3);
            int candidate = 3;
            while (candidate <= max)
            {
                bool isPrime = true;
                foreach (int prime in found)
                {
                    if (prime * prime > candidate)
                    {
                        break;
                    }
                    if (candidate % prime == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    found.Add(candidate);
                    yield return candidate;
                }
                candidate += 2;
            }
        }
    }
}