# story-writing-assistant
An AI-powered assistant to help you write engaging stories. A GUI application school project submission.

## Book Management System
### Introduction
This documentation outlines the structure and functionality of a **Book Management System** used by the **Story Writing Assistant** application implemented in C#. The system provides a framework for managing books, chapters, and associated data. The primary features include the definition of entities (`BaseEntity`, `Book`, and `Chapter`), a centralized control class (`Control`), and functionality for saving and loading data using JSON serialization.

### BaseEntity Class

#### Purpose
The `BaseEntity` class serves as the base class for entities in the system. It implements the `INotifyPropertyChanged` interface to notify subscribers when a property changes.

#### Properties and Methods
- **Id:** A unique identifier for each entity, initialized using `Guid.NewGuid().ToString()`.
- **PropertyChanged:** An event that notifies subscribers when a property changes.
- **OnPropertyChanged(string propertyName):** A protected method to invoke the `PropertyChanged` event.

### Book Class

#### Purpose
The `Book` class represents a book in the system, inheriting from `BaseEntity`. It includes properties such as Title, Author, Description, and a list of Chapters.

#### Properties and Methods
- **Title:** The title of the book, with property change notification.
- **Author:** The author of the book.
- **Description:** A description of the book.
- **Chapters:** A list of chapters associated with the book.
- **Default Constructor:** Initializes properties with default values.
- **Constructor with Title Parameter:** Sets the title using the default constructor.

### Chapter Class

#### Purpose
The `Chapter` class represents a chapter in a book, also inheriting from `BaseEntity`. It includes properties such as Title, Description, and Content.

#### Properties and Methods
- **Title:** The title of the chapter, with property change notification.
- **Description:** A description of the chapter.
- **Content:** The content of the chapter.
- **Default Constructor:** Initializes properties with default values.
- **Constructor with Title Parameter:** Sets the title using the default constructor.

### Control Class

#### Purpose
The `Control` class serves as a centralized hub for managing the application's state, including a list of books, the currently selected book, and the currently selected chapter. It also handles saving and loading data, as well as navigation.

#### Properties and Methods
- **Books:** A list of books managed by the application.
- **CurrentBook:** The currently selected book.
- **CurrentChapter:** The currently selected chapter within the current book.
- **ApiKey:** An API key used for OpenAI services.
- **Save():** Saves the current state of books and API key to a JSON file.
- **Load():** Loads previously saved data from the JSON file.
- **Navigate(Window window, string uri):** Navigates to a specified page, saving the current state before navigation.

### Data Class

#### Purpose
The `Data` class is a simple container for serializing and deserializing data. It includes properties for a list of `Books` and an `ApiKey`.

#### Properties
- **Books:** A list of books.
- **ApiKey:** An API key.

### Usage

1. **Entity Creation:** Create instances of `Book` and `Chapter` to represent books and chapters in the system.
2. **Data Management:** Use the `Control` class to manage the application's state, including saving and loading data.
3. **Event Handling:** Utilize the `PropertyChanged` event in entities to subscribe to property changes.
4. **Navigation:** Use the `Navigate` method in the `Control` class to navigate between pages.

### Example

```csharp
// Creating a new book
Book myBook = new Book("Software Development Fundamentals");

// Subscribing to property changes
myBook.PropertyChanged += (sender, args) => Console.WriteLine($"Property {args.PropertyName} changed.");

// Modifying the title triggers the event
myBook.Title = "Software Development Fundamentals 2: The Sequel";
```

This documentation provides an overview of the implemented classes and their functionalities. Developers can use this system as a foundation for building book management applications, extending and customizing it based on specific requirements.

---
## Applications of OOP concepts
### Encapsulation
#### Private Field with Public Property (_title and Title)
In the `Book` and `Chapter` classes, the `_title` field is declared as private, and access to it is provided through a public property `Title`. This ensures that the internal state of the `Book`/`Chapter` class (represented by `_title`) is encapsulated and can only be accessed or modified through the defined getter and setter.

#### Property with Validation (Title Property)
In the `Book` and `Chapter` classes, the `Title` property includes a validation mechanism in the setter. It checks whether the new value is different from the current value before updating it. This is a form of encapsulation where the internal state is protected, and changes to it are controlled through a well-defined interface (property).

#### Constructor Initialization
The constructors initialize the `Book`/`Chapter` object, encapsulating the logic for setting default values and ensuring that the object is in a valid state upon creation.

#### Event (PropertyChanged Event)
In the `BaseEntity` class, The `PropertyChanged` event is used to notify external entities (like UI components) when the value of the `Title` property changes. This allows external components to respond to changes in the internal state without direct access to the internal fields.

### Inheritance
The `MainWindow` class is declared with a base class of `Window`: `public partial class MainWindow : Window`.

The `MainWindow` class inherits from the `Window` class, which is part of the WPF framework.

The `Book` and `Chapter` classes both inherit from the `BaseEntity` class. This means that `Book` and `Chapter` inherit the properties and methods of the `BaseEntity` class.

### Polymorphism
In the `MainWindow` class, the `OnClosing` method is overridden using the `protected override` keywords. It overrides a part of WPF's event handling mechanism.
