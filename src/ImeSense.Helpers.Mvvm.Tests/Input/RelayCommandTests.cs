using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ImeSense.Helpers.Mvvm.Input;

namespace ImeSense.Helpers.Mvvm.Tests.Input {
    [TestClass]
    public class RelayCommandTests {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Execute_Null_ThrowArgumentNullException() {
            var command = new RelayCommand(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CanExecute_Null_ThrowArgumentNullException() {
            var ticks = 0;
            var command = new RelayCommand(() => ticks++, null);
        }

        [TestMethod]
        public void Execute_Ticks_CorrectIncreaseOfTicks() {
            var ticks = 0;
            var command = new RelayCommand(() => ticks++);
            Assert.IsTrue(command.CanExecute(null));
            Assert.IsTrue(command.CanExecute(new object()));

            var commandSender = default(object);
            var commandEventArgs = default(EventArgs);
            command.CanExecuteChanged += (sender, eventArgs) => {
                commandSender = sender;
                commandEventArgs = eventArgs;
            };
            command.NotifyCanExecuteChanged();
            Assert.AreSame(commandSender, command);
            Assert.AreSame(commandEventArgs, EventArgs.Empty);

            command.Execute(null);
            Assert.AreEqual(ticks, 1);

            command.Execute(new object());
            Assert.AreEqual(ticks, 2);
        }

        [TestMethod]
        public void CanExecute_TicksAndTrue_CorrectIncreaseOfTicks() {
            var ticks = 0;
            var command = new RelayCommand(() => ticks++, () => true);
            Assert.IsTrue(command.CanExecute(null));
            Assert.IsTrue(command.CanExecute(new object()));

            command.Execute(null);
            Assert.AreEqual(ticks, 1);

            command.Execute(new object());
            Assert.AreEqual(ticks, 2);
        }

        [TestMethod]
        public void CanExecute_TicksAndFalse_CorrectIncreaseOfTicks() {
            var ticks = 0;
            var command = new RelayCommand(() => ticks++, () => false);
            Assert.IsFalse(command.CanExecute(null));
            Assert.IsFalse(command.CanExecute(new object()));

            command.Execute(null);
            Assert.AreEqual(ticks, 1);

            command.Execute(new object());
            Assert.AreEqual(ticks, 2);
        }
    }
}
