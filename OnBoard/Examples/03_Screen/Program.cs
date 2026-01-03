using nanoFramework.M5Stack;
using nanoFramework.UI;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

// Initialize M5Stack Fire screen
M5Core2.InitializeScreen();
var screen = M5Core2.Screen;

Debug.WriteLine("M5Stack Fire - Screen Example");
Debug.WriteLine("=============================");

try
{
    // Clear screen with black background
    screen.Clear();

    // Draw header
    screen.DrawString(10, 10, "M5Stack Fire", Color.White, Font.DefaultFont);
    screen.DrawString(10, 30, "Screen Demo", Color.Cyan, Font.DefaultFont);
    
    // Draw a line separator
    screen.DrawLine(0, 50, 320, 50, Color.White);

    int y = 60;
    int counter = 0;

    while (true)
    {
        // Clear content area (keep header)
        screen.DrawRectangle(0, 60, 320, 180, 0, 0, Color.Black, 0, 0, Color.Black, 0, 0, true);

        // Display counter
        screen.DrawString(10, y, $"Counter: {counter}", Color.Yellow, Font.DefaultFont);
        
        // Display time
        screen.DrawString(10, y + 20, $"Time: {DateTime.UtcNow:HH:mm:ss}", Color.Green, Font.DefaultFont);

        // Draw some shapes
        int shapeY = y + 50;
        
        // Rectangle
        screen.DrawRectangle(10, shapeY, 60, 40, 2, 2, Color.Red, 0, 0, Color.Red, 0, 0, false);
        screen.DrawString(15, shapeY + 12, "Rect", Color.Red, Font.DefaultFont);

        // Filled rectangle
        screen.DrawRectangle(80, shapeY, 60, 40, 2, 2, Color.Blue, 0, 0, Color.Blue, 0, 0, true);
        screen.DrawString(85, shapeY + 12, "Fill", Color.White, Font.DefaultFont);

        // Circle (using ellipse)
        screen.DrawEllipse(180, shapeY + 20, 20, 20, Color.Green, true);
        screen.DrawString(175, shapeY + 12, "Circle", Color.Green, Font.DefaultFont);

        // Draw a progress bar
        int progress = (counter % 100);
        int barWidth = (int)(progress * 3.0);
        screen.DrawRectangle(10, shapeY + 60, 300, 20, 1, 1, Color.White, 0, 0, Color.White, 0, 0, false);
        screen.DrawRectangle(12, shapeY + 62, barWidth, 16, 0, 0, Color.Cyan, 0, 0, Color.Cyan, 0, 0, true);
        screen.DrawString(130, shapeY + 65, $"{progress}%", Color.White, Font.DefaultFont);

        counter++;
        Thread.Sleep(100);
    }
}
catch (Exception ex)
{
    Debug.WriteLine($"Error: {ex.Message}");
}
