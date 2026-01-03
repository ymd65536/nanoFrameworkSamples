# PORT A サンプル

M5Stack FireのPORT Aを使用したサンプルコード集です。

## PORT A 仕様

PORT Aは、M5Stack Fireの拡張ポートで、I2C通信とGPIOに対応しています。

### ピン配置
- GPIO21 (SDA)
- GPIO22 (SCL)
- 5V
- GND

## サンプル一覧

### 01_BME280_Sensor
BME280環境センサー（温度・湿度・気圧）を読み取るサンプル

### 02_MPU6050_IMU
MPU6050 IMUセンサー（加速度・ジャイロ）を読み取るサンプル

### 03_OLED_Display
OLEDディスプレイに文字を表示するサンプル

### 04_CustomI2C
カスタムI2C通信の基本的な使い方のサンプル

### 05_GPIO_Output
GPIO出力でLEDを制御するサンプル

### 06_GPIO_Input
GPIO入力でボタンの状態を読み取るサンプル

### 07_I2C_Scanner
I2Cバス上のデバイスをスキャンするサンプル

### 08_BME280_Display
BME280センサーのデータをOLEDディスプレイに表示するサンプル

## 必要なNuGetパッケージ

- nanoFramework.Hardware.Esp32
- nanoFramework.System.Device.Gpio
- nanoFramework.System.Device.I2c
- Iot.Device.Bmxx80 (BME280使用時)
- Iot.Device.Mpu6050 (MPU6050使用時)

## 使用方法

1. Visual Studioでプロジェクトを開く
2. 必要なNuGetパッケージをインストール
3. サンプルコードをコピー
4. M5Stack Fireにデプロイ

## 注意事項

- I2Cデバイスを接続する前に、正しい電圧（3.3V/5V）を確認してください
- 複数のI2Cデバイスを接続する場合、アドレスの競合に注意してください
