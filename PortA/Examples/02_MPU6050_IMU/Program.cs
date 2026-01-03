using System;
using System.Device.I2c;
using System.Threading;
using Iot.Device.Mpu6050;

Console.WriteLine("M5Stack Fire - MPU6050 IMU Example");
Console.WriteLine("===================================");

// M5Stack Fire I2C configuration (Port A: GPIO21=SDA, GPIO22=SCL)
var i2cSettings = new I2cConnectionSettings(1, Mpu6050.DefaultI2cAddress)
{
    BusSpeed = I2cBusSpeed.FastMode
};

using var i2cDevice = I2cDevice.Create(i2cSettings);
using var mpu6050 = new Mpu6050(i2cDevice);

Console.WriteLine($"MPU6050 initialized on I2C address: 0x{Mpu6050.DefaultI2cAddress:X2}");
Console.WriteLine("Reading accelerometer and gyroscope data...");
Console.WriteLine();

while (true)
{
    var accel = mpu6050.GetAccelerometer();
    var gyro = mpu6050.GetGyroscope();
    var temp = mpu6050.GetTemperature();

    Console.Clear();
    Console.WriteLine("M5Stack Fire - MPU6050 Readings");
    Console.WriteLine("================================");
    Console.WriteLine($"Accelerometer (m/s²):");
    Console.WriteLine($"  X: {accel.X:F3}");
    Console.WriteLine($"  Y: {accel.Y:F3}");
    Console.WriteLine($"  Z: {accel.Z:F3}");
    Console.WriteLine();
    Console.WriteLine($"Gyroscope (°/s):");
    Console.WriteLine($"  X: {gyro.X:F3}");
    Console.WriteLine($"  Y: {gyro.Y:F3}");
    Console.WriteLine($"  Z: {gyro.Z:F3}");
    Console.WriteLine();
    Console.WriteLine($"Temperature: {temp.DegreesCelsius:F2}°C");

    Thread.Sleep(500);
}
