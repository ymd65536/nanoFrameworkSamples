using System;
using System.Device.Gpio;
using System.Threading;

Console.WriteLine("M5Stack Fire - GPIO Input Example");
Console.WriteLine("==================================");

// M5Stack Fire Port A pins: GPIO21 (SDA), GPIO22 (SCL)
// Using GPIO22 as input example with pull-up
const int INPUT_PIN = 22;

using var gpio = new GpioController();

gpio.OpenPin(INPUT_PIN, PinMode.InputPullUp);
Console.WriteLine($"GPIO{INPUT_PIN} configured as INPUT with PULL-UP");
Console.WriteLine("Monitoring pin state...");
Console.WriteLine();

var lastState = PinValue.High;
var changeCount = 0;

// Register event handler for pin value changes
gpio.RegisterCallbackForPinValueChangedEvent(
    INPUT_PIN,
    PinEventTypes.Falling | PinEventTypes.Rising,
    (sender, args) =>
    {
        changeCount++;
        var newState = gpio.Read(INPUT_PIN);
        Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss}] Pin changed! {args.ChangeType} -> {newState} (Change #{changeCount})");
    });

Console.WriteLine("Event handler registered. Press CTRL+C to exit.");
Console.WriteLine();

while (true)
{
    var currentState = gpio.Read(INPUT_PIN);
    
    if (currentState != lastState)
    {
        Console.WriteLine($"State: {currentState}");
        lastState = currentState;
    }
    
    Thread.Sleep(100);
}
