using System;
using System.Device.I2c;
using System.Threading;
using nanoFramework.Hardware.Esp32;

namespace I2C_Scanner
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("I2C Scanner Sample - PORT A");
            Console.WriteLine("Scanning I2C bus for devices...");
            Console.WriteLine();

            // PORT A I2C設定 (GPIO21=SDA, GPIO22=SCL)
            Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);
            Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);

            int devicesFound = 0;

            // I2Cアドレス範囲: 0x03 - 0x77 をスキャン
            Console.WriteLine("Scanning addresses 0x03 to 0x77...");
            Console.WriteLine();

            for (int address = 0x03; address <= 0x77; address++)
            {
                I2cConnectionSettings settings = new I2cConnectionSettings(1, address);
                I2cDevice device = null;

                try
                {
                    device = I2cDevice.Create(settings);
                    
                    // デバイスにアクセスを試みる
                    byte[] buffer = new byte[1];
                    device.Read(buffer);

                    // 成功した場合、デバイスが存在
                    Console.WriteLine($"Device found at address 0x{address:X2} ({address})");
                    devicesFound++;
                }
                catch
                {
                    // デバイスが存在しない場合は例外が発生
                    // 何もしない
                }
                finally
                {
                    device?.Dispose();
                }

                // 進捗表示
                if (address % 16 == 0)
                {
                    Console.Write(".");
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Scan complete. Found {devicesFound} device(s).");
            Console.WriteLine();

            // 一般的なデバイスアドレスのリファレンス
            Console.WriteLine("Common I2C device addresses:");
            Console.WriteLine("  0x3C, 0x3D - OLED Display (SSD1306)");
            Console.WriteLine("  0x68, 0x69 - MPU6050 IMU");
            Console.WriteLine("  0x76, 0x77 - BME280/BMP280 Sensor");
            Console.WriteLine("  0x48-0x4F  - ADS1115 ADC");
            Console.WriteLine("  0x50-0x57  - EEPROM (AT24C)");
            Console.WriteLine();

            // 継続的にスキャン
            while (true)
            {                
                Console.WriteLine("Press any key to scan again (waiting 10 seconds)...");
                Thread.Sleep(10000);
                
                Console.WriteLine();
                Console.WriteLine("Rescanning...");
                devicesFound = 0;

                for (int address = 0x03; address <= 0x77; address++)
                {
                    I2cConnectionSettings settings = new I2cConnectionSettings(1, address);
                    I2cDevice device = null;

                    try
                    {
                        device = I2cDevice.Create(settings);
                        byte[] buffer = new byte[1];
                        device.Read(buffer);
                        Console.WriteLine($"Device found at address 0x{address:X2}");
                        devicesFound++;
                    }
                    catch { }
                    finally
                    {
                        device?.Dispose();
                    }
                }

                Console.WriteLine($"Found {devicesFound} device(s).");
                Console.WriteLine();
            }
        }
    }
}
