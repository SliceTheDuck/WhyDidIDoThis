# GE# (Ge-Sharp)

GE# (pronounced "Gee Sharp") is a programming language that offers a unique approach to C# development. It's designed to provide a more accessible and intuitive coding experience for German-speaking developers or those who prefer using German keywords. GE# leverages the power and versatility of the C# language while replacing standard English keywords with their German equivalents. This allows developers to write code using familiar German terminology, potentially reducing cognitive load and improving code readability for a specific audience.

**File Extension:** `.gsh`

GE# source code files use the `.gsh` file extension. This distinguishes them from standard C# files and prevents conflicts with C# compilers. The `.gsh` extension signals to the GE# compiler (`gshc`) that the file contains GE# code that needs to be translated to C# before compilation.

# GSHC (Ge-Sharp Compiler)

GSHC is the command-line tool responsible for compiling GE# code into standard C# code. It acts as a translator, converting the German keywords in `.gsh` files into their corresponding C# equivalents. GSHC is envisioned as a comprehensive language tool, handling project management, compilation, and other development tasks.

**Future Goals:**

*   **`.gshproj` Project Files:** Support for `.gshproj` files, which will define project settings, dependencies, and build configurations for GE# projects.
*   **Project Creation:** Ability to create new GE# projects from the command line, including generating necessary files and directories.
*   **Package Management:** Integration with NuGet or a similar package manager to handle dependencies.

## Features

*   **German Keyword Translation:** Translates GE# code with German keywords into valid C# code. The translation is based on a predefined mapping (e.g., `wenn` to `if`, `ganzzahl` to `int`).
*   **Command-Line Interface (CLI):** Provides a command-line interface for compiling GE# code.
*   **C# Code Generation:** Generates human-readable C# code as output.
*   **Assembly Generation (Optional):** Can automatically compile the generated C# code into an assembly (DLL or EXE) using the C# compiler.
*   **Error Reporting:** Provides informative error messages with line numbers to help identify and fix issues in the GE# code.
*   **Wrapper Generation:** Includes a wrapper generator to create German-keyword-based APIs for existing C# libraries, simplifying their use in GE# projects.
*   **Customizable Keyword Mapping:** Allows users to customize the keyword mappings to suit their preferences or specific project requirements (future enhancement).
*    **gshproj Support:** Ability to define project settings, dependencies, and build configurations for GE# projects.

## Usage

**Basic Compilation:**

```bash
gshc <input_file.gsh> -o <output_directory>
```

This command translates the `input_file.gsh` file into C# code and saves it to the specified `output_directory`.

**Compiling to an Assembly:**

```bash
gshc <input_file.gsh> -o <output_directory> -assembly <output_assembly.dll>
```

This command translates the `input_file.gsh` file into C# code, saves it to the specified `output_directory`, and then compiles the C# code into an assembly named `output_assembly.dll`.

**Generating Wrappers:**

```bash
gshc -wrap <assembly_path.dll> -o <output_directory>
```

This command generates GE# wrappers for the specified C# assembly and saves the generated code to the specified `output_directory`.

**Help Information:**

```bash
gshc -help
```

This command displays a list of available options and their descriptions.