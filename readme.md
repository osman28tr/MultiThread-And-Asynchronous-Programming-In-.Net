<div align="center">

<h1>🧵 Multithread Programlama ile Asenkron İşlemler</h1>

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
- [ReaderWriterLock & ReaderWriterLockSlim](#-readerwriterlock--readerwriterlockslim)
- [Threadler Arası İletişim ve Haberleşme](#-threadler-arası-iletişim-ve-haberleşme)
  - [Signalling](#signalling)
  - [ManualResetEventSlim](#manualreseteventslim)
  - [AutoResetEvent](#autoresetevent)
  - [EventWaitHandle](#eventwaithandle)
  - [CountdownEvent](#countdownevent)
- [Thread Pool](#-thread-pool)
  - [Yapılanması](#yapılanması)
  - [Kullanılan Metot: QueueUserWorkItem](#kullanılan-metot-queueuserworkitem)
  - [Maksimum ve Minimum Thread Sayısının Belirlenmesi](#maksimum-ve-minimum-thread-sayısının-belirlenmesi)
- [Wait Handles Yapılanması](#-wait-handles-yapılanması)
  - [RegisterWaitForSingleObject](#registerwaitforsingleobject)
  - [WaitHandle.WaitAll](#waithandlewaitall)
  - [WaitHandle.WaitAny](#waithandlewaitany)
  - [WaitHandle.SignalAndWait](#waithandlesignalandwait)
- [Barrier Class](#-barrier-class)
- [Thread Local Storage (TLS)](#-thread-local-storage-tls)
  - [ThreadStatic Attribute](#threadstatic-attribute)
  - [ThreadLocal\<T\> Class](#threadlocalt-class)
  - [GetData & SetData Fonksiyonları](#getdata--setdata-fonksiyonları)
- [Timers](#-timers)
  - [Multithread Timer](#multithread-timer)
  - [Singlethread Timer](#singlethread-timer)
- [Genel Karşılaştırma](#-genel-karşılaştırma)
- [Kaynakça](#-kaynakça)

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

## 📖 ReaderWriterLock & ReaderWriterLockSlim

Race condition durumlarında ortak kullanılan kaynağa aynı anda erişen thread'lerden **yalnızca birine yazma veya değiştirme izni** verilir, diğer thread'ler ise **sadece okuma** yapabilir.

> [!IMPORTANT]
> Yazma işlemi yapan thread'in işlemi süresince diğer thread'ler **ne okuma ne de yazma** yapabilir.

- Okuma ve yazma işlemlerini **ayrı ayrı** ele alan yapılanmalardır.
- **Okuma işlemlerinin yoğun, yazma işleminin nadir** olduğu durumlarda kullanılır.
- `Slim` versiyonu daha **hafif, verimli ve dispose edilebilir** yapıdadır.

```csharp
private static readonly ReaderWriterLockSlim _rwLock = new();
private static int veri = 0;

static void Oku()
{
    _rwLock.EnterReadLock();
    try
    {
        Console.WriteLine($"Okunan değer: {veri}");
    }
    finally
    {
        _rwLock.ExitReadLock();
    }
}

static void Yaz()
{
    _rwLock.EnterWriteLock();
    try
    {
        veri++;
    }
    finally
    {
        _rwLock.ExitWriteLock();
    }
}
```

---

## 📡 Threadler Arası İletişim ve Haberleşme

### Signalling

Thread'ler arası **haberleşmeyi, iletişimi ve senkronizasyonu** sağlayan yapılanmalardır. Bir thread'de belirli bir işlemin gerçekleştiğini diğer thread'e bildirmek ve diğer thread'in bu şarta göre işlemine devam etmesini sağlamak amacıyla kullanılır.

Race condition, critical section kontrolü ve thread'ler arası güvenli paylaşım sağlayabilir.

**Signalling yaklaşımında kullanılan araçlar:** `ManualResetEventSlim`, `AutoResetEvent`, `EventWaitHandle`, `CountdownEvent`

### ManualResetEventSlim

Birden fazla thread'in belirli bir olayın gerçekleşmesini beklemesini ve olay gerçekleştikten sonra işlemlerine devam etmesini sağlar.

- Sinyal gönderildiğinde sinyali alan **tüm thread'ler aktif hale gelir** ve asenkron şekilde çalışmaya devam eder.
- Tekrar sinyal gönderilebilmesi için event'in **manuel olarak resetlenmesi** gerekir.

```csharp
ManualResetEventSlim manualEvent = new(false);

Thread thread1 = new(() =>
{
    Console.WriteLine("Thread 1 sinyal bekliyor...");
    manualEvent.Wait();
    Console.WriteLine("Thread 1 devam etti.");
});

Thread thread2 = new(() =>
{
    Console.WriteLine("Thread 2 sinyal bekliyor...");
    manualEvent.Wait();
    Console.WriteLine("Thread 2 devam etti.");
});

thread1.Start();
thread2.Start();

Thread.Sleep(1000);
manualEvent.Set();   // bekleyen tüm thread'ler serbest kalır
// manualEvent.Reset(); // tekrar sinyal beklenir hale getirmek için manuel reset
```

### AutoResetEvent

Bir thread'in bir olayı beklemesini ve olay gerçekleştikten sonra işlemine devam edebilmesini sağlar.

**`initialState` parametresi:**

| Değer | Anlamı |
|---|---|
| `false` | Nesne **sinyal bekleyen** durumdadır. Thread'in işlemin tamamlandığına dair sinyal verebilmesi için bu değerin `false` olması gerekir. |
| `true` | Nesne **sinyal verilmiş** durumdadır. Bu durumda her thread'in `AutoResetEvent`'i resetlenmesi gerekir. |

- Beklemeye alınan thread'lerden **yalnızca biri** çalışmasına devam eder.
- Diğer thread'ler, işletim sistemi tarafından belirlenen **kuyruk sırasına** göre bekler.

> 🔫 **Silah benzetmesi:** `false` denildiğinde kurma kolu çekilmiştir ve event set edilmeyi bekler. `true` denildiğinde kurma kolunun tekrar çekilmesi, yani **reset** edilmesi gerekir. Reset edildikten sonra kurma kolu çekilir ve event set edilebilir.

> [!NOTE]
> `AutoResetEvent`'te bir thread `Set` fonksiyonuyla sinyali verdikten sonra, event arka planda **otomatik olarak resetlenir.** Böylece sinyali alan thread işlemine devam ederken ilgili event tekrar set edilip yeni sinyal gönderilebilir. Bu otomatik reset, **her sinyal gönderiminde** geçerlidir.

> [!TIP]
> **Benzetme:** `AutoResetEvent` bir **turnikeye** (her sinyalde tek geçiş), `ManualResetEvent` ise bir **kale kapısına** (açılınca herkes geçer) benzetilebilir.

```csharp
AutoResetEvent autoEvent = new(false);

for (int i = 1; i <= 3; i++)
{
    int no = i;
    new Thread(() =>
    {
        Console.WriteLine($"Thread {no} sinyal bekliyor...");
        autoEvent.WaitOne();
        Console.WriteLine($"Thread {no} devam etti.");
    }).Start();
}

for (int i = 0; i < 3; i++)
{
    Thread.Sleep(1000);
    autoEvent.Set();   // her sinyalde yalnızca bir thread devam eder
}
```

### EventWaitHandle

`AutoResetEvent` ile `ManualResetEvent` yapılarını **ortak bir yapıda** kullanmak amacıyla kullanılan bir yapılanmadır.

```csharp
// AutoResetEvent davranışı
EventWaitHandle autoHandle = new(false, EventResetMode.AutoReset);

// ManualResetEvent davranışı
EventWaitHandle manualHandle = new(false, EventResetMode.ManualReset);
```

### CountdownEvent

Belirli sayıda thread'in işlemlerini **kümülatif olarak bitirip** ilgili thread'e sinyal vermesine dayalı bir yapılanmadır.

- Instance'a verilen adet kadar thread'in işlemini bitirip `Signal` vermesi beklenir.
- **N adet thread** işlemini bitirip sinyal verdiğinde, `Wait` durumunda bekleyen ana thread veya başka bir worker thread serbest bırakılır ve çalışmasına devam eder.

```csharp
CountdownEvent countdown = new(3); // 3 thread'in sinyal vermesi beklenecek

for (int i = 1; i <= 3; i++)
{
    int no = i;
    new Thread(() =>
    {
        Console.WriteLine($"Thread {no} çalışıyor...");
        Thread.Sleep(500 * no);
        Console.WriteLine($"Thread {no} bitti.");
        countdown.Signal();
    }).Start();
}

countdown.Wait(); // 3 sinyal gelene kadar bekler
Console.WriteLine("Tüm thread'ler tamamlandı.");
```

---

## 🏊 Thread Pool

Birden fazla thread'i **ortak bir havuzda yönetmek**, tekrar kullanılabilir hale getirmek ve daha efektif bir davranış sergilemek için kullanılan bir yapılanmadır.

Thread pool ile gereksiz thread oluşturmanın önüne geçip işlemci kaynakları daha verimli kullanılabilir. Örneğin bir thread işlemini bitirdiğinde başka bir operasyonu gerçekleştirecek yeni bir thread oluşturulması gerekiyorsa, bu maliyet ödenmez; işlemini bitiren thread bu yeni operasyonu üstlenir. Böylece yeni thread oluşturma maliyetinin ve olası thread fazlalığından doğan karmaşıklığın önüne geçilmiş olur.

> [!NOTE]
> Thread pool, esasında **object pooling** tasarım desenindeki davranışın thread bazında implemente edilmiş halidir.

### Yapılanması

- Havuzdaki her bir thread belirli bir işlemi gerçekleştirmek için **bekler.**
- İşlemini gerçekleştirdikten sonra **havuza geri döner** ve sonraki işlemler için yine hazırda bekler.
- Bu sayede gereksiz thread oluşturmanın önüne geçilir; thread'lerin sayıları ve planlamaları operasyonlara başlamadan önce belirlenmiş olur ve daha verimli bir çalışma sergilenir.

### Kullanılan Metot: QueueUserWorkItem

```csharp
ThreadPool.QueueUserWorkItem(state =>
{
    Console.WriteLine($"Havuzdaki thread Id: {Environment.CurrentManagedThreadId}");
});

// Parametre göndermek için
ThreadPool.QueueUserWorkItem(state =>
{
    Console.WriteLine($"Gelen veri: {state}");
}, "merhaba");
```

### Maksimum ve Minimum Thread Sayısının Belirlenmesi

`ThreadPool`'u daha optimize kullanabilmek için her yeni operasyona karşılık yeni bir thread oluşturmak yerine:

- **Minimum thread sayısı** ile başlangıçta bu maliyet ödenir ve hızlı reaksiyon alınır.
- **Maksimum thread sayısı** ile thread pool'da en fazla kaç thread'in hazır bekletilebileceği belirlenir.

```csharp
ThreadPool.GetMinThreads(out int minWorker, out int minIO);
ThreadPool.GetMaxThreads(out int maxWorker, out int maxIO);

ThreadPool.SetMinThreads(4, 4);
ThreadPool.SetMaxThreads(10, 10);
```

> [!TIP]
> `ThreadPool`, **yoğun iş taleplerinde ve operasyonlarında** kullanılmaya uygundur. Hem manuel thread oluşturmadaki karmaşıklığı giderir hem de yoğun iş yüküne karşılık daha optimize, verimli, ölçeklenebilir ve performanslı bir çözüm sunar.

---

## ⏳ Wait Handles Yapılanması

**Signalling** yaklaşımını benimseyen genel bir yapılanmadır. Yani bir thread'in başka bir thread'den sinyal beklemesini sağlar ve işlemini buna göre gerçekleştirir. Esasında signalling yapılanmasındaki tüm araçları kapsayan bir yapıdır. `ThreadPool` ile birlikte kullanılabilir.

### RegisterWaitForSingleObject

`RegisterWaitForSingleObject` metodu ile thread pool yapılanmasında kullanılabilir.

> [!WARNING]
> `Register` ile ilgili event'in referansı tutulduktan sonra, optimizasyon açısından bu referans ve benzeri nesnelerin `Unregister` ile **serbest bırakılması** gerekir.

```csharp
ManualResetEvent evt = new(false);

RegisteredWaitHandle registered = ThreadPool.RegisterWaitForSingleObject(
    evt,
    (state, timedOut) =>
    {
        Console.WriteLine(timedOut ? "Zaman aşımı oldu." : "Sinyal alındı.");
    },
    null,
    Timeout.Infinite,
    true); // yalnızca bir kez çalış

evt.Set();

// İş bittiğinde kaynakları serbest bırak
registered.Unregister(evt);
```

### WaitHandle.WaitAll

İlgili thread'in, **tüm** `WaitHandle` yapılandırmalarından sinyal gelmesi durumunda işlemine devam etmesi için kullanılan bir fonksiyondur.

```csharp
ManualResetEvent[] events = { new(false), new(false), new(false) };

// Üçünden de sinyal gelene kadar bekler
WaitHandle.WaitAll(events);
```

### WaitHandle.WaitAny

İlgili thread'in, `WaitHandle` yapılandırmalarının **en az birinden** sinyal gelmesi durumunda işlemine devam etmesi için kullanılan bir fonksiyondur.

```csharp
ManualResetEvent[] events = { new(false), new(false), new(false) };

// Herhangi birinden sinyal gelince devam eder, sinyal veren event'in index'ini döner
int index = WaitHandle.WaitAny(events);
Console.WriteLine($"{index}. event sinyal verdi.");
```

### WaitHandle.SignalAndWait

<!-- TODO: notlarda bu başlığın açıklaması henüz yazılmamış, açıklama eklenecek. -->
İki signal yapılanmasından birinin diğerinden sinyal beklemesini ve geldiği takdirde ise işlemini yapıp ikincisinin de sinyal gönderip ilgili thread’in çalışmaya devam etmesini sağlayan bir fonksiyondur.

```csharp
AutoResetEvent autoResetEvent1 = new(false);
AutoResetEvent autoResetEvent2 = new(false);

autoResetEvent1.Set();
autoResetEvent2.Set();

WaitHandle.SignalAndWait(autoResetEvent1, autoResetEvent2);

Console.WriteLine("Merhaba, Main Thread");
```

---

## 🚦 Barrier Class

Thread'leri herhangi bir `t` anında **ortak bir noktada buluşturmak** ve ona göre bir davranış sergilemek için kullanılan sınıftır. Esasında bir **senkronizasyon aracıdır.**

**Çalışma mantığı:**

1. Thread'lerden biri barrier noktasına geldikten sonra diğer thread'lere sinyal gönderir.
2. Diğer thread'lerden herhangi biri barrier noktasına sinyal göndermediyse, işlemine devam etmez ve barrier noktasında **bekler.**
3. Ardından diğer thread'ler de aynı davranışı gerçekleştirir.
4. Barrier noktasında **tüm thread'lerden sinyal alındıktan sonra**, thread'ler sıfırdan başlatılıyormuş gibi barrier noktasından sonra çalışmalarına devam ederler.

```csharp
Barrier barrier = new(3); // 3 thread barrier noktasında buluşacak

for (int i = 1; i <= 3; i++)
{
    int no = i;
    new Thread(() =>
    {
        Console.WriteLine($"Thread {no} barrier noktasına geliyor...");
        Thread.Sleep(500 * no);

        barrier.SignalAndWait(); // diğer thread'ler gelene kadar bekler

        Console.WriteLine($"Thread {no} barrier noktasını geçti.");
    }).Start();
}
```

> [!TIP]
> Tüm thread'ler barrier noktasında buluştuğu anda **bir işlem gerçekleştirmek** istiyorsak `Barrier` sınıfının **ikinci overload'ını** kullanabiliriz.

```csharp
Barrier barrier = new(3, b =>
{
    // Tüm thread'ler barrier noktasında buluştuğunda çalışır
    Console.WriteLine("Tüm thread'ler barrier noktasında buluştu.");
});
```

---

## 🗄️ Thread Local Storage (TLS)

Her bir thread için ayrılan, diğer thread'leri etkilemeyen, kendilerine has ayrılmış alanlarda veriler üzerinde değişiklik veya işlem yapabildiğimiz **her bir thread'e özgü izole alanlardır.**

- TLS'de oluşturulan her bir verinin **replikaları** ilgili thread'lerin local storage'ında oluşturulur.
- Her bir thread için **local bir davranış** sergilenir.
- Esasında tutarlılığı sağlayan bir **senkronizasyon aracıdır.**

**TLS araçları:** `ThreadStatic` Attribute, `ThreadLocal<T>` Class, `GetData` & `SetData` Fonksiyonları

### ThreadStatic Attribute

Yalnızca **static** değişkenler için kullanılan bir TLS uygulamasıdır. Tip olarak bir attribute'tür. TLS'in uygulanacağı ilgili değişkenin başına `ThreadStatic` attribute'unun tanımlanması yeterlidir.

```csharp
[ThreadStatic]
private static int sayac;

static void Main()
{
    Thread thread1 = new(() =>
    {
        sayac = 10;
        Console.WriteLine($"Thread 1 : {sayac}"); // 10
    });

    Thread thread2 = new(() =>
    {
        sayac = 20;
        Console.WriteLine($"Thread 2 : {sayac}"); // 20
    });

    thread1.Start();
    thread2.Start();
    thread1.Join();
    thread2.Join();

    Console.WriteLine($"Main Thread : {sayac}"); // 0, her thread'in kendi kopyası vardır
}
```

> [!WARNING]
> `[ThreadStatic]` alanına verilen başlangıç değeri (`= 5` gibi) **yalnızca değişkeni ilk kullanan thread için** atanır. Diğer thread'lerde alan `default` değeriyle (`0`, `null` vb.) başlar.

### `ThreadLocal<T>` Class

Hem **static** hem de **instance** değişkenler için TLS sağlar ve her bir thread için başlangıçta **default replikasyon** sağlar.

1. Kullanmak istediğimiz değişkenin tipi generic olarak verilerek bir instance oluşturulur.
2. Bu değişkenin değerine `.Value` property'si ile erişilir.

```csharp
using ThreadLocal<int> sayac = new();

Thread thread1 = new(() =>
{
    sayac.Value = 5;
    Console.WriteLine($"Thread 1 : {sayac.Value}"); // 5
});

Thread thread2 = new(() =>
{
    Console.WriteLine($"Thread 2 : {sayac.Value}"); // 0, default değer
});

thread1.Start();
thread1.Join();
thread2.Start();
thread2.Join();
```

### GetData & SetData Fonksiyonları

<!-- TODO: notlarda bu başlığın açıklaması henüz yazılmamış, açıklama eklenecek. -->

---

## ⏰ Timers

Multithread programlamada belirli **periyodik işlemleri otomatik olarak** gerçekleştirmek için kullanılan yapılandırmalardır. **Multithread timer** ve **single thread timer** olmak üzere 2 şekilde incelenebilir.

<table>
  <tr>
    <th align="left" width="180">Tür</th>
    <th align="left">Açıklama</th>
    <th align="left">Örnek</th>
  </tr>
  <tr>
    <td><b>Multithread Timer</b></td>
    <td>Singlethread timer'lara göre daha <b>güçlü, hassas ve esnektir.</b></td>
    <td><code>System.Threading.Timer</code></td>
  </tr>
  <tr>
    <td><b>Singlethread Timer</b></td>
    <td>Windows Forms ve WPF uygulamalarında kullanılan timer'lardır.</td>
    <td><code>System.Windows.Forms.Timer</code>, <code>DispatcherTimer</code></td>
  </tr>
</table>

### Multithread Timer

```csharp
using System.Threading;

// (callback, state, dueTime, period)
using var timer = new System.Threading.Timer(
    state => Console.WriteLine($"Timer tetiklendi: {DateTime.Now:T}"),
    null,
    0,      // dueTime: ilk tetiklenmeden önceki bekleme (ms)
    1000);  // period: periyot (ms)

Thread.Sleep(5000);
```

> [!NOTE]
> Timer'ın **zamansal periyodik değerini değiştirmek** için `timer.Change` fonksiyonu kullanılır.

```csharp
timer.Change(0, 2000); // artık 2 saniyede bir tetiklenir
```

> [!TIP]
> Timer'ı **sadece bir kez tetiklemek** için timer instance'ının **4. parametresi** olan periyodik değere `Timeout.Infinite` değerinin verilmesi gerekir.

```csharp
using var birKez = new System.Threading.Timer(
    state => Console.WriteLine("Yalnızca bir kez çalıştı."),
    null,
    1000,
    Timeout.Infinite);
```

### Singlethread Timer

Windows Forms ve WPF uygulamalarında kullanılan timer'lardır.

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
  <tr>
    <td><code>ReaderWriterLock / ReaderWriterLockSlim</code></td>
    <td>Blocking</td>
    <td>Uygulama</td>
    <td>Okuma yoğun, yazma nadir senaryolar için; okuma ve yazma ayrı ele alınır</td>
  </tr>
  <tr>
    <td><code>ManualResetEventSlim</code></td>
    <td>Signalling</td>
    <td>Uygulama</td>
    <td>Sinyalde bekleyen tüm thread'ler devam eder, manuel reset gerekir</td>
  </tr>
  <tr>
    <td><code>AutoResetEvent</code></td>
    <td>Signalling</td>
    <td>Uygulama</td>
    <td>Her sinyalde tek thread devam eder, otomatik reset (turnike)</td>
  </tr>
  <tr>
    <td><code>EventWaitHandle</code></td>
    <td>Signalling</td>
    <td>Uygulama</td>
    <td>Auto/Manual reset yapılarını ortak yapıda sunar</td>
  </tr>
  <tr>
    <td><code>CountdownEvent</code></td>
    <td>Signalling</td>
    <td>Uygulama</td>
    <td>N thread bitince bekleyen thread'i serbest bırakır</td>
  </tr>
  <tr>
    <td><code>ThreadPool</code></td>
    <td>Havuz</td>
    <td>Uygulama</td>
    <td>Thread'leri havuzda tutup tekrar kullanır, object pooling davranışı</td>
  </tr>
  <tr>
    <td><code>WaitHandle.WaitAll / WaitAny</code></td>
    <td>Signalling</td>
    <td>Uygulama</td>
    <td>Tüm veya en az bir handle'dan sinyal gelince devam eder</td>
  </tr>
  <tr>
    <td><code>Barrier</code></td>
    <td>Signalling</td>
    <td>Uygulama</td>
    <td>Thread'leri ortak bir noktada buluşturur, hepsi gelince devam ettirir</td>
  </tr>
  <tr>
    <td><code>ThreadStatic</code></td>
    <td>TLS</td>
    <td>Uygulama</td>
    <td>Yalnızca static değişkenler için, her thread'e özel kopya</td>
  </tr>
  <tr>
    <td><code>ThreadLocal&lt;T&gt;</code></td>
    <td>TLS</td>
    <td>Uygulama</td>
    <td>Static ve instance değişkenler için, <code>.Value</code> ile erişim</td>
  </tr>
  <tr>
    <td><code>Timer</code></td>
    <td>Periyodik</td>
    <td>Uygulama</td>
    <td>Multithread ve singlethread (WinForms/WPF) olarak ikiye ayrılır, periyot <code>Change</code> ile değişir</td>
  </tr>
</table>

---

## 🔗 Kaynakça
Bu dokümantasyon hazırlanırken <b>Gençay Yıldız</b> tarafından yayınlanan <b>Asenkron ve Multithread Programlama</b> video eğitim serisinden faydalanılmıştır. Paylaştığı değerli bilgiler ve anlaşılır, detaylı anlatımı için kendisine teşekkür ederim.

📺 Eğitim serisi: [Asenkron ve Multithread Programlama](https://www.youtube.com/watch?v=vyfwwypSd0E&list=PLQVXoXFVVtp1wcNcTWS1GlC0r4ItcQvAj)

---

<div align="center">
  <sub>📚 Asenkron ve Multithread Programlama notları</sub>
</div>

