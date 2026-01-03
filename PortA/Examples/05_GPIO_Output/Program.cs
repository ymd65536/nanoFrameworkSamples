using System;
using System.Device.Gpio;
using System.Threading;
using nanoFramework.Hardware.Esp32;

namespace GPIO_Output
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("GPIO Output Sample - PORT A");

            // PORT A GPIO設定
            // GPIO21とGPIO22をGPIO出力として使用
            int gpio21 = 21;
            int gpio22 = 22;

            // GPIO機能として設定
            Configuration.SetPinFunction(gpio21, DeviceFunction.GPIO);
            Configuration.SetPinFunction(gpio22, DeviceFunction.GPIO);

            GpioController gpioController = new GpioController();

            try
            {
                // GPIOピンをオープン
                gpioController.OpenPin(gpio21, PinMode.Output);
                gpioController.OpenPin(gpio22, PinMode.Output);

                Console.WriteLine($"GPIO {gpio21} and {gpio22} configured as output");
                Console.WriteLine("Starting LED blink pattern...");

                bool state21 = false;
                bool state22 = false;

                while (true)
                {
                    // GPIO21をトグル
                    state21 = !state21;
                    gpioController.Write(gpio21, state21 ? PinValue.High : PinValue.Low);
                    Console.WriteLine($"GPIO21: {(state21 ? "HIGH" : "LOW")}");

                    Thread.Sleep(500);

                    // GPIO22をトグル
                    state22 = !state22;
                    gpioController.Write(gpio22, state22 ? PinValue.High : PinValue.Low);
                    Console.WriteLine($"GPIO22: {(state22 ? "HIGH" : "LOW")}");

                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // クリーンアップ
                if (gpioController != null)
                {
                    gpioController.ClosePin(gpio21);
                    gpioController.ClosePin(gpio22);
                    gpioController.Dispose();
                }
            }

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
