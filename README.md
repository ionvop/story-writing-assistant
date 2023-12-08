# story-writing-assistant
An AI-powered assistant to help you write engaging stories. A GUI application school project submission.

## Classes involved
### Book class
`Id` A unique ID to differentiate itself from other `Book` objects.

`Title` The title of the book.

`Author` The author of the book.

`Description` The summary of the book.

`Chapters` A list of `Chapter` objects.

#### Constructors
`Book()` Creates a book with a blank title.

`Book(string title)` Creates a book with title.

### Chapter class
`Id` A unique ID to differentiate itself from other `Chapter` objects.

`Title` The title of the chapter.

`Description` The summary of the chapter.

`Content` The story of the chapter.

#### Constructors
`Book()` Creates a chapter with a blank title.

`Book(string title)` Creates a chapter with title.

---
# Applications of OOP concepts
## Encapsulation (Book/Chapter class)
### Private Field with Public Property (_title and Title)
The `_title` field is declared as private, and access to it is provided through a public property `Title`. This ensures that the internal state of the `Book`/`Chapter` class (represented by `_title`) is encapsulated and can only be accessed or modified through the defined getter and setter.

### Property with Validation (Title Property)
The `Title` property includes a validation mechanism in the setter. It checks whether the new value is different from the current value before updating it. This is a form of encapsulation where the internal state is protected, and changes to it are controlled through a well-defined interface (property).

### Constructor Initialization
The constructors initialize the `Book`/`Chapter` object, encapsulating the logic for setting default values and ensuring that the object is in a valid state upon creation.

### Event (PropertyChanged Event)
The `PropertyChanged` event is used to notify external entities (like UI components) when the value of the `Title` property changes. This allows external components to respond to changes in the internal state without direct access to the internal fields.

## Inheritance (MainWindow/Window class)
The `MainWindow` class is declared with a base class of `Window`: `public partial class MainWindow : Window`.

The `MainWindow` class inherits from the `Window` class, which is part of the WPF framework.

## Polymorphism (MainWindow/Window class)
The `OnClosing` method is overridden using the protected override keywords. It overrides a part of WPF's event handling mechanism.
