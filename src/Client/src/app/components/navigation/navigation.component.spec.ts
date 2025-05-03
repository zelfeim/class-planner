import { ComponentFixture, TestBed } from "@angular/core/testing";

import { NavigationComponent } from "./navigation.component";
import { provideExperimentalZonelessChangeDetection } from "@angular/core";

describe("NavigationComponent", () => {
    let component: NavigationComponent;
    let fixture: ComponentFixture<NavigationComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            providers: [provideExperimentalZonelessChangeDetection()],
            imports: [NavigationComponent]
        }).compileComponents();

        fixture = TestBed.createComponent(NavigationComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it("should create", () => {
        expect(component).toBeTruthy();
    });
});
