# nanoFramework Samples for M5Stack Fire V2.7

M5Stack Fire V2.7で動作する.NET nanoFrameworkのサンプルコード集です。

## 概要

このリポジトリには、M5Stack Fire V2.7の内蔵デバイスと外部接続デバイス（PORT A経由）を使用するサンプルコードが含まれています。すべてのサンプルは.NET 10のトップレベルステートメントで記述されています。

## M5Stack Fire V2.7について

### V2.7の主な特徴

M5Stack Fire V2.7は、従来のFireモデルから以下の点が改善されています：

1. **ガラススクリーン採用** - アクリルから強化ガラスに変更（耐傷性・視認性向上）
2. **GROVEポート5.1V安定供給** - 外部モジュールへの安定した電源供給
3. **IMU: MPU6886** - 6軸IMU（3軸加速度 + 3軸ジャイロ）を搭載
4. **USB-UART: CH9102F** - 最新のUSBシリアル変換チップ
5. **500mAhバッテリー** - 従来モデルより大容量
6. **産業グレード筐体** - より堅牢な設計

### 主要仕様

- **CPU**: ESP32-D0WDQ6 (デュアルコア 240MHz)
- **メモリ**: 8MB PSRAM + 16MB Flash
- **ディスプレイ**: 2.0インチ 320×240 IPS (ILI9342C)
- **IMU**: MPU6886 (6軸)
- **マイク**: アナログマイク BSE3729
- **スピーカー**: 1Wスピーカー
- **RGB LED**: 10個のSK6812 LED
- **バッテリー**: 500mAh 3.7V リチウムイオン
- **電源管理**: IP5306
- **サイズ**: 54×54×28.6mm
- **重量**: 約88.8g

### 従来Fireとの違い

| 項目 | Fire V2.7 | Fire V2.6以前 |
|------|-----------|---------------|
| ディスプレイ | ガラススクリーン | アクリルスクリーン |
| GROVEポート電源 | 5.1V安定供給 | 5V程度 |
| IMU | MPU6886 (6軸) | MPU6886+BMM150 (9軸) または MPU6886のみ |
| USB-UART | CH9102F | CP2104 |
| バッテリー | 500mAh | 150mAh～500mAh |
| 筐体 | 産業グレード | 通常グレード |

> **注意**: V2.7では**地磁気センサー（BMM150）が非搭載**です。コンパス機能が必要な場合は外部モジュールを使用してください。

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

## 必要な環境

### ハードウェア
- M5Stack Fire V2.7

### ソフトウェア
- Visual Studio 2022以降
- .NET nanoFramework Extension for Visual Studio
- nanoff (nanoFramework Firmware Flasher)
- CH9102Fドライバー（Windows用、必要に応じて）

## セットアップ手順

### 1. CH9102Fドライバーのインストール（Windows）

M5Stack Fire V2.7はCH9102F USBシリアルチップを使用しています。Windows環境で認識されない場合は、ドライバーをインストールしてください。

- [CH9102Fドライバーダウンロード](https://www.wch.cn/downloads/CH343SER_ZIP.html)

### 2. ファームウェアの書き込み

```bash
# nanoffのインストール
dotnet tool install -g nanoff

# M5Stack Fire V2.7にファームウェアを書き込み
nanoff --target M5CORE --update --serialport COM3
```

> **ポート番号の確認**: デバイスマネージャーでCH9102FのCOMポート番号を確認してください。

### 3. Visual Studioプロジェクトの作成

1. Visual Studioで新しいnanoFrameworkプロジェクトを作成
2. 必要なNuGetパッケージをインストール

### 4. 必要なNuGetパッケージ

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

PORT Aに外部I2Cデバイスを接続して使用するサンプルです。**V2.7では5.1V安定供給**により、より安定した動作が期待できます。

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

M5Stack Fire V2.7内蔵のデバイスを使用するサンプルです。

**例: MPU6886 IMU (6軸)**
```csharp
using nanoFramework.M5Stack;

var imu = Fire.AccelerometerGyroscope;
var accel = imu.GetAccelerometer();
var gyro = imu.GetGyroscope();

Console.WriteLine($"加速度: X={accel.X:F3} Y={accel.Y:F3} Z={accel.Z:F3}");
Console.WriteLine($"ジャイロ: X={gyro.X:F3} Y={gyro.Y:F3} Z={gyro.Z:F3}");
```

> **重要**: V2.7には**地磁気センサー（BMM150）が搭載されていません**。コンパス機能が必要な場合は、PORT Aに外部地磁気センサーモジュール（QMC5883LやHMC5883Lなど）を接続してください。

## ピン配置

### PORT A (I2C拡張ポート)
| ピン | 機能 | GPIO | 電圧 |
|------|------|------|------|
| 赤   | VCC  | -    | **5.1V** (V2.7で安定化) |
| 黒   | GND  | GND  | GND |
| 白   | SDA  | GPIO21 | 3.3V信号 |
| 黄   | SCL  | GPIO22 | 3.3V信号 |

### 内蔵デバイスピン
| デバイス | GPIO | 備考 |
|---------|------|------|
| MPU6886 (I2C) | GPIO21 (SDA), GPIO22 (SCL) | I2Cアドレス: 0x68 |
| Button A | GPIO39 | プルアップ必要 |
| Button B | GPIO38 | プルアップ必要 |
| Button C | GPIO37 | プルアップ必要 |
| Speaker | GPIO25 | PWM出力 |
| LED Bar | GPIO15 | SK6812×10個 |
| Microphone | GPIO34 | アナログ入力 |
| TFT Display (SPI) | MOSI:23, CLK:18, CS:14, DC:27, RST:33 | ILI9342C |
| SD Card (SPI) | MOSI:23, MISO:19, CLK:18, CS:4 | 最大16GB |

## トラブルシューティング

### ファームウェアが書き込めない
- **CH9102Fドライバー**がインストールされているか確認
- USBケーブルがデータ転送対応か確認
- COMポート番号が正しいか確認（デバイスマネージャー）
- M5Stack Fireのリセットボタンを押してから再試行

### I2Cデバイスが認識されない
- デバイスが正しく接続されているか確認
- I2Cアドレスが正しいか確認（I2C Scannerサンプルで確認可能）
- V2.7では**5.1V電源供給**されているため、3.3V専用デバイスは注意が必要

### 地磁気センサーが使えない
- **V2.7にはBMM150が非搭載**です
- 外部地磁気センサーモジュール（QMC5883L、HMC5883L等）をPORT Aに接続してください

### デプロイエラーが発生する
- nanoFrameworkファームウェアが正しく書き込まれているか確認
- Visual StudioでnanoFramework拡張機能がインストールされているか確認
- NuGetパッケージのバージョンが互換性があるか確認

### 画面が見づらい
- V2.7は**ガラススクリーン**採用で視認性が向上していますが、明るさ調整が必要な場合はバックライト制御（GPIO32）を使用してください

## V2.7固有の注意事項

1. **地磁気センサー非搭載**: BMM150が搭載されていないため、コンパス機能が必要な場合は外部モジュールが必要です
2. **GROVEポート電圧**: 5.1Vに昇圧されているため、3.3V専用デバイスを接続する場合はレベル変換が必要です
3. **ガラススクリーン**: 耐傷性は向上していますが、落下には注意してください
4. **CH9102Fドライバー**: Windows環境では専用ドライバーが必要な場合があります

## 参考リンク

- [M5Stack Fire V2.7公式ドキュメント](https://docs.m5stack.com/ja/core/fire_v2.7)
- [nanoFramework公式サイト](https://www.nanoframework.net/)
- [nanoFramework GitHub](https://github.com/nanoframework)
- [nanoFramework Discord](https://discord.gg/gCyBu8T)
- [CH9102Fドライバー](https://www.wch.cn/downloads/CH343SER_ZIP.html)

## ライセンス

MIT License

## 貢献

プルリクエストを歓迎します！バグ報告や機能リクエストはIssuesにお願いします。

---

**対象デバイス**: M5Stack Fire V2.7  
**ファームウェア**: .NET nanoFramework  
**開発環境**: .NET 10 (トップレベルステートメント対応)