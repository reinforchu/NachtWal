# NachtWal: Das Anwendungssystem für automatische Verteidigung

## Versions

### 1.0.3.4

QueryStringおよびBodyのkey名も検査するように実装しました。  
URLデコードするコードが残っていたため、Anti-XSSがバイパスされてしまう問題を改修しました。  
情報提供：mageさん([@mage_1868](https://twitter.com/mage_1868)) [Issue](https://twitter.com/mage_1868/status/809643608978571268)  
情報提供にあたって、この場を借りて感謝申し上げます。

### 1.0.2.3

静的メンバ変数に起因する、メモリの扱いのクラッシュバグを改修しました。

### 1.0.1.2

XSS検査の文字数チェックの論理演算子の誤りを改修しました。

### 1.0.0.1

初回リリース。  

### 採番基準

| 1     | 0     | 0     | 0   |
|:------|:------|:------|:----|
| Major | Minor | Build | Fix |

### 開発ステータス

| Status | Nightly       | Dev       | Stable    |
|:------ |:--------------|:----------|:----------|
| Alpha  | Alpha/Nightly | Alpha/Dev | -         |
| Beta   | Beta/Nightly  | Beta/Dev  | -         |
| RC     | -             | RC/Dev    | RC/Stable |
| Stable | -             | -         | Stable    |

## Futures

* StoredのXSSを検知させる(実装可能)
* XMLで設定ファイルの読み込み
* Set-Cookieのセキュリティ設定
* StackTraceやエラーページの抑制
* ロギング機能の実装
* しっかりとした例外処理の実装
* テキスト以外のリクエストの際にException吐く問題を改修
* CGI環境でもある程度動くようにする