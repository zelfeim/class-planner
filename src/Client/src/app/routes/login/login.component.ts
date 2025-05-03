import { Component, inject } from "@angular/core";
import { AuthService } from "../../services/auth.service";

@Component({
    selector: "cp-login",
    imports: [],
    templateUrl: "./login.component.html",
    styleUrl: "./login.component.scss"
})
export class LoginComponent {
    protected readonly authService: AuthService = inject(AuthService);
}
