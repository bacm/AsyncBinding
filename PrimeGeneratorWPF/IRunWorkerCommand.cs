using System;
using System.ComponentModel;
using System.Windows.Input;

namespace PrimeGeneratorWPF
{
    public interface IRunWorkerCommand : ICommand
    {
        object Result { get; }
        bool IsExecuting { get; }

        event EventHandler RunWorkerStarting;
        event RunWorkerCompletedEventHandler RunWorkerCompleted;
    }
}