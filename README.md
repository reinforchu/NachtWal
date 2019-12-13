# NachtWal - 自動防衛運用システム

**IIS用の脆弱性緩和モジュール**  
Webサーバ・アプリケーションとして、推奨されるセキュリティ設定に上書きします。  
通信をキャプチャし透過的に動作するため、既存の設定や環境に影響を与えません。  
また、簡易的にXSS(Reflected)を検知し防御します。  
今後、ポリシーによる一元管理を実装予定です。

## Quick Start

最新版を [Download](https://github.com/reinforchu/NachtWal/releases) します。

1. C:\inetpub\wwwroot\bin ディレクトリへ NachtWal.dll をコピーします。
2. IISマネージャーから対象のWebサイトのモジュール設定を開きます。
3. 「マネージ モジュールを追加」を開きます。
4. 次のように入力または選択します。名前「NachtWal」種類「NachtWal.Firewall」
5. OKを押下しモジュールを有効にします。

DLLのコピー先のパスは環境によって変わります。(ルート直下の \bin ディレクトリです)  
CGIなどASP.NET以外の環境では十分にテストされていません。  
安定性を重視する場合、マネージ モジュールの設定において次をチェックすることを推奨します。  
「ASP.NET アプリケーションまたはマネージ ハンドラーへの要求のみ呼び出す(&I)」  
この場合、CGIと同時に静的ファイルなどにも適用されないことにご注意ください。

## システム要件

.NET Framework 2.0 以上かつ、ASP.NET が使用可能な環境の IIS 上で動作します。

### OS/IISバージョン

| OS                                     | IIS  | Status      |
|:---------------------------------------|:-----|------------:|
| Windows Server 2016    / Windows 10    | 10.0 | Supported   |
| Windows Server 2012 R2 / Windows 8.1   | 8.5  | Supported   |
| Windows Server 2012    / Windows 8     | 8.0  | Supported   |
| Windows Server 2008 R2 / Windows 7     | 7.5  | Supported   |
| Windows Server 2008    / Windows Vista | 7.0  | Supported   |
| Windows Server 2003 R2 / Windows XP    | 6.0  | Unsupported |

要件を満たした設定であれば、OS/IISのバージョンに依存することなく動作すると思います。

### 動作に必要な設定

| Requirements          | Setting     |
|:----------------------|------------:|
| ASP.NET               | Enable      |
| .NET CLR Version      | v2.0 later  |
| .NET Extension        | Enable      |
| Manage pipeline mode  | Integration |

.NET CLR 2.0/4.0 どちらのバージョンでも動作します。  
IISの機能構成(サーバーマネージャー)とIISマネージャー(コンピュータの管理)から設定・確認が可能です。

## デモ環境

本モジュールが動作している、XSS攻撃の検知・防御のデモサイトを用意しました。  
BurpやZAPなどのツールレベルであればブロックするかと思います。  
[XSS Attack Demo Site](http://hack.vet/xss) ※IIS 7.5  
[デモサイトのソースコード](https://github.com/reinforchu/NachtWal/blob/master/xss/XSSAttackDemoSite.ashx)  
XSS攻撃の検知・防御機能は、まだ実験的です。   
Storedはそのうち実装します。

### XSS Audit interception fields

| Fields                | Status      |
|:----------------------|------------:|
| QueryString Key/Value | Supported   |
| Body        Key/Value | Supported   |
| Cookie      Key/Value | Future      |
| JSON        Key/Value | Future      |
| Base64 encoded params | Unsupported |
| Path separate  param  | Unsupported |
| Some header fields    | Unsupported |
| UserAgent   field     | Future      |
| Referer     field     | Future      |
| ViewState             | Unsupported |

**CGI環境は、ASP.NETの仕様上の問題によりBodyは非対応です。**

### XSS攻撃の各検知方法の精度(感覚)

| Accuracy   | False Positive | False Negative |
|:-----------|:--------------:|:--------------:|
| Reflected  | Bad            | Good           |
| Point      | Medium         | Medium         |

Reflected: XSS攻撃の可能性のある文字が、Reflectedするかしないかで真偽を決定する検知方法です。  
可能性として検知されたものは全てブロックするため、False Positive が非常に多いです。  
しかしながら、False Negative が限りなく最小になります。  
　  
Point: XSS攻撃のシグネチャにマッチするかしないかを検査し、点数加算方式で評価する検知方法です。  
複数の攻撃シグネチャをベースに、XSS攻撃あるかどうかの傾向を数値化し評価します。  
ただしパターンマッチによる検査のため、高度な攻撃によっては検知できないケースがあります。  
可能性のとして検知されたものから誤検知を排除するため、False Positive は少なくなります。

## 利用範囲

どなたでもお使いいただけます。

## 権利表記

全てのコード資産は私(@reinforchu)が開発したものであります。  
独自研究、オープンソースコミュニティへの貢献、サイバーセキュリティ活動を目的としています。

## Contact

Twitter: [@reinforchu](https://twitter.com/reinforchu)  
E-Mail: rein@hack.vet