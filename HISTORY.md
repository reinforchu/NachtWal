# NachtWal - Reinforced Mitigation Security Filter

## Versions

### 1.1.6.1

メソッド名のTypoを修正しました。  
Etagを削除する実装をしました。  
Windows Server 2008/Vista 環境で動作することを検証しました。  
権利関係に関するドキュメントを追加しました。

### 1.1.5.0

バージョン採番基準と開発フェーズを明確に定義しました。  
「X-AspNetMvc-Version」を除去するよう実装しました。  
このほか、いくつかのコード整形を行いました。

### 1.0.3.4

QueryStringおよびBodyのkey名も検査するように実装しました。  
URLデコードするコードが残っていたため、Anti-XSSがバイパスされてしまう問題を改修しました。  
ご報告いただきました、mageさんに感謝申し上げます。  
Reporter: mageさん([@mage_1868](https://twitter.com/mage_1868))  
Issue: [https://twitter.com/mage_1868/status/809643608978571268](https://twitter.com/mage_1868/status/809643608978571268)

### 1.0.2.3

静的メンバ変数に起因する、メモリの扱いのクラッシュバグを改修しました。

### 1.0.1.2

XSS検査の文字数チェックの論理演算子の誤りを改修しました。

### 1.0.0.1

初回リリース。  

## 採番基準表

| 1     | 0     | 0     | 0   |
|:------|:------|:------|:----|
| Major | Minor | Build | Fix |

## 開発ステータス表

| Status |
|:------ |
| Alpha  |
| Beta   |
| RC     |
| Stable |

## Futures

* StoredのXSS攻撃を検知させる実装をする(データをPoolさせるメモリを確保することで実装可能)
* XMLで設定ファイル(ポリシー)の読み込みを実装する
* Set-Cookieのセキュリティ設定を高められるように実装する
* StackTraceやエラーページの抑制する
* ロギング機能の実装する
* 真面目に例外処理の実装をする
* XSS攻撃の検知方法を機械学習で実装する
* ブラックリスト・ホワイトリストを実装する