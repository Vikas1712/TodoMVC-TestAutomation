# TestAutomationFramework C# UI and API
NET 6 LTS (C# 10), SpecFlow, RestSharp, Selenium
# ✨ SeleniumCSharp

---

🚀 This repository contains a Selenium framework implemented using C# (CSharp) for UI testing.  \
The framework utilizes various tools and components to facilitate efficient and reliable UI testing.

---

## ✨ Requirements.

- Visual Studio IDE 2022 Or JetBrain Rider 2022.2.3
- Chrome Browser

---

## ✨ Instructions for Windows

- Clone the repo **git clone https://github.com/Vikas1712/TodoMVC-TestAutomation**
- Once done, open the solution in Visual Studio IDE or JetBrain Rider  **SeleniumCSharp.sln**
- Install Nuget packgae **Right-click on project and click "Manage NuGet Package" and install it**

---

## ✨ Package information

The framework utilizes the following components:

- **Selenium WebDriver**: An open-source web automation framework for cross-browser testing.
- **SpecFlow**: A tool supporting Behavior Driven Development (BDD) practices in the .NET framework.
- **ExtentReport**: A reporting tool for generating custom logs, snapshots, pie charts, and dashboards.

---

## ✨ Structure

The framework is structured into the following directories:

- **Base**- Contain page initialization with base class.
- **Config**-  Handles the configuration of the config XML file.
- **Extension**- Provides WebDriver and WebElement extension methods.
- **Feature**- Contains the feature files for BDD scenarios
- **Hooks**-  Includes SpecFlow hooks for setup and teardown.
- **Pages**- Contains page objects following the Page Object Model (POM) pattern.
- **Steps**- Defines the step definitions for the SpecFlow scenarios.
- **Result**- Stores the test result.
- **ReadMe**- Contains the framework development and instruction document.

---

## ✨ Execution Instructions

To execute the tests, perform the following steps:

1. Set up the required test environment dependencies. For front-end testing, make sure you have a compatible web browser and WebDriver set up.
2. Configure any necessary test environment settings or configurations.
3. Run the tests from your IDE or execute the test projects via the command line.
4. Monitor the test execution progress and review the test results.
---

## ✨ Docker

This project includes Docker support. You can run the project with Docker by following these steps:

1. Install Docker on your machine.
2. Pull the necessary Docker images by running the appropriate Docker pull commands.
3. Set up the required test environment dependencies within the Docker container.
4. Build and run the project within the Docker container, ensuring the necessary configurations are in place.

---

## ✨ Codebase is developed on below  components
The framework includes the following code components:
- [🚀 **Page Initialization with Base Class**]
  - Provides a base abstract class for page initialization, eliminating the need to repeatedly pass the WebDriver object.
- [🚀 **Page Navigation**]
  - Establishes relationships between pages and performs operations, potentially returning a page object.
- [🚀 **Page Generator**]
  - Implements the Factory Design Pattern to avoid object initialization in tests.
- [🚀 **Introduction to Generics in C#**] 
  - Utilizes generics in C# to create classes or functions that can work with any data type.
- [🚀 **WebDriver and WebElement Extension Methods**]
  - Adds extension methods to WebDriver and WebElement classes for additional functionality.

---

## 🚀 Page Initialization with Base Class


### Old Code Implementation:

    Many times, we try to pass the WebDriver object over and over again from one class to another class.
    By the means of Constructor or passing it as a parameter in the method where IWebDriver instance is required.

![image](https://user-images.githubusercontent.com/13363157/180227839-15b0eefe-e70b-4fbd-b82f-910d411764d5.png)

### New	Code Implementation

      Created a base abstract class and create a private property

![image](https://user-images.githubusercontent.com/13363157/180231069-97e8d61a-e6fd-47fa-a092-727cdbca867e.png)

---

## 🚀 Page Navigation

### Old Code function without return type

    We create a function or method without any return type.

![image](https://user-images.githubusercontent.com/13363157/180240799-628e1b95-74f7-452b-8b79-bd32bfd1214f.png)

### New Code for Page Navigation

    Ensure that business logic is embedded in our test code.
    We can establish relationships between each page.
    While an operation is performed on one method it may or may not return a page object.

![image](https://user-images.githubusercontent.com/13363157/180239114-a15383c9-51fd-4ee8-aac1-b02afcdf81e1.png)

---

## 🚀 Page Generator

---

#### The Thumb Rule of Factory Design Pattern is the avoiding object initialization in the test.

#### Old Code

![image](https://user-images.githubusercontent.com/13363157/180242809-7adb3591-26fc-4d35-8b6e-baaddc260a95.png)

#### New Code

        How did we accomplish this? Generics in C# were used to accomplish this. 
        In the following section, we discuss this topic in more detail

![image](https://user-images.githubusercontent.com/13363157/180243477-caceffc6-cf37-4cb7-befa-704d7c74cf63.png)

---

## 🚀 Introduction to Generics in C#

    Generics in C# give the user the ability to write classes or functions that can work with any data type easily.
    We might say a generic class or function that is compatible with any other data type. 
    All we need to do is define it with a placeholder.
    To put it simply, we have to write a set of methods or classes that take arguments based on the data type. 
    The constructor will therefore generate code to handle the specified data type when it encounters a compiler.

### Generics Implementation in Code

![tempsnip1](https://user-images.githubusercontent.com/13363157/180247732-a0cbb510-f019-4dfa-9a02-0d694ee1600d.png)

---

## 🚀 WebDriver and WebElement Extension Methods

    Extension Methods enable you to add methods to an existing type without creating a new derived type.
    An extension method is a static method of a static class.
    Where the “this” modifier is applied to the first parameter.
    The type of the first parameter will be the type that is extended.

![image](https://user-images.githubusercontent.com/13363157/180249818-79f459f1-5883-459d-815a-6b96a8b61f25.png)

---

## Calling extension method in script

![image](https://user-images.githubusercontent.com/13363157/180250869-ddcbd2a8-f089-4a26-8200-178fe102a88a.png)

---

## 🚀 Executing on Docker

- Install Docker
  - If you haven't already, install Docker on your machine. You can download Docker from the official website: https://www.docker.com/get-started.
- Pull the Chrome Docker Image
  - Open a terminal or command prompt and run the following command to pull the Chrome Docker image
  - **docker pull selenium/standalone-chrome**
  - Open a terminal or command prompt and run the following command to pull the Selenium Grid Docker image
  - **docker pull selenium/hub:4.0.0**
- Start the Chrome Docker Container 
  - Run the following command to start a Docker container using the Chrome image
  - **docker run -d -p 4444:4444 --name selenium-chrome selenium/standalone-chrome**
- Start the Selenium Grid Hub
  - Open a terminal or command prompt and run the following command to start the Selenium Grid hub
  - **docker run -d -p 4444:4444 --name selenium-hub selenium/hub:4.0.0**
- Make sure the Selenium Grid hub and node containers are running **docker run** while executing your tests. 
- You can stop the containers by running **docker stop selenium-hub** and **docker stop selenium-node-chrome** in the terminal or command prompt.

---

## 🚀 Further Improvement

- Executing test in parallel
