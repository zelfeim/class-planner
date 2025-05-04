import { ComponentFixture, TestBed } from "@angular/core/testing";

import { LecturersComponent } from "./lecturers.component";
import { provideExperimentalZonelessChangeDetection } from "@angular/core";

describe("LecturersComponent", () => {
    let component: LecturersComponent;
    let fixture: ComponentFixture<LecturersComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            providers: [provideExperimentalZonelessChangeDetection()],
            imports: [LecturersComponent]
        }).compileComponents();

        fixture = TestBed.createComponent(LecturersComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it("should create", () => {
        expect(component).toBeTruthy();
    });
});
