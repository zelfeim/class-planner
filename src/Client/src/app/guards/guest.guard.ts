import { CanActivateFn, RedirectCommand, Router, UrlTree } from "@angular/router";
import { AuthService } from "../services/auth.service";
import { inject } from "@angular/core";
import { RoutePath } from "../enums/route-path";

export const guestGuard: CanActivateFn = (): true | RedirectCommand => {
    const router: Router = inject(Router);
    const authService: AuthService = inject(AuthService);

    if (!authService.isAuthenticated()) {
        return true;
    }

    const homePath: UrlTree = router.parseUrl(RoutePath.HOME);

    return new RedirectCommand(homePath);
};
