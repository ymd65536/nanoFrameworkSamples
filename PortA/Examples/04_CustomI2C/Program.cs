using System;
using System.Device.I2c;
using System.Threading;
using nanoFramework.Hardware.Esp32;

namespace CustomI2C
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Custom I2C Communication Sample - PORT A");

            // PORT A I2C設定 (GPIO21=SDA, GPIO22=SCL)
            Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);
            Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);

            // I2C設定 (デバイスアドレスは適宜変更)
            int deviceAddress = 0x50; // 例: EEPROMのアドレス
            I2cConnectionSettings i2cSettings = new I2cConnectionSettings(1, deviceAddress);
            I2cDevice i2cDevice = I2cDevice.Create(i2cSettings);

            try
            {
                Console.WriteLine($"I2C Device connected at address: 0x{deviceAddress:X2}");

                // 書き込みデータ準備
                byte[] writeBuffer = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04 };
                byte[] readBuffer = new byte[4];

                while (true)
                {                    
                    try
                    {
                        // データ書き込み
                        Console.WriteLine("Writing data...");
                        i2cDevice.Write(writeBuffer);
                        Console.WriteLine($"Written {writeBuffer.Length} bytes");

                        Thread.Sleep(100);

                        // データ読み取り
                        Console.WriteLine("Reading data...");
                        i2cDevice.Read(readBuffer);
                        
                        Console.Write("Read data: ");
                        foreach (byte b in readBuffer)
                        {
                            Console.Write($"0x{b:X2} ");
                        }
                        Console.WriteLine();

                        // WriteRead の例
                        byte[] command = new byte[] { 0x00 }; // レジスタアドレス
                        byte[] response = new byte[2];
                        
                        i2cDevice.WriteRead(command, response);
                        Console.WriteLine($"WriteRead response: 0x{response[0]:X2} 0x{response[1]:X2}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"I2C Error: {ex.Message}");
                    }

                    Console.WriteLine("---");
                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                i2cDevice?.Dispose();
            }

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
