import { ComponentFixture, TestBed } from "@angular/core/testing";

import { YearGroupsComponent } from "./year-groups.component";
import { provideExperimentalZonelessChangeDetection } from "@angular/core";

describe("YearGroupsComponent", () => {
    let component: YearGroupsComponent;
    let fixture: ComponentFixture<YearGroupsComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            providers: [provideExperimentalZonelessChangeDetection()],
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
