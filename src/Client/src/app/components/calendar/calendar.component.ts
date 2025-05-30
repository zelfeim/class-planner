import {
    AfterViewInit,
    Component,
    computed,
    effect,
    inject,
    Injector,
    Input,
    Renderer2,
    runInInjectionContext,
    signal,
    Signal,
    WritableSignal
} from "@angular/core";
import { CalendarEvent } from "../../types/calendar-event";
import { NgClass, NgForOf, NgStyle } from "@angular/common";
import { DAY_NAMES } from "../../constants/days";
import { FormatHourPipe } from "../../pipes/format-hour.pipe";
import { addDays, addWeeks, differenceInWeeks, isSameWeek, nextDay } from "date-fns";
import { CalendarDisplayMode } from "../../enums/calendar-display-mode";
import { Day } from "../../enums/day";
import { MatButton } from "@angular/material/button";
import { CALENDAR_FIRST_VISIBLE_DAY } from "../../constants/calendar-first-visible-day";
import { CALENDAR_DISPLAYED_DAYS } from "../../constants/calendar-displayed-days";
import { FormatDateRangePipe } from "../../pipes/format-date-range.pipe";

@Component({
    selector: "cp-calendar",
    imports: [NgForOf, FormatHourPipe, NgStyle, NgClass, MatButton, FormatDateRangePipe],
    templateUrl: "./calendar.component.html",
    styleUrl: "./calendar.component.scss",
    standalone: true
})
export class CalendarComponent implements AfterViewInit {
    @Input({ required: true })
    public from!: Date;
    @Input({ required: true })
    public to!: Date;
    @Input({ required: true })
    public events!: CalendarEvent[];
    @Input({ required: true })
    public groupSegmentations!: number[];

    protected displayMode: WritableSignal<CalendarDisplayMode> = signal(CalendarDisplayMode.WORKING_DAYS);

    private readonly renderer: Renderer2 = inject(Renderer2);
    private readonly injector: Injector = inject(Injector);

    private readonly totalWeeks: Signal<number> = computed((): number => Math.abs(Math.ceil(differenceInWeeks(this.from, this.to))));
    private readonly weeksOffset: WritableSignal<number> = signal(0);
    private readonly firstCalendarDay: Signal<Date> = computed(
        (): Date => nextDay(this.from, CALENDAR_FIRST_VISIBLE_DAY[this.displayMode()])
    );

    protected readonly currentFirstDisplayedDay: Signal<Date> = computed((): Date => addWeeks(this.firstCalendarDay(), this.weeksOffset()));
    protected readonly currentLastDisplayedDay: Signal<Date> = computed(
        (): Date => addDays(this.currentFirstDisplayedDay(), this.getDisplayedDays().length - 1)
    );
    protected readonly hasPreviousWeek: Signal<boolean> = computed((): boolean => this.weeksOffset() > 0);
    protected readonly hasNextWeek: Signal<boolean> = computed((): boolean => this.weeksOffset() < this.totalWeeks() - 1);

    protected readonly DAY_NAMES: Record<Day, string> = DAY_NAMES;
    protected readonly CALENDAR_DISPLAYED_HOURS: number = 12;
    protected readonly HOUR_HEIGHT: number = 100;
    private readonly CALENDAR_START_HOUR: number = 8;

    public ngAfterViewInit(): void {
        runInInjectionContext(this.injector, (): void => {
            effect((): void => {
                this.clearEvents();
                this.renderEvents(this.currentFirstDisplayedDay());
            });
        });
    }

    protected updateDisplayMode(mode: CalendarDisplayMode): void {
        this.displayMode.set(mode);
    }

    protected goToPreviousWeek(): void {
        if (this.weeksOffset() > 0) {
            this.weeksOffset.update((currentOffset: number): number => currentOffset - 1);
        }
    }

    protected goToNextWeek(): void {
        this.weeksOffset.update((currentOffset: number): number => currentOffset + 1);
    }

    protected getDisplayedDays(): Day[] {
        return CALENDAR_DISPLAYED_DAYS[this.displayMode()];
    }

    protected getRowHeightStyle(): Record<string, string> {
        return {
            height: `${this.HOUR_HEIGHT}px`
        };
    }

    protected getTemplateHours(): number[] {
        return Array.from({ length: this.CALENDAR_DISPLAYED_HOURS }, (_, i: number): number => i + this.CALENDAR_START_HOUR);
    }

    protected getDayGroupEventsCellClass(day: number, segmentation: number, group: number): string {
        return `cell-${day}-${segmentation}-${group}`;
    }

    protected getSegmentationGroups(segmentation: number): number[] {
        return Array.from({ length: segmentation }, (_, index: number): number => index);
    }

    private clearEvents(): void {
        const elements: HTMLCollection = document.getElementsByClassName("day-group-events-column");

        for (let i: number = 0; i < elements.length; i++) {
            while (elements[i].firstChild) {
                elements[i].removeChild(elements[i].firstChild!);
            }
        }
    }

    private renderEvents(currentFirstDisplayedDay: Date): void {
        this.events
            .filter((event: CalendarEvent): boolean => isSameWeek(event.start, currentFirstDisplayedDay))
            .forEach((event: CalendarEvent): void => {
                const elements: HTMLCollection = document.getElementsByClassName(
                    this.getDayGroupEventsCellClass(event.start.getDay(), event.groupSegmentation, event.group)
                );

                if (!elements[0]) {
                    console.error("event has not been rendered");
                }

                elements[0].appendChild(this.createEventTile(event));
            });
    }

    private createEventTile(event: CalendarEvent): HTMLDivElement {
        const eventStartingHour: number = event.start.getHours() + event.start.getMinutes() / 60;
        const offset: number = eventStartingHour - this.CALENDAR_START_HOUR;
        const height: number = (event.durationInMin / 60) * this.HOUR_HEIGHT;

        const eventTile: HTMLDivElement = this.renderer.createElement("div");
        this.renderer.addClass(eventTile, "event-tile");
        this.renderer.setStyle(eventTile, "margin-top", `${offset * this.HOUR_HEIGHT}px`);
        this.renderer.setStyle(eventTile, "height", `${height}px`);
        this.renderer.setStyle(eventTile, "background", this.getColor(event.title));

        const eventTitleText: HTMLElement = this.renderer.createElement("span");
        this.renderer.appendChild(eventTitleText, this.renderer.createText(event.title));

        const eventGroupText: HTMLElement = this.renderer.createElement("span");
        this.renderer.appendChild(eventGroupText, this.renderer.createText(`gr. ${event.group + 1}/${event.groupSegmentation}`));

        const eventClassroomText: HTMLElement = this.renderer.createElement("span");
        this.renderer.appendChild(eventClassroomText, this.renderer.createText(event.classroom));

        this.renderer.appendChild(eventTile, eventTitleText);
        this.renderer.appendChild(eventTile, eventClassroomText);
        this.renderer.appendChild(eventTile, eventGroupText);

        return eventTile;
    }

    // TODO pass from outside
    private getColor(eventTitle: string): string {
        switch (eventTitle) {
            case "PZ (ćw)":
                return "lightyellow";
            case "SW (ćw)":
                return "#a9a980";
            default:
                return "lightblue";
        }
    }

    protected readonly CalendarDisplayMode = CalendarDisplayMode;
}
