using System;
using System.Device.I2c;
using System.Threading;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using nanoFramework.Hardware.Esp32;

// M5Stack Fire - PORT A BME280センサーサンプル
// BME280温湿度・気圧センサーをPORT Aに接続して使用します

namespace M5StackFire.PortA.BME280
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("M5Stack Fire - PORT A BME280 Sensor");
            Console.WriteLine("=====================================");

            // PORT A のI2Cピン設定
            // GPIO21 = SDA (Data)
            // GPIO22 = SCL (Clock)
            Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);
            Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);

            // I2C設定 (BME280のデフォルトアドレス: 0x76 または 0x77)
            var settings = new I2cConnectionSettings(1, Bme280.DefaultI2cAddress);
            var i2cDevice = I2cDevice.Create(settings);

            try
            {
                using (var bme280 = new Bme280(i2cDevice))
                {
                    Console.WriteLine("BME280 初期化完了");

                    // センサー設定
                    bme280.TemperatureSampling = Sampling.UltraHighResolution;
                    bme280.PressureSampling = Sampling.UltraHighResolution;
                    bme280.HumiditySampling = Sampling.UltraHighResolution;

                    Console.WriteLine("測定開始...");
                    Console.WriteLine();

                    while (true)
                    {
                        // 測定実行
                        bme280.SetPowerMode(Bmx280PowerMode.Forced);
                        Thread.Sleep(100);

                        // データ読み取り
                        if (bme280.TryReadTemperature(out var temperature) &&
                            bme280.TryReadPressure(out var pressure) &&
                            bme280.TryReadHumidity(out var humidity))
                        {
                            Console.WriteLine($"温度: {temperature.DegreesCelsius:F2} °C");
                            Console.WriteLine($"湿度: {humidity.Percent:F1} %");
                            Console.WriteLine($"気圧: {pressure.Hectopascals:F1} hPa");
                            Console.WriteLine("---");
                        }

                        Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"エラー: {ex.Message}");
            }

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
