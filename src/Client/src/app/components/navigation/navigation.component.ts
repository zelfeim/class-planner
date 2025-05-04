import { Component } from "@angular/core";
import { RoutePath } from "../../enums/route-path";
import { MatIconModule } from "@angular/material/icon";
import { RouterLink } from "@angular/router";
import { IfAuthenticatedDirective } from "../../directives/if-authenticated.directive";

@Component({
    selector: "cp-navigation",
    imports: [MatIconModule, RouterLink, IfAuthenticatedDirective],
    templateUrl: "./navigation.component.html",
    styleUrl: "./navigation.component.scss"
})
export class NavigationComponent {
    protected readonly RoutePath: typeof RoutePath = RoutePath;
}
