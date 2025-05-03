import { Component, inject } from "@angular/core";
import { AuthService } from "../../services/auth.service";

@Component({
    selector: "cp-home",
    imports: [],
    templateUrl: "./home.component.html",
    styleUrl: "./home.component.scss"
})
export class HomeComponent {
    protected readonly authService: AuthService = inject(AuthService);
}
