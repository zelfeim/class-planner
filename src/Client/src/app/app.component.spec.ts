import { TestBed } from "@angular/core/testing";
import { AppComponent } from "./app.component";
import { provideExperimentalZonelessChangeDetection } from "@angular/core";
import { provideRouter } from "@angular/router";
import { RoutePath } from "./enums/route-path";
import { HomeComponent } from "./routes/home/home.component";

describe("AppComponent", () => {
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            providers: [provideExperimentalZonelessChangeDetection(), provideRouter([{ path: RoutePath.HOME, component: HomeComponent }])],
            imports: [AppComponent]
        }).compileComponents();
    });

    it("should create the app", () => {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.componentInstance;
        expect(app).toBeTruthy();
    });
});
