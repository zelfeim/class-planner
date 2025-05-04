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
        loadComponent: () => import("./routes/home/home.component").then((m) => m.HomeComponent)
    },
    {
        path: RoutePath.YEAR_GROUPS,
        loadComponent: () => import("./routes/year-groups/year-groups.component").then((m) => m.YearGroupsComponent)
    },
    {
        path: RoutePath.LECTURERS,
        loadComponent: () => import("./routes/lecturers/lecturers.component").then((m) => m.LecturersComponent),
        canActivate: [authenticatedGuard]
    },
    {
        path: RoutePath.CLASSROOMS,
        loadComponent: () => import("./routes/classrooms/classrooms.component").then((m) => m.ClassroomsComponent),
        canActivate: [authenticatedGuard]
    },
    {
        path: "**",
        redirectTo: RoutePath.HOME
    }
];
