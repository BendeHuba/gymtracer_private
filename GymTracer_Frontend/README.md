# GymTracer Frontend

> **Vizsgaremek rövid bemutatása**  
> A GymTracer frontend az edzőtermi üzleti folyamatok felhasználóbarát kezelőfelülete Angular alapon.  
> A kliensoldal szerepkör szerint eltérő nézeteket ad (ügyfél/edző/staff/admin), és közvetlenül a GymTracer backend API-ra épül.

---

## Tartalomjegyzék

1. [Projektáttekintés](#projektáttekintés)
2. [Technológiák](#technológiák)
3. [Funkcionális képességek](#funkcionális-képességek)
4. [Frontend architektúra](#frontend-architektúra)
5. [Mappaszerkezet](#mappaszerkezet)
6. [Telepítési és futtatási leírás](#telepítési-és-futtatási-leírás)
7. [Build/Test parancsok](#buildtest-parancsok)
8. [Környezeti konfigurációk](#környezeti-konfigurációk)
9. [Route és guard dokumentáció](#route-és-guard-dokumentáció)
10. [API integrációs áttekintés](#api-integrációs-áttekintés)
11. [Kódrészletek (valós projektkód alapján)](#kódrészletek-valós-projektkód-alapján)
12. [Demó felhasználók és jelszavak](#demó-felhasználók-és-jelszavak)
13. [UI és témakezelés](#ui-és-témakezelés)
14. [Hibakeresés](#hibakeresés)
15. [Készítők](#készítők)

---

## Projektáttekintés

A frontend célja, hogy a backend üzleti funkcióit egyértelmű és gyors kezelőfelületen adja át.

Fő use case-ek:
- belépés/regisztráció,
- profilkezelés,
- jegyek áttekintése és kezelése,
- edzések böngészése és foglalása,
- edzői jelenlétkezelés,
- staff/admin statisztikák és felhasználó-váltás.

A rendszer külön figyelmet fordít a **munkatársi módra**, ahol a staff felhasználó másik ügyfél nevében is dolgozhat.

---

## Technológiák

| Terület | Technológia | Megjegyzés |
|---|---|---|
| Framework | Angular 20 | standalone komponensek |
| Nyelv | TypeScript | szigorú típusosság |
| UI | Angular Material + Tailwind | dark mode és custom theme |
| HTTP | `HttpClient` + interceptor | token automatikus csatolás |
| Állapot | service + `localStorage` | auth/session állapot |
| Teszt | Karma/Jasmine + Cypress | unit + e2e lehetőség |

---

## Funkcionális képességek

### Felhasználói oldal (customer)
- profil megtekintése/módosítása,
- saját jegyek,
- edzések listázása és jelentkezés,
- saját tréning előzmények.

### Edzői oldal (trainer)
- saját edzések kezelése,
- résztvevők jelenlétének állítása.

### Staff/admin oldal
- felhasználó keresés,
- staff mód kezelése,
- statisztikák,
- admin extra nézetek (bevétel, kártyahasználat).

---

## Frontend architektúra

### Fő építőelemek

| Elem | Leírás |
|---|---|
| `app.routes.ts` | route-fa és guard láncok |
| `AuthService` | login/logout/session/actingUser |
| `auth.interceptor.ts` | bearer token automatikus hozzáadása |
| `ThemeService` | sötét mód + staff/pretend témák |
| oldalkomponensek | profil, jegyek, statisztika, edzések |

### Kommunikációs minta

1. komponens esemény,
2. service API-hívás,
3. backend válasz,
4. komponensállapot frissül,
5. router/guard dönt a hozzáférésről.

---

## Mappaszerkezet

| Útvonal | Tartalom |
|---|---|
| `src/app/app.routes.ts` | teljes route definíció |
| `src/app/guards/` | auth/role/mode guardok |
| `src/app/services/` | auth, trainer, scanner tracker, theme |
| `src/environments/` | API URL és időablak konfigurációk |
| `src/styles.css` | Tailwind témaszínek és breakpoint |
| `angular.json` | build/serve konfiguráció |

---

## Telepítési és futtatási leírás

### Előfeltételek

- Node.js 20+
- npm 10+
- futó GymTracer backend

### 1) Projekt mappa

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Frontend
```

### 2) Függőségek telepítése

```bash
npm install
```

> CI/sandbox környezetben a Cypress bináris letöltés időnként problémás lehet. Ilyenkor:

```bash
CYPRESS_INSTALL_BINARY=0 npm install
```

### 3) Fejlesztői indítás

```bash
npm run start
```

Fejlesztői URL:

- `http://localhost:4200`

---

## Build/Test parancsok

| Parancs | Jelentés |
|---|---|
| `npm run start` | dev szerver |
| `npm run build` | production build |
| `npm run watch` | development watch build |
| `npm run test` | Karma/Jasmine unit teszt |
| `npm run cy:open` | Cypress interaktív |
| `npm run cy:run` | Cypress headless |

### Ellenőrzött build példa

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Frontend
CYPRESS_INSTALL_BINARY=0 npm install
npm run build
```

---

## Környezeti konfigurációk

A projekt file replacementet használ.

| Környezet | Fájl | `apiUrl` |
|---|---|---|
| Production | `src/environments/environment.ts` | `https://api.gymtracer.jcloud.jedlik.cloud/api` |
| Development | `src/environments/dev/environment.development.ts` | `http://localhost:5065/api` |

További beállítások:

| Kulcs | Jelentés | Alapérték |
|---|---|---|
| `pastTrainingDays` | múltbeli edzéslistázási ablak | `14` |
| `futureTrainingDays` | jövőbeli edzéslistázási ablak | `60` |

---

## Route és guard dokumentáció

### Route tábla

| Útvonal | Komponens | Guard(ok) | Cél |
|---|---|---|---|
| `/` | MainPage | – | nyitóoldal |
| `/login` | Login | `guestGuard` | bejelentkezés |
| `/registration` | Registration | `guestGuard` | regisztráció |
| `/trainings` | Trainings | `authGuard`, `userModeGuard` | edzések lista |
| `/trainings/:id` | TrainingDetails | `authGuard`, `userModeGuard` | edzés részletek |
| `/my-trainings` | MyTrainingsPage | `authGuard`, `trainerGuard`, `userModeGuard` | edzői nézet |
| `/profile` | ProfilePage | `authGuard`, `userModeGuard` | profil |
| `/tickets` | TicketsPage | `authGuard`, `userModeGuard` | jegyek |
| `/users` | UserSearch | `authGuard`, `staffGuard`, `staffModeGuard` | staff user választó |
| `/statistics` | StatisticsPage | `authGuard`, `staffGuard`, `staffModeGuard` | látogatottság |
| `/income` | IncomePage | `authGuard`, `adminGuard`, `staffModeGuard` | bevétel |
| `/card-usage` | CardusagePage | `authGuard`, `adminGuard`, `staffModeGuard` | kártyalog |
| `**` | redirect | – | fallback főoldal |

### Guard logika

| Guard | Mikor enged |
|---|---|
| `guestGuard` | csak kijelentkezett látogató |
| `authGuard` | legalább egy bejelentkezett user (`actingUser`) |
| `trainerGuard` | trainer/staff/admin |
| `staffGuard` | staff/admin |
| `adminGuard` | admin |
| `staffModeGuard` | staff mód aktív és nincs `pretendedUser` |
| `userModeGuard` | staff módban legyen kiválasztott felhasználó |

---

## API integrációs áttekintés

### Fő service-ek

| Service | Feladat |
|---|---|
| `AuthService` | login, logout, regisztráció, session tárolás |
| `TrainerService` | edzői funkciók (edzések, jelenlét) |
| `ScannerTrackerService` | kártyaolvasás/kapuhoz kötődő kliens logika |
| `ThemeService` | dark/staff/pretend vizuális módok |

### Tipikus endpoint-hívások frontendből

| Művelet | Backend útvonal |
|---|---|
| Bejelentkezés | `POST /Auth/login` |
| Regisztráció | `POST /Auth/registration` |
| Kijelentkezés | `POST /Auth/logout` |
| Profil lekérés | `GET /User/{id}/profile` |
| Edzéslista | `GET /Training` |
| Jegyek lekérése | `GET /Ticket/user/{id}` |

---

## Kódrészletek (valós projektkód alapján)

### 1) Auth API URL használat (`AuthService`)

```ts
Register(user: RegistrationCredentials){
  return this.http.post<RegistrationUserDto>(`${environment.apiUrl}/Auth/registration`, user);
}

Login(user: LoginCredentials){
  return this.http.post<UserLoginDto>(`${environment.apiUrl}/Auth/login`, user);
}
```

### 2) Session mentés localStorage-ba

```ts
localStorage.setItem('auth_token', this.token);
localStorage.setItem('token_valid_to', this.validUntil.toISOString());
localStorage.setItem('current_user', JSON.stringify(this.user));
localStorage.removeItem('pretended_user');
```

### 3) Aktív felhasználó kiválasztás (normál vs staff-pretend)

```ts
get actingUser(){
  return this.pretendedUser ?? this.user ?? null;
}

get actingUserRole(){
  return this.actingUser?.role ?? UserRole.not_found;
}
```

### 4) Route + guard lánc példa

```ts
{path: 'my-trainings', component: MyTrainingsPage, canActivate: [authGuard, trainerGuard, userModeGuard]}
```

### 5) Környezeti file replacement (`angular.json`)

```json
"fileReplacements": [
  {
    "replace": "src/environments/environment.ts",
    "with": "src/environments/dev/environment.development.ts"
  }
]
```

### 6) Theme tokenok (`styles.css`)

```css
@theme {
  --color-primary-400: #f87171;
  --color-primary-500: #ef4444;
  --color-primary-600: #dc2626;
  --breakpoint-nav: 1180px;
}
```

---

## Demó felhasználók és jelszavak

A frontend a backend seed felhasználóival tesztelhető.

> **Megjegyzés:** a jelszavak a backend seed fájlban hash-elve tároltak, ezért a forráskódból nem olvasható ki biztosan plaintext jelszó.  
> Gyors demóhoz javasolt saját felhasználó regisztrációja a `/registration` oldalon.

### Seed felhasználó minták

| Név | E-mail | Szerepkör | Jelszó állapot |
|---|---|---|---|
| Adminisztrátor Anna | `admin@gym.hu` | admin | hash-elve |
| Recepciós Ricsi | `ricsi.staff@gym.hu` | staff | hash-elve |
| Edző Elemér | `elemer.edzo@gym.hu` | trainer | hash-elve |
| Kovács János | `janos.kovacs@email.com` | customer | hash-elve |

### Gyors demó forgatókönyv

1. backend indítása lokálban (`http://localhost:5065`),
2. frontend futtatása (`npm run start`),
3. új user regisztráció,
4. login,
5. `/trainings`, `/tickets`, `/profile` oldalak bejárása.

---

## UI és témakezelés

A frontend Tailwind theme tokeneket használ.

| Mód | Fő primer szín |
|---|---|
| alap | piros |
| staff mode | sárga |
| pretend mode | narancs |
| dark mód | sötétített variáns |

`styles.css` szerint a navigációs breakpoint: `--breakpoint-nav: 1180px`.

---

## Hibakeresés

| Jelenség | Lehetséges ok | Javítás |
|---|---|---|
| Üres lista oldalak | rossz `apiUrl` | ellenőrizd environment fájlokat |
| Folyamatos visszairányítás | guard tilt | szerepkör/mode állapot ellenőrzés |
| 401 minden kérésre | token hiány/lejárat | login újra + localStorage vizsgálat |
| Build hiba `ng: not found` | nincs telepített függőség | `npm install` futtatása |
| Cypress install gond | hálózati korlát | `CYPRESS_INSTALL_BINARY=0 npm install` |

---

## Készítők

| Név | Szerep |
|---|---|
| **Bende Huba** | projekt készítő, frontend/backend fejlesztés |

---

## Rövid összegzés

A GymTracer frontend egy szerepkör-alapú, Angular-alapú kliensalkalmazás, amely:

- stabil auth/session kezelést ad,
- route/guard szinten jól szegmentálja a hozzáférést,
- backend API-ra építve kezeli az edzőtermi üzleti folyamatokat,
- fejlesztői környezetben gyorsan futtatható és tesztelhető.

---

## Oldalankénti használati útmutató

### `/login`

- cél: bejelentkezés,
- bemenet: e-mail + jelszó,
- siker esetén token mentés,
- hiba esetén felhasználóbarát visszajelzés.

### `/registration`

- cél: új ügyfélfiók létrehozás,
- ellenőrzés: kötelező mezők,
- siker esetén továbblépés bejelentkezésre.

### `/trainings`

- edzéslista dátumablakkal,
- user szerepkörhöz igazodó műveletek,
- részletek megnyitása `/:id` útvonalon.

### `/tickets`

- felhasználó jegyeinek listája,
- fizetési állapotok megjelenítése,
- aktív jegy kiválasztása edzésjelentkezéshez.

### `/profile`

- személyes adatok kezelése,
- role megjelenítés,
- sessionhoz kötött felhasználói kontextus.

### `/my-trainings`

- edzői munkafelület,
- saját edzések listája,
- jelenléti státusz állítása.

### `/users` + staff mód

- staff felhasználó cél-ügyfelet választ,
- kiválasztás után user-mode műveletek,
- staff mód és pretend mód vizuálisan elkülönítve.

### `/statistics`, `/income`, `/card-usage`

- üzleti riport oldalak,
- role-lánc: auth + staff/admin + staff mode,
- admin oldalak csak admin jogosultsággal.

---

## Állapotkezelési részletek (AuthService)

| Állapot | Jelentés |
|---|---|
| `user` | ténylegesen bejelentkezett account |
| `pretendedUser` | staff által kiválasztott ügyfél |
| `actingUser` | a rendszer által éppen használt user |
| `actingUserRole` | guard döntésekhez használt szerepkör |

### LocalStorage kulcsok

| Kulcs | Tartalom |
|---|---|
| `auth_token` | bearer token |
| `token_valid_to` | token lejárat időpont |
| `current_user` | bejelentkezett user JSON |
| `pretended_user` | staff célfelhasználó JSON |

---

## Interceptor működés röviden

A `auth.interceptor.ts` feladata:

- token jelenlét ellenőrzése,
- Authorization header hozzáadása minden API kéréshez,
- backend auth hiba esetén egységes kliensoldali reakció lehetősége.

Ez csökkenti a komponensekben az ismételt HTTP boilerplate kód mennyiségét.

---

## Gyakori fejlesztői munkafolyamat

1. backend futtatás lokálban,
2. frontend `npm run start`,
3. auth flow ellenőrzés,
4. route guard ellenőrzés több szerepkörrel,
5. build + unit teszt,
6. (opcionális) cypress smoke.

### Gyors ellenőrző parancsblokk

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Frontend
CYPRESS_INSTALL_BINARY=0 npm install
npm run build
npm run test
```

---

## API hibák felhasználói kezelése (javaslat)

| Backend válasz | Frontend reakció |
|---|---|
| `401` | kijelentkeztetés + login oldal |
| `403` | átirányítás főoldalra / jogosultsági üzenet |
| `404` | „Nem található” üzenet |
| `409` | üzleti konfliktus üzenet (pl. duplikáció) |
| `500` | általános hiba toast/panel |

---

## Teljesítmény és UX megjegyzések

- route guardokkal korán kiszűrhető a jogosulatlan navigáció,
- lazy komponensstratégiával tovább optimalizálható a kezdeti csomagméret,
- API hívásoknál egységes loading/hiba komponens javítja a UX-et,
- statisztikai oldalaknál cache-elés csökkentheti a backend terhelést.

---

## Vizsgaremek értékelési fókusz

A frontend rész jól bemutatja:

1. szerepkör-alapú kliens oldali navigációt,
2. backend-integrált auth/session kezelést,
3. valós üzleti felület felépítését,
4. guard-centrikus hozzáférésvédelmet,
5. modern Angular + Tailwind alapú UI megközelítést.

