using System;
using System.Device.Gpio;
using System.Threading;

Console.WriteLine("M5Stack Fire - GPIO Output Example");
Console.WriteLine("===================================");

// M5Stack Fire Port A pins: GPIO21 (SDA), GPIO22 (SCL)
// Using GPIO21 as output example
const int OUTPUT_PIN = 21;

using var gpio = new GpioController();

gpio.OpenPin(OUTPUT_PIN, PinMode.Output);
Console.WriteLine($"GPIO{OUTPUT_PIN} configured as OUTPUT");
Console.WriteLine("Starting blink pattern...");
Console.WriteLine();

var state = PinValue.Low;
var counter = 0;

while (true)
{
    gpio.Write(OUTPUT_PIN, state);
    
    Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss}] GPIO{OUTPUT_PIN} = {state} (Count: {counter})");
    
    state = state == PinValue.Low ? PinValue.High : PinValue.Low;
    counter++;
    
    Thread.Sleep(500);
}
