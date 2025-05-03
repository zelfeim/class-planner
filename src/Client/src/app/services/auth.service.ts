import { inject, Injectable, signal, WritableSignal } from "@angular/core";
import { Router } from "@angular/router";
import { RoutePath } from "../enums/route-path";

@Injectable({
    providedIn: "root"
})
export class AuthService {
    private readonly _router: Router = inject(Router);
    private readonly _isAuthenticated: WritableSignal<boolean> = signal(true);

    public get isAuthenticated(): WritableSignal<boolean> {
        return this._isAuthenticated;
    }

    public async login(): Promise<void> {
        // TODO
        this._isAuthenticated.set(true);
        await this._router.navigateByUrl(RoutePath.HOME);
    }

    public async logout(): Promise<void> {
        // TODO
        this._isAuthenticated.set(false);
        await this._router.navigateByUrl(RoutePath.LOGIN);
    }
}
