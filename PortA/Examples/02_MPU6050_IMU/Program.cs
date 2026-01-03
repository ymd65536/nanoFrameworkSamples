using System;
using System.Device.I2c;
using System.Numerics;
using System.Threading;
using Iot.Device.Mpu6050;
using nanoFramework.Hardware.Esp32;

namespace MPU6050_IMU
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("MPU6050 IMU Sensor Sample - PORT A");

            // PORT A I2C設定 (GPIO21=SDA, GPIO22=SCL)
            Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);
            Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);

            // I2C設定 (MPU6050のデフォルトアドレス: 0x68)
            I2cConnectionSettings i2cSettings = new I2cConnectionSettings(1, Mpu6050.DefaultI2cAddress);
            I2cDevice i2cDevice = I2cDevice.Create(i2cSettings);

            try
            {
                using (Mpu6050 mpu6050 = new Mpu6050(i2cDevice))
                {
                    Console.WriteLine("MPU6050 initialized successfully!");
                    Console.WriteLine($"Device ID: 0x{mpu6050.ReadByte(Register.WHO_AM_I):X2}");
                    Console.WriteLine("Reading IMU data...");
                    Console.WriteLine();

                    while (true)
                    {
                        // 加速度データ読み取り
                        Vector3 acceleration = mpu6050.GetAccelerometer();
                        
                        // ジャイロデータ読み取り
                        Vector3 gyroscope = mpu6050.GetGyroscope();
                        
                        // 温度読み取り
                        var temperature = mpu6050.GetTemperature();

                        // 結果表示
                        Console.WriteLine("Accelerometer (m/s²):");
                        Console.WriteLine($"  X: {acceleration.X:F3}, Y: {acceleration.Y:F3}, Z: {acceleration.Z:F3}");
                        
                        Console.WriteLine("Gyroscope (°/s):");
                        Console.WriteLine($"  X: {gyroscope.X:F3}, Y: {gyroscope.Y:F3}, Z: {gyroscope.Z:F3}");
                        
                        Console.WriteLine($"Temperature: {temperature.DegreesCelsius:F2} °C");
                        Console.WriteLine("---");

                        Thread.Sleep(500);
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
