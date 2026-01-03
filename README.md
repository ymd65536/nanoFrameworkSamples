# nanoFramework Samples for M5Stack Fire

M5Stack Fireで動作する.NET nanoFrameworkのサンプルコード集です。

## 概要

このリポジトリには、M5Stack Fireの内蔵デバイスと外部接続デバイス（PORT A経由）を使用するサンプルコードが含まれています。すべてのサンプルは.NET 10のトップレベルステートメントで記述されています。

## リポジトリ構成

```
nanoFrameworkSamples/
├── README.md
├── PortA/                              # 外部I2Cデバイス用サンプル
│   ├── README.md
│   └── Examples/
│       ├── 01_BME280_Sensor/          # 外部BME280温湿度・気圧センサー
│       ├── 02_MPU6050_IMU/            # 外部MPU6050 IMUセンサー
│       ├── 03_OLED_Display/           # 外部OLEDディスプレイ
│       ├── 04_CustomI2C/              # カスタムI2C通信
│       ├── 05_GPIO_Output/            # GPIO出力制御
│       ├── 06_GPIO_Input/             # GPIO入力読み取り
│       └── 07_I2C_Scanner/            # I2Cバススキャナー
│
└── OnBoard/                            # M5Stack Fire内蔵デバイス用サンプル
    ├── README.md
    └── Examples/
        ├── 01_MPU6886_IMU/            # 内蔵MPU6886 (6軸IMU)
        ├── 02_Buttons/                # 内蔵3ボタン
        ├── 03_Screen/                 # 内蔵TFT液晶ディスプレイ
        ├── 04_Speaker/                # 内蔵スピーカー/ブザー
        ├── 05_LED_Bar/                # 内蔵RGB LEDバー (10個)
        └── 06_SDCard/                 # SDカード読み書き
```

## M5Stack Fire仕様

### 内蔵デバイス

- **MPU6886**: 6軸IMU（3軸加速度 + 3軸ジャイロ）
- **TFTディスプレイ**: 2.0インチ 320x240 IPS液晶
- **ボタン**: 3つの物理ボタン（A, B, C）
- **スピーカー**: 1Wスピーカー/ブザー
- **RGB LEDバー**: 10個のSK6812 RGB LED
- **SDカード**: microSDカードスロット
- **バッテリー**: 600mAh リチウムイオン充電池

### 拡張ポート

- **PORT A**: I2C拡張ポート (GPIO21=SDA, GPIO22=SCL)
- **PORT B**: GPIO拡張ポート
- **PORT C**: UART拡張ポート

## 必要な環境

### ハードウェア
- M5Stack Fire

### ソフトウェア
- Visual Studio 2022以降
- .NET nanoFramework Extension for Visual Studio
- nanoff (nanoFramework Firmware Flasher)

## セットアップ手順

### 1. ファームウェアの書き込み

```bash
# nanoffのインストール
dotnet tool install -g nanoff

# M5Stack Fireにファームウェアを書き込み
nanoff --target M5CORE --update --serialport COM3
```

### 2. Visual Studioプロジェクトの作成

1. Visual Studioで新しいnanoFrameworkプロジェクトを作成
2. 必要なNuGetパッケージをインストール

### 3. 必要なNuGetパッケージ

#### 基本パッケージ
```xml
<ItemGroup>
  <PackageReference Include="nanoFramework.CoreLibrary" Version="1.*" />
  <PackageReference Include="nanoFramework.Hardware.Esp32" Version="1.*" />
  <PackageReference Include="System.Device.Gpio" Version="1.*" />
  <PackageReference Include="System.Device.I2c" Version="1.*" />
</ItemGroup>
```

#### センサーライブラリ（必要に応じて）
```xml
<ItemGroup>
  <!-- BME280センサー用 -->
  <PackageReference Include="nanoFramework.Iot.Device.Bmxx80" Version="1.*" />
  
  <!-- MPU6050/MPU6886センサー用 -->
  <PackageReference Include="nanoFramework.Iot.Device.Mpu6050" Version="1.*" />
  
  <!-- OLEDディスプレイ用 -->
  <PackageReference Include="nanoFramework.Iot.Device.Ssd13xx" Version="1.*" />
</ItemGroup>
```

## サンプルの使い方

### PORT A - 外部デバイスサンプル

PORT Aに外部I2Cデバイスを接続して使用するサンプルです。

**例: BME280センサー**
```csharp
using System.Device.I2c;
using Iot.Device.Bmxx80;
using nanoFramework.Hardware.Esp32;

// PORT A のI2Cピン設定
Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);
Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);

// I2C設定
var settings = new I2cConnectionSettings(1, Bme280.DefaultI2cAddress);
using var i2cDevice = I2cDevice.Create(settings);
using var bme280 = new Bme280(i2cDevice);

// センサー読み取り
if (bme280.TryReadTemperature(out var temperature))
{
    Console.WriteLine($"温度: {temperature.DegreesCelsius:F2}°C");
}
```

### OnBoard - 内蔵デバイスサンプル

M5Stack Fire内蔵のデバイスを使用するサンプルです。

**例: MPU6886 IMU**
```csharp
using nanoFramework.M5Stack;

var imu = Fire.AccelerometerGyroscope;
var accel = imu.GetAccelerometer();

Console.WriteLine($"加速度: X={accel.X:F3} Y={accel.Y:F3} Z={accel.Z:F3}");
```

## ピン配置

### PORT A (I2C拡張ポート)
| ピン | 機能 | GPIO |
|------|------|------|
| 赤   | 5V   | 5V   |
| 黒   | GND  | GND  |
| 白   | SDA  | GPIO21 |
| 黄   | SCL  | GPIO22 |

### 内蔵デバイスピン
| デバイス | GPIO |
|---------|------|
| MPU6886 (I2C) | GPIO21 (SDA), GPIO22 (SCL) |
| Button A | GPIO39 |
| Button B | GPIO38 |
| Button C | GPIO37 |
| Speaker | GPIO25 |
| LED Bar | GPIO15 |
| TFT Display | SPI (MOSI:23, CLK:18, CS:14, DC:27) |
| SD Card | SPI (MOSI:23, MISO:19, CLK:18, CS:4) |

## トラブルシューティング

### ファームウェアが書き込めない
- USBケーブルがデータ転送対応か確認
- COMポート番号が正しいか確認
- M5Stack Fireのリセットボタンを押してから再試行

### I2Cデバイスが認識されない
- デバイスが正しく接続されているか確認
- I2Cアドレスが正しいか確認（I2C Scannerサンプルで確認可能）
- プルアップ抵抗が必要な場合がある

### デプロイエラーが発生する
- nanoFrameworkファームウェアが正しく書き込まれているか確認
- Visual StudioでnanoFramework拡張機能がインストールされているか確認
- NuGetパッケージのバージョンが互換性があるか確認

## 参考リンク

- [nanoFramework公式サイト](https://www.nanoframework.net/)
- [nanoFramework GitHub](https://github.com/nanoframework)
- [M5Stack Fire公式ドキュメント](https://docs.m5stack.com/en/core/fire)
- [nanoFramework Discord](https://discord.gg/gCyBu8T)

## ライセンス

MIT License

## 貢献

プルリクエストを歓迎します！バグ報告や機能リクエストはIssuesにお願いします。
