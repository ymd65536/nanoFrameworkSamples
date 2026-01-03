# M5Stack Fire Internal Devices

This folder contains examples for using the internal devices on the M5Stack Fire with .NET nanoFramework.

## M5Stack Fire Overview

The M5Stack Fire is a powerful ESP32-based development kit with various built-in sensors and peripherals:

### Built-in Devices

- **MPU6886**: 6-axis IMU (Inertial Measurement Unit) with 3-axis gyroscope and 3-axis accelerometer
- **Three Buttons**: A, B, and C buttons for user input
- **2.0" TFT LCD Screen**: 320x240 pixel IPS display (ILI9342C)
- **Speaker/Buzzer**: Built-in speaker for audio output
- **RGB LED Bar**: 10 SK6812 addressable RGB LEDs on the side
- **SD Card Slot**: MicroSD card reader for storage
- **PSRAM**: 4MB PSRAM for extended memory
- **Battery**: 600mAh rechargeable lithium battery with charging circuit

## Examples

### 01 - MPU6886 IMU
Demonstrates reading accelerometer and gyroscope data from the MPU6886 sensor.

### 02 - Buttons
Shows how to read the three physical buttons (A, B, C) and respond to button presses.

### 03 - Screen
Displays text and graphics on the built-in TFT LCD screen.

### 04 - Speaker
Plays tones and sounds using the built-in speaker/buzzer.

### 05 - RGB LED Bar
Controls the 10 addressable RGB LEDs on the side panel.

### 06 - SD Card
Reads and writes files to the microSD card.

## Requirements

- .NET nanoFramework firmware for M5Stack Fire
- nanoFramework.M5Stack NuGet package
- Visual Studio 2022 or later with nanoFramework extension

## Getting Started

1. Flash your M5Stack Fire with the latest nanoFramework firmware
2. Install the nanoFramework Visual Studio extension
3. Create a new nanoFramework application
4. Add the nanoFramework.M5Stack NuGet package
5. Copy the example code and deploy to your device

## Resources

- [nanoFramework Documentation](https://docs.nanoframework.net/)
- [M5Stack Fire Documentation](https://docs.m5stack.com/en/core/fire)
- [nanoFramework GitHub](https://github.com/nanoframework)

## License

These examples are provided as-is for educational purposes.
