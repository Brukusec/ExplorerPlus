# ExplorerPlus

ExplorerPlus is a unique file explorer application designed to provide enhanced functionality over traditional file explorers. It offers a clean interface, easy navigation, and additional features such as logging, command prompt integration, and execution from randomized locations.

<p align="center">
<img src="https://github.com/Brukusec/ExplorerPlus/blob/master/Explorer.jpg" width="50%" height="50%">
</p>

### Features
- **Back Navigation:** Easily navigate back to the previous directory.
- **Randomized Execution:** For enhanced security, ExplorerPlus creates random folders and executes files within these locations. This aims to prevent Endpoint Detection and Response (EDR) systems from making correlations based on file locations.
- **Standalone Executable Support:** ExplorerPlus is specifically designed to work with standalone executables.
- **Log File Generation:** Automatically generates a log file named timestamp_log.log that captures actions performed on files, such as opening and closing.
- **Command Prompt Integration:** Right-click on a file to open it in a command prompt window.
- **User-Friendly Interface:** Clean and intuitive design that makes file browsing a breeze.

### Getting Started

#### Prerequisites
- Windows OS
- .NET Framework


### Compiling
If you wish to compile ExplorerPlus from source, follow these steps:

#### Prerequisites
- Visual Studio (2019 or newer recommended)
- .NET Framework (version used in the project)

#### Steps
1. Clone the Repository:
```
git clone https://github.com/Brukusec/ExplorerPlus.git
```
2. **Open the Solution:**
*  Navigate to the directory where you cloned the repository and open the '**ExplorerPlus.sln**' file with Visual Studio.
3. **Build the Solution:**
*  Press Ctrl + Shift + B or navigate to Build > Build Solution from the top menu.


### Installation
1. Download the latest release from the [releases page](https://github.com/Brukusec/ExplorerPlus/releases/tag/v1.0.0). <!-- Replace '#' with the actual link to your releases page if you have one. -->
1. Extract the downloaded file.
1. Run ExplorerPlus.exe.

### Usage
1. Navigate through directories using the main interface.
1. Use the back button to return to the previous directory.
1. Double-click on a file to open it.
1. Right-click on a file and select '**CMD**' to open it in a command prompt window.
1. Check the '**timestamp_log.log**' file in the application's directory for a log of actions performed.

### License

This project is licensed under the GNU GENERAL PUBLIC LICENSE. See the [LICENSE](https://github.com/Brukusec/ExplorerPlus/blob/master/LICENSE.txt) file for details.
