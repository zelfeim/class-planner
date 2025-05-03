import { Component, inject } from "@angular/core";
import { AuthService } from "../../services/auth.service";
import { NgOptimizedImage } from "@angular/common";
import { MatButtonModule } from "@angular/material/button";
import { IfAuthenticatedDirective } from "../../directives/if-authenticated.directive";

@Component({
    selector: "cp-header",
    imports: [NgOptimizedImage, MatButtonModule, IfAuthenticatedDirective],
    templateUrl: "./header.component.html",
    styleUrl: "./header.component.scss"
})
export class HeaderComponent {
    private readonly _authService: AuthService = inject(AuthService);

    protected async onLogoutClick(): Promise<void> {
        await this._authService.logout();
    }

    protected async OnLoginClick(): Promise<void> {
        await this._authService.login();
    }
}
