using Microsoft.VisualStudio.TestTools.UnitTesting;

using ImeSense.Helpers.Mvvm.Input;

namespace ImeSense.Helpers.Mvvm.Tests.Input {
    /// <summary>
    /// Test cases for <see cref="RelayCommand{T}" />
    /// </summary>
    [TestClass]
    public class RelayCommandOfTTests {
        /// <summary>
        /// Asserts given action throws <see cref="ArgumentException" /> with specific parameter name
        /// </summary>
        /// <param name="action">Input <see cref="Action"/> to invoke</param>
        /// <param name="parameterName">Expected parameter name</param>
        public static void ThrowArgumentException(Action action, string parameterName) {
            var success = false;

            try {
                action();
            } catch (Exception exception) {
                Assert.IsTrue(exception.GetType() == typeof(ArgumentException));
                Assert.AreEqual(parameterName, ((ArgumentException) exception).ParamName);

                success = true;
            }

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void CanExecute_Null_ThrowArgumentNullException() {
            var text = string.Empty;
            Assert.ThrowsException<ArgumentNullException>(() => new RelayCommand<string>(source => text = source, null));
        }

        [TestMethod]
        public void Execute_String_CorrectStringAssignment() {
            var text = string.Empty;
            var command = new RelayCommand<string>(source => text = source);
            Assert.IsTrue(command.CanExecute("Text"));
            Assert.IsTrue(command.CanExecute(null));

            ThrowArgumentException(() => command.CanExecute(new object()), "parameter");
            ThrowArgumentException(() => command.CanExecute(42), "parameter");

            ThrowArgumentException(() => command.Execute(new object()), "parameter");
            ThrowArgumentException(() => command.Execute(42), "parameter");

            var commandSender = default(object);
            var commandEventArgs = default(EventArgs);
            command.CanExecuteChanged += (sender, eventArgs) => {
                commandSender = sender;
                commandEventArgs = eventArgs;
            };
            command.NotifyCanExecuteChanged();
            Assert.AreSame(commandSender, command);
            Assert.AreSame(commandEventArgs, EventArgs.Empty);

            command.Execute((object) "Hello");
            Assert.AreEqual(text, "Hello");

            command.Execute(null);
            Assert.AreEqual(text, null);
        }

        [TestMethod]
        public void CanExecute_DoubleString_CorrectStringsAssignment() {
            var text = string.Empty;
            var command = new RelayCommand<string>(source => text = source, source => source != null);
            Assert.IsTrue(command.CanExecute("Text"));
            Assert.IsFalse(command.CanExecute(null));

            ThrowArgumentException(() => command.CanExecute(new object()), "parameter");
            ThrowArgumentException(() => command.CanExecute(42), "parameter");

            ThrowArgumentException(() => command.Execute(new object()), "parameter");
            ThrowArgumentException(() => command.Execute(42), "parameter");

            command.Execute((object) "Hello");
            Assert.AreEqual(text, "Hello");

            command.Execute(null);
            Assert.IsNull(text);
        }

        [TestMethod]
        public void CanExecute_Number_CorrectStringsAssignment() {
            var n = 0;
            var command = new RelayCommand<int>(i => n = i);
            Assert.IsFalse(command.CanExecute(null));

            ThrowArgumentException(() => command.CanExecute("Hello"), "parameter");
            ThrowArgumentException(() => command.CanExecute(3.14f), "parameter");

            ThrowArgumentException(() => command.Execute(null), "parameter");
            ThrowArgumentException(() => command.Execute("Hello"), "parameter");
            ThrowArgumentException(() => command.Execute(3.14f), "parameter");
        }

        [TestMethod]
        public void CanExecute_DoubleNumber_CorrectNumbersAssignment() {
            var n = 0;
            var command = new RelayCommand<int>(i => n = i, i => i > 0);
            Assert.IsFalse(command.CanExecute(null));

            ThrowArgumentException(() => command.CanExecute("Hello"), "parameter");
            ThrowArgumentException(() => command.CanExecute(3.14f), "parameter");

            ThrowArgumentException(() => command.Execute(null), "parameter");
            ThrowArgumentException(() => command.Execute("Hello"), "parameter");
            ThrowArgumentException(() => command.Execute(3.14f), "parameter");
        }
    }
}
