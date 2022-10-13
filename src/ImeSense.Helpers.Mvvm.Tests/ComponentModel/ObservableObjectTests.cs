using System.ComponentModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ImeSense.Helpers.Mvvm.ComponentModel;

namespace ImeSense.Helpers.Mvvm.Tests.ComponentModel;

/// <summary>
/// Test cases for <see cref="ObservableObject" />
/// </summary>
[TestClass]
public class ObservableObjectTests {
    public class SimpleModel<T> : ObservableObject {
        private T? _data;

        public T? Data {
            get => _data;
            set => SetProperty(ref _data, value);
        }
    }

    [TestMethod]
    public void UpdateEventMethods_SimpleModel_CorrectUpdateArguments() {
        var model = new SimpleModel<int>();

        var changingEventArgs = default(PropertyChangingEventArgs);
        var changingValue = default(int);

        var changedEventArgs = default(PropertyChangedEventArgs);
        var changedValue = default(int);

        model.PropertyChanging += (sender, eventArgs) => {
            Assert.IsNull(changingEventArgs);
            Assert.AreEqual(changedValue, default);
            Assert.AreSame(model, sender);
            Assert.IsNotNull(sender);
            Assert.IsNotNull(eventArgs);

            changingEventArgs = eventArgs;
            changingValue = model.Data;
        };

        model.PropertyChanged += (sender, eventArgs) => {
            Assert.IsNotNull(changingEventArgs);
            Assert.AreEqual(changedValue, default);
            Assert.AreSame(model, sender);
            Assert.IsNotNull(sender);
            Assert.IsNotNull(eventArgs);

            changedEventArgs = eventArgs;
            changedValue = model.Data;
        };

        model.Data = 42;

        Assert.AreEqual(changingEventArgs?.PropertyName, "Data");
        Assert.AreEqual(changingValue, 0);
        Assert.AreEqual(changedEventArgs?.PropertyName, "Data");
        Assert.AreEqual(changedValue, 42);
    }

    public class Person {
        public string? Name { get; set; }
    }

    public class WrappedModelWithProperty : ObservableObject {
        private Person Person { get; set; }

        public WrappedModelWithProperty(Person person) => Person = person;

        public Person PersonProxy => Person;

        public string? Name {
            get => Person.Name;
            set => SetProperty(Person.Name, value, Person, (person, name) => person.Name = name);
        }
    }

    [TestMethod]
    public void UpdateEventMethods_WrappedModelWithProperty_CorrectUpdateArguments() {
        var model = new WrappedModelWithProperty(new Person { Name = "Marie" });

        var changingEventArgs = default(PropertyChangingEventArgs);
        var changingValue = default(string);

        var changedEventArgs = default(PropertyChangedEventArgs);
        var changedValue = default(string);

        model.PropertyChanging += (sender, eventArgs) => {
            Assert.AreSame(model, sender);

            changingEventArgs = eventArgs;
            changingValue = model.Name;
        };

        model.PropertyChanged += (sender, eventArgs) => {
            Assert.AreSame(model, sender);

            changedEventArgs = eventArgs;
            changedValue = model.Name;
        };

        model.Name = "Alexander";

        Assert.AreEqual(changingEventArgs?.PropertyName, "Name");
        Assert.AreEqual(changingValue, "Marie");
        Assert.AreEqual(changedEventArgs?.PropertyName, "Name");
        Assert.AreEqual(changedValue, "Alexander");
        Assert.AreEqual(model.PersonProxy?.Name, "Alexander");
    }

    public class WrappedModelWithField : ObservableObject {
        private readonly Person _person;

        public WrappedModelWithField(Person person) => _person = person;

        public Person PersonWrapper => _person;

        public string? Name {
            get => _person.Name;
            set => SetProperty(_person.Name, value, _person, (person, name) => person.Name = name);
        }
    }

    [TestMethod]
    public void UpdateEventMethods_WrappedModelWithField_CorrectUpdateArguments() {
        var model = new WrappedModelWithField(new Person { Name = "Marie" });

        var changingEventArgs = default(PropertyChangingEventArgs);
        var changingValue = default(string);

        var changedEventArgs = default(PropertyChangedEventArgs);
        var changedValue = default(string);

        model.PropertyChanging += (sender, eventArgs) => {
            Assert.AreSame(model, sender);

            changingEventArgs = eventArgs;
            changingValue = model.Name;
        };

        model.PropertyChanged += (sender, eventArgs) => {
            Assert.AreSame(model, sender);

            changedEventArgs = eventArgs;
            changedValue = model.Name;
        };

        model.Name = "Alexander";

        Assert.AreEqual(changingEventArgs?.PropertyName, "Name");
        Assert.AreEqual(changingValue, "Marie");
        Assert.AreEqual(changedEventArgs?.PropertyName, "Name");
        Assert.AreEqual(changedValue, "Alexander");
        Assert.AreEqual(model.PersonWrapper.Name, "Alexander");
    }
}
