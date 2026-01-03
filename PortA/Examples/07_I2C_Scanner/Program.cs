using System;
using System.Device.I2c;
using System.Threading;

Console.WriteLine("M5Stack Fire - I2C Scanner");
Console.WriteLine("===========================");
Console.WriteLine("Port A: GPIO21=SDA, GPIO22=SCL");
Console.WriteLine();

const int I2C_BUS = 1;
var devicesFound = 0;

while (true)
{
    Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss}] Scanning I2C bus {I2C_BUS}...");
    Console.WriteLine("     0  1  2  3  4  5  6  7  8  9  A  B  C  D  E  F");
    
    devicesFound = 0;
    
    for (byte address = 0; address < 128; address++)
    {
        if (address % 16 == 0)
        {
            Console.Write($"{address:X2}: ");
        }
        
        // Skip reserved addresses
        if (address < 0x03 || address > 0x77)
        {
            Console.Write("   ");
        }
        else
        {
            try
            {
                var settings = new I2cConnectionSettings(I2C_BUS, address)
                {
                    BusSpeed = I2cBusSpeed.StandardMode
                };
                
                using var device = I2cDevice.Create(settings);
                var buffer = new byte[1];
                device.Read(buffer);
                
                Console.Write($"{address:X2} ");
                devicesFound++;
            }
            catch
            {
                Console.Write("-- ");
            }
        }
        
        if ((address + 1) % 16 == 0)
        {
            Console.WriteLine();
        }
    }
    
    Console.WriteLine();
    Console.WriteLine($"Scan complete. Found {devicesFound} device(s).");
    Console.WriteLine("Next scan in 5 seconds...");
    Console.WriteLine();
    
    Thread.Sleep(5000);
}
