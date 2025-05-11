import { AfterViewInit, Component, inject, ViewChild } from "@angular/core";
import { LecturersService } from "../../services/lecturers.service";
import { MatTableDataSource, MatTableModule } from "@angular/material/table";
import { Lecturer } from "../../types/lecturers";
import { MatSort, MatSortModule } from "@angular/material/sort";
import { LiveAnnouncer } from "@angular/cdk/a11y";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { take } from "rxjs";
import { RoutePath } from "../../enums/route-path";
import { RouterLink } from "@angular/router";
import { MatIconModule } from "@angular/material/icon";

@Component({
    selector: "cp-lecturers",
    imports: [MatTableModule, MatSortModule, MatIconModule, RouterLink],
    templateUrl: "./lecturers.component.html",
    styleUrl: "./lecturers.component.scss"
})
export class LecturersComponent implements AfterViewInit {
    @ViewChild(MatSort)
    protected readonly matSort!: MatSort;
    protected readonly displayedColumns: string[] = ["email", "name", "lastName", "preview-calendar"];
    protected readonly dataSource: MatTableDataSource<Lecturer> = new MatTableDataSource<Lecturer>();
    protected readonly RoutePath: typeof RoutePath = RoutePath;

    private readonly _lecturersService: LecturersService = inject(LecturersService);
    private readonly _liveAnnouncer: LiveAnnouncer = inject(LiveAnnouncer);

    constructor() {
        // TODO: allow user to refresh?
        this._lecturersService
            .getAll()
            .pipe(take(1), takeUntilDestroyed())
            .subscribe((lecturers: Lecturer[]): void => {
                this.dataSource.data = lecturers;
            });
    }

    ngAfterViewInit(): void {
        this.dataSource.sort = this.matSort;
    }

    protected async announceSortChange(): Promise<void> {
        await this._liveAnnouncer.announce("Zmieniono sortowanie wykładowców");
    }
}
