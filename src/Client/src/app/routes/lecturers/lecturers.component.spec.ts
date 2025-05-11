import { ComponentFixture, TestBed } from "@angular/core/testing";
import { LecturersComponent } from "./lecturers.component";
import { provideExperimentalZonelessChangeDetection } from "@angular/core";
import { provideRouter } from "@angular/router";
import { RoutePath } from "../../enums/route-path";
import { HomeComponent } from "../home/home.component";

describe("LecturersComponent", () => {
    let component: LecturersComponent;
    let fixture: ComponentFixture<LecturersComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            providers: [provideExperimentalZonelessChangeDetection(), provideRouter([{ path: RoutePath.HOME, component: HomeComponent }])],
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
