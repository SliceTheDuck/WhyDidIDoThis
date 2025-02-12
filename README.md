# gsharp

gsharp is a C# compiler and wrapper generator for dependent assemblies. It includes functionality to transform C# code into G# code using custom keyword mappings.

## Features

- Load keyword mappings from embedded resources
- Transform C# code into G# code
- Generate wrappers for dependent assemblies
- Retrieve all C# keywords

## Usage

### Transform C# Code into G# Code

```sh
dotnet run -- transform <source-file>