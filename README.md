# NachtWal - Reinforced Mitigation Security Filter

**IISの脆弱性緩和モジュール**  
Webサーバ・アプリケーションとして、推奨されるセキュリティ設定に上書きします。  
通信をキャプチャし透過的に動作するため、設定や環境に影響を与えることが少ないです。  
また、簡易的にXSS(Reflected)を検知し防御します。  
将来的にはポリシーによる一元管理を構想しています。

## Quick Start

最新版を [Download](https://github.com/reinforchu/NachtWal/releases) します。

1. C:\inetpub\wwwroot\bin フォルダへ NachtWal.dll をコピーします。
2. IISマネージャーから対象のWebサイトのモジュール設定を開きます。
3. 「マネージ モジュールを追加」を開きます。
4. 次のように入力または選択します。名前「NachtWal」種類「NachtWal.Firewall」
5. OKを押下しモジュールを有効にします。

DLLのコピー先のパスは環境によって変わります。  
CGIなどASP.NET以外の環境では十分にテストされていません。  
予防策として、マネージ モジュールの設定において次をチェックすることを推奨します。  
「ASP.NET アプリケーションまたはマネージ ハンドラーへの要求のみ呼び出す(&I)」

## システム要件

.NET Framework 2.0 以上かつ、ASP.NET が使用可能な環境の IIS 上で動作しますが、いくつかの制約があります。  
CGIには部分的に対応していますが、.NETの制約の問題を確認しているため非推奨です。

### OS/IISバージョン

| OS                                     | IIS  | Status      |
|:---------------------------------------|:-----|------------:|
| Windows Server 2016    / Windows 10    | 10.0 | Supported   |
| Windows Server 2012 R2 / Windows 8.1   | 8.5  | Supported   |
| Windows Server 2012    / Windows 8     | 8.0  | Supported   |
| Windows Server 2008 R2 / Windows 7     | 7.5  | Supported   |
| Windows Server 2008    / Windows Vista | 7.0  | Maybe       |
| Windows Server 2003 R2 / Windows XP    | 6.0  | Unsupported |

要件を満たした設定であれば、OS/IISのバージョンに依存することなく動作すると思います。

### 設定要件

| Requirements          | Setting     |
|:----------------------|------------:|
| ASP.NET               | Enable      |
| .NET CLR Version      | v2.0 leter  |
| .NET Extension        | Enable      |
| Manage pipeline mode  | Integration |

.NET CLR 2.0/4.0 どちらのバージョンでも動作します。  
IISの機能構成(サーバーマネージャー)とIISマネージャー(コンピュータの管理)から設定・確認が可能です。

## デモ環境

本モジュールが動作している、XSS攻撃の検知・防御のデモサイトを用意しました。  
BurpやZAPなどのツールレベルであればブロックするかと思います。  
[XSS Attack Demo Site](http://hack.vet/xss) ※IIS 7.5  
XSS攻撃の検知・防御機能は、まだ実験的です。  
偽陽性を減らす検知ロジックを実装中です。しかしながら、偽陰性が増えることになると思います。  
※旧検知方法は「Lunatic」という設定名で残します。  
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

## 利用範囲

どなたでもお使いいただけます。

## Contact

Twitter: [@reinforchu](https://twitter.com/reinforchu)  
E-Mail: rein@hack.vet