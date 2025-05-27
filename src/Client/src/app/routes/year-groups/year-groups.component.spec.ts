import { ComponentFixture, TestBed } from "@angular/core/testing";

import { YearGroupsComponent } from "./year-groups.component";
import { provideExperimentalZonelessChangeDetection } from "@angular/core";
import { provideRouter } from "@angular/router";
import { RoutePath } from "../../enums/route-path";

describe("YearGroupsComponent", () => {
    let component: YearGroupsComponent;
    let fixture: ComponentFixture<YearGroupsComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            providers: [
                provideExperimentalZonelessChangeDetection(),
                provideRouter([{ path: RoutePath.YEAR_GROUPS, component: YearGroupsComponent }])
            ],
            imports: [YearGroupsComponent]
        }).compileComponents();

        fixture = TestBed.createComponent(YearGroupsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it("should create", () => {
        expect(component).toBeTruthy();
    });
});
