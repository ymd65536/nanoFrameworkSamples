using System;
using System.Device.I2c;
using System.Threading;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using Iot.Device.Ssd13xx;
using nanoFramework.Hardware.Esp32;

namespace BME280_Display
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("BME280 + OLED Display Sample - PORT A");

            // PORT A I2C設定 (GPIO21=SDA, GPIO22=SCL)
            Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);
            Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);

            // BME280センサー設定
            I2cConnectionSettings bme280Settings = new I2cConnectionSettings(1, Bme280.DefaultI2cAddress);
            I2cDevice bme280Device = I2cDevice.Create(bme280Settings);

            // OLEDディスプレイ設定
            I2cConnectionSettings displaySettings = new I2cConnectionSettings(1, 0x3C);
            I2cDevice displayDevice = I2cDevice.Create(displaySettings);

            try
            {
                using (Bme280 bme280 = new Bme280(bme280Device))
                using (Ssd1306 display = new Ssd1306(displayDevice, Ssd1306.DisplayResolution.W128xH64))
                {
                    // BME280初期化
                    bme280.Reset();
                    Thread.Sleep(100);
                    bme280.SetPowerMode(Bmx280PowerMode.Normal);

                    // ディスプレイ初期化
                    display.ClearScreen();

                    Console.WriteLine("BME280 and OLED initialized!");
                    Console.WriteLine("Displaying sensor data...");

                    while (true)
                    {
                        // センサーデータ読み取り
                        var temperature = bme280.ReadTemperature();
                        var pressure = bme280.ReadPressure();
                        var humidity = bme280.ReadHumidity();

                        // コンソール出力
                        Console.WriteLine($"Temp: {temperature.DegreesCelsius:F1}°C, " +
                                        $"Press: {pressure.Hectopascals:F0}hPa, " +
                                        $"Humid: {humidity.Percent:F0}%");

                        // ディスプレイに表示
                        display.ClearScreen();

                        // タイトル
                        DrawText(display, 0, 0, "BME280 Sensor");
                        DrawText(display, 0, 10, "-------------");

                        // 温度
                        DrawText(display, 0, 20, $"Temp: {temperature.DegreesCelsius:F1}C");

                        // 湿度
                        DrawText(display, 0, 30, $"Humid: {humidity.Percent:F0}%");

                        // 気圧
                        DrawText(display, 0, 40, $"Press: {pressure.Hectopascals:F0}hPa");

                        // 時刻
                        DrawText(display, 0, 50, DateTime.UtcNow.ToString("HH:mm:ss"));

                        display.Show();

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

        // 簡易的な文字描画メソッド
        private static void DrawText(Ssd1306 display, int x, int y, string text)
        {
            // 注: 実際のフォントレンダリングには専用ライブラリが必要
            // ここでは概念的な実装
            for (int i = 0; i < text.Length && (x + i * 6) < 128; i++)
            {
                DrawChar(display, x + i * 6, y, text[i]);
            }
        }

        private static void DrawChar(Ssd1306 display, int x, int y, char c)
        {
            // 簡易的な文字描画（実際のフォントデータが必要）
            // サンプルとしてピクセルパターンを表示
            for (int dy = 0; dy < 8 && y + dy < 64; dy++)
            {
                for (int dx = 0; dx < 5 && x + dx < 128; dx++)
                {
                    if ((dx + dy) % 2 == 0)
                    {
                        display.DrawPixel(x + dx, y + dy, true);
                    }
                }
            }
        }
    }
}
