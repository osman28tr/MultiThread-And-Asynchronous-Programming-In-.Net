<div align="center">

<h1>🧵 Asenkron ve Multithread Programlama</h1>

<p><em>C# / .NET ile thread, senkronizasyon ve eşzamanlılık</em></p>

<p>
  <img src="https://img.shields.io/badge/C%23-.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="C# .NET" />
  <img src="https://img.shields.io/badge/Konu-Multithreading-0A66C2?style=for-the-badge" alt="Multithreading" />
  <img src="https://img.shields.io/badge/Seviye-Orta-2EA44F?style=for-the-badge" alt="Seviye" />
</p>

</div>

---

## 📑 İçindekiler

- [Temel Kavramlar](#-temel-kavramlar)
- [Asenkron ve Multithread Farkı](#-asenkron-ve-multithread-farkı)
- [Thread Özellikleri](#-thread-özellikleri)
- [Race Condition](#-race-condition)
- [Senkronizasyon Teknikleri](#-senkronizasyon-teknikleri)
  - [Lock](#1-lock-tekniği)
  - [Join](#2-join-tekniği)
- [Blocking Senkronizasyon](#-blocking-senkronizasyon)
  - [Monitor.Enter / Monitor.Exit](#monitorenter-ve-monitorexit)
  - [LockTaken Parametresi](#locktaken-parametresi)
  - [Monitor.TryEnter](#monitortryenter)
  - [Mutex](#mutex)
  - [Semaphore & SemaphoreSlim](#semaphore--semaphoreslim)
- [Non-Blocking Senkronizasyon](#-non-blocking-senkronizasyon)
  - [Volatile](#1-volatile-keyword)
  - [Interlocked](#2-interlocked-sınıfı)
  - [MemoryBarrier](#3-memorybarrier-metodu)
- [Spinning Senkronizasyon](#-spinning-senkronizasyon)
  - [SpinLock](#spinlock)
  - [SpinWait](#spinwait)
- [Genel Karşılaştırma](#-genel-karşılaştırma)

---

## 📌 Temel Kavramlar

<table>
  <tr>
    <th align="left" width="200">Kavram</th>
    <th align="left">Açıklama</th>
  </tr>
  <tr>
    <td><b>Thread</b></td>
    <td>İş parçacığı. Programlama açısından metot gibi düşünülebilir.</td>
  </tr>
  <tr>
    <td><b>Ana Thread</b></td>
    <td>.NET ile oluşturulan uygulamalardaki <code>Main</code> metodu olarak düşünülebilir.</td>
  </tr>
  <tr>
    <td><b>Asenkron &amp; Multithread</b></td>
    <td>Ana thread'i bloklamadan kodu ve işlemi daha verimli ve performanslı hale getirmeyi sağlayan yaklaşımlardır.</td>
  </tr>
</table>

---

## ⚖️ Asenkron ve Multithread Farkı

<table>
  <tr>
    <th align="left" width="180">Özellik</th>
    <th align="left">Asenkron Programlama</th>
    <th align="left">Multithread Programlama</th>
  </tr>
  <tr>
    <td><b>Thread bağımlılığı</b></td>
    <td>Bir veya birden fazla thread'e bağlı değildir. Tek thread'de de çoklu thread'de de uygulanabilir.</td>
    <td>Thread bazlı bir yaklaşımdır.</td>
  </tr>
  <tr>
    <td><b>Çalışma mantığı</b></td>
    <td>İşlemleri bloklamadan yürütür.</td>
    <td>Kümülatif bir işlemi thread'lere bölüp asenkron şekilde dağıtır.</td>
  </tr>
  <tr>
    <td><b>Seviye</b></td>
    <td>Daha <b>yüksek seviyeli</b> programatik yaklaşım.</td>
    <td>Daha <b>alt seviyeli</b>, işletim sistemiyle daha yakın iletişim.</td>
  </tr>
  <tr>
    <td><b>Amaç</b></td>
    <td colspan="2" align="center">Daha yüksek verimlilik, daha düşük maliyet</td>
  </tr>
</table>

---

## 🔧 Thread Özellikleri

- Oluşturulan her thread'e **işletim sistemi tarafından bir ID atanır.**
- Thread'ler çalışma süreleri boyunca birbirlerini beklemeyen yapılardır. Ancak biri bitince diğerini bekleyen senaryolar da olabilir.

### `IsBackground` Property'si

> [!NOTE]
> `IsBackground` eşzamanlılığın önüne geçmez. Thread'ler yine eşzamanlı çalışır. Bu property yalnızca ilgili thread'in **ana thread'e bağlı olup olmadığına** karar verir.

- Varsayılan değeri **`false`**'tur.

---

## 💥 Race Condition

İki veya daha fazla thread'in **aynı kaynağı** (veri, değişken, bellek alanı vb.) kullandığı durumlarda, bu kaynakta oluşan bir değişiklik veya istenmeyen bir durum yüzünden diğer thread'lerin işleyişinin bozulmasıdır.

Bu durumları önlemek için **senkronizasyon teknikleri** kullanılır. En bilinen ve temel teknik **Locking**'dir ve kodun **kritik bölgelerinde** uygulanır.

---

## 🔐 Senkronizasyon Teknikleri

### 1. Lock Tekniği

1. `object` türünden bir referans nesnesi (`_locking`) oluşturulur.
2. Thread'ler arasında ortak kullanılan kritik kaynak, bu thread'ler içinde `lock` bloğuna alınır.
3. `_locking` nesnesini **ilk kilitleyen thread** işini bitirip kilidi açana kadar diğer thread'ler bekler.
4. Thread'lerin lock sırasını **işletim sistemi** belirler.

```csharp
		int i = 1; //ortak kaynak
		object _locking = new();
		Thread thread = new(() =>
		{
			lock (_locking)
			{
				while (i <= 10) //kritik bölge
				{
					i++;
					Console.WriteLine($"Thread 1 : {i}");
				}
			}
		});

		Thread thread2 = new Thread(() =>
		{
			lock (_locking)
			{
				while (i > 0)
				{
					i--;
					Console.WriteLine($"Thread 2 : {i}");
				}
			}
		});
		thread.Start();
		thread2.Start();
```

> [!IMPORTANT]
> `lock` işlemi thread'in **tüm çalışmasını kilitlemez**, yalnızca ilgili `lock` bloğunu kilitli tutar. Aynı thread'deki lock'suz diğer kod blokları çalışmaya devam edebilir.

> [!TIP]
> **Best practice:** `lock` bloğunun içine çok fazla kod veya işlem koymayın.

### 2. Join Tekniği

Thread'leri **sıraya koymak** için kullanılır. Örneğin önce `thread1` çalışıp bitsin, sonra `thread2` çalışsın denilen senaryoda:

1. `thread1` **start** edilir.
2. `thread1` **join** edilir ve bitmesi beklenir.
3. `thread2` **start** edilir.

```csharp
		int i = 1;
		Thread thread = new(() =>
		{

			while (i <= 10)
			{
				i++;
				Console.WriteLine($"Thread 1 : {i}");
			}

		});

		Thread thread2 = new Thread(() =>
		{

			while (i > 0)
			{
				i--;
				Console.WriteLine($"Thread 2 : {i}");
			}

		});
		thread.Start();
		thread.Join();
		thread2.Start();
```
> [!WARNING]
> Senkronizasyon yalnızca **kritik bölgelere** (ortak kaynakların kullanıldığı kısımlara) uygulanır, kodun tamamına uygulanmaz.

Senkronizasyon **blocking** ve **non-blocking** olmak üzere 2 şekilde sağlanır. Bunların dışında bir de **spinning** tekniği vardır.

---

## 🚧 Blocking Senkronizasyon

### `Monitor.Enter` ve `Monitor.Exit`

İkisi de aslında lock mekanizmasıdır; `lock`'un daha fonksiyonel versiyonlarıdır.

> [!NOTE]
> `lock` mekanizması arka planda `Monitor.Enter` ve `Monitor.Exit` fonksiyonlarını kullanır.

> [!CAUTION]
> `Monitor.Enter` / `Monitor.Exit` kullanılırken bloktaki bir **exception** durumunda `Monitor.Exit` ile lock'tan çıkılması gerekir. `lock` mekanizmasında bu kontrole gerek yoktur, çünkü exception durumunda arka planda `Monitor.Exit` otomatik tetiklenir. `lock` kullanılmıyorsa bu kontrol **manuel** yapılmalıdır.

```csharp
bool lockTaken = false;
try
{
    Monitor.Enter(_locking, ref lockTaken);
    sayac++;
}
finally
{
    if (lockTaken)
        Monitor.Exit(_locking);
}
```

### `LockTaken` Parametresi

Senkronizasyonda dikkat edilmesi gereken kritik konulardan biridir. `Monitor.Enter` fonksiyonunun **ikinci parametresidir** ve ilgili thread'in ortak nesneyi **başarılı şekilde kilitleyip kilitleyemediğini** kontrol etmek için kullanılır.

### `Monitor.TryEnter`

- İlgili thread'in kilidi **verilen süre içinde alıp almadığını** kontrol etmeyi ve işlemleri ona göre yapmayı sağlar.
- `lockTaken` kontrolü de bu metodun içinde yer alır.

```csharp
bool lockTaken = false;
try
{
    Monitor.TryEnter(_locking, 1000, ref lockTaken); // 1000 ms

    if (lockTaken)
    {
        // kilit alındı
    }
    else
    {
        // süre içinde kilit alınamadı
    }
}
finally
{
    if (lockTaken)
        Monitor.Exit(_locking);
}
```

### Mutex

Mutex de kritik bölgeye kilit koyar ve diğer thread'lerin bu kaynaklarda çalışmasını engeller. Farkı, davranışın daha alt seviyede, yani **process veya bilgisayar seviyesinde** olmasıdır.

> 💡 **Örnek:** Ekran alıntısı aracı. Her tıklamada yalnızca tek bir pencere açılır. Bu, process seviyesinde bir yapılanmadır.

```csharp
using var mutex = new Mutex(false, "Global\\BenimUygulamam");

if (mutex.WaitOne(0))
{
    try
    {
        // uygulama çalışıyor
    }
    finally
    {
        mutex.ReleaseMutex();
    }
}
else
{
    Console.WriteLine("Uygulama zaten çalışıyor.");
}
```

### Semaphore & SemaphoreSlim

Kritik bölgeye **yalnızca belirli sayıda thread'in** erişmesine izin verir. İzin verilen sayı dışındaki thread'ler bekletilir. Senkronizasyonu daha manevratik ve fonksiyonel hale getirir.

`Wait` fonksiyonuna **milisaniye cinsinden süre** verilebilir. Bu süre içinde thread kritik bölgeye erişemezse beklemeyi bırakır ve geri dönüş değerine göre ilerlenebilir.

```csharp
private static readonly SemaphoreSlim _semaphore = new(3); // aynı anda en fazla 3 thread

static void Calis()
{
    _semaphore.Wait();
    try
    {
        // kritik bölge
    }
    finally
    {
        _semaphore.Release();
    }
}
```

---

## 🏎️ Non-Blocking Senkronizasyon

Thread'ler arasında **blocklama olmadan**, yoğun ve paralel işlemlerde kullanılan; blocking'e göre daha performanslı ve yerine göre daha az maliyetli bir senkronizasyondur.

### 1. `volatile` Keyword

Mikroişlemciler veriyi daha hızlı okumak için bellek yerine **data register**'dan okuyabilir:

1. Veri önce belleğe kaydedilir.
2. Bir kopyası data register'a kaydedilir.
3. Sonraki çağrılar data register'dan yapılır.

Veride değişiklik olduğunda register o anda güncellenmeyebilir ve **eski veri** okunabilir. Bu da tutarsızlığa yol açar.

`volatile`, bu varsayılan davranışı geçersiz kılar ve değişken üzerindeki çağrıların **bellekten yapılmasını garanti eder.**

> [!WARNING]
> Tek başına yeterli olmayabilir. Race condition olan kaynaklarda senkronizasyon teknikleriyle birlikte, hatta bazen blocking senkronizasyonla birlikte kullanılması gerekebilir.

`volatile` ile işaretlenen bir değişkenin her yerde register optimizasyonunu devre dışı bırakması performans kaybı yaratabilir. Bu davranış **yalnızca ilgili kod bloğunda** istenirse `Volatile` sınıfı kullanılır.

```csharp
private volatile bool _calisiyor = true;

// Yalnızca ilgili noktada
int deger = Volatile.Read(ref _veri);
Volatile.Write(ref _veri, 42);
```

### 2. `Interlocked` Sınıfı

Birden fazla thread'in ortak kaynağa eriştiği durumlarda **non-blocking** davranış sergilemeyi sağlar.

- Değişkeni **atomik** olarak ele alır.
- Bir thread işlem yaparken diğer thread'lerin aynı değişkene okuma/yazma yapmasını engeller.
- Artırma, azaltma, değer değiştirme gibi işlemleri atomik yapar.
- `volatile`'a göre **daha güvenli ve sıkı** bir yaklaşımdır.

**Metotları:** `Increment`, `Decrement`, `Add`, `Exchange`, `CompareExchange`

```csharp
private static int sayac = 0;

Interlocked.Increment(ref sayac);
Interlocked.Decrement(ref sayac);
Interlocked.Add(ref sayac, 5);
int eski = Interlocked.Exchange(ref sayac, 100);
Interlocked.CompareExchange(ref sayac, 200, 100); // değer 100 ise 200 yap
```

### 3. `MemoryBarrier` Metodu

Derleyici ve mikroişlemci optimizasyonları sonucunda thread'lerin **belleğe erişim sırası değişebilir.** `MemoryBarrier` bu sıralamayı kontrol altına almak için kullanılır.

- **Full fence** ve **half fence** olmak üzere iki şekilde kullanılır.
- `Interlocked` sınıfında da barrier mantığı vardır; `MemoryBarrier` yapılan işlemi bir adım daha güvence altına alır.

```csharp
Thread.MemoryBarrier(); // full fence
```

---

## 🔄 Spinning Senkronizasyon

Belirli bir döngüde kaynağa erişmek için kaynağın durumunun **sürekli kontrol edilmesidir.**

| | Blocking | Spinning |
|---|---|---|
| **Kaynak yönetimi** | İşletim sistemi thread'lerin kaynak kullanımını ve serbestliğini yönetir | İşletim sistemi açısından bir sorumluluk yoktur |
| **Thread durumu** | Thread bekletilir | Thread sürekli kontrol eder |
| **Kullanım** | Uzun süreli beklemeler | Kısa süreli beklemeler |

> [!WARNING]
> Spinning'de sürekli bir kontrol mekanizması çalıştığı için işlemci gereksiz yere yorulur ve boşa kaynak kullanılır. Uzun süreli bekleme durumlarında **blocking** mekanizmalarını tercih etmek daha doğrudur.

### SpinLock

C#'ta senkronizasyon için geliştirilmiş yapısal bir mekanizmadır ve davranış olarak spinning sergiler.

- Thread **uyutulmaz.**
- Kritik bölgeye erişim şartı sağlanana kadar sürekli kontrol eder.
- Kilitleme yapar ama thread'leri uyutmaz.

```csharp
private static SpinLock _spinLock = new();

bool taken = false;
try
{
    _spinLock.Enter(ref taken);
    // kritik bölge
}
finally
{
    if (taken) _spinLock.Exit();
}
```

### SpinWait

`SpinLock`'a nazaran **daha performanslı, daha az maliyetli ve optimize** bir sınıftır.

```csharp
SpinWait.SpinUntil(() => hazir); // şart sağlanana kadar bekler
```

---

## 📊 Genel Karşılaştırma

<table>
  <tr>
    <th align="left">Teknik</th>
    <th align="left">Tür</th>
    <th align="left">Seviye</th>
    <th align="left">Kısa Not</th>
  </tr>
  <tr>
    <td><code>lock</code></td>
    <td>Blocking</td>
    <td>Uygulama</td>
    <td>Temel teknik, arka planda <code>Monitor</code> kullanır</td>
  </tr>
  <tr>
    <td><code>Join</code></td>
    <td>Blocking</td>
    <td>Uygulama</td>
    <td>Thread'leri sıraya koyar</td>
  </tr>
  <tr>
    <td><code>Monitor.Enter/Exit</code></td>
    <td>Blocking</td>
    <td>Uygulama</td>
    <td><code>lock</code>'un fonksiyonel versiyonu, <code>finally</code> ile <code>Exit</code> şart</td>
  </tr>
  <tr>
    <td><code>Monitor.TryEnter</code></td>
    <td>Blocking</td>
    <td>Uygulama</td>
    <td>Süre sınırlı kilit alma</td>
  </tr>
  <tr>
    <td><code>Mutex</code></td>
    <td>Blocking</td>
    <td>Process / Bilgisayar</td>
    <td>Tek instance kontrolü gibi senaryolar</td>
  </tr>
  <tr>
    <td><code>Semaphore / SemaphoreSlim</code></td>
    <td>Blocking</td>
    <td>Uygulama</td>
    <td>Aynı anda N thread'e izin verir</td>
  </tr>
  <tr>
    <td><code>volatile</code></td>
    <td>Non-blocking</td>
    <td>Mikroişlemci</td>
    <td>Bellekten okuma garantisi</td>
  </tr>
  <tr>
    <td><code>Interlocked</code></td>
    <td>Non-blocking</td>
    <td>Atomik işlem</td>
    <td><code>volatile</code>'dan daha güvenli</td>
  </tr>
  <tr>
    <td><code>MemoryBarrier</code></td>
    <td>Non-blocking</td>
    <td>Mikroişlemci</td>
    <td>Bellek erişim sırasını kontrol eder</td>
  </tr>
  <tr>
    <td><code>SpinLock</code></td>
    <td>Spinning</td>
    <td>Uygulama</td>
    <td>Thread uyutulmaz, CPU tüketir</td>
  </tr>
  <tr>
    <td><code>SpinWait</code></td>
    <td>Spinning</td>
    <td>Uygulama</td>
    <td><code>SpinLock</code>'tan daha optimize</td>
  </tr>
</table>

---

<div align="center">
  <sub>📚 Asenkron ve Multithread Programlama notları</sub>
</div>
