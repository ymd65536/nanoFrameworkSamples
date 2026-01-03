using System;
using System.Device.Gpio;
using System.Threading;
using nanoFramework.Hardware.Esp32;

namespace GPIO_Input
{
    public class Program
    {
        private static GpioController gpioController;
        private static int gpio21 = 21;

        public static void Main()
        {
            Console.WriteLine("GPIO Input Sample - PORT A");

            // PORT A GPIO設定
            // GPIO21を入力として使用
            Configuration.SetPinFunction(gpio21, DeviceFunction.GPIO);

            gpioController = new GpioController();

            try
            {
                // GPIOピンをプルアップ入力として設定
                gpioController.OpenPin(gpio21, PinMode.InputPullUp);

                Console.WriteLine($"GPIO {gpio21} configured as input with pull-up");

                // 割り込みイベント設定
                gpioController.RegisterCallbackForPinValueChangedEvent(
                    gpio21,
                    PinEventTypes.Falling | PinEventTypes.Rising,
                    OnGpioValueChanged);

                Console.WriteLine("Waiting for GPIO state changes...");
                Console.WriteLine("Press Ctrl+C to exit");

                // 定期的に状態を読み取り
                while (true)
                {
                    PinValue value = gpioController.Read(gpio21);
                    Console.WriteLine($"Current GPIO21 state: {(value == PinValue.High ? "HIGH" : "LOW")}");
                    Thread.Sleep(2000);
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
                    gpioController.UnregisterCallbackForPinValueChangedEvent(gpio21, OnGpioValueChanged);
                    gpioController.ClosePin(gpio21);
                    gpioController.Dispose();
                }
            }

            Thread.Sleep(Timeout.Infinite);
        }

        private static void OnGpioValueChanged(object sender, PinValueChangedEventArgs e)
        {
            Console.WriteLine($"[EVENT] GPIO {e.PinNumber} changed to: {(e.ChangeType == PinEventTypes.Rising ? "HIGH" : "LOW")} at {DateTime.UtcNow:HH:mm:ss.fff}");
        }
    }
}
