# NachtWal

IISのセキュリティレベルを高めるマネージモジュール  
推奨されるセキュリティ設定を強制的(現状)に設定します。  
簡易的にXSS(Reflected)を検知し防御します。  
ASP.NET環境を想定しているため、実運用には程遠いです。

## Quick Start

最新版を [Download](https://github.com/reinforchu/NachtWal/releases) します。

1. C:\inetpub\wwwroot\bin フォルダ(環境によって変わります)へ、NachtWal.dllをコピーします。
2. IISのマネージャーから対象のWebサイトのモジュールを開きます。
3. マネージ モジュールを追加を開きます。
4. 次の通りに入力します。名前「NachtWal」種類「NachtWal.Firewall」
5. バグ回避のためにチェックを入れます。「ASP.NET アプリケーションまたはマネージ ハンドラ……」(推奨)
6. OKを押下します。

## システム要件

.NET Framework 2.0 以上かつ、ASP.NETが使用可能な環境の IIS 上で動作します。  
IISは統合モードで実行をしなければならないなど、いくつかの制約があります。  
FastCGI には部分的に対応していますが、避けられないバグを観測しているため非推奨です。

### 動作OS

+ Windows Server 2016
+ Windows Server 2012 R2
+ Windows Server 2012
+ Windows Server 2008 R2
+ Windows Server 2008
+ Windows 10 x64
+ Windows 7 x64

## XSS検知のデモ

XSSの検知のデモサイトを用意しました。  
ツールレベルであれば全部ブロックするはずです。  
[ASP.NET XSS Tester](http://hack.vet/xss)

## 利用範囲

どなたでもお使いいただけます。

## Contact

Twitter: [@reinforchu](https://twitter.com/reinforchu)  
E-Mail: rein@hack.vet