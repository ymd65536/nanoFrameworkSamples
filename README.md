# nanoFramework サンプルコード集

.NET nanoFrameworkを使用したM5Stack Fireのサンプルコード集です。

## 含まれるサンプル

### PORT A サンプル
M5Stack FireのPORT A（Grove互換I2Cポート）を使用するサンプル集

詳細は [PortA/README.md](PortA/README.md) を参照してください。

## 必要な環境

- .NET nanoFramework
- M5Stack Fire
- Visual Studio 2022 or later

## セットアップ

1. Visual Studioで.NET nanoFrameworkの開発環境をセットアップ
2. M5Stack Fireにnanoframeworkファームウェアを書き込み
   ```
   nanoff --target M5Core2 --update --serialport COM3
   ```
3. 各サンプルプロジェクトを開いてビルド・デプロイ

## ライセンス

MIT License
