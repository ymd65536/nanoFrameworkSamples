using System.Threading;
using nanoFramework.M5Stack;
using nanoFramework.Hardware.Esp32;

using Console = nanoFramework.M5Stack.Console;

Fire.InitializeScreen();
Console.Clear();

// Initialize M5Stack Fire
Console.WriteLine("M5Stack Fire - RGB LED Bar Example");
Console.WriteLine("===================================");
Console.WriteLine("The M5Stack Fire has 10 SK6812 RGB LEDs on the side.");
Console.WriteLine("");

// LED bar configuration for M5Stack Fire
const int LED_COUNT = 10;
// const int LED_PIN = 15; // GPIO 15 for LED bar

Console.WriteLine("Starting LED animations...");
Console.WriteLine("");

int animation = 0;

while (true)
{
    switch (animation % 5)
    {
        case 0:
            Console.WriteLine("Animation: Rainbow cycle");
            RainbowCycle();
            break;

        case 1:
            Console.WriteLine("Animation: Color wipe - Red");
            ColorWipe(255, 0, 0);
            break;

        case 2:
            Console.WriteLine("Animation: Color wipe - Green");
            ColorWipe(0, 255, 0);
            break;

        case 3:
            Console.WriteLine("Animation: Color wipe - Blue");
            ColorWipe(0, 0, 255);
            break;

        case 4:
            Console.WriteLine("Animation: Theater chase");
            TheaterChase(255, 255, 255);
            break;
    }
    animation++;
}

RgbColor Wheel(int pos, int v)
{
    pos = 255 - pos;
    if (pos < 85)
    {
        return new RgbColor((byte)v, 0, (byte)(pos * 3));
    }
    else if (pos < 170)
    {
        pos -= 85;
        return new RgbColor(0, (byte)(pos * 3), (byte)(255 - pos * 3));
    }
    else
    {
        pos -= 170;
        return new RgbColor((byte)(pos * 3), (byte)(255 - pos * 3), 0);
    }
}

void SetLedColor(int ledIndex, byte r, byte g, byte b)
{
    // Placeholder for actual LED control
    // In a real implementation, this would send data to SK6812 LEDs
    // using RMT or SPI to generate the precise timing required
    Console.WriteLine($"  LED {ledIndex}: R={r}, G={g}, B={b}");
    Thread.Sleep(1000);
    Console.Clear();
}

void RainbowCycle()
{
    for (int j = 0; j < 256; j += 5)
    {
        for (int i = 0; i < LED_COUNT; i++)
        {
            int pos = ((i * 256 / LED_COUNT) + j) & 255;
            var color = Wheel(pos, (255 - (pos * 3)));
            SetLedColor(i, color.R, color.G, color.B);
        }
        Thread.Sleep(50);
    }
}

void ColorWipe(byte r, byte g, byte b)
{
    for (int i = 0; i < LED_COUNT; i++)
    {
        SetLedColor(i, r, g, b);
        Thread.Sleep(100);
    }
    Thread.Sleep(500);

    // Turn off
    for (int i = 0; i < LED_COUNT; i++)
    {
        SetLedColor(i, 0, 0, 0);
    }
}

void TheaterChase(byte r, byte g, byte b)
{
    for (int j = 0; j < 10; j++)
    {
        for (int q = 0; q < 3; q++)
        {
            for (int i = 0; i < LED_COUNT; i += 3)
            {
                int pos = i + q;
                if (pos < LED_COUNT)
                {
                    SetLedColor(pos, r, g, b);
                }
            }
            Thread.Sleep(100);

            for (int i = 0; i < LED_COUNT; i += 3)
            {
                int pos = i + q;
                if (pos < LED_COUNT)
                {
                    SetLedColor(pos, 0, 0, 0);
                }
            }
        }
    }
}

struct RgbColor
{
    public byte R;
    public byte G;
    public byte B;

    public RgbColor(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }
}
