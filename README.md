# NachtWal

**Reinforced Mitigation Security Filter**  
**IISの脆弱性緩和モジュール**  
Webサーバ・アプリケーションとして、推奨されるセキュリティ設定に上書きします。  
将来的にはポリシーファイルによって、一元管理を想定しています。  
透過的に動作するため、サーバの設定や環境を汚しません。  
また、簡易的にXSS(Reflected)を検知し防御します。  
現在はASP.NETに依存する実装方法のため、導入可能環境が限定的です。

## Quick Start

最新版を [Download](https://github.com/reinforchu/NachtWal/releases) します。

1. C:\inetpub\wwwroot\bin フォルダ(環境によって変わります)へ、NachtWal.dllをコピーします。
2. IISのマネージャーから対象のWebサイトのモジュールを開きます。
3. マネージ モジュールを追加を開きます。
4. 次の通りに入力します。名前「NachtWal」種類「NachtWal.Firewall」
5. ASP.NET以外は上手く動かない問題を回避するために次をチェックを入れます。「ASP.NET アプリケーションまたは……」
6. OKを押下します。

## システム要件

.NET Framework 2.0 以上かつ、ASP.NET が使用可能な環境の IIS 上で動作します。  
IISは統合モードで実行をしなければならないなど、いくつかの制約があります。  
CGIには部分的に対応していますが、避けられないバグを観測しているため非推奨です。

### 動作確認表

| OS                     | Check       |
|:-----------------------|------------:|
| Windows Server 2016    | Maybe       |
| Windows Server 2012 R2 | Supported   |
| Windows Server 2012    | Maybe       |
| Windows Server 2008 R2 | Maybe       |
| Windows Server 2008    | Maybe       |
| Windows Server 2003 R2 | Unsupported |
| Windows 10             | Supported   |
| Windows 8.1            | Maybe       |
| Windows 8              | Maybe       |
| Windows 7              | Supported   |
| Windows Vista          | Maybe       |
| Windows XP             | Unsupported |

※IIS/.NET Framework/ASP.NETのバージョンや、IISの設定による挙動の違いなどが全て把握出来ていません。

## XSSの攻撃検知・防御のデモ

XSSの攻撃検知・防御のデモサイトを用意しました。  
BurpやZAPなどのツールレベルであればブロックするかと思います。  
[XSS Attack Demo Site](http://hack.vet/xss) ※Windows Server 2008 R2  
まだ実験的な開発段階です。  
英文などの正常値の入力でも、XSS攻撃の可能性として検知することが非常に多いです。

## 利用範囲

どなたでもお使いいただけます。

## Contact

Twitter: [@reinforchu](https://twitter.com/reinforchu)  
E-Mail: rein@hack.vet