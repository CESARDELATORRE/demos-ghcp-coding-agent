# GitHub Copilot Coding Agent Demo Repository

This repository is specifically designed for demonstrating GitHub Copilot Coding Agent capabilities with various programming languages and development scenarios.

**ALWAYS reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.**

## Working Effectively

### Pre-installed Development Environment
The environment comes pre-configured with:
- **.NET SDK**: 8.0.119 with ASP.NET Core runtime
- **Node.js**: v20.19.5 with npm 10.8.2 and yarn 1.22.22
- **Python**: 3.12.3 
- **Git**: 2.51.0
- **Essential tools**: curl, wget, and standard Linux utilities

### Repository Structure
This is a minimal demo repository containing:
```
.
├── .github/
│   └── copilot-instructions.md  # This file
├── .gitignore                   # Configured for .NET/Visual Studio projects
└── README.md                    # Basic repository description
```

### Creating Demo Projects

#### .NET Projects
Create and run .NET console applications:
```bash
# Create a new console app
dotnet new console --name YourDemoName
cd YourDemoName

# Build the project - takes ~3-6 seconds. NEVER CANCEL. Set timeout to 60+ seconds.
dotnet build

# Run the application
dotnet run
```

Create other .NET project types:
```bash
# Web API
dotnet new webapi --name YourApiDemo

# ASP.NET Core MVC
dotnet new mvc --name YourWebDemo

# Class library
dotnet new classlib --name YourLibraryDemo

# xUnit test project
dotnet new xunit --name YourTestDemo
```

#### Node.js Projects
Create and run Node.js applications:
```bash
# Initialize a new Node.js project
npm init -y

# Install dependencies (example with Express)
npm install express

# Create a simple server
echo 'const express = require("express");
const app = express();
const port = 3000;

app.get("/", (req, res) => {
  res.send("Hello from Express!");
});

app.listen(port, () => {
  console.log(`Server running at http://localhost:${port}`);
});' > server.js

# Run the application
node server.js
```

#### Python Projects
Create and run Python applications:
```bash
# Create a simple Python script
echo 'print("Hello from Python!")
def main():
    print("This is a demo Python application")

if __name__ == "__main__":
    main()' > demo.py

# Run the script
python3 demo.py

# Create a virtual environment (if needed for complex demos)
python3 -m venv venv
source venv/bin/activate
pip install requests
```

### Build and Test Validation

#### For .NET Projects
```bash
# Always run restore first for new projects
dotnet restore

# Build - takes 3-6 seconds typically. NEVER CANCEL. Set timeout to 60+ seconds.
dotnet build

# Run tests (if test projects exist)
dotnet test

# Run with specific configuration
dotnet run --configuration Release
```

#### For Node.js Projects
```bash
# Install dependencies - takes 10-30 seconds typically. NEVER CANCEL. Set timeout to 120+ seconds.
npm install

# Run tests (if configured in package.json)
npm test

# Start application
npm start

# Run in development mode (if configured)
npm run dev
```

#### For Python Projects
```bash
# Install dependencies (if requirements.txt exists)
pip install -r requirements.txt

# Run tests with unittest
python3 -m unittest discover

# Run tests with pytest (if installed)
pytest

# Run the main application
python3 main.py
```

### Manual Validation Requirements

**CRITICAL**: After creating any demo project, ALWAYS perform these validation steps:

1. **Build Verification**: Ensure the project builds without errors
2. **Execution Test**: Run the application and verify it starts correctly
3. **Functional Test**: Execute at least one complete user scenario:
   - For console apps: Run the program and verify expected output
   - For web apps: Start the server and test basic HTTP endpoints
   - For APIs: Make sample API calls and verify responses
   - For libraries: Create a test consumer and verify functionality

### Common Development Patterns

#### Creating a Full-Stack Demo
```bash
# Create a .NET API backend
dotnet new webapi --name DemoApi
cd DemoApi
dotnet build
dotnet run &  # Run in background

# Create a React frontend in parallel
cd ..
npx create-react-app demo-frontend
cd demo-frontend
npm start &  # Run in background

# Verify both are running
curl http://localhost:5000/weatherforecast  # Test API
curl http://localhost:3000  # Test React app
```

#### Creating Microservices Demo
```bash
# Service 1: User service (.NET)
dotnet new webapi --name UserService
cd UserService
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet build
cd ..

# Service 2: Order service (Node.js)
mkdir OrderService && cd OrderService
npm init -y
npm install express body-parser
# Create service implementation
cd ..

# Both services can be tested independently
```

### Timeout Guidelines and Performance Expectations

**CRITICAL TIMING INFORMATION (VALIDATED):**

- **Dotnet new console**: ~1-2 seconds
- **Dotnet new webapi**: ~4-5 seconds  
- **Dotnet build**: 3-6 seconds (first build), 1-3 seconds (incremental)
- **Dotnet restore**: 5-30 seconds depending on packages
- **npm init**: <1 second
- **npm install**: 10-30 seconds (simple projects), up to 2-3 minutes (complex projects)
- **npm start/run**: 5-15 seconds
- **Python script execution**: 1-3 seconds
- **pip install**: 5-60 seconds depending on packages

**NEVER CANCEL BUILD OR INSTALLATION COMMANDS**. Always set appropriate timeouts:
- Dotnet commands: 60+ seconds timeout
- npm install: 120+ seconds timeout
- pip install: 120+ seconds timeout

### Validation Scenarios

When testing changes to demo projects, ALWAYS run through these scenarios:

#### Console Applications
1. Build the application successfully
2. Run the application and verify expected console output
3. Test with different input parameters (if applicable)
4. Verify error handling works correctly

#### Web Applications
1. Build and start the application
2. Verify the application starts on the expected port (note: actual port may differ from configured port - check startup logs)
3. Test at least one HTTP endpoint manually using curl or browser
4. Verify error responses work correctly
5. Test shutdown process

#### APIs
1. Build and start the API service
2. Test GET endpoints: `curl http://localhost:port/endpoint`
3. Test POST endpoints with sample data
4. Verify response formats (JSON, XML, etc.)
5. Test error scenarios (invalid input, not found, etc.)

### Common Issues and Workarounds

#### .NET Issues
- **Build in /tmp directory**: Avoid creating .NET projects in /tmp - use /home/runner or subdirectories for reliable builds
- **Port conflicts**: Use `dotnet run --urls "http://localhost:5001"` to specify custom ports
- **Package conflicts**: Run `dotnet clean` then `dotnet restore` to resolve
- **Permission issues**: Ensure proper file permissions on project directories

#### Node.js Issues  
- **Port conflicts**: Use `PORT=3001 npm start` or modify package.json
- **Module not found**: Run `npm install` to ensure all dependencies are installed
- **EADDRINUSE errors**: Kill existing processes using the port

#### Python Issues
- **Module not found**: Use virtual environments and pip install
- **Permission errors**: Use `python3 -m pip install --user` for user installations
- **Version conflicts**: Specify Python version explicitly with `python3`

### Essential Commands Reference

Quick reference for frequently used commands:

```bash
# Repository exploration
ls -la                          # List all files including hidden
find . -name "*.json"          # Find specific file types
grep -r "search-term" .        # Search content in files

# Project creation
dotnet new --list              # List available .NET templates
npm init                       # Initialize Node.js project
python3 -m venv env            # Create Python virtual environment

# Development workflow
dotnet watch run               # Auto-reload .NET applications
npm run dev                    # Development mode (if configured)
python3 -m http.server 8000    # Quick Python HTTP server

# Testing and validation
curl -I http://localhost:port  # Test HTTP endpoint health
netstat -tulpn | grep :port    # Check if port is in use
ps aux | grep process-name     # Check running processes
```

### Repository Best Practices

When creating demos in this repository:

1. **Always create demos in subdirectories** to keep the root clean
2. **Use descriptive directory names** like `dotnet-api-demo` or `react-frontend-demo`
3. **Include README.md files** in each demo directory explaining the specific demo
4. **Test thoroughly** before committing - run through complete user scenarios
5. **Document any special requirements** or setup steps in demo-specific README files
6. **Clean up temporary files** - use the .gitignore patterns to exclude build artifacts

### Troubleshooting

If you encounter issues:

1. **Check this file first** for known solutions
2. **Verify environment setup** - ensure required tools are available
3. **Check logs** - look for error messages in build/run output
4. **Test in isolation** - create minimal reproduction in /home/runner or subdirectories (avoid /tmp for .NET projects)
5. **Validate networking** - ensure ports are available and services can communicate

Remember: This repository is designed for demonstrations and learning. Focus on creating clear, working examples that showcase GitHub Copilot capabilities effectively.