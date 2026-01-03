# PortA - M5Stack Fire専用実装ガイド

このプロジェクトは、M5Stack FireのPortAを使用したnanoFrameworkサンプルです。

## M5Stack Fireについて

M5Stack Fireは、ESP32ベースの開発ボードで、複数の拡張ポートを備えています。PortAは、I2C通信およびGPIO用のポートとして使用できます。

## PortAのピン配置

M5Stack FireのPortAは、本体側面の赤いポートで、以下のピン配置となっています：

| ピン番号 | 機能 | GPIO |
|---------|------|------|
| 1 | GND | GND |
| 2 | SDA | GPIO21 |
| 3 | SCL | GPIO22 |
| 4 | 5V | 5V |

## 実装方法

### GPIO使用例

```csharp
using System;
using System.Device.Gpio;
using System.Threading;

namespace M5StackFire.PortA
{
    public class Program
    {
        // PortAのピン定義
        private const int SDA_PIN = 21;  // GPIO21
        private const int SCL_PIN = 22;  // GPIO22

        public static void Main()
        {
            Console.WriteLine("M5Stack Fire - PortA GPIO Sample");
            
            var controller = new GpioController();
            
            // GPIO21をOutput設定
            controller.OpenPin(SDA_PIN, PinMode.Output);
            
            try
            {
                while (true)
                {
                    // LED点滅など
                    controller.Write(SDA_PIN, PinValue.High);
                    Console.WriteLine("GPIO21: HIGH");
                    Thread.Sleep(1000);
                    
                    controller.Write(SDA_PIN, PinValue.Low);
                    Console.WriteLine("GPIO21: LOW");
                    Thread.Sleep(1000);
                }
            }
            finally
            {
                controller.ClosePin(SDA_PIN);
            }
        }
    }
}
```

### I2C使用例

PortAは標準的にI2C通信用として設計されています。

```csharp
using System;
using System.Device.I2c;
using nanoFramework.Hardware.Esp32;

namespace M5StackFire.PortA
{
    public class Program
    {
        private const int I2C_BUS_ID = 1;
        
        public static void Main()
        {
            Console.WriteLine("M5Stack Fire - PortA I2C Sample");
            
            // I2Cピンの設定（PortA用）
            Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);   // SDA
            Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);  // SCL
            
            // I2Cデバイスの設定例（アドレスは接続するデバイスに応じて変更）
            var i2cSettings = new I2cConnectionSettings(I2C_BUS_ID, 0x3C)
            {
                BusSpeed = I2cBusSpeed.StandardMode
            };
            
            using (var device = I2cDevice.Create(i2cSettings))
            {
                // I2C通信処理
                byte[] readBuffer = new byte[1];
                device.Read(readBuffer);
                Console.WriteLine($"Read data: 0x{readBuffer[0]:X2}");
            }
        }
    }
}
```

## 注意事項

1. **電圧レベル**: M5Stack FireのGPIOは3.3Vロジックです。5Vピンは電源供給用ですが、GPIO入出力は3.3Vで動作します。

2. **プルアップ抵抗**: I2C通信を使用する場合、PortAには既に基板上にプルアップ抵抗が実装されている場合があります。外部でプルアップ抵抗を追加する前に確認してください。

3. **ピンの共有**: GPIO21とGPIO22は、内部I2Cバス（IMU、PMICなど）でも使用される可能性があるため、他の内部デバイスとの競合に注意してください。

4. **電流制限**: GPIOピンからの出力電流は最大40mA程度です。大きな電流を必要とするデバイスは外部電源を使用してください。

## 必要なNuGetパッケージ

```xml
<ItemGroup>
    <PackageReference Include="nanoFramework.CoreLibrary" Version="1.*" />
    <PackageReference Include="nanoFramework.Hardware.Esp32" Version="1.*" />
    <PackageReference Include="System.Device.Gpio" Version="1.*" />
    <PackageReference Include="System.Device.I2c" Version="1.*" />
</ItemGroup>
```

## 参考リンク

- [M5Stack公式ドキュメント](https://docs.m5stack.com/)
- [nanoFramework公式サイト](https://www.nanoframework.net/)
- [ESP32ピン配置](https://docs.espressif.com/projects/esp-idf/en/latest/esp32/)

## ライセンス

このサンプルコードはMITライセンスの下で公開されています。
