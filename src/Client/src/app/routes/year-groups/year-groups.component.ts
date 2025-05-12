import { AfterViewInit, Component, inject, ViewChild } from "@angular/core";
import { MatTableDataSource, MatTableModule } from "@angular/material/table";
import { YearGroup } from "../../types/year_groups";
import { MatSort, MatSortModule } from "@angular/material/sort";
import { LiveAnnouncer } from "@angular/cdk/a11y";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { take } from "rxjs";
import { RoutePath } from "../../enums/route-path";
import { RouterLink } from "@angular/router";
import { MatIconModule } from "@angular/material/icon";
import { YearGroupsService } from "../../services/year_groups.service";

@Component({
    selector: "cp-yeargroup",
    imports: [MatTableModule, MatSortModule, MatIconModule, RouterLink],
    templateUrl: "./year-groups.component.html",
    styleUrl: "./year-groups.component.scss"
})
export class YearGroupsComponent implements AfterViewInit {
    @ViewChild(MatSort)
    protected readonly matSort!: MatSort;

    protected readonly displayedColumns: string[] = [  'rocznik', 'opiekunRoku', 'trybStudiow','preview-calendar'];
    protected readonly dataSource: MatTableDataSource<YearGroup> = new MatTableDataSource<YearGroup>();
    protected readonly RoutePath: typeof RoutePath = RoutePath;

    private readonly _yeargroupsService: YearGroupsService = inject(YearGroupsService);
    private readonly _liveAnnouncer: LiveAnnouncer = inject(LiveAnnouncer);

    constructor() {
        this._yeargroupsService
            .getAll()
            .pipe(take(1), takeUntilDestroyed())
            .subscribe((yeargroups: YearGroup[]) => {
                console.log(yeargroups);
                this.dataSource.data = yeargroups;
            });
    }

    ngAfterViewInit(): void {
        this.dataSource.sort = this.matSort;
    }

    protected async announceSortChange(): Promise<void> {
        await this._liveAnnouncer.announce("Zmieniono sortowanie Roczników");
    }
}
