# Purpose of this repository
This solution contains kind of seed work used in my other project. It improves development speed and makes other project more consistent.
# Project structure
I've decided to group core functionalities into four categories, all of them have corresponding project in the solution.
### 1. Core.Application.Abstractions
Here you can find abstractions used in application layer, which is responsible for task delegation to certain parts of domain layer. It contains no specific implementation - just interfaces.
### 2. Core.Domain.Abstractions
Use these abstractions if you need to make your project compliant with DDD methodology. The project contains some interfaces and base classes for DDD components like entity, repository etc.
### 3. Core.Infrastructure
Project consists of default implementations which are needed in almost every project.
### 4. Core.Utilities
Project contains some extension methods, and useful tools like swagger - generally helpers.
# Roadmap
Project hasn't been finished yet. It needs to mellow, while other projects using it will be implemented. Then, it will be released maybe as a NuGet which will make using it easier and provide some features like versioning.
