using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using PrimeGeneratorWPF.Properties;

namespace PrimeGeneratorWPF
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly SendCancelCommand _sendCancelCommand;
        private readonly RunCommand _runCommand;
        private string _availableAction;
        private string _prime;
        private IRunWorkerCommand _runCommand1;

        public ViewModel()
        {
            _runCommand = new RunCommand { CanCancel = true};
            _runCommand.RunWorkerStarting += RunCommandOnRunWorkerStarting;
            _runCommand.ValueReturned += RunCommandOnValueReturned;

            _sendCancelCommand = new SendCancelCommand();
            _sendCancelCommand.RunWorkerCompleted += SendCancelCommandOnRunWorkerCompleted;

            RunCommand = _runCommand;
        }

        private void RunCommandOnValueReturned(object sender, ValueReturnedEventArgs eventArgs)
        {
            Prime = Convert.ToInt32(eventArgs.Object).ToString(CultureInfo.InvariantCulture);
        }

        public string Prime
        {
            get { return _prime; }
            set
            {
                _prime = value;
                OnPropertyChanged();
            }
        }

        public string AvailableAction
        {
            get { return _availableAction; }
            set
            {
                _availableAction = value;
                OnPropertyChanged();
            }
        }

        public IRunWorkerCommand RunCommand
        {
            get { return _runCommand1; }
            private set
            {
                _runCommand1 = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SendCancelCommandOnRunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            AvailableAction = "Start";
            _runCommand.CancelWork();
            RunCommand = _runCommand;
        }

        private void RunCommandOnRunWorkerStarting(object sender, EventArgs eventArgs)
        {
            AvailableAction = "Cancel";
            RunCommand = _sendCancelCommand;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}