import { AfterViewInit, Component, inject, ViewChild } from "@angular/core";
import { MatSort, MatSortModule } from "@angular/material/sort";
import { MatTableDataSource, MatTableModule } from "@angular/material/table";
import { LiveAnnouncer } from "@angular/cdk/a11y";
import { take } from "rxjs";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { RoutePath } from "../../enums/route-path";
import { YearGroup } from "../../types/year-group";
import { YearGroupsService } from "../../services/year-groups.service";
import { MatIconModule } from "@angular/material/icon";
import { RouterLink } from "@angular/router";
import { TranslateStudyModePipe } from "../../pipes/translate-study-mode.pipe";

@Component({
    selector: "cp-year-groups",
    imports: [MatTableModule, MatSortModule, MatIconModule, RouterLink, TranslateStudyModePipe],
    templateUrl: "./year-groups.component.html",
    styleUrl: "./year-groups.component.scss"
})
export class YearGroupsComponent implements AfterViewInit {
    @ViewChild(MatSort)
    protected readonly matSort!: MatSort;
    protected readonly displayedColumns: string[] = ["name", "study-mode", "preview-calendar"];
    protected readonly dataSource: MatTableDataSource<YearGroup> = new MatTableDataSource<YearGroup>();
    protected readonly RoutePath: typeof RoutePath = RoutePath;

    private readonly _yearGroupsService: YearGroupsService = inject(YearGroupsService);
    private readonly _liveAnnouncer: LiveAnnouncer = inject(LiveAnnouncer);

    constructor() {
        // TODO: allow user to refresh?
        this._yearGroupsService
            .getAll()
            .pipe(take(1), takeUntilDestroyed())
            .subscribe((data: YearGroup[]): void => {
                this.dataSource.data = data;
            });
    }

    ngAfterViewInit(): void {
        this.dataSource.sort = this.matSort;
    }

    protected async announceSortChange(): Promise<void> {
        await this._liveAnnouncer.announce("Zmieniono sortowanie roczników");
    }
}
