# GymTracer Frontend

A GymTracer frontend Angular alapú kliensalkalmazás, amely a backend API-ra épülő napi üzleti folyamatokat teszi kezelhetővé.  
A rendszer szerepköralapú nézeteket és guardolt navigációt használ, így a felhasználó mindig a jogosultságának megfelelő felületet látja.

## Rövid munkafolyamat-ellenőrzőlista

- [ ] Fejlesztői környezet előkészítése (Node, npm, backend elérés)
- [ ] Függőségek telepítése és build futtatása
- [ ] Auth/session flow ellenőrzése (login/logout/token)
- [ ] Route + guard működés tesztelése több szerepkörrel
- [ ] Staff mode és pretend mode viselkedés ellenőrzése
- [ ] Hiba- és fallback állapotok kipróbálása

## Tartalom

- [Projektcél és működési fókusz](#projektcél-és-működési-fókusz)
- [Technológiai háttér](#technológiai-háttér)
- [Frontend architektúra](#frontend-architektúra)
- [Routing és guard logika](#routing-és-guard-logika)
- [Session és authentikációs működés](#session-és-authentikációs-működés)
- [API integráció – hogyan kommunikál a backenddel](#api-integráció--hogyan-kommunikál-a-backenddel)
- [Felhasználói módok (normál / staff / pretend)](#felhasználói-módok-normál--staff--pretend)
- [UI és témakezelés](#ui-és-témakezelés)
- [Környezeti konfiguráció](#környezeti-konfiguráció)
- [Lokális futtatás](#lokális-futtatás)
- [Fejlesztői parancsok és tesztelés](#fejlesztői-parancsok-és-tesztelés)
- [Hibakeresés](#hibakeresés)
- [Átadás előtti ellenőrzőlista](#átadás-előtti-ellenőrzőlista)

## Projektcél és működési fókusz

A frontend célja, hogy a backend üzleti funkcióit gyors, role-aware kezelőfelületen adja át.

Fő működési területek:

- **customer nézet**: profil, jegyek, edzések, jelentkezés,
- **trainer nézet**: saját edzések kezelése, jelenlét adminisztráció,
- **staff nézet**: user keresés, ügyfél nevében műveletek,
- **admin nézet**: statisztikák, bevétel, kártyahasználati riportok.

A kliens logika központi eleme, hogy a jogosultságok nem csak UI szinten, hanem route/guard szinten is szűrve vannak.

## Technológiai háttér

| Terület | Megoldás | Szerepe |
|---|---|---|
| Framework | Angular 20 | komponens- és route-rendszer |
| Nyelv | TypeScript | típusbiztos klienslogika |
| UI | Angular Material + Tailwind | konzisztens komponensek és design tokenek |
| HTTP | `HttpClient` + `authInterceptor` | tokenes API kommunikáció |
| Állapot | service alapú + `localStorage` | session és módkezelés |
| Teszt | Karma/Jasmine + Cypress script | unit és e2e alapok |

## Frontend architektúra

A projekt logikája három fő rétegre osztható:

1. **Nézetek és komponensek** – oldalak és UI interakciók.
2. **Service réteg** – auth, API hívások, theme/mode állapot.
3. **Router + guard réteg** – hozzáférés- és üzemmód-szabályok.

Főbb fájlok:

| Fájl | Szerep |
|---|---|
| `src/app/app.routes.ts` | teljes route-fa és guard láncok |
| `src/app/services/auth.service.ts` | login/logout/session + acting user |
| `src/app/services/auth.interceptor.ts` | bearer token csatolás + session header feldolgozás |
| `src/app/services/theme.service.ts` | dark/staff/pretend mód vizuális kezelése |
| `src/environments/*` | API URL és időablak beállítások |
| `src/styles.css` | Tailwind theme tokenek és breakpoint |

## Routing és guard logika

### Route térkép

| Útvonal | Guard(ok) | Funkció |
|---|---|---|
| `/` | – | nyitóoldal |
| `/login` | `guestGuard` | bejelentkezés |
| `/registration` | `guestGuard` | regisztráció |
| `/trainings` | `authGuard`, `userModeGuard` | edzések böngészése |
| `/trainings/:id` | `authGuard`, `userModeGuard` | edzés részletek |
| `/my-trainings` | `authGuard`, `trainerGuard`, `userModeGuard` | edzői oldal |
| `/profile` | `authGuard`, `userModeGuard` | profil |
| `/tickets` | `authGuard`, `userModeGuard` | jegyek |
| `/users` | `authGuard`, `staffGuard`, `staffModeGuard` | staff user-választás |
| `/statistics` | `authGuard`, `staffGuard`, `staffModeGuard` | látogatottság |
| `/income` | `authGuard`, `adminGuard`, `staffModeGuard` | bevételi admin nézet |
| `/card-usage` | `authGuard`, `adminGuard`, `staffModeGuard` | kártyahasználati admin nézet |
| `**` | redirect | fallback a főoldalra |

### Guardok szerepe röviden

- `authGuard`: belépés nélkül tilt.
- `trainerGuard`: trainer/staff/admin szerepkört vár.
- `staffGuard`: staff/admin szerepkört vár.
- `adminGuard`: csak admin.
- `staffModeGuard`: staff módban, **pretended user nélkül** enged.
- `userModeGuard`: staff módban kötelező egy kiválasztott ügyfél.

`authGuard` részlet:

```ts
if (auth.actingUserRole === UserRole.not_found) {
  router.navigate(['/login'], {
    state: {
      message: 'A kért oldal megtekintéséhez be kell jelentkeznie.',
      type: 'error'
    }
  });
  return false;
}
```

## Session és authentikációs működés

Az `AuthService` egyszerre kezeli:

- ténylegesen belépett felhasználó (`user`),
- staff által kiválasztott felhasználó (`pretendedUser`),
- token és lejárati idő (`token`, `validUntil`).

Kulcs logika:

```ts
get actingUser(){
  return this.pretendedUser ?? this.user ?? null;
}

get actingUserRole(){
  return this.actingUser?.role ?? UserRole.not_found;
}
```

**Miért fontos?**  
A guardok és a nézetek mindig az aktuális működési kontextusból döntik el, mit lehet megnyitni.

Session mentés:

```ts
localStorage.setItem('auth_token', this.token);
localStorage.setItem('token_valid_to', this.validUntil.toISOString());
localStorage.setItem('current_user', JSON.stringify(this.user));
localStorage.removeItem('pretended_user');
```

## API integráció – hogyan kommunikál a backenddel

A kommunikáció központi mintája:

1. service meghívja a backend endpointot,
2. interceptor csatolja a Bearer tokent,
3. response headerből szükség esetén session frissítés történik,
4. komponens a service válaszából frissíti a UI-t.

Auth endpoint hívások:

```ts
Register(user: RegistrationCredentials){
  return this.http.post<RegistrationUserDto>(`${environment.apiUrl}/Auth/registration`, user);
}

Login(user: LoginCredentials){
  return this.http.post<UserLoginDto>(`${environment.apiUrl}/Auth/login`, user);
}

Logout(){
  return this.http.post<LogoutDto>(`${environment.apiUrl}/Auth/logout`, {});
}
```

Interceptor részlet:

```ts
if (auth.token) {
  clonedReq = req.clone({
    headers: req.headers.set('Authorization', `Bearer ${auth.token}`)
  });
}
```

Session fejléc feldolgozás:

```ts
const sessionHeader = event.headers.get('session');
if (sessionHeader) {
  const { validTo } = JSON.parse(sessionHeader);
  if (validTo) {
    auth.extendSession(validTo);
  }
}
```

## Felhasználói módok (normál / staff / pretend)

A rendszer három eltérő működési kontextust kezel:

### 1) Normál mód

- `pretendedUser === null`
- a felhasználó a saját adataival dolgozik
- `userModeGuard` normál működésben átenged

### 2) Staff mód

- `ThemeService.isStaffMode === true`
- staff/admin felhasználó ügyfélkezelési workflowt használ
- bizonyos oldalak csak staff módban érhetők el (`/users`, `/statistics`, stb.)

### 3) Pretend mód

- staff kiválasztott egy ügyfelet (`pretendedUser` nem null)
- user-oldali műveletek a kiválasztott ügyfél kontextusában futnak
- `staffModeGuard` ilyenkor tiltja a staff-only dashboard oldalakat

`staffModeGuard` részlet:

```ts
if (auth.pretendedUser) {
  router.navigate(['/profile']);
  return false;
}

if (!theme.isStaffMode) {
  router.navigate(['/']);
  return false;
}
```

## UI és témakezelés

A projekt Tailwind `@theme` tokeneket és osztályalapú módváltást használ.

Primer színek:

- alap: piros,
- staff mód: sárga (`theme-yellow`),
- pretend mód: narancs (`theme-orange`),
- dark mód: sötétített változatok.

Navigációs breakpoint:

- `--breakpoint-nav: 1180px`

`ThemeService` döntés:

```ts
if (this.isStaffMode){
  if(this.isPretendMode){
    return "theme-orange"
  }
  else{
    return "theme-yellow"
  }
}
```

## Környezeti konfiguráció

| Környezet | Fájl | API URL |
|---|---|---|
| Production | `src/environments/environment.ts` | `https://api.gymtracer.jcloud.jedlik.cloud/api` |
| Development | `src/environments/dev/environment.development.ts` | `http://localhost:5065/api` |

További releváns értékek:

| Kulcs | Jelentés | Alap |
|---|---|---|
| `pastTrainingDays` | múltbeli edzéslista időablak | `14` |
| `futureTrainingDays` | jövőbeli edzéslista időablak | `60` |

## Lokális futtatás

### Előfeltételek

- Node.js 20+
- npm 10+
- futó backend (`http://localhost:5065`)

### Indítás

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Frontend
CYPRESS_INSTALL_BINARY=0 npm install
npm run start
```

Fejlesztői URL: `http://localhost:4200`

## Fejlesztői parancsok és tesztelés

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Frontend

npm run build
npm run watch
npm run test
npm run cy:open
npm run cy:run
```

Megjegyzés:

- Sandbox/CI környezetben Cypress bináris letöltésnél célszerű a `CYPRESS_INSTALL_BINARY=0` opció.

## Hibakeresés

| Jelenség | Tipikus ok | Teendő |
|---|---|---|
| login után azonnali kilépés | token/session állapot sérült | `localStorage` kulcsok ellenőrzése és új login |
| tiltott oldalra dob vissza | guard logika aktiválódik | szerepkör + mode állapot ellenőrzése |
| minden API hívás `401` | hiányzó/lejárt Bearer token | auth flow és interceptor ellenőrzése |
| üres listák | rossz environment API URL | `environment.ts` és dev replacement ellenőrzése |
| `ng: not found` | függőségek hiányoznak | `npm install` futtatása |
| unit tesztben `_HttpClient` provider hiba | hiányzó test provider konfiguráció | érintett spec setup frissítése |

## Átadás előtti ellenőrzőlista

- [ ] `npm install` rendben
- [ ] `npm run build` rendben
- [ ] kritikus route-ok kézi ellenőrzése szerepkörönként
- [ ] auth + session megújítás működés validálva
- [ ] staff/pretend mód működés validálva
- [ ] `npm run test` eredmény áttekintve

## Készítő

- **Bende Huba**
