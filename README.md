# Rake Inventory Management System

## Introduction

Rake is an Inventory Management System built with .NET 7 and C#. It highlights the use of Domain-Driven Design (DDD), CQRS, and Event Sourcing.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)
- [License](#license)

## Features

- Inventory management domain modeled using DDD.
- Write/read separation using CQRS.
- Event Sourcing approach for capturing state changes.

## Getting Started

### Prerequisites

- .NET SDK 7.x
- Git

### Run locally

1. Clone the repository:
   ```bash
   git clone https://github.com/Toby2xl/Rake.git
   cd Rake
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Build:
   ```bash
   dotnet build
   ```
4. Run:
   ```bash
   dotnet run
   ```

> If the solution contains multiple projects (API, worker, etc.), run the specific project with:
> `dotnet run --project <path-to-csproj>`

## Technologies Used

- .NET 7
- C#
- Domain-Driven Design (DDD)
- CQRS
- Event Sourcing

## Contributing

Contributions are welcome.

1. Fork the repo
2. Create a feature branch
3. Commit your changes
4. Open a pull request

## License

Add a license (for example, MIT) by including a `LICENSE` file in the repository.