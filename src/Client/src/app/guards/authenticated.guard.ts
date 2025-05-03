import { ActivatedRouteSnapshot, CanActivateFn, RedirectCommand, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { AuthService } from "../services/auth.service";
import { inject } from "@angular/core";
import { RoutePath } from "../enums/route-path";

export const authenticatedGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): true | RedirectCommand => {
    const router: Router = inject(Router);
    const authService: AuthService = inject(AuthService);

    if (authService.isAuthenticated()) {
        return true;
    }

    const loginPath: UrlTree = router.parseUrl(RoutePath.LOGIN);

    return new RedirectCommand(loginPath);
};
