# story-writing-assistant
This web application leverages OpenAI's API to assist in generating content based on user prompts.

## Features
- Create and manage multiple books and chapters, including titles and descriptions.
- Use the OpenAI API to generate text based on prompts and previous context.
- Import and export your book and chapter data in JSON format.
- Clear or delete data including books, chapters, histories, and chats.
- A responsive design for mobile and desktop devices.

## Usage
1. **Home**: View and manage your list of books. You can create a new book, edit, delete, or reorder existing books.
2. **Chapters**: Once you open a book, you can view its chapters or create new ones. Each chapter can be edited with a title and description.
3. **Content**: Here you can edit the content of the selected chapter. You can also interact via prompts to generate text using the OpenAI service.
4. **Chat**: This feature allows you to chat with the AI by sending messages and viewing the responses. You can also have multiple chats per chapter.
5. **Settings**: Configure your OpenAI API settings, including the API endpoint and your API key.

## Settings
1. **OpenAI Endpoint URL**: Default is set to `https://api.openai.com/v1/chat/completions`. You can change it if needed.
2. **API Key**: Provide your OpenAI API key for the application to make requests to the OpenAI API.
3. **Model**: Choose the model (e.g., `gpt-4o-mini`) that you would like the assistant to use when generating the text.

## Development
- The code utilizes HTML, CSS, and JavaScript.
- The web application stores data in the browser's local storage.
- JavaScript functions handle user interactions, such as creating, editing, and organizing the writing data.
