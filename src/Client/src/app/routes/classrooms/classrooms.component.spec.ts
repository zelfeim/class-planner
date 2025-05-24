import { ComponentFixture, TestBed } from "@angular/core/testing";

import { ClassroomsComponent } from "./classrooms.component";
import { provideExperimentalZonelessChangeDetection } from "@angular/core";
import { provideRouter } from "@angular/router";
import { RoutePath } from "../../enums/route-path";

describe("ClassroomsComponent", () => {
    let component: ClassroomsComponent;
    let fixture: ComponentFixture<ClassroomsComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            providers: [
                provideExperimentalZonelessChangeDetection(),
                provideRouter([{ path: RoutePath.CLASSROOMS, component: ClassroomsComponent }])
            ],
            imports: [ClassroomsComponent]
        }).compileComponents();

        fixture = TestBed.createComponent(ClassroomsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it("should create", () => {
        expect(component).toBeTruthy();
    });
});
