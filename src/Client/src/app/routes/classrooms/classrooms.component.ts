import { AfterViewInit, Component, inject, ViewChild } from "@angular/core";
import { MatSort, MatSortModule } from "@angular/material/sort";
import { MatTableDataSource, MatTableModule } from "@angular/material/table";
import { LiveAnnouncer } from "@angular/cdk/a11y";
import { RoutePath } from "../../enums/route-path";
import { ClassroomsService } from "../../services/classrooms.service";
import { Classroom } from "../../types/classroom";
import { take } from "rxjs";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { MatIconModule } from "@angular/material/icon";

@Component({
    selector: "cp-classrooms",
    imports: [MatTableModule, MatSortModule, MatIconModule],
    templateUrl: "./classrooms.component.html",
    styleUrl: "./classrooms.component.scss"
})
export class ClassroomsComponent implements AfterViewInit {
    @ViewChild(MatSort)
    protected readonly matSort!: MatSort;
    protected readonly displayedColumns: string[] = ["name"];
    protected readonly dataSource: MatTableDataSource<Classroom> = new MatTableDataSource<Classroom>();
    protected readonly RoutePath: typeof RoutePath = RoutePath;

    private readonly _classroomsService: ClassroomsService = inject(ClassroomsService);
    private readonly _liveAnnouncer: LiveAnnouncer = inject(LiveAnnouncer);

    constructor() {
        this._classroomsService
            .getAll()
            .pipe(take(1), takeUntilDestroyed())
            .subscribe((data: Classroom[]): void => {
                this.dataSource.data = data;
            });
    }

    ngAfterViewInit(): void {
        this.dataSource.sort = this.matSort;
    }

    protected async announceSortChange(): Promise<void> {
        await this._liveAnnouncer.announce("Zmieniono sortowanie sal");
    }
}
