using System;
using System.Device.I2c;
using System.Threading;
using nanoFramework.Hardware.Esp32;
using Iot.Device.Ssd13xx;
using Iot.Device.Ssd13xx.Commands;
using System.Drawing;

namespace OLED_Display
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("OLED Display Sample - PORT A");

            // PORT A I2C設定 (GPIO21=SDA, GPIO22=SCL)
            Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);
            Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);

            // I2C設定 (SSD1306のデフォルトアドレス: 0x3C)
            I2cConnectionSettings i2cSettings = new I2cConnectionSettings(1, 0x3C);
            I2cDevice i2cDevice = I2cDevice.Create(i2cSettings);

            try
            {
                // SSD1306 OLED 128x64
                using (Ssd1306 display = new Ssd1306(i2cDevice, Ssd1306.DisplayResolution.W128xH64))
                {
                    display.ClearScreen();
                    Console.WriteLine("OLED Display initialized!");

                    int counter = 0;

                    while (true)
                    {
                        display.ClearScreen();

                        // 簡単なテキスト表示（ピクセル描画）
                        DrawText(display, 0, 0, "M5Stack Fire");
                        DrawText(display, 0, 10, "PORT A OLED");
                        DrawText(display, 0, 20, "---");
                        DrawText(display, 0, 30, $"Count: {counter}");
                        DrawText(display, 0, 40, DateTime.UtcNow.ToString("HH:mm:ss"));

                        display.Show();

                        counter++;
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Thread.Sleep(Timeout.Infinite);
        }

        // シンプルな文字描画（8x8ピクセルフォント想定）
        private static void DrawText(Ssd1306 display, int x, int y, string text)
        {
            // 注: 実際のフォント描画には追加のライブラリが必要です
            // ここでは簡易的にピクセルを設定
            for (int i = 0; i < text.Length && (x + i * 6) < 128; i++)
            {
                // 各文字を6ピクセル幅で表示（簡易表示）
                DrawChar(display, x + i * 6, y, text[i]);
            }
        }

        private static void DrawChar(Ssd1306 display, int x, int y, char c)
        {
            // 簡易的な文字表示（実装例）
            // 実際にはフォントデータが必要
            for (int dy = 0; dy < 8 && y + dy < 64; dy++)
            {
                for (int dx = 0; dx < 5 && x + dx < 128; dx++)
                {
                    // サンプルとして簡単なパターンを表示
                    if ((dx + dy) % 2 == 0)
                    {
                        display.DrawPixel(x + dx, y + dy, true);
                    }
                }
            }
        }
    }
}
