using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace PrimeGeneratorWPF.Tests
{
    [TestClass]
    public class AsyncCommandTests
    {
        Mock<AsyncCommand> _command;

        [TestInitialize]
        public void Initialize()
        {
            _command = new Mock<AsyncCommand>();
        }

        // todo: find another way to test this without blocking everything if it fails
        [TestMethod]
        public void WhenWorkSucceed_ItShouldCallWorkEnded()
        {
            var wh = new ManualResetEvent(false);

            _command.Object.RunWorkerCompleted +=
                (sender, args) => wh.Set();

            _command.Object.Execute(null);

            wh.WaitOne();
        }
    }
}
