# GymTracer Backend

A GymTracer backend az edzőtermi üzleti folyamatok szerveroldali motorja.  
Feladata, hogy megbízható REST API-n keresztül kiszolgálja a hitelesítést, jogosultságkezelést, jegy- és fizetési műveleteket, edzéskezelést, beléptetést és statisztikákat.

## Rövid munkafolyamat-ellenőrzőlista

- [ ] Környezet előkészítése (.NET 8, MySQL/MariaDB, connection string)
- [ ] Build és teszt futtatása (`restore`, `build`, `test`)
- [ ] API indítása és Swagger ellenőrzése
- [ ] Auth + role alapú végpontok kipróbálása
- [ ] Validációs és üzleti szabályok ellenőrzése (különösen training/gate folyamatok)
- [ ] Hiba- és edge-case válaszok ellenőrzése (400/401/403/404/409)

## Tartalom

- [Projektcél és működési fókusz](#projektcél-és-működési-fókusz)
- [Technológiai háttér](#technológiai-háttér)
- [Architektúra és kérés-életciklus](#architektúra-és-kérés-életciklus)
- [Fő modulok és felelősségek](#fő-modulok-és-felelősségek)
- [Adatmodell áttekintés](#adatmodell-áttekintés)
- [Hitelesítés és jogosultság](#hitelesítés-és-jogosultság)
- [DataValidator részletesen](#datavalidator-részletesen)
- [API végpontok – mit csinálnak](#api-végpontok--mit-csinálnak)
- [Lokális futtatás](#lokális-futtatás)
- [Konfiguráció és környezeti változók](#konfiguráció-és-környezeti-változók)
- [Fejlesztői workflow](#fejlesztői-workflow)
- [Biztonsági és üzemeltetési megjegyzések](#biztonsági-és-üzemeltetési-megjegyzések)
- [Hibakeresés](#hibakeresés)
- [Átadás előtti ellenőrzőlista](#átadás-előtti-ellenőrzőlista)

## Projektcél és működési fókusz

A backend célja, hogy egyetlen, konzisztens API felületen keresztül kezelje az edzőtermi működés kulcsterületeit:

- **felhasználók és szerepkörök** (customer/trainer/staff/admin),
- **jegyek és fizetések** (vásárlás, státusz, felhasználhatóság),
- **edzések és jelentkezések** (időablak, részvétel, férőhely),
- **kártyás beléptetés** (kapu- és főkapu logika),
- **riportok és statisztikák** (forgalom, kártyahasználat, ticket értékesítés).

A rendszer EF Core alapon, relációs modellre épül, migrációkkal verziózott adatbázis-sémával.

## Technológiai háttér

| Terület | Megoldás | Szerepe |
|---|---|---|
| Platform | ASP.NET Core 8 (`net8.0`) | REST API hostolás |
| ORM | Entity Framework Core | entitások, relációk, migrációk |
| Adatbázis | MySQL / MariaDB | üzleti adatok tárolása |
| Auth | egyedi auth handler + token policy | bejelentkezés, session-élettartam |
| Password | PBKDF2 | jelszó-hash és ellenőrzés |
| API dokumentáció | Swagger (Development) | gyors endpoint tesztelés |

## Architektúra és kérés-életciklus

Egy tipikus kérés útja:

1. A frontend HTTP kérést küld egy `/api/...` végpontra.
2. A middleware lánc fut (`UseCors`, `UseAuthentication`, `UseAuthorization`).
3. Az `AuthHandler` ellenőrzi a Bearer tokent, frissíti a session lejáratát.
4. A controller role és üzleti feltételek alapján feldolgozza a kérést.
5. EF Core tranzakcióval/lekérdezéssel adatbázisműveletek történnek.
6. A backend strukturált JSON választ ad vissza.

`Program.cs` részlet:

```csharp
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "MyAuthentication";

}).AddScheme<AuthOptions, AuthHandler>("MyAuthentication", options =>
{
    builder.Configuration.GetSection(AuthOptions.SectionName).Bind(options);
}).AddBearerToken();

builder.Services.AddAuthorization(options =>
{
    var sessionTokenPolicy = new AuthorizationPolicyBuilder().RequireClaim("SessionToken").Build();
    options.AddPolicy("SessionToken", sessionTokenPolicy);
    options.DefaultPolicy = sessionTokenPolicy;
});
```

**Mit jelent ez gyakorlatban?**  
Az alap policy szerint minden védett végpont csak érvényes `SessionToken` claimmel érhető el.

## Fő modulok és felelősségek

| Modul | Felelősség |
|---|---|
| `Controllers/` | végpontok, input feldolgozás, üzleti szabályok |
| `Auth/` | token kezelés, auth handler, jelszó hash/verify |
| `Context/` | `DbContext`, indexek, constraint-ek, seed adatok |
| `DataValidator/` + `Extensions/ValidatorExtension` | láncolható validációs DSL |
| `Models/` | domain entitások és enumok |
| `Migrations/` | adatbázis séma evolúció |

## Adatmodell áttekintés

A fő entitások egymásra épülve fedik le a működést:

- `User` – felhasználói profil, szerepkör, aktív állapot.
- `Token` – session token, lejárati idő (`RevokedAt`), user-kapcsolat.
- `Card` – felhasználóhoz tartozó belépőkártya, visszavonhatóság.
- `Ticket` – jegytípus, ár, típus, felhasználási korlát.
- `Payment` – fizetési rekord (határidő, fizetési dátum, bizonylatszám).
- `UserTicket` – user + ticket + payment kapcsolat, felhasználás és lejárat.
- `Training` + `TrainingUser` – edzések és résztvevők, jelenlét kezelése.
- `UsageLog` – kártyahasználat naplózása kapu szerint.

A `GymTracerDbContext` fontosabb séma-szabályai:

- egyedi index több kulcson (`Email`, `TokenString`, `Card.Code`, stb.),
- összetett egyediség (`TrainingUser`: `TrainingId + UserId`),
- `Ticket.IsActive` default és index,
- seed adatok JSON fájlokból (`ExampleData/*`).

## Hitelesítés és jogosultság

### Login / token kiadás

`AuthController.Login` sikeres hitelesítés után token rekordot hoz létre:

```csharp
tokenHandler.GenerateTokenData(out string tokenString, out DateTime createdAt, out DateTime revokedAt);

dbContext.Tokens.Add(new models.Token {
    UserId = dbUser.Id,
    CreatedAt = createdAt,
    RevokedAt = revokedAt,
    TokenString = tokenString
});
```

### Token ellenőrzés és session hosszabbítás

`AuthHandler` minden hitelesített kérésnél:

- beolvassa az `Authorization: Bearer ...` fejlécet,
- ellenőrzi a token létezését és lejáratát,
- meghosszabbítja a sessiont (`RevokedAt = now + expiration`),
- `session` response headerben visszaküldi az új `validTo` időpontot.

```csharp
if (sessionToken is null || sessionToken.RevokedAt <= tokenHandler.Now())
    return await Task.FromResult(AuthenticateResult.Fail("Authentication failed"));

sessionToken.RevokedAt = tokenHandler.Now().AddMinutes(Options.ExpirationInMinutes);
```

### Password kezelés

PBKDF2 alapú hash:

```csharp
byte[] passwordHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, algorithm, hashLength);
return $"$pbkdf2${algorithm.Name?.ToLower()}${iterations}${saltString}${hashString}";
```

> Fontos: a seed felhasználók jelszavai hash-elve vannak tárolva, plaintext forma nincs a repository-ban.

## DataValidator részletesen

A backend egyik legfontosabb erőssége a saját, láncolható validátor API, amely tisztán tartja a controller logikát.

### 1) Validátor létrehozása

```csharp
public static class Validator
{
    public static Validator<T> Create<T>(T validatonTarget)
    {
        return new Validator<T>(validatonTarget);
    }
}
```

A `Validator<T>` gyűjti a hibákat (`Errors`) és ad egy `IsValid` állapotot.

### 2) Szabályok láncolása (Training)

```csharp
trainingValidator.Validate(t => t.StartTime, "edzés kezdete")
    .NotDefault()
    .After(tokenHandler.Now().AddMinutes(-1))
    .Before(tokenHandler.Now().AddMonths(1));

trainingValidator.Validate(t => t.MaxParticipant, "résztvevő szám")
    .GreaterThan(0ul)
    .LessThan(100ul);
```

Ez nem csak formális validáció: üzleti szabályt is véd (pl. időablak, férőhely-limit).

### 3) Kollekció validáció (Ticket lista)

```csharp
ticketsValidator.Validate(t => t, "Ticket", "jegy")!
    .NotNull()!
    .ForEach(ticket =>
    {
        ticket.ThenValidate(ticket => ticket.Description, "leírás").NotNullOrEmpty();
        ticket.ThenValidate(ticket => ticket.Price, "ár").Min(0ul).Max(ulong.MaxValue);
        ticket.ThenValidate(ticket => ticket.Type, "típus").InEnum();
    });
```

### 4) Controller használat

```csharp
var validatorResult = ValidateTraining(training);
if (!validatorResult.IsValid)
    return BadRequest(new { validatorResult.Errors });
```

**Gyakorlati előnyök:**

- egységes hibaformátum,
- könnyebb karbantartás,
- jól olvasható üzleti szabályok,
- új szabályok gyors bővíthetősége.

## API végpontok – mit csinálnak

Alap prefix: `/api`.

### Auth (`/api/auth`)

| Metódus | Útvonal | Mit csinál |
|---|---|---|
| `POST` | `/auth/registration` | új customer regisztráció, alap ellenőrzésekkel |
| `POST` | `/auth/login` | felhasználó hitelesítése, token kiadás |
| `POST` | `/auth/logout` | aktuális token azonnali visszavonása |

### User (`/api/user`)

| Metódus | Útvonal | Mit csinál |
|---|---|---|
| `GET` | `/user/{id}/profile` | profiladatok + napi belépés infó |
| `PUT` | `/user/{id}/profile` | profil módosítás (email validációval) |
| `DELETE` | `/user/{id}` | user deaktiválás + aktív token/kártya visszavonás |
| `GET` | `/user/{id}/card` | aktív kártyák listázása |
| `POST` | `/user/{id}/card` | új kártya kibocsátása |
| `DELETE` | `/user/{id}/card/{card_id}` | kártya visszavonása |
| `GET` | `/user/{id}/training` | user edzéseinek listázása |
| `POST` | `/user/{id}/training/{training_id}/ticket/{ticket_id}` | jelentkezés edzésre tickettel |
| `DELETE` | `/user/{id}/training/{training_id}` | lejelentkezés |
| `GET` | `/user` | staff/admin keresés név/email/guid szerint |
| `PUT` | `/user/{id}/role` | szerepkör módosítás (admin) |

### Ticket (`/api/ticket`)

| Metódus | Útvonal | Mit csinál |
|---|---|---|
| `GET` | `/ticket` | elérhető jegytípusok listája |
| `GET` | `/ticket/user/{id}` | user érvényes jegyei |
| `GET` | `/ticket/user/{id}/unpaid` | user rendezetlen ticketjei |
| `POST` | `/ticket/{ticket_id}/user/{id}/{is_paid}` | ticket és payment létrehozás |
| `PATCH` | `/ticket/user/{id}/pay/{payment_id}` | nyitott fizetés rendezése |

### Training (`/api/training`)

| Metódus | Útvonal | Mit csinál |
|---|---|---|
| `GET` | `/training` | edzéslista |
| `GET` | `/training/user/{id}` | edzőhöz tartozó edzések |
| `GET` | `/training/{training_id}` | edzés részletei |
| `POST` | `/training/user/{id}` | új edzés létrehozása (validációval) |
| `PUT` | `/training/{training_id}` | edzés frissítése |
| `DELETE` | `/training/{training_id}` | edzés deaktiválása |
| `PATCH` | `/training/{training_id}/user/{id}/presence/{presence}` | jelenlét módosítás |

### Gate (`/api/gate`)

| Metódus | Útvonal | Mit csinál |
|---|---|---|
| `POST` | `/gate/{gate_id}/card/{card_code}/enter` | kapu belépés érvényes jegy ellenőrzéssel |
| `POST` | `/gate/{gate_id}/card/{card_code}/enter-main` | főkapu belépés napi és ticket logikával |

### Statistic (`/api/statistic`)

| Metódus | Útvonal | Mit csinál |
|---|---|---|
| `GET` | `/statistic/gym?daysBack=&weeksBack=` | látogatottság napi/heti bontásban |
| `GET` | `/statistic/tickets` | jegyértékesítési statisztika |
| `GET` | `/statistic/card` | kártyahasználati napló admin nézethez |

## Lokális futtatás

### Előfeltételek

- .NET SDK 8+
- futó MySQL/MariaDB szerver
- érvényes `gymtracerDb` kapcsolat

### Parancsok

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend

dotnet restore
dotnet build GymTracer.sln

export ConnectionStrings__gymtracerDb="server=localhost;port=3306;database=gymtracerdb;user=<felhasznalo>;password=<jelszo>;"
dotnet run --project /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend/GymTracer.csproj
```

Elérési címek:

- HTTP: `http://localhost:5065`
- HTTPS: `https://localhost:7261`
- Swagger: `https://localhost:7261/swagger`

## Konfiguráció és környezeti változók

| Kulcs | Jelentés |
|---|---|
| `ConnectionStrings:gymtracerDb` | adatbázis kapcsolat |
| `ConnectionStrings__gymtracerDb` | ugyanez env változóként |
| `AuthHandler:ExpirationInMinutes` | session időablak |
| `AuthHandler:TokenLength` | token karakterhossz |
| `PasswordHandler:AlgorithmName` | hash algoritmus neve |
| `PasswordHandler:Iterations` | PBKDF2 iteráció |
| `PasswordHandler:HashLength` | hash byte hossz |
| `PasswordHandler:SaltLength` | salt byte hossz |

## Fejlesztői workflow

### Build és teszt

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend

dotnet restore
dotnet build GymTracer.sln
dotnet test GymTracer.sln
```

### Migrációk

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend

dotnet ef migrations add <migration_nev>
dotnet ef database update
```

## Biztonsági és üzemeltetési megjegyzések

- A jelszavak PBKDF2 hash formában tárolódnak.
- Tokenes authentikáció miatt minden védett hívásnál kötelező az `Authorization` fejléc.
- Role alapú végpontvédelem több szinten érvényesül (`Authorize` + üzleti ellenőrzések).
- Production környezetben ajánlott minden érzékeny beállítást secretből adni.
- A token session hosszabbítás miatt a frontendnek érdemes kezelni a `session` response headert.

## Hibakeresés

| Probléma | Tipikus ok | Teendő |
|---|---|---|
| `401 Unauthorized` | hiányzó vagy lejárt token | új login, header ellenőrzés |
| `403 Forbidden` | szerepkör nem megfelelő | role és guard/endpoint páros ellenőrzése |
| `400 Bad Request` validációs hibával | DataValidator szabály megsértése | mezők és üzleti feltételek javítása |
| DB kapcsolódási hiba | hibás connection string vagy DB nem elérhető | `ConnectionStrings__gymtracerDb` + DB státusz ellenőrzése |
| CORS hiba frontendről | origin nincs policy-ben | `Program.cs` CORS lista ellenőrzése |

## Átadás előtti ellenőrzőlista

- [ ] `dotnet restore` rendben
- [ ] `dotnet build` rendben
- [ ] `dotnet test` rendben
- [ ] Auth flow manuálisan ellenőrizve (registration/login/logout)
- [ ] Role végpontok ellenőrizve (customer/trainer/staff/admin)
- [ ] Training és Gate kritikus folyamatok validálva
- [ ] Környezeti változók dokumentálva és ellenőrizve

## Készítő

- **Bende Huba**
