import { ComponentFixture, TestBed } from "@angular/core/testing";

import { ClassroomsComponent } from "./classrooms.component";
import { provideExperimentalZonelessChangeDetection } from "@angular/core";

describe("ClassroomsComponent", () => {
    let component: ClassroomsComponent;
    let fixture: ComponentFixture<ClassroomsComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            providers: [provideExperimentalZonelessChangeDetection()],
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
