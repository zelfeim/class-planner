import { AfterViewInit, Component, inject, ViewChild } from "@angular/core";
import { ClassroomsService } from "../../services/classrooms.service";
import { MatTableDataSource, MatTableModule } from "@angular/material/table";
import { Classroom } from "../../types/classrooms";
import { MatSort, MatSortModule } from "@angular/material/sort";
import { LiveAnnouncer } from "@angular/cdk/a11y";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { take } from "rxjs";
import { RoutePath } from "../../enums/route-path";
import { RouterLink } from "@angular/router";
import { MatIconModule } from "@angular/material/icon";

@Component({
  selector: "cp-classrooms",
  imports: [MatTableModule, MatSortModule, MatIconModule, RouterLink],
  templateUrl: "./classrooms.component.html",
  styleUrl: "./classrooms.component.scss",
})
export class ClassroomsComponent implements AfterViewInit {
  @ViewChild(MatSort)
  protected readonly matSort!: MatSort;
  protected readonly displayedColumns: string[] = ["classroom"];
  protected readonly dataSource: MatTableDataSource<Classroom> = new MatTableDataSource<Classroom>();
  protected readonly RoutePath: typeof RoutePath = RoutePath;

  private readonly _classroomsService: ClassroomsService = inject(ClassroomsService);
  private readonly _liveAnnouncer: LiveAnnouncer = inject(LiveAnnouncer);

  constructor() {
    // TODO: allow user to refresh?
    this._classroomsService
      .getAll()
      .pipe(take(1), takeUntilDestroyed())
      .subscribe((classrooms: Classroom[]): void => {
        this.dataSource.data = classrooms;
      });
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.matSort;
  }

  protected async announceSortChange(): Promise<void> {
    await this._liveAnnouncer.announce("Zmieniono sortowanie sal");
  }
}
