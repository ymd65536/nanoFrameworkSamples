# M5StackをnanoFrameworkで使う方法

このドキュメントでは、M5StackデバイスをnanoFrameworkで使用するための手順とガイドを提供します。

ユーザーとの会話には日本語で回答してください。

## nanoFramework プログラミングガイド

- 名前空間
- 標準出力

### 名前空間

M5StackをnanoFrameworkで使用するには、以下の名前空間をインポートしてください。

```csharp
using nanoFramework.M5Stack;
```

### 標準出力

Console.WriteLineメソッドを使用して標準出力ができます。

```csharp
Console.WriteLine("Hello, nanoFramework!");
```

M5Stackのスクリーンを使用する場合はInitializeScreenメソッドを呼び出してから、Console.Clearメソッドで画面をクリアします。以下の例はM5Stack Fireの場合です。

```csharp
Fire.InitializeScreen();
Console.Clear();
```

名前空間の`using System`を使うと、Consoleクラスが競合する可能性があります。

出力されるエラーの例

```text
error CS0104: 'Console' is an ambiguou
s reference between 'nanoFramework.M5Stack.Console' and 'System.Console'
```

エラーが発生した場合は、次の選択肢のいずれかを使用して解決してください。

PCのコンソールに出力する場合は、`using Console = System.Console;`のようにエイリアスを設定してください。

```csharp
using Console = System.Console;
```

M5Stackの画面に出力する場合は、`using Console = nanoFramework.M5Stack.Console;`のようにエイリアスを設定してください。

```csharp
using Console = nanoFramework.M5Stack.Console;
```

### Math

System.Math名前空間を使用する場合は、以下のようにインポートしてください。

```csharp
using Math = System.Math;
```
