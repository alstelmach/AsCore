# Purpose of this repository
These projects contain seed work which needs to be implemented almost in every other web application. They improve development speed and make other project more consistent. 
# Project structure
Core functionalities are grouped into four different categories. All of them have corresponding solution project, and their own NuGet packages available at [NuGet Gallery](https://www.nuget.org/). 
### Core.Application.Abstractions

[![Nuget Package](https://badgen.net/nuget/v/AsCore.Application.Abstractions)](https://www.nuget.org/packages/AsCore.Application.Abstractions/)

Here are the abstractions typically used in the application layer. The project contains no specific implementations - just interfaces, which may help you to square your code away.
### Core.Domain.Abstractions

[![Nuget Package](https://badgen.net/nuget/v/AsCore.Domain.Abstractions)](https://www.nuget.org/packages/AsCore.Domain.Abstractions/)

Here are the building blocks for domain driven systems. The project contains some interfaces and base classes representing some DDD components, eg. aggregate root, repository etc.
### Core.Infrastructure

[![Nuget Package](https://badgen.net/nuget/v/AsCore.Infrastructure)](https://www.nuget.org/packages/AsCore.Infrastructure/)

Project constists of commonly used infrastructure layer implementations.

### Core.Utilities

[![Nuget Package](https://badgen.net/nuget/v/AsCore.Utilities)](https://www.nuget.org/packages/AsCore.Utilities/)

Package containing useful utility tools and extensions, colloquially called "helpers".

# Roadmap
These projects will be improved, and some other functionalities may be added to them, during the other applications development. The next versions of the packages coming soon :)
