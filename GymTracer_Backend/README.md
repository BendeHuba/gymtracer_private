# GymTracer Backend

Edzőtermi adminisztrációs rendszer szerveroldali komponense ASP.NET Core 8 alapon.  
A backend REST API-n keresztül kezeli a hitelesítést, jogosultságokat, edzéseket, jegyeket, kártyákat és statisztikákat.

## Gyors munkafolyamat-ellenőrzőlista

- [ ] Környezet előkészítése (.NET 8, adatbázis, connection string)
- [ ] Projekt restore + build futtatása
- [ ] API indítása és Swagger ellenőrzése
- [ ] Kulcs végpontok kipróbálása (auth, user, training, ticket)
- [ ] Tesztek futtatása és hibák átnézése

## Tartalom

- [Projektáttekintés](#projektáttekintés)
- [Technológiai háttér](#technológiai-háttér)
- [Gyorsindítás](#gyorsindítás)
- [Konfiguráció](#konfiguráció)
- [API áttekintés](#api-áttekintés)
- [Validáció a backendben](#validáció-a-backendben)
- [Fejlesztői parancsok](#fejlesztői-parancsok)
- [Hibakeresés](#hibakeresés)

## Projektáttekintés

A backend fő célja, hogy egységes, szerepkör-alapú API-t adjon az alábbi folyamatokra:

- felhasználókezelés,
- jegy- és fizetéskezelés,
- edzésfoglalás és jelenlétkezelés,
- beléptetés kártya alapján,
- admin/staff riportok.

A rendszer EF Core-t használ, a séma migrációkkal követhető és telepíthető.

## Technológiai háttér

| Terület | Megoldás |
|---|---|
| Platform | ASP.NET Core 8 (`net8.0`) |
| Adatelérés | Entity Framework Core + MySQL provider |
| Adatbázis | MySQL / MariaDB |
| Auth | egyedi session tokenes authentikáció + policy |
| Jelszókezelés | PBKDF2 (`PasswordHandler`) |
| API dokumentáció | Swagger (Development környezetben) |

## Gyorsindítás

### Előfeltételek

- .NET SDK 8+
- futó MySQL/MariaDB
- érvényes connection string

### Lokális indítás

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend

dotnet restore
dotnet build GymTracer.sln
```

```bash
export ConnectionStrings__gymtracerDb="server=localhost;port=3306;database=gymtracerdb;user=<felhasznalo>;password=<jelszo>;"
dotnet run --project /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend/GymTracer.csproj
```

Elérés:

- HTTP: `http://localhost:5065`
- HTTPS: `https://localhost:7261`
- Swagger: `https://localhost:7261/swagger`

> Feltételezés: lokális fejlesztésnél a backend eléri az adatbázist, és a `ConnectionStrings__gymtracerDb` helyes értékre van állítva.

## Konfiguráció

A backend a DB kapcsolatot a `ConnectionStrings:gymtracerDb` kulcsból olvassa (env megfelelője: `ConnectionStrings__gymtracerDb`).

Részlet a `Program.cs` fájlból:

```csharp
var configuration = builder.Configuration;
var connString = configuration.GetConnectionString("gymtracerDb");

builder.Services.AddDbContext<GymTracerDbContext>(o =>
{
    o.UseMySQL(connString);
});
```

Kiemelt auth/security beállítások:

| Kulcs | Jelentés |
|---|---|
| `AuthHandler:ExpirationInMinutes` | token élettartam |
| `AuthHandler:TokenLength` | token hossz |
| `PasswordHandler:AlgorithmName` | hash algoritmus |
| `PasswordHandler:Iterations` | PBKDF2 iteráció |
| `PasswordHandler:HashLength` | hash hossz |
| `PasswordHandler:SaltLength` | só hossza |

## API áttekintés

Alap prefix: `/api`

| Modul | Példa útvonal | Megjegyzés |
|---|---|---|
| Auth | `POST /api/auth/login` | bejelentkezés, token kiadás |
| User | `GET /api/user/{id}/profile` | profil lekérdezés |
| Ticket | `GET /api/ticket/user/{id}` | felhasználó jegyei |
| Training | `POST /api/training/user/{id}` | edzés létrehozás |
| Gate | `POST /api/gate/{gate_id}/card/{card_code}/enter-main` | beléptetés |
| Statistic | `GET /api/statistic/tickets` | admin riport |

Minta bejelentkezés:

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "admin@gym.hu",
  "password": "<jelszo>"
}
```

## Validáció a backendben

A projekt saját validátor-láncot használ (`DataValidator` + `ValidatorExtension`).  
A kontroller oldalon ez jól olvashatóan, üzleti szabályokkal együtt jelenik meg.

### 1) Validátor futtatása endpointban

```csharp
var validatorResult = ValidateTraining(training);
if (!validatorResult.IsValid)
    return BadRequest(new { validatorResult.Errors });
```

**Mit csinál?**  
Ha bármely mező sérti a szabályokat, a hívó rendezett hibalistát kap `400 Bad Request` válaszban.

### 2) Szabályok definiálása

```csharp
trainingValidator.Validate(t => t.StartTime, "edzés kezdete")
    .NotDefault()
    .After(tokenHandler.Now().AddMinutes(-1))
    .Before(tokenHandler.Now().AddMonths(1));

trainingValidator.Validate(t => t.MaxParticipant, "résztvevő szám")
    .GreaterThan(0ul)
    .LessThan(100ul);
```

**Miért hasznos?**  
A szabályok közel maradnak a domainhez, így a karbantartás és a hibakeresés egyszerűbb.

### 3) Validátor létrehozása

```csharp
public static class Validator
{
    public static Validator<T> Create<T>(T validationTarget)
    {
        return new Validator<T>(validationTarget);
    }
}
```

**Megjegyzés:** ez egy könnyen újrahasznosítható minta, amit más kontrollerekben is következetesen lehet alkalmazni.

## Fejlesztői parancsok

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend

dotnet restore
dotnet build GymTracer.sln
dotnet test GymTracer.sln
```

## Hibakeresés

| Jelenség | Tipikus ok | Teendő |
|---|---|---|
| `401 Unauthorized` | hiányzó/lejárt token | új login, Authorization header ellenőrzése |
| `403 Forbidden` | nem megfelelő szerepkör | role ellenőrzése az endpointon |
| DB connection hiba | hibás connection string | `ConnectionStrings__gymtracerDb` vizsgálata |
| CORS hiba | origin nincs engedélyezve | `Program.cs` CORS beállítások ellenőrzése |

## Készítő

- **Bende Huba**
