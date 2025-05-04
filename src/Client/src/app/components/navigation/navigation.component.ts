import { Component } from "@angular/core";
import { RoutePath } from "../../enums/route-path";
import { MatIconModule } from "@angular/material/icon";
import { RouterLink } from "@angular/router";

@Component({
    selector: "cp-navigation",
    imports: [MatIconModule, RouterLink],
    templateUrl: "./navigation.component.html",
    styleUrl: "./navigation.component.scss"
})
export class NavigationComponent {
    protected readonly RoutePath = RoutePath;
}
