# GymTracer Backend

> **Vizsgaremek rövid bemutatása**  
> A GymTracer backend egy edzőtermi adminisztrációs és ügyfélkezelő rendszer szerveroldali része.  
> Feladata a hitelesítés, jogosultságkezelés, jegy- és fizetéskezelés, edzésfoglalás, beléptetés és statisztikák kiszolgálása REST API-n keresztül.

---

## Tartalomjegyzék

1. [Projektáttekintés](#projektáttekintés)
2. [Technológiai összefoglaló](#technológiai-összefoglaló)
3. [Főbb funkciók](#főbb-funkciók)
4. [Architektúra és működési modell](#architektúra-és-működési-modell)
5. [Mappaszerkezet](#mappaszerkezet)
6. [Telepítési és futtatási leírás](#telepítési-és-futtatási-leírás)
7. [Konfigurációk és környezeti változók](#konfigurációk-és-környezeti-változók)
8. [Adatmodell összefoglaló](#adatmodell-összefoglaló)
9. [Hitelesítés és jogosultság](#hitelesítés-és-jogosultság)
10. [API útvonalak részletesen](#api-útvonalak-részletesen)
11. [Kódrészletek (valós projektkód alapján)](#kódrészletek-valós-projektkód-alapján)
12. [Demó felhasználók és jelszavak](#demó-felhasználók-és-jelszavak)
13. [Gyakori fejlesztői folyamatok](#gyakori-fejlesztői-folyamatok)
14. [Hibakeresés](#hibakeresés)
15. [Készítők](#készítők)

---

## Projektáttekintés

A backend a következő problémákat oldja meg:

- több szerepkör (ügyfél/edző/recepciós/admin) közös rendszerben kezelése,
- valós idejű edzésjelentkezés és jelenlétkezelés,
- jegyekhez kötött beléptetés és felhasználás-ellenőrzés,
- adminisztratív statisztikák biztosítása vezetői döntésekhez.

A rendszer erősen adatvezérelt: minden fő entitás (felhasználó, kártya, jegy, edzés, fizetés, napló) relációs adatbázisban tárolódik, EF Core migrációkkal menedzselve.

---

## Technológiai összefoglaló

| Terület | Megoldás | Megjegyzés |
|---|---|---|
| Platform | ASP.NET Core 8 (Web API) | `net8.0` target |
| ORM | Entity Framework Core | migrációk és seed adatok |
| Adatbázis | MySQL/MariaDB | lokálban és Kubernetesen is használva |
| Auth | egyedi bearer token + policy | `SessionToken` alapértelmezett policy |
| Jelszókezelés | PBKDF2 | paraméterezhető iteráció/hash/só |
| API leírás | Swagger (Development) | `swagger` UI |
| Konténer | Docker + K8s deployment | `deployment.yaml` |

---

## Főbb funkciók

### 1) Felhasználókezelés
- profil lekérdezés/módosítás,
- szerepkör módosítás (admin),
- deaktiválás.

### 2) Kártyakezelés
- felhasználói kártyák listázása,
- új kártya kibocsátás,
- kártya visszavonás.

### 3) Jegyek és fizetések
- jegytípus-lista,
- felhasználói jegyvásárlás,
- fizetési státusz frissítése.

### 4) Edzésmenedzsment
- edzéslista szűréssel,
- edzés létrehozás/szerkesztés/törlés,
- jelentkezés/lejelentkezés,
- jelenlét igazolás.

### 5) Beléptetés
- kapu beléptetés kártya alapján,
- főkapu beléptetés külön logikával.

### 6) Statisztikák
- látogatottsági kimutatás,
- jegyeladási statisztika,
- kártyahasználati napló.

---

## Architektúra és működési modell

### Magas szintű folyamat

1. kliens (Angular) API kérést küld,
2. backend hitelesít és authorizál,
3. kontroller validál,
4. EF Core művelet fut,
5. üzleti logika alapján válasz JSON.

### Rétegek

| Réteg | Fő felelősség |
|---|---|
| Controller | HTTP végpont, bemenet-ellenőrzés, státuszkód |
| Auth | token- és jelszókezelés |
| Context | adatbázis-hozzáférés, relációk, seed |
| Model | entitások és domain enumok |

---

## Mappaszerkezet

| Útvonal | Tartalom |
|---|---|
| `Controllers/` | `AuthController`, `UserController`, `TicketController`, `TrainingController`, `GateController`, `StatisticController` |
| `Auth/` | `AuthHandler`, `TokenHandler`, `PasswordHandler`, options osztályok |
| `Context/` | `GymTracerDbContext` + EF konfiguráció |
| `Models/` | entitások (`User`, `Card`, `Ticket`, stb.) |
| `Migrations/` | EF migrációk |
| `ExampleData/` | induló mintaadatok JSON-ban |
| `Gymtracer.Tests/` | egységtesztek |

---

## Telepítési és futtatási leírás

## A) Lokális futtatás

### Előfeltételek

- .NET SDK 8+
- MySQL vagy MariaDB
- elérhető adatbázis és érvényes connection string

### 1. Klónozott projekt elérés

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend
```

### 2. Függőségek és fordítás

```bash
dotnet restore
dotnet build GymTracer.sln
```

### 3. Környezeti változó beállítás

```bash
export ConnectionStrings__gymtracerDb="server=localhost;port=3306;database=gymtracerdb;user=<felhasznalo>;password=<jelszo>;"
```

### 4. Indítás

```bash
dotnet run --project /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend/GymTracer.csproj
```

### 5. Elérés

| Cél | URL |
|---|---|
| API (HTTP) | `http://localhost:5065` |
| API (HTTPS) | `https://localhost:7261` |
| Swagger | `https://localhost:7261/swagger` |

---

## B) Build és teszt parancsok

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend

dotnet test GymTracer.sln
```

> A repository aktuális állapotában a tesztek lefutnak, de figyelmeztetések jelenhetnek meg (nullable/migration naming).

---

## C) Konténeres/Kubernetes futtatás (rövid)

A `deployment.yaml` alapján:

- MariaDB deployment + PVC,
- phpMyAdmin deployment,
- backend deployment,
- ClusterIP service-ek.

Kiemelt környezeti változók K8s-ben:

| Változó | Szerep |
|---|---|
| `ConnectionStrings__gymtracerDb` | backend DB kapcsolat |
| `PasswordHandler__AlgorithmName` | hash algoritmus (`SHA256`) |
| `PasswordHandler__Iterations` | PBKDF2 iterációk |
| `PasswordHandler__HashLength` | hash hossz |
| `PasswordHandler__SaltLength` | só hossz |
| `AuthHandler__ExpirationInMinutes` | token lejárat |
| `AuthHandler__TokenLength` | token hossz |

---

## Konfigurációk és környezeti változók

## Kötelező

| Kulcs | Példa | Megjegyzés |
|---|---|---|
| `ConnectionStrings:gymtracerDb` | `server=...` | app config-ból olvasva |
| `ConnectionStrings__gymtracerDb` | env változó | productionben jellemzően secretből |

## Ajánlott auth paraméterek

| Kulcs | Tipikus érték |
|---|---|
| `PasswordHandler:AlgorithmName` | `SHA256` |
| `PasswordHandler:Iterations` | `10` |
| `PasswordHandler:HashLength` | `48` |
| `PasswordHandler:SaltLength` | `16` |
| `AuthHandler:ExpirationInMinutes` | `5` |
| `AuthHandler:TokenLength` | `128` |

---

## Adatmodell összefoglaló

| Entitás | Funkció |
|---|---|
| `User` | felhasználói profil és szerepkör |
| `Card` | belépőkártya |
| `Ticket` | jegy/bérlet típus |
| `UserTicket` | felhasználóhoz rendelt jegy |
| `Payment` | fizetési rekord |
| `Training` | csoportos edzés |
| `TrainingUser` | jelentkezés és jelenlét |
| `UsageLog` | belépési napló |
| `Token` | aktív session token |

---

## Hitelesítés és jogosultság

- A backend alapértelmezett policy-ja: **SessionToken**.
- Token nélkül a védett végpontok 401-et adnak.
- Szerepkör alapú korlátozás `[Authorize(Roles = ...)]` attribútummal történik.

### Szerepkörök

| Enum | Jelentés |
|---|---|
| `customer` | ügyfél |
| `trainer` | edző |
| `staff` | recepciós/munkatárs |
| `admin` | adminisztrátor |

---

## API útvonalak részletesen

Alap prefix: `/api`

## 1) Auth (`/api/auth`)

| Metódus | Útvonal | Rövid leírás | Auth |
|---|---|---|---|
| POST | `/auth/registration` | új felhasználó regisztráció | publikus |
| POST | `/auth/login` | bejelentkezés, token kiadás | publikus |
| POST | `/auth/logout` | token visszavonás | bejelentkezett |

## 2) User (`/api/user`)

| Metódus | Útvonal | Rövid leírás | Auth |
|---|---|---|---|
| GET | `/user/{id}/profile` | profil adatok | felhasználó |
| PUT | `/user/{id}/profile` | profil frissítés | felhasználó |
| DELETE | `/user/{id}` | felhasználó deaktiválás | felhasználó |
| GET | `/user/{id}/card` | kártyák lekérdezése | felhasználó |
| POST | `/user/{id}/card` | új kártya | felhasználó |
| DELETE | `/user/{id}/card/{card_id}` | kártya törlés | felhasználó |
| GET | `/user/{id}/training?arePreviousNeeded={bool}` | user edzései | felhasználó |
| POST | `/user/{id}/training/{training_id}/ticket/{ticket_id}` | jelentkezés edzésre | felhasználó |
| DELETE | `/user/{id}/training/{training_id}` | lejelentkezés | felhasználó |
| GET | `/user?name=&email=&guid=` | keresés név/email/guid szerint | staff/admin |
| PUT | `/user/{id}/role` | szerepkör módosítás | admin |

## 3) Ticket (`/api/ticket`)

| Metódus | Útvonal | Rövid leírás | Auth |
|---|---|---|---|
| GET | `/ticket` | jegytípus lista | publikus |
| GET | `/ticket/user/{id}` | user jegyei | felhasználó |
| GET | `/ticket/user/{id}/unpaid` | user nem fizetett jegyei | felhasználó |
| POST | `/ticket/{ticket_id}/user/{id}/{is_paid}` | jegy hozzárendelés/fizetés | felhasználó |
| PATCH | `/ticket/user/{id}/pay/{payment_id}` | fizetés rendezése | felhasználó |

## 4) Training (`/api/training`)

| Metódus | Útvonal | Rövid leírás | Auth |
|---|---|---|---|
| GET | `/training` | edzéslista | publikus |
| GET | `/training/{training_id}` | adott edzés részlete | publikus |
| GET | `/training/user/{id}` | edző edzései | trainer/staff/admin |
| POST | `/training/user/{id}` | új edzés | trainer/staff/admin |
| PUT | `/training/{training_id}` | edzés módosítás | trainer/staff/admin |
| DELETE | `/training/{training_id}` | edzés törlés/deaktiválás | trainer/staff/admin |
| PATCH | `/training/{training_id}/user/{id}/presence/{presence}` | jelenlét állítás | trainer/staff/admin |

## 5) Statistic (`/api/statistic`)

| Metódus | Útvonal | Rövid leírás | Auth |
|---|---|---|---|
| GET | `/statistic/gym?daysBack=&weeksBack=` | látogatottság (napi/heti) | staff/admin |
| GET | `/statistic/tickets` | jegystatisztika | admin |
| GET | `/statistic/card` | kártyahasználati log | admin |

## 6) Gate (`/api/gate`)

| Metódus | Útvonal | Rövid leírás | Auth |
|---|---|---|---|
| POST | `/gate/{gate_id}/card/{card_code}/enter?force={bool}` | kapu beléptetés | staff/admin |
| POST | `/gate/{gate_id}/card/{card_code}/enter-main?force={bool}` | főkapu beléptetés | staff/admin |

---

## Kódrészletek (valós projektkód alapján)

### 1) DB kapcsolat beolvasása (`Program.cs`)

```csharp
var configuration = builder.Configuration;
var connString = configuration.GetConnectionString("gymtracerDb");

builder.Services.AddDbContext<GymTracerDbContext>(o =>
{
    o.UseMySQL(connString);
});
```

### 2) CORS beállítás (éles + debug origin)

```csharp
policy.AllowAnyHeader()
      .AllowAnyMethod();

policy.WithOrigins("https://gymtracer.jcloud.jedlik.cloud");

#if DEBUG
policy.WithOrigins("http://localhost:4200");
#endif
```

### 3) Seed adatok betöltése (`GymTracerDbContext`)

```csharp
var users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText("ExampleData/Users.json"), options) ?? [];
modelBuilder.Entity<User>().HasData(users);
```

### 4) Jelszó-összehasonlítás PBKDF2-vel (`PasswordHandler`)

```csharp
byte[] passwordHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, algorithm, hashLength);
return freshPasswordHash == passwordHash;
```

### 5) Authorize példa statisztikai végponton

```csharp
[HttpGet("tickets")]
[Authorize(Roles = nameof(User_Role.admin))]
public IActionResult GetTicketsStatistics()
```

### 6) Bejelentkezés hívási példa (HTTP)

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "admin@gym.hu",
  "password": "<jelszo>"
}
```

### 7) Tipikus válaszváz (szemléltető)

```json
{
  "token": "<egyedi-token>",
  "validTo": "2026-05-01T10:05:00",
  "user": {
    "id": 1,
    "name": "Adminisztrátor Anna",
    "role": 3
  }
}
```

---

## Demó felhasználók és jelszavak

A seed felhasználók az `ExampleData/Users.json` fájlban találhatók.

> **Fontos:** a repository-ban a jelszavak **hash-elve** vannak eltárolva (PBKDF2), ezért a nyílt szöveges jelszó nem olvasható ki közvetlenül a forrásból.  
> Demó közben javasolt saját tesztfiók létrehozása a regisztrációs végponton.

### Kiemelt demó felhasználók (seed)

| Név | E-mail | Szerepkör | Jelszó a repo-ban |
|---|---|---|---|
| Adminisztrátor Anna | `admin@gym.hu` | admin | hash-elve |
| Recepciós Ricsi | `ricsi.staff@gym.hu` | staff | hash-elve |
| Edző Elemér | `elemer.edzo@gym.hu` | trainer | hash-elve |
| Edző Eszter | `eszter.edzo@gym.hu` | trainer | hash-elve |
| Kovács János | `janos.kovacs@email.com` | customer | hash-elve |
| Nagy Anna | `anna.nagy@email.com` | customer | hash-elve |

### Ha gyorsan kell működő bejelentkezési jelszó teszthez

1. indítsd a backendet,
2. regisztrálj új felhasználót a `/api/auth/registration` végponton,
3. azonnal be tudsz lépni az új fiókkal,
4. staff/admin jogosultságot külön admin művelettel lehet adni.

---

## Gyakori fejlesztői folyamatok

### Új feature hozzáadása

1. Model bővítése,
2. szükséges migráció elkészítése,
3. Controller endpoint implementálása,
4. jogosultság és validáció,
5. teszt bővítés,
6. frontend endpoint bekötés.

### Migration workflow

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend

dotnet ef migrations add <migration_nev>
dotnet ef database update
```

### API gyors ellenőrzés

- Swaggerrel endpoint teszt,
- role alapú végpont ellenőrzés több tokennel,
- hibakódok ellenőrzése (400/401/403/404/409).

---

## Hibakeresés

| Probléma | Tipikus ok | Teendő |
|---|---|---|
| 401 Unauthorized | hiányzó/lejárt token | újra login, Authorization fejléc ellenőrzése |
| 403 Forbidden | rossz szerepkör | megfelelő role-lal tesztelj |
| DB connection error | hibás connection string | `ConnectionStrings__gymtracerDb` ellenőrzése |
| CORS hiba | nem engedélyezett origin | `Program.cs` CORS policy ellenőrzése |
| migration futási hiba | séma inkonzisztencia | migration és DB állapot összehangolása |

---

## Készítők

| Név | Szerep |
|---|---|
| **Bende Huba** | projekt készítő, fejlesztő |

> A repository Git history alapján a fő készítő: **Bende Huba**.

---

## Rövid összefoglaló

A GymTracer backend egy teljes, szerepkör-alapú edzőtermi API:

- hitelesítés + authorization,
- felhasználó/jegy/edzés/beléptetés kezelés,
- statisztikai riportok,
- lokális és Kubernetes futtatási lehetőség,
- seed adatokkal gyors demózhatóság.

---

## Részletes endpoint paraméter-táblák

## Auth végpontok - bemenetek

| Útvonal | Kötelező mezők | Opcionális mezők | Példa |
|---|---|---|---|
| `POST /api/auth/registration` | `name`, `email`, `password` | `birthDate` | új customer fiók |
| `POST /api/auth/login` | `email`, `password` | – | token lekérése |
| `POST /api/auth/logout` | – | – | aktuális session lezárása |

## User végpontok - path/query mezők

| Útvonal | Path/query paraméterek | Rövid megjegyzés |
|---|---|---|
| `GET /api/user/{id}/profile` | `id` | user azonosító |
| `PUT /api/user/{id}/profile` | `id` + body | név/email/birthdate módosítás |
| `GET /api/user?name=&email=&guid=` | `name`, `email`, `guid` | staff/admin keresés |
| `GET /api/user/{id}/training` | `id`, `arePreviousNeeded` | múltbeli/jövőbeli lista |

## Training végpontok - tipikus body mezők

| Útvonal | Gyakori mezők | Leírás |
|---|---|---|
| `POST /api/training/user/{id}` | név, kezdés, vég, férőhely | új edzés létrehozás |
| `PUT /api/training/{training_id}` | módosítandó mezők | edzésfrissítés |
| `PATCH /api/training/{training_id}/user/{id}/presence/{presence}` | `presence` bool | jelenlét állítás |

---

## Tipikus végpont-szcenáriók (lépésről lépésre)

### 1) Új ügyfél regisztráció + bejelentkezés

1. `POST /api/auth/registration`
2. `POST /api/auth/login`
3. kapott token tárolása kliensben
4. profil/jegy/edzés oldalak elérése tokennel

### 2) Edzésre jelentkezés

1. user tokennel belép,
2. elérhető edzések lekérése: `GET /api/training`,
3. user aktív jegy lekérése: `GET /api/ticket/user/{id}`,
4. jelentkezés: `POST /api/user/{id}/training/{training_id}/ticket/{ticket_id}`.

### 3) Staff beléptetés

1. staff tokennel dolgozik,
2. kártya leolvasás,
3. beléptető hívás: `POST /api/gate/{gate_id}/card/{card_code}/enter-main`,
4. rendszer naplózza a használatot és visszaadja az eredményt.

### 4) Admin bevételi/statisztikai ellenőrzés

1. admin login,
2. jegyeladási statisztika: `GET /api/statistic/tickets`,
3. kártyahasználat: `GET /api/statistic/card`,
4. adatok vizualizálása frontend oldalon.

---

## HTTP státuszkód referencia

| Kód | Jelentés | Mikor fordul elő |
|---|---|---|
| `200 OK` | sikeres lekérdezés/módosítás | normál üzleti folyamat |
| `201 Created` | sikeres létrehozás | regisztráció/új erőforrás |
| `400 Bad Request` | hibás kérés | hiányzó vagy érvénytelen mező |
| `401 Unauthorized` | nincs hitelesítés | token hiány/lejárat |
| `403 Forbidden` | nincs jogosultság | nem megfelelő szerepkör |
| `404 Not Found` | nem létező erőforrás | rossz azonosító |
| `409 Conflict` | üzleti ütközés | duplikált/ütköző művelet |
| `500 Internal Server Error` | szerver oldali hiba | váratlan kivétel |

---

## Biztonsági megfontolások

- jelszó **nem plaintext** formában tárolódik,
- tokenes auth miatt minden klienshívásnál szükséges a helyes Authorization fejléc,
- role-based endpoint védelem csökkenti a jogosulatlan műveletek kockázatát,
- productionben javasolt a connection string és auth paraméterek secret kezelése,
- javasolt rövid token lejárat és rendszeres session megújítás.

---

## Release/átadás előtti ellenőrzőlista

- [ ] `dotnet restore` sikeres
- [ ] `dotnet build` sikeres
- [ ] `dotnet test` sikeres
- [ ] kritikus endpointok kipróbálva Swaggerből
- [ ] role hozzáférések ellenőrizve (customer/trainer/staff/admin)
- [ ] DB migrációk állapota rendben
- [ ] környezeti változók valós környezetre állítva

---

## Oktatási/vizsgaremek fókusz (értékelési szempontból)

Ez a backend jól demonstrálja:

1. modern .NET API építési mintákat,
2. role-based jogosultsági rendszert,
3. adatvezérelt működést EF Core-ral,
4. deployment-képes konfigurációs szemléletet,
5. frontenddel együttműködő, valós üzleti folyamatokat.

