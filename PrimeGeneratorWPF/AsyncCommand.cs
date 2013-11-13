using System;
using System.ComponentModel;

namespace PrimeGeneratorWPF
{
    // based on http://tech.norabble.com/2008/10/asynchronous-wpf.html
    public abstract class AsyncCommand : IRunWorkerCommand
    {
        private bool _isExecuting;
        public bool CanCancel { get; set; }

        public event EventHandler CanExecuteChanged;
        public event EventHandler RunWorkerStarting;

        public event RunWorkerCompletedEventHandler RunWorkerCompleted;

        public object Result { get; private set; }

        public bool IsExecuting
        {
            get { return _isExecuting; }
            private set
            {
                _isExecuting = value;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            try
            {
                var worker = new BackgroundWorker();

                OnRunWorkerStarting(worker);

                worker.DoWork += (sender, e) => OnExecute(e.Argument);
                worker.RunWorkerCompleted += (sender, e) => OnRunWorkerCompleted(e);
                worker.RunWorkerAsync(parameter);
            }
            catch (Exception ex)
            {
                OnRunWorkerCompleted(new RunWorkerCompletedEventArgs(null, ex, true));
            }
        }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        protected abstract object OnExecute(object parameter);

        private void OnRunWorkerStarting(BackgroundWorker worker)
        {
            IsExecuting = true;
            if (RunWorkerStarting != null)
                RunWorkerStarting(worker, EventArgs.Empty);
        }

        private void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            Result = e.Result;
            IsExecuting = false;
            if (RunWorkerCompleted != null)
                RunWorkerCompleted(this, e);
        }
    }
}