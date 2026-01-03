using System;
using System.Device.I2c;
using System.Threading;

Console.WriteLine("M5Stack Fire - Custom I2C Example");
Console.WriteLine("==================================");

// M5Stack Fire I2C configuration (Port A: GPIO21=SDA, GPIO22=SCL)
const int I2C_BUS = 1;
const byte DEVICE_ADDRESS = 0x68; // Example: MPU6050 or RTC address

var i2cSettings = new I2cConnectionSettings(I2C_BUS, DEVICE_ADDRESS)
{
    BusSpeed = I2cBusSpeed.FastMode
};

using var i2cDevice = I2cDevice.Create(i2cSettings);

Console.WriteLine($"I2C Device initialized:");
Console.WriteLine($"  Bus: {I2C_BUS}");
Console.WriteLine($"  Address: 0x{DEVICE_ADDRESS:X2}");
Console.WriteLine($"  Speed: {i2cSettings.BusSpeed}");
Console.WriteLine();

var readBuffer = new byte[6];
var writeBuffer = new byte[] { 0x3B }; // Example register address

while (true)
{
    try
    {
        // Write register address
        i2cDevice.Write(writeBuffer);
        
        // Read data
        i2cDevice.Read(readBuffer);
        
        Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss}] Data read:");
        for (int i = 0; i < readBuffer.Length; i++)
        {
            Console.Write($"0x{readBuffer[i]:X2} ");
        }
        Console.WriteLine();
        
        // Convert to 16-bit values (example)
        var value1 = (short)((readBuffer[0] << 8) | readBuffer[1]);
        var value2 = (short)((readBuffer[2] << 8) | readBuffer[3]);
        var value3 = (short)((readBuffer[4] << 8) | readBuffer[5]);
        
        Console.WriteLine($"Converted values: {value1}, {value2}, {value3}");
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    
    Thread.Sleep(1000);
}
