Contributing to VRCScalerOSC

Thank you for your interest in contributing to VRCScalerOSC! This document provides guidelines and instructions for contributing to the project.

Table of Contents
- Getting Started
- Development Setup
- Code Standards
- Commit Guidelines
- Pull Request Process
- Reporting Issues
- Support for VRChat Avatar Tools

Getting Started

1. Fork the repository on GitHub
2. Clone your fork locally
3. Create a new branch for your feature or fix
4. Make your changes
5. Push to your fork
6. Submit a pull request

Development Setup

Prerequisites
- .NET 9 SDK or later
- Visual Studio 2022 or Visual Studio Code
- Git

Cloning the Repository
git clone https://github.com/phone2345/VRCScalerOSC.git
cd VRCScalerOSC

Building the Project
dotnet restore
dotnet build -c Release

The project includes three main components:
- VRCScalerOSC - Core library with OSC, avatar scaling, and VRChat integration
- VRCScalerOSC_Windows - Windows Forms desktop application
- VRCScalerOSC_Console - Cross-platform console application

Running the Applications

Windows Desktop Application
dotnet run --project VRCScalerOSC_Windows -c Release

Console Application
dotnet run --project VRCScalerOSC_Console -c Release

Code Standards

Language and Framework
- Language: C# with .NET 9
- Platform Support: Windows and Linux
- Architecture: MVVM pattern with Model-View-ViewModel separation

Code Style
- Follow Microsoft C# Coding Conventions
- Use meaningful variable and method names
- Add XML documentation comments for public types and methods
- Keep methods focused and maintainable
- Use nullable reference types (enabled in project)

Project Structure
VRCScalerOSC/
??? Model/               # Data models and business logic
??? ViewModel/           # MVVM ViewModel classes
??? Controller/          # Application controller and settings management
??? Service/             # OSC and VRChat integration services
??? View/                # Console view and UI components
??? Localization/        # Multi-language support (English, Chinese, Japanese, Korean)

Adding Features

When adding new avatar tool support or scaling features:
1. Create appropriate model classes in Model/SupportAvatarTool/
2. Implement scaling logic in dedicated avatar tool classes
3. Update Controller_Scaler to handle new tool types
4. Add localization strings for all supported languages
5. Test with relevant VRChat avatars

Localization
The project supports multiple languages:
- English (enUS)
- Simplified Chinese (zhCN)
- Traditional Chinese (zhTW)
- Japanese (jaJP)
- Korean (koKR)

When adding new features with UI strings:
1. Add English strings to Localization_enUS.cs
2. Add translations to all other language files
3. Use Localization.GetString() to access localized strings

OSC Protocol Integration
When modifying OSC communication:
1. Review Service_VRCOSCQuery for VRChat OSC query protocol
2. Review Service_VRCOSCProtocols for OSC data handling
3. Update OSCData model if new message types are added
4. Ensure backward compatibility with existing settings

Commit Guidelines

Commit Messages
Follow conventional commit format:
type(scope): subject

type:
- feat: A new feature
- fix: A bug fix
- docs: Documentation changes
- style: Code style changes (formatting, etc.)
- refactor: Code refactoring
- perf: Performance improvements
- test: Adding or updating tests
- chore: Build, dependency updates, etc.

Examples:
feat(controller): add new avatar scaling mode
fix(osc): resolve connection timeout issue
docs(readme): update installation instructions

Commit Practices
- Make logical, atomic commits
- One feature or fix per commit
- Write clear commit messages
- Reference GitHub issues when applicable (e.g., "Fixes #123")

Pull Request Process

Before Submitting
1. Ensure your code compiles without errors or warnings
2. Test your changes thoroughly
3. Update documentation if needed
4. Add translations for UI strings in all supported languages
5. Verify the automated build passes

Creating a Pull Request
1. Push your changes to your fork
2. Create a PR against the master branch
3. Use descriptive title and description
4. Reference related issues (e.g., "Closes #123")
5. Include before/after screenshots for UI changes

PR Requirements
- Code must pass automated build verification
- Clear description of changes and motivation
- Tests should pass (if applicable)
- Documentation should be updated
- No breaking changes without discussion

After Submission
- Respond to code review comments promptly
- Make requested changes in new commits
- Ensure builds pass after each update
- Be respectful and collaborative in discussions

Reporting Issues

How to Report a Bug
1. Check existing issues to avoid duplicates
2. Use a clear, descriptive title
3. Describe the exact steps to reproduce
4. Explain expected vs. actual behavior
5. Include your environment (OS, .NET version)
6. Attach logs or screenshots if applicable

Feature Requests
1. Clearly describe the desired feature
2. Explain the use case and motivation
3. Provide examples of how it would work
4. Consider implementation complexity

Support for VRChat Avatar Tools

Currently Supported Tools
- VRCScaler
- MagScaler
- JackalAvatarScalerV3
- MenuControlCamera
- RSSAdjOld

Adding Support for New Tools
1. Create a new class inheriting from or implementing avatar tool interface
2. Place it in Model/SupportAvatarTool/
3. Implement required scaling and data handling methods
4. Register the tool in Controller_Scaler
5. Add documentation and examples
6. Test thoroughly with VRChat avatars using the tool

Testing with VRChat
- Ensure OSC communication works correctly
- Test avatar scaling with various scaling rates
- Verify gesture input recognition (if applicable)
- Check compatibility with different VRChat avatar versions

Performance Considerations
- Minimize OSC message frequency
- Profile code for smooth 60+ FPS operation
- Consider memory usage for long-running processes
- Test on both Windows and Linux platforms

Questions or Need Help?

- Check GitHub Issues for similar questions
- Review existing code for similar implementations
- Create a discussion or issue if clarification is needed
- Be patient and respectful in community interactions

License

By contributing to VRCScalerOSC, you agree that your contributions will be licensed under the MIT License, consistent with the project license.

Thank you for contributing to VRCScalerOSC!
