using nanoFramework.M5Stack;
using nanoFramework.Hardware.Esp32;
using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;

// Initialize M5Stack Fire
M5Core2.InitializeScreen();
Debug.WriteLine("M5Stack Fire - Buttons Example");
Debug.WriteLine("===============================");
Debug.WriteLine("Press buttons A, B, or C");
Debug.WriteLine("");

// M5Stack Fire button pins
const int BUTTON_A_PIN = 39;
const int BUTTON_B_PIN = 38;
const int BUTTON_C_PIN = 37;

// Setup GPIO controller
var gpio = new GpioController();

// Configure button pins as input with pull-up
var buttonA = gpio.OpenPin(BUTTON_A_PIN, PinMode.InputPullUp);
var buttonB = gpio.OpenPin(BUTTON_B_PIN, PinMode.InputPullUp);
var buttonC = gpio.OpenPin(BUTTON_C_PIN, PinMode.InputPullUp);

// Track button states
bool lastStateA = true;
bool lastStateB = true;
bool lastStateC = true;

int countA = 0;
int countB = 0;
int countC = 0;

while (true)
{
    // Read button states (LOW = pressed, HIGH = not pressed)
    bool stateA = buttonA.Read() == PinValue.High;
    bool stateB = buttonB.Read() == PinValue.High;
    bool stateC = buttonC.Read() == PinValue.High;

    // Check Button A
    if (!stateA && lastStateA)
    {
        countA++;
        Debug.WriteLine($"Button A pressed! Count: {countA}");
    }

    // Check Button B
    if (!stateB && lastStateB)
    {
        countB++;
        Debug.WriteLine($"Button B pressed! Count: {countB}");
    }

    // Check Button C
    if (!stateC && lastStateC)
    {
        countC++;
        Debug.WriteLine($"Button C pressed! Count: {countC}");
    }

    // Update last states
    lastStateA = stateA;
    lastStateB = stateB;
    lastStateC = stateC;

    // Small delay for debouncing
    Thread.Sleep(50);
}
