# GymTracer Frontend

A GymTracer frontend egy **Angular 20** alapú kliensalkalmazás, amely az edzőtermi folyamatokat teszi elérhetővé különböző szerepkörök számára.

Fő célok:
- gyors napi használat (profil, jegyek, edzések)
- szerepkör alapú felület (customer / trainer / staff / admin)
- staff „más nevében” működés támogatása
- statisztikai és admin oldalak kezelése

---

## Technológiai háttér

| Terület | Technológia |
|---|---|
| Keretrendszer | Angular 20 |
| Nyelv | TypeScript |
| UI | Angular Material + Tailwind CSS |
| Állapot | Angular service-ek, localStorage |
| Teszt | Karma/Jasmine (+ Cypress e2e script) |

---

## Projektstruktúra (röviden)

| Mappa/Fájl | Szerep |
|---|---|
| `src/app/app.routes.ts` | kliens oldali route-ok |
| `src/app/guards/` | jogosultsági és mód guardok |
| `src/app/services/` | API hívások és session kezelés |
| `src/environments/` | környezeti konfigurációk |
| `angular.json` | build/serve konfiguráció |
| `package.json` | script-ek és függőségek |

---

## Előfeltételek

- Node.js 20+
- npm 10+
- futó GymTracer backend (helyben vagy éles URL-en)

---

## Telepítés és futtatás

```bash
cd /home/runner/work/gymtracer_private/gymtracer_private/GymTracer_Frontend
npm install
npm run start
```

Alapértelmezett fejlesztői URL:
- `http://localhost:4200`

---

## Hasznos parancsok

| Parancs | Jelentés |
|---|---|
| `npm run start` | fejlesztői szerver indítása |
| `npm run build` | produkciós build |
| `npm run watch` | folyamatos build fejlesztői módban |
| `npm run test` | unit tesztek |
| `npm run cy:open` | Cypress UI |
| `npm run cy:run` | Cypress headless futtatás |

---

## Környezeti konfiguráció

A frontend API alap URL-t környezetből olvas:

| Fájl | `apiUrl` |
|---|---|
| `src/environments/environment.ts` | `https://api.gymtracer.jcloud.jedlik.cloud/api` |
| `src/environments/dev/environment.development.ts` | `http://localhost:5065/api` |

Fejlesztői buildnél az Angular file replacement a `dev/environment.development.ts` fájlt használja.

---

## Bejelentkezés és session

Az `AuthService` kezeli:
- bejelentkezett felhasználó (`user`)
- staff általi megszemélyesített felhasználó (`pretendedUser`)
- aktuálisan használt identitás (`actingUser`)
- token és lejárat localStorage-ban

Tárolt kulcsok:
- `auth_token`
- `token_valid_to`
- `current_user`
- `pretended_user`

---

## Frontend route-ok

| Útvonal | Komponens | Guard(ok) | Leírás |
|---|---|---|---|
| `/` | MainPage | – | Kezdőoldal |
| `/login` | Login | `guestGuard` | Bejelentkezés |
| `/registration` | Registration | `guestGuard` | Regisztráció |
| `/trainings` | Trainings | `authGuard`, `userModeGuard` | Edzések listája |
| `/trainings/:id` | TrainingDetails | `authGuard`, `userModeGuard` | Edzés részletek |
| `/my-trainings` | MyTrainingsPage | `authGuard`, `trainerGuard`, `userModeGuard` | Edző saját edzései |
| `/profile` | ProfilePage | `authGuard`, `userModeGuard` | Profil |
| `/tickets` | TicketsPage | `authGuard`, `userModeGuard` | Jegyek/bérletek |
| `/users` | UserSearch | `authGuard`, `staffGuard`, `staffModeGuard` | Felhasználóválasztó staff módhoz |
| `/statistics` | StatisticsPage | `authGuard`, `staffGuard`, `staffModeGuard` | Látogatottsági statisztikák |
| `/income` | IncomePage | `authGuard`, `adminGuard`, `staffModeGuard` | Bevételi statisztikák |
| `/card-usage` | CardusagePage | `authGuard`, `adminGuard`, `staffModeGuard` | Kártyahasználati napló |

`**` (ismeretlen útvonal) → átirányítás a kezdőlapra.

---

## Jogosultsági logika (röviden)

| Guard | Funkció |
|---|---|
| `authGuard` | csak bejelentkezett felhasználó |
| `trainerGuard` | trainer/staff/admin hozzáférés |
| `staffGuard` | staff/admin hozzáférés |
| `adminGuard` | csak admin |
| `staffModeGuard` | staff mód aktív legyen, megszemélyesítés nélkül |
| `userModeGuard` | staff módban csak kiválasztott felhasználóval enged tovább |

---

## Napi használati példa (fejlesztői)

1. Backend indítása (`http://localhost:5065`)
2. Frontend indítása (`npm run start`)
3. Bejelentkezés megfelelő szerepkörrel
4. Funkciók tesztelése route-onként
5. Változtatás után `npm run build` és `npm run test`

---

## Gyakori hibák

- **401/403 a frontendben**: token hiányzik/lejárt, vagy szerepkör nem megfelelő.
- **Nem töltődnek az adatok**: `apiUrl` nincs a futó backendre állítva.
- **Navigáció visszadob**: valamelyik guard blokkolja az útvonalat (staff mód/user mód állapot).
