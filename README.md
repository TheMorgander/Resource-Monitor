# Resource Monitor
## Overview
This is a C# WPF Application that creates an overlay at the top of the screen showing system resource usage.

Resources include:

* CPU Usage/Temperature
* GPU Usage/Temperature
* RAM Usage
* Disk Read/Write Rate
* Network Upload/Download Rate

  
<img align="center" src="https://github.com/TheMorgander/Resource-Monitor/blob/master/Resources/Images/Example%201.png?raw=true"/>

## Usage
Once installed, you can access the settings window by double clicking the overlay at the top of the screen. From there, you can select which hardware to record values from. Each hardware has different sensors, which can be selected after picking the assosiated hardware.

Right clicking the overlay will hide it temporarily in case you need to access something under the overlay.

**Refresh Frequency** determines how frequently metrics are taken in ms.

**Hidden Delay** sets how long the overlay is hidden when right clicking in ms.

## External Libraries
This project uses LibreHardwareMonitor for collecting system resources.

See: https://github.com/LibreHardwareMonitor/LibreHardwareMonitor 
