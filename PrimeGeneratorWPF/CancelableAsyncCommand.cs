using System;
using System.ComponentModel;
using PrimeGeneratorWPF.Properties;

namespace PrimeGeneratorWPF
{
    public abstract class CancelableAsyncCommand : AsyncCommand
    {
        protected bool CancelationInProgress { get; private set; }

        public void CancelWork()
        {
            if (!IsExecuting)
            {
                throw new InvalidOperationException(
                    Resources.Exception_CancelJob_NotRunning);
            }

            if (!CanCancel)
            {
                throw new InvalidOperationException(
                    Resources.Exception_CancelJob_CannotCancel);
            }

            CancelationInProgress = true;
            RunWorkerCompleted += OnRunWorkerCompleted;
        }

        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            CancelationInProgress = false;
            RunWorkerCompleted -= OnRunWorkerCompleted;
        }
    }
}