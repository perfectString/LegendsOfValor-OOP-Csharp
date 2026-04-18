# Full Project Description
# Legends of Valor – The Guild Trials

**C# OOP Regular Exam** - SoftUni C# OOP course, 09 August 2025 

**✅ Exam Result: 290/300 points**

## Project Overview

Implemented a complete **Guild Wars simulation system** following **SOLID principles** with inheritance, polymorphism, repositories, and command pattern. 
Features hero recruitment, guild management, training, and automated war resolution with detailed reporting.

## Key Features

- **Structure** (50 pts): Base `Hero` class + 3 derived types (`Warrior`, `Sorcerer`, `Spellblade`) with validation, guild compatibility, and training logic 
- **Guild Management**: `Guild` class with wealth tracking, hero legion, recruitment costs, training costs, and war consequences 
- **Repositories**: `HeroRepository` and `GuildRepository` implementing `IRepository<T>` for CRUD operations 
- **Business Logic** (150 pts): `Controller` implementing `IController` with 7 commands:
  - `AddHero`, `CreateGuild`, `RecruitHero`, `TrainingDay`, `StartWar`, `ValorState`, `Exit`
  - Full validation, error handling, and formatted output per spec
- **Unit Tests** (100 pts): Comprehensive test coverage for `MythicLegion.Tests` class without mocking 

## Architecture 
```
LegendsOfValor
│
├── Core/
│   ├── Contracts/
│   │   ├── IController.cs
│   │   ├── IRepository.cs
│   │   
│   ├── Engine                             # Program entry point
│   └── Controller                         # Business logic layer
│       
│
├── Models/
│   ├── Contracts/   
│   ├── Hero.cs                              # Base class
│   ├── Warrior.cs
│   ├── Sorcerer.cs
│   ├── Spellblade.cs
│   └── Guild.cs                             # Guild management
│
├── Repositories/
│   ├── Contracts/
│   ├── HeroRepository.cs
│   └── GuildRepository.cs
│
│
├── Utilities/                               # Constants and messages
│   ├── ExceptionMessages.cs
│   ├── OutputMessages.cs
│   └── Results/

```

## Test Architecture 
```
MysticLegion
├── MysticLegion
│   ├──  Hero.cs
│   │  
│   ├── Legion.cs
│   │                             
│   └── Program.cs
│
│   
├── MysticLegion.Tests
│   
```
## Technologies

- **.NET 6.0** with **Visual Studio 2022**
- **OOP Principles**: Inheritance, Polymorphism, Encapsulation, Abstraction
- **SOLID**: Single Responsibility, Open/Closed, Dependency Inversion via interfaces
- **Design Patterns**: Repository
- **Testing**: no mocking, full coverage


## Learning Outcomes

Mastered C# OOP inheritance hierarchies, interface contracts, repository pattern, command pattern, and production-quality validation/error handling in a complex domain model 

---
*Repository contains complete source code, test cases, and sample inputs/outputs. For more information please reffer to the problem description document*
