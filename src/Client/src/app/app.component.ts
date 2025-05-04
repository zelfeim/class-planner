import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { HeaderComponent } from "./components/header/header.component";
import { NavigationComponent } from "./components/navigation/navigation.component";

@Component({
    selector: "cp-root",
    imports: [RouterOutlet, HeaderComponent, NavigationComponent],
    templateUrl: "./app.component.html",
    styleUrl: "./app.component.scss"
})
export class AppComponent {}
