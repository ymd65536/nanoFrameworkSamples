using nanoFramework.M5Stack;
using nanoFramework.Hardware.Esp32;
using System;
using System.Device.Gpio;
using System.Device.Pwm;
using System.Diagnostics;
using System.Threading;

// Initialize M5Stack Fire
Debug.WriteLine("M5Stack Fire - Speaker/Buzzer Example");
Debug.WriteLine("=====================================");

// M5Stack Fire speaker pin (GPIO 25)
const int SPEAKER_PIN = 25;

try
{
    // Configure PWM for speaker
    Configuration.SetPinFunction(SPEAKER_PIN, DeviceFunction.PWM1);
    
    // Create PWM channel for speaker
    var pwmController = PwmChannel.CreateFromPin(SPEAKER_PIN, 1000, 0.0);
    
    Debug.WriteLine("Playing musical notes...");
    Debug.WriteLine("");

    // Musical notes frequencies (in Hz)
    int[] notes = { 262, 294, 330, 349, 392, 440, 494, 523 }; // C, D, E, F, G, A, B, C
    string[] noteNames = { "C", "D", "E", "F", "G", "A", "B", "C" };

    while (true)
    {
        // Play scale up
        Debug.WriteLine("Playing scale up...");
        for (int i = 0; i < notes.Length; i++)
        {
            Debug.WriteLine($"Note: {noteNames[i]} ({notes[i]} Hz)");
            PlayTone(pwmController, notes[i], 300);
            Thread.Sleep(100);
        }

        Thread.Sleep(500);

        // Play scale down
        Debug.WriteLine("Playing scale down...");
        for (int i = notes.Length - 1; i >= 0; i--)
        {
            Debug.WriteLine($"Note: {noteNames[i]} ({notes[i]} Hz)");
            PlayTone(pwmController, notes[i], 300);
            Thread.Sleep(100);
        }

        Thread.Sleep(500);

        // Play a simple melody
        Debug.WriteLine("Playing melody...");
        int[] melody = { 262, 262, 392, 392, 440, 440, 392 }; // Twinkle twinkle
        int[] durations = { 400, 400, 400, 400, 400, 400, 800 };

        for (int i = 0; i < melody.Length; i++)
        {
            PlayTone(pwmController, melody[i], durations[i]);
            Thread.Sleep(50);
        }

        Debug.WriteLine("");
        Thread.Sleep(2000);
    }
}
catch (Exception ex)
{
    Debug.WriteLine($"Error: {ex.Message}");
}

void PlayTone(PwmChannel pwm, int frequency, int durationMs)
{
    if (frequency > 0)
    {
        pwm.Frequency = frequency;
        pwm.DutyCycle = 0.5; // 50% duty cycle
        pwm.Start();
        Thread.Sleep(durationMs);
        pwm.Stop();
    }
    else
    {
        Thread.Sleep(durationMs);
    }
}
