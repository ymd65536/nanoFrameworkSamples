using System;
using System.Device.I2c;
using System.Threading;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using nanoFramework.Hardware.Esp32;

namespace BME280_Sensor
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("BME280 Sensor Sample - PORT A");

            // PORT A I2C設定 (GPIO21=SDA, GPIO22=SCL)
            Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);
            Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);

            // I2C設定
            I2cConnectionSettings i2cSettings = new I2cConnectionSettings(1, Bme280.DefaultI2cAddress);
            I2cDevice i2cDevice = I2cDevice.Create(i2cSettings);

            try
            {
                using (Bme280 bme280 = new Bme280(i2cDevice))
                {
                    // センサー初期化
                    bme280.Reset();
                    Thread.Sleep(100);

                    // 測定設定
                    bme280.SetPowerMode(Bmx280PowerMode.Normal);

                    Console.WriteLine("BME280 initialized successfully!");
                    Console.WriteLine("Reading sensor data...");
                    Console.WriteLine();

                    while (true)
                    {
                        // センサーデータ読み取り
                        var temperature = bme280.ReadTemperature();
                        var pressure = bme280.ReadPressure();
                        var humidity = bme280.ReadHumidity();

                        // 結果表示
                        Console.WriteLine($"Temperature: {temperature.DegreesCelsius:F2} °C");
                        Console.WriteLine($"Pressure: {pressure.Hectopascals:F2} hPa");
                        Console.WriteLine($"Humidity: {humidity.Percent:F2} %");
                        Console.WriteLine("---");

                        Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
