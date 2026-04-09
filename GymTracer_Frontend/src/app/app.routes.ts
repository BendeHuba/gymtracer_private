import { Routes } from '@angular/router';
import { Mainlayout } from './layout/mainlayout/mainlayout';
import { MainPage } from './mainpage/main-page/main-page';
import { Login } from './login/login/login';
import { Registration } from './registration/registration/registration';
import { guestGuard } from './guards/guest-guard';
import { Trainings } from './trainings/trainings/trainings';
import { authGuard } from './guards/auth-guard';
import { TrainingDetails } from './trainingdetails/training-details/training-details';
import { ProfilePage } from './profile/profile';
import { CardsPage } from './cards/cards';
import { TicketsPage } from './tickets/tickets';
import { UsersPage } from './users/users';
import { StatisticsPage } from './statistics/statistics';
import { IncomePage } from './income/income';
import { CardUsagePage } from './card-usage/card-usage';
import { MyTrainingsPage } from './my-trainings/my-trainings';
import { staffGuard } from './guards/staff-guard';
import { adminGuard } from './guards/admin-guard';
import { trainerGuard } from './guards/trainer-guard';

export const routes: Routes = [
    {
        path: '',
        component: Mainlayout,
        children: [
            {path: '', component: MainPage},
            {path: 'login', component: Login, canActivate: [guestGuard]},
            {path: 'registration', component: Registration, canActivate: [guestGuard]},
            {path: 'trainings', component: Trainings, canActivate: [authGuard]},
            {path: 'trainings/:id', component: TrainingDetails, canActivate: [authGuard]},
            {path: 'profile', component: ProfilePage, canActivate: [authGuard]},
            {path: 'cards', component: CardsPage, canActivate: [authGuard]},
            {path: 'tickets', component: TicketsPage, canActivate: [authGuard]},
            {path: 'users', component: UsersPage, canActivate: [authGuard, staffGuard]},
            {path: 'statistics', component: StatisticsPage, canActivate: [authGuard, staffGuard]},
            {path: 'income', component: IncomePage, canActivate: [authGuard, adminGuard]},
            {path: 'card-usage', component: CardUsagePage, canActivate: [authGuard, adminGuard]},
            {path: 'my-trainings', component: MyTrainingsPage, canActivate: [authGuard, trainerGuard]},
        ]
    },
    {
        path: '**',
        redirectTo: ''
    }
];
