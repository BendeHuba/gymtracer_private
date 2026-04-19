# GymTracer Frontend

Angular alapú kliensalkalmazás, amely a GymTracer backend API-ra épül.  
A felület szerepkör szerint eltérő munkafolyamatokat támogat (customer, trainer, staff, admin), guard-alapú hozzáférésvédelemmel.

## Gyors munkafolyamat-ellenőrzőlista

- [ ] Függőségek telepítése és környezet ellenőrzése
- [ ] Frontend build futtatása
- [ ] Auth flow kipróbálása (login/logout/session)
- [ ] Route + guard viselkedés ellenőrzése több szerepkörrel
- [ ] Unit tesztek futtatása és eredmények áttekintése

## Tartalom

- [Projektáttekintés](#projektáttekintés)
- [Technológiai háttér](#technológiai-háttér)
- [Gyorsindítás](#gyorsindítás)
- [Környezeti beállítások](#környezeti-beállítások)
- [Routing és jogosultság](#routing-és-jogosultság)
- [API integráció példák](#api-integráció-példák)
- [UI és témakezelés](#ui-és-témakezelés)
- [Fejlesztői parancsok](#fejlesztői-parancsok)
- [Hibakeresés](#hibakeresés)

## Projektáttekintés

A frontend célja, hogy a backend funkciói gyorsan és kiszámíthatóan használhatók legyenek a napi működésben:

- ügyféloldali profil/jegy/edzés műveletek,
- edzői jelenlétkezelés,
- staff/admin oldali keresés és riportok,
- staff mód + "pretend user" workflow.

## Technológiai háttér

| Terület | Megoldás |
|---|---|
| Framework | Angular 20 |
| Nyelv | TypeScript |
| UI | Angular Material + Tailwind |
| HTTP | `HttpClient` + interceptor |
| Állapot | service alapú, `localStorage`-gel |
| Teszt | Karma/Jasmine + Cypress script támogatás |

## Gyorsindítás

### Előfeltételek

- Node.js 20+
- npm 10+
- futó GymTracer backend

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Frontend
CYPRESS_INSTALL_BINARY=0 npm install
npm run start
```

Frontend URL: `http://localhost:4200`

## Környezeti beállítások

A projekt file replacementet használ fejlesztői és éles API URL között.

| Környezet | Fájl | `apiUrl` |
|---|---|---|
| Production | `src/environments/environment.ts` | `https://api.gymtracer.jcloud.jedlik.cloud/api` |
| Development | `src/environments/dev/environment.development.ts` | `http://localhost:5065/api` |

Részlet:

```ts
export const environment : EnvironmentModel = {
    apiUrl: "http://localhost:5065/api",
    pastTrainingDays: 14,
    futureTrainingDays: 60
}
```

## Routing és jogosultság

Kulcs route-ok és guard láncok:

| Útvonal | Guard(ok) | Cél |
|---|---|---|
| `/trainings` | `authGuard`, `userModeGuard` | edzéslista |
| `/my-trainings` | `authGuard`, `trainerGuard`, `userModeGuard` | edzői felület |
| `/users` | `authGuard`, `staffGuard`, `staffModeGuard` | staff user választó |
| `/statistics` | `authGuard`, `staffGuard`, `staffModeGuard` | staff/admin statisztika |
| `/income` | `authGuard`, `adminGuard`, `staffModeGuard` | admin bevétel |
| `/card-usage` | `authGuard`, `adminGuard`, `staffModeGuard` | admin kártyahasználat |

A route-definíció mintája:

```ts
{path: 'my-trainings', component: MyTrainingsPage, canActivate: [authGuard, trainerGuard, userModeGuard]}
```

## API integráció példák

### 1) Auth hívások

```ts
Register(user: RegistrationCredentials){
  return this.http.post<RegistrationUserDto>(`${environment.apiUrl}/Auth/registration`, user);
}

Login(user: LoginCredentials){
  return this.http.post<UserLoginDto>(`${environment.apiUrl}/Auth/login`, user);
}
```

**Röviden:** az auth service központilag kezeli a belépési hívásokat, így a komponensekben nem kell ismétlődő HTTP boilerplate.

### 2) Aktív felhasználói kontextus (staff mód támogatás)

```ts
get actingUser(){
  return this.pretendedUser ?? this.user ?? null;
}

get actingUserRole(){
  return this.actingUser?.role ?? UserRole.not_found;
}
```

**Röviden:** a guardok mindig az aktuálisan használt felhasználóval dolgoznak (normál vagy pretend módban).

### 3) Session mentés

```ts
localStorage.setItem('auth_token', this.token);
localStorage.setItem('token_valid_to', this.validUntil.toISOString());
localStorage.setItem('current_user', JSON.stringify(this.user));
localStorage.removeItem('pretended_user');
```

## UI és témakezelés

A `styles.css` alapján a projekt Tailwind theme tokeneket használ.  
Fő primer színek:

- alap mód: piros,
- staff mód: sárga,
- pretend mód: narancs.

Navigációs breakpoint: `--breakpoint-nav: 1180px`.

## Fejlesztői parancsok

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Frontend

npm run build
npm run test
npm run cy:run
```

## Hibakeresés

| Jelenség | Tipikus ok | Teendő |
|---|---|---|
| Üres listaoldalak | hibás `apiUrl` | environment fájlok ellenőrzése |
| Folyamatos redirect | guard tiltás | auth/mode állapot ellenőrzése |
| `401` minden kérésre | token hiány/lejárat | login újra, localStorage ellenőrzés |
| `ng: not found` | hiányzó függőség | `npm install` futtatása |
| Cypress telepítési gond | sandbox hálózati limit | `CYPRESS_INSTALL_BINARY=0 npm install` |

## Készítő

- **Bende Huba**
