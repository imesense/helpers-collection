using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImeSense.Helpers.Mvvm.Tests {
    [TestClass]
    public class RelayCommandTests {
        [TestMethod]
        public void Execute_Ticks_CorrectIncreaseOfTicks() {
            var ticks = 0;
            var command = new RelayCommand(() => ticks++);
            Assert.IsTrue(command.CanExecute(null));
            Assert.IsTrue(command.CanExecute(new object()));

            var sender = default(object);
            var eventArgs = default(EventArgs);
            command.CanExecuteChanged += (s, e) => {
                sender = s;
                eventArgs = e;
            };
            command.NotifyCanExecuteChanged();
            Assert.AreSame(sender, command);
            Assert.AreSame(eventArgs, EventArgs.Empty);

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
