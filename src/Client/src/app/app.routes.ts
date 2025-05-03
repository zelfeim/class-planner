import { Routes } from "@angular/router";
import { RoutePath } from "./enums/route-path";
import { authenticatedGuard } from "./guards/authenticated.guard";
import { guestGuard } from "./guards/guest.guard";

export const routes: Routes = [
    {
        path: RoutePath.LOGIN,
        loadComponent: () => import("./routes/login/login.component").then((m) => m.LoginComponent),
        canActivate: [guestGuard]
    },
    {
        path: RoutePath.HOME,
        loadComponent: () => import("./routes/home/home.component").then((m) => m.HomeComponent),
        canActivate: [authenticatedGuard]
    }
];
