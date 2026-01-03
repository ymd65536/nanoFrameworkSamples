using System;
using System.Device.I2c;
using System.Threading;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using UnitsNet;

Console.WriteLine("M5Stack Fire - BME280 Display Example");
Console.WriteLine("=====================================");

// M5Stack Fire I2C configuration (Port A: GPIO21=SDA, GPIO22=SCL)
var i2cSettings = new I2cConnectionSettings(1, Bme280.DefaultI2cAddress)
{
    BusSpeed = I2cBusSpeed.FastMode
};

using var i2cDevice = I2cDevice.Create(i2cSettings);
using var bme280 = new Bme280(i2cDevice);

// Configure sensor
bme280.SetPowerMode(Bmx280PowerMode.Normal);

Console.WriteLine($"BME280 initialized on I2C address: 0x{Bme280.DefaultI2cAddress:X2}");
Console.WriteLine("Reading environmental data...");
Console.WriteLine();

var sampleCount = 0;

while (true)
{
    if (bme280.TryReadTemperature(out var temperature) &&
        bme280.TryReadPressure(out var pressure) &&
        bme280.TryReadHumidity(out var humidity))
    {
        Console.Clear();
        Console.WriteLine("M5Stack Fire - BME280 Environmental Monitor");
        Console.WriteLine("===========================================");
        Console.WriteLine($"Sample: {sampleCount}");
        Console.WriteLine($"Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC");
        Console.WriteLine();
        Console.WriteLine($"Temperature: {temperature.DegreesCelsius:F2}°C ({temperature.DegreesFahrenheit:F2}°F)");
        Console.WriteLine($"Pressure:    {pressure.Hectopascals:F2} hPa");
        Console.WriteLine($"Humidity:    {humidity.Percent:F2}%");
        Console.WriteLine();
        
        // Calculate approximate altitude (assuming sea level pressure = 1013.25 hPa)
        var altitude = 44330.0 * (1.0 - Math.Pow(pressure.Hectopascals / 1013.25, 0.1903));
        Console.WriteLine($"Approx. Altitude: {altitude:F2}m");
        
        sampleCount++;
    }
    else
    {
        Console.WriteLine("Failed to read sensor data");
    }
    
    Thread.Sleep(2000);
}
