using nanoFramework.M5Stack;
using nanoFramework.Hardware.Esp32;
using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using nanoFramework.System.IO.FileSystem;

// Initialize M5Stack Fire
Debug.WriteLine("M5Stack Fire - SD Card Example");
Debug.WriteLine("===============================");
Debug.WriteLine("");

try
{
    // M5Stack Fire SD Card pins
    // MOSI: GPIO 23
    // MISO: GPIO 19  
    // CLK:  GPIO 18
    // CS:   GPIO 4

    Debug.WriteLine("Initializing SD Card...");
    
    // Configure SPI for SD Card
    Configuration.SetPinFunction(23, DeviceFunction.SPI1_MOSI);
    Configuration.SetPinFunction(19, DeviceFunction.SPI1_MISO);
    Configuration.SetPinFunction(18, DeviceFunction.SPI1_CLOCK);
    
    // Mount SD Card
    var sdCard = new SDCard(
        new SDCard.SDCardSpiParameters
        {
            spiBus = 1,
            chipSelectPin = 4
        });

    sdCard.Mount();
    Debug.WriteLine("SD Card mounted successfully!");
    Debug.WriteLine("");

    // Get drive info
    var drives = DriveInfo.GetDrives();
    if (drives != null && drives.Length > 0)
    {
        foreach (var drive in drives)
        {
            Debug.WriteLine($"Drive: {drive.Name}");
            Debug.WriteLine($"  Type: {drive.DriveType}");
            Debug.WriteLine($"  Format: {drive.DriveFormat}");
            Debug.WriteLine($"  Total Size: {drive.TotalSize / 1024 / 1024} MB");
            Debug.WriteLine($"  Free Space: {drive.TotalFreeSpace / 1024 / 1024} MB");
            Debug.WriteLine("");
        }
    }

    // Test file operations
    string testDir = "D:\\nanoFramework";
    string testFile = "D:\\nanoFramework\\test.txt";

    Debug.WriteLine("Testing file operations...");
    Debug.WriteLine("");

    // Create directory
    if (!Directory.Exists(testDir))
    {
        Directory.CreateDirectory(testDir);
        Debug.WriteLine($"Created directory: {testDir}");
    }

    // Write to file
    Debug.WriteLine("Writing to file...");
    using (var writer = new StreamWriter(testFile, false))
    {
        writer.WriteLine("M5Stack Fire - nanoFramework");
        writer.WriteLine($"Test Date: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
        writer.WriteLine("This is a test file created by nanoFramework.");
        writer.WriteLine("");
        
        for (int i = 1; i <= 5; i++)
        {
            writer.WriteLine($"Line {i}: Sample data");
        }
    }
    Debug.WriteLine($"File written: {testFile}");
    Debug.WriteLine("");

    // Read from file
    Debug.WriteLine("Reading from file...");
    Debug.WriteLine("--- File Contents ---");
    using (var reader = new StreamReader(testFile))
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            Debug.WriteLine(line);
        }
    }
    Debug.WriteLine("--- End of File ---");
    Debug.WriteLine("");

    // Get file info
    var fileInfo = new FileInfo(testFile);
    Debug.WriteLine($"File Info:");
    Debug.WriteLine($"  Name: {fileInfo.Name}");
    Debug.WriteLine($"  Size: {fileInfo.Length} bytes");
    Debug.WriteLine($"  Created: {fileInfo.CreationTime}");
    Debug.WriteLine("");

    // List files in directory
    Debug.WriteLine($"Files in {testDir}:");
    var files = Directory.GetFiles(testDir);
    foreach (var file in files)
    {
        Debug.WriteLine($"  - {file}");
    }
    Debug.WriteLine("");

    Debug.WriteLine("SD Card test completed successfully!");

    // Keep running
    while (true)
    {
        Thread.Sleep(1000);
    }
}
catch (Exception ex)
{
    Debug.WriteLine($"Error: {ex.Message}");
    Debug.WriteLine("Make sure:");
    Debug.WriteLine("  1. SD card is inserted");
    Debug.WriteLine("  2. SD card is formatted (FAT32)");
    Debug.WriteLine("  3. SD card is not write-protected");
}
