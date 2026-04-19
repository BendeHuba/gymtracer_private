# GymTracer Backend

A GymTracer backend egy **ASP.NET Core 8 + Entity Framework Core + MySQL** alapú REST API, amely egy edzőtermi rendszer üzleti logikáját kezeli.

Főbb funkciók:
- felhasználókezelés és jogosultságok (customer, trainer, staff, admin)
- hitelesítés tokennel
- bérletek/jegyek kezelése és fizetés
- edzések létrehozása, módosítása, jelentkezés
- kapu beléptetés és használati naplózás
- statisztikai végpontok

---

## Technológiai háttér

| Terület | Technológia |
|---|---|
| Platform | .NET 8 (ASP.NET Core Web API) |
| ORM | Entity Framework Core |
| Adatbázis | MySQL |
| Auth | Egyedi token alapú hitelesítés |
| API dokumentáció | Swagger (Development módban) |

---

## Projektstruktúra (röviden)

| Mappa/Fájl | Szerep |
|---|---|
| `Controllers/` | REST végpontok |
| `Context/` | EF DbContext |
| `Models/` | Entitások és enumok |
| `Auth/` | Jelszó- és tokenkezelés |
| `Migrations/` | Adatbázis migrációk |
| `GymTracer.sln` | Solution (`GymTracer` + `Gymtracer.Tests`) |
| `Program.cs` | DI, CORS, auth, middleware pipeline |

---

## Előfeltételek

- .NET SDK 8.0+
- MySQL adatbázis
- működő connection string a `ConnectionStrings__gymtracerDb` beállításhoz

---

## Telepítés és indítás

### 1) Forráskód
```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend
```

### 2) Függőségek és build
```bash
dotnet restore
dotnet build GymTracer.sln
```

### 3) Konfiguráció
A backend a connection stringet innen olvassa:
- `ConnectionStrings:gymtracerDb`
- környezeti változóként: `ConnectionStrings__gymtracerDb`

Példa (Linux/macOS):
```bash
export ConnectionStrings__gymtracerDb="server=...;port=3306;database=...;user=...;password=..."
```

### 4) Indítás
```bash
dotnet run --project /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend/GymTracer.csproj
```

Fejlesztői alap URL (`launchSettings.json` alapján):
- `http://localhost:5065`
- `https://localhost:7261`

Swagger (Development):
- `https://localhost:7261/swagger`

---

## Tesztelés

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Backend
dotnet test GymTracer.sln
```

---

## Hitelesítés és jogosultság

- A legtöbb végpont tokenes hitelesítést vár.
- Bejelentkezés után kapott tokent `Authorization: Bearer <token>` fejlécben kell küldeni.
- Szerepkörök:
  - `customer`
  - `trainer`
  - `staff`
  - `admin`

---

## API végpontok

Alap útvonal: `/api`

### Auth (`/api/auth`)

| Metódus | Útvonal | Leírás | Jogosultság |
|---|---|---|---|
| POST | `/auth/registration` | Regisztráció | publikus |
| POST | `/auth/login` | Bejelentkezés, token kiadás | publikus |
| POST | `/auth/logout` | Kijelentkezés | bejelentkezett |

### User (`/api/user`)

| Metódus | Útvonal | Leírás | Jogosultság |
|---|---|---|---|
| GET | `/user/{id}/profile` | Profil lekérdezés | customer/trainer/staff/admin |
| PUT | `/user/{id}/profile` | Profil módosítás | customer/trainer/staff/admin |
| DELETE | `/user/{id}` | Felhasználó deaktiválás | customer/trainer/staff/admin |
| GET | `/user/{id}/card` | Kártyák lekérdezése | customer/trainer/staff/admin |
| POST | `/user/{id}/card` | Új kártya létrehozása | customer/trainer/staff/admin |
| DELETE | `/user/{id}/card/{card_id}` | Kártya visszavonása | customer/trainer/staff/admin |
| GET | `/user/{id}/training?arePreviousNeeded={bool}` | Felhasználó edzései | customer/trainer/staff/admin |
| POST | `/user/{id}/training/{training_id}/ticket/{ticket_id}` | Jelentkezés edzésre | customer/trainer/staff/admin |
| DELETE | `/user/{id}/training/{training_id}` | Lejelentkezés edzésről | customer/trainer/staff/admin |
| GET | `/user?name=&email=&guid=` | Felhasználó keresés | staff/admin |
| PUT | `/user/{id}/role` | Szerepkör módosítás | admin |

### Ticket (`/api/ticket`)

| Metódus | Útvonal | Leírás | Jogosultság |
|---|---|---|---|
| GET | `/ticket` | Jegytípusok listázása | publikus |
| GET | `/ticket/user/{id}` | Felhasználó aktív jegyei | customer/trainer/staff/admin |
| GET | `/ticket/user/{id}/unpaid` | Kifizetetlen jegyek | customer/trainer/staff/admin |
| POST | `/ticket/{ticket_id}/user/{id}/{is_paid}` | Jegyvásárlás (+ fizetés létrehozás) | customer/trainer/staff/admin |
| PATCH | `/ticket/user/{id}/pay/{payment_id}` | Függő fizetés rendezése | customer/trainer/staff/admin |

### Training (`/api/training`)

| Metódus | Útvonal | Leírás | Jogosultság |
|---|---|---|---|
| GET | `/training` | Edzések listázása/szűrése | publikus |
| GET | `/training/{training_id}` | Egy edzés lekérdezése | publikus |
| GET | `/training/user/{id}` | Edző saját edzései | trainer/staff/admin |
| POST | `/training/user/{id}` | Edzés létrehozása | trainer/staff/admin |
| PUT | `/training/{training_id}` | Edzés módosítása | trainer/staff/admin |
| DELETE | `/training/{training_id}` | Edzés törlése/deaktiválása | trainer/staff/admin |
| PATCH | `/training/{training_id}/user/{id}/presence/{presence}` | Jelenlét állítása | trainer/staff/admin |

### Statistic (`/api/statistic`)

| Metódus | Útvonal | Leírás | Jogosultság |
|---|---|---|---|
| GET | `/statistic/gym?daysBack=&weeksBack=` | Napi/heti látogatottsági statisztika | staff/admin |
| GET | `/statistic/tickets` | Jegyeladási statisztika | admin |
| GET | `/statistic/card` | Kártyahasználati napló | admin |

### Gate (`/api/gate`)

| Metódus | Útvonal | Leírás | Jogosultság |
|---|---|---|---|
| POST | `/gate/{gate_id}/card/{card_code}/enter?force={bool}` | Beléptetés kapun | staff/admin |
| POST | `/gate/{gate_id}/card/{card_code}/enter-main?force={bool}` | Főkapu beléptetés, használatkezeléssel | staff/admin |

---

## Tipikus fejlesztői folyamat

1. Környezeti változók beállítása (`ConnectionStrings__gymtracerDb`)
2. `dotnet restore`
3. `dotnet build`
4. `dotnet run`
5. Swaggerben végpontok kipróbálása
6. Módosítás után `dotnet test`

---

## Hibakeresési tippek

- **401/403 válasz**: hiányzó vagy lejárt token, illetve nem megfelelő szerepkör.
- **Adatbázis hiba indításkor**: ellenőrizd a connection stringet.
- **CORS probléma**: frontend origin legyen engedélyezve (`Program.cs` CORS policy).
