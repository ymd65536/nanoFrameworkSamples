using nanoFramework.M5Stack;
using System;
using System.Device.I2c;
using System.Diagnostics;
using System.Threading;

// Initialize M5Stack Fire
M5Core2.InitializeScreen();
Debug.WriteLine("M5Stack Fire - MPU6886 IMU Example");
Debug.WriteLine("====================================");

try
{
    // Initialize MPU6886 IMU sensor
    var mpu6886 = new nanoFramework.M5Stack.Mpu6886();
    
    Debug.WriteLine("MPU6886 initialized successfully!");
    Debug.WriteLine("Reading accelerometer and gyroscope data...");
    Debug.WriteLine("");

    while (true)
    {
        // Read accelerometer data (in g)
        var accel = mpu6886.GetAccelerometer();
        
        // Read gyroscope data (in degrees/second)
        var gyro = mpu6886.GetGyroscope();
        
        // Read temperature (in Celsius)
        var temp = mpu6886.GetTemperature();

        Debug.WriteLine($"Accelerometer (g):  X={accel.X:F3}  Y={accel.Y:F3}  Z={accel.Z:F3}");
        Debug.WriteLine($"Gyroscope (°/s):    X={gyro.X:F3}  Y={gyro.Y:F3}  Z={gyro.Z:F3}");
        Debug.WriteLine($"Temperature:        {temp:F2}°C");
        Debug.WriteLine("---");

        Thread.Sleep(500);
    }
}
catch (Exception ex)
{
    Debug.WriteLine($"Error: {ex.Message}");
    Debug.WriteLine("Make sure your M5Stack Fire has the MPU6886 sensor.");
}
