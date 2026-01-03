using System;
using System.Device.I2c;
using System.Device.Gpio;
using System.Threading;
using Iot.Device.Ssd13xx;
using Iot.Device.Ssd13xx.Commands;

Console.WriteLine("M5Stack Fire - OLED Display Example");
Console.WriteLine("====================================");

// M5Stack Fire I2C configuration (Port A: GPIO21=SDA, GPIO22=SCL)
var i2cSettings = new I2cConnectionSettings(1, 0x3C)
{
    BusSpeed = I2cBusSpeed.FastMode
};

using var i2cDevice = I2cDevice.Create(i2cSettings);
using var display = new Ssd1306(i2cDevice, Ssd13xx.DisplayResolution.OLED128x64);

display.ClearScreen();
Console.WriteLine("OLED Display initialized (128x64)");

var counter = 0;

while (true)
{
    display.ClearScreen();
    
    // Draw text
    display.DrawString(0, 0, "M5Stack Fire", 1, true);
    display.DrawString(0, 12, "OLED Display", 1, true);
    display.DrawString(0, 24, "Port A I2C", 1, true);
    display.DrawString(0, 40, $"Count: {counter}", 1, true);
    display.DrawString(0, 52, DateTime.UtcNow.ToString("HH:mm:ss"), 1, true);
    
    display.Display();
    
    Console.WriteLine($"Updated display - Counter: {counter}");
    counter++;
    
    Thread.Sleep(1000);
}
