# ASP.NET 7.0 Web API Demo

This project is a demo showcasing my skills in developing a robust Web API using ASP.NET Core 7.0. The original project was realized for a customer, and I received approval to showcase this demo publicly. In the real application, OpenAI was used for generating dynamic quiz content, but in this demo, the OpenTrivia API is integrated for trivia purposes. The project implements multiple key features such as authentication, QuizSession management, and custom question handling.

## Technologies Used

- **ASP.NET Core 7.0**: For building scalable, high-performance APIs.
- **Entity Framework Core**: For database management with SQL Server as the primary provider.
- **JWT Authentication**: To secure the API with token-based authentication.
- **OpenTrivia API**: Used in this demo to fetch trivia questions (replaced OpenAI from the real application).
- **Swagger**: For API documentation and testing interface.
- **AutoMapper**: For simplifying object-to-object mapping between entities and DTOs.
- **Dependency Injection**: Standard ASP.NET Core DI system for managing services.
- **Repository and Unit of Work Pattern**: To maintain clean data access and separation of concerns.
- **CQRS Pattern**: For segregating read and write operations, improving scalability and maintainability.

## Key Project Features

1. **QuizSession Management**
   - Start a new session.
   - Answer questions in a quiz.
   - Track progress within an ongoing quiz.
   - View results at the end of the quiz session.

   The QuizSession is designed to provide a dynamic and engaging quiz experience where questions can be retrieved from the OpenTrivia API and then answered in real time. This feature can easily be expanded with different question sources, such as OpenAI for real-time content generation in the original application.

2. **Authentication and Authorization**
   - JWT Authentication ensures secure access to the API. Only authenticated users can start quiz sessions, submit answers, and manage custom questions.
   - Role-based Access Control ensures that specific features (like question creation) are restricted to authorized roles.

3. **Custom Question Management**
   Users can manage their own custom quiz questions. The following operations are supported:
   - Create new questions: Users can define their own questions and answers.
   - Update existing questions: Modify the details of the created questions.
   - Retrieve questions: Fetch custom questions using various filters.
   - Delete questions: Remove questions from the system.

4. **OpenTrivia API Integration**
   The API is integrated with the OpenTrivia API to provide trivia categories and questions. Users can:
   - Fetch trivia categories from the OpenTrivia API.
   - Request trivia questions by providing parameters such as category, difficulty, and question type.

5. **Multiple Controllers**
   The demo is structured with multiple controllers, each managing a specific set of features:
   - QuizSession Controller: Manages the lifecycle of a quiz session (start, progress, and results).
   - OpenTrivia Controller: Handles fetching trivia categories and questions from the OpenTrivia API.
   - Question Controller: Manages custom question CRUD operations (Create, Read, Update, Delete).

6. **Database Integration**
   - **Entity Framework Core**: Manages database interactions for storing quiz sessions, custom questions, and user data.
   - **SQL Server**: Serves as the database provider for persisting data.

7. **Swagger for API Documentation**
   The project includes Swagger for interactive API documentation, making it easy to test and explore all the available endpoints.

8. **Error Handling and Validation**
   The API includes comprehensive error handling and input validation to ensure that invalid requests are handled gracefully.

## Architecture and Design Patterns

- **Layered Architecture**: The project follows a clean, layered architecture that separates concerns between the API, business logic, and data layers.
- **Repository Pattern**: Used to encapsulate the logic required to access data sources.
- **Unit of Work**: Ensures that multiple operations against the database are treated as a single transaction.
- **CQRS Pattern**: Separates commands and queries, making the system more scalable and maintainable.

## Use Cases

- **QuizSession**: Users can start a new quiz session, answer questions, and get feedback on their performance.
- **Question Management**: Allows users to create, update, retrieve, and delete custom quiz questions.
- **Trivia Integration**: The API provides trivia questions from the OpenTrivia API with customizable filters.
- **Authentication**: Only authenticated users can interact with certain endpoints, ensuring a secure experience.

## How to Run the Project

1. Clone the repository:
   ```bash
   git clone [https://github.com/yourusername/quiz-api-demo.git](https://github.com/abdeloow/aspnetcore-quiz-webapi-template.git)
2. Navigate to the project directory:
   ```bash
   cd quiz-api-demo
3. Resote dependencies:
   ```bash
   dotnet restore
4. Update the database connection string in "_appsettings.json_".
5. Apply the migrations to set up the database:
   ```bash
   dotnet ef database update
6. Run the application
   ```bash
   dotnet run
7. Access the Swagger UI for API documentation at:
   ```bash
   http://localhost:5111/swagger

## Libraries and Frameworks Used
  -  ASP.NET Core 7.0
  -  Entity Framework Core
  -  SQL Server
  -  OpenTrivia API
  -  Swagger / Swashbuckle
  -  AutoMapper
  -  JWT Authentication
  -  FluentValidation (for input validation)
  -  CQRS (Command and Query Segregation)
  -  Repository Pattern
  -  Unit of Work Pattern

## Notes
This project is a demo that showcases my skills in building robust, scalable APIs using modern technologies and best practices. The core functionality has been adapted for demonstration purposes, with trivia questions provided by the OpenTrivia API (the real application uses OpenAI for dynamic question generation). The project demonstrates how authentication, quiz session management, and custom question handling can be integrated into a cohesive application.

## License
This project is open-source and available under the MIT License.
