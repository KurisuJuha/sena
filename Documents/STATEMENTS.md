# 構文

senaの構文はシンプルです。

## 条件分岐

```sena
if (<条件式>)
    <処理>
}
```

## 反復処理

```sena
for (<識別子> in <コレクション>)
    <処理>
}
```

```sena
while (<条件式>)
    <処理>
}
```

## 宣言

```sena
<型識別子> <識別子>;
```

```sena
let <識別子>;
```

```sena
<型識別子> <識別子> = <式>;
```

```sena
let <識別子> = <式>;
```

## 関数定義

```sena
func <void, 型識別子> <識別子>(<型識別子> <識別子>)
    <処理>
}
```

## return

```sena
return <式>;
```

## break

```sena
break;
```

## continue

```sena
continue;
```

## 式文

```sena
<式>;
```

## 構造体定義

```sena
struct <識別子>
    <文>
}
```

## インターフェース定義

```sena
interface <識別子>
    <文>
}
```
