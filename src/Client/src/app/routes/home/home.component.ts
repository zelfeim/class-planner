import { Component } from "@angular/core";
import { CalendarComponent } from "../../components/calendar/calendar.component";
import { CalendarEvent } from "../../types/calendar-event";
import { CalendarEventType } from "../../enums/calendar-event-type";
import { CalendarDisplayMode } from "../../enums/calendar-display-mode";

@Component({
    selector: "cp-home",
    imports: [CalendarComponent],
    templateUrl: "./home.component.html",
    styleUrl: "./home.component.scss"
})
export class HomeComponent {
    protected from: Date = new Date(2024, 8, 1);
    protected to: Date = new Date(2025, 5, 1);

    protected events: CalendarEvent[] = [
        {
            start: new Date(2024, 8, 2, 8, 0),
            durationInMin: 45,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ (ćw)",
            groupSegmentation: 3,
            group: 0,
            classroom: "A2/6"
        },
        {
            start: new Date(2024, 8, 3, 8, 0),
            durationInMin: 90,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ (ćw)",
            groupSegmentation: 3,
            group: 1,
            classroom: "A2/6"
        },
        {
            start: new Date(2024, 8, 3, 8, 30),
            durationInMin: 90,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ (ćw)",
            groupSegmentation: 3,
            group: 2,
            classroom: "A2/6"
        },
        {
            start: new Date(2024, 8, 4, 10, 0),
            durationInMin: 60,
            lecturer: "A. Froń",
            type: CalendarEventType.Laboratory,
            title: "SW (ćw)",
            groupSegmentation: 2,
            group: 0,
            classroom: "E0/15"
        },
        {
            start: new Date(2024, 8, 4, 11, 0),
            durationInMin: 60,
            lecturer: "A. Froń",
            type: CalendarEventType.Laboratory,
            title: "SW (ćw)",
            groupSegmentation: 2,
            group: 1,
            classroom: "E0/15"
        },
        {
            start: new Date(2024, 8, 5, 9, 0),
            durationInMin: 90,
            lecturer: "A. Froń",
            type: CalendarEventType.Lecture,
            title: "SW (w)",
            groupSegmentation: 1,
            group: 0,
            classroom: "C1"
        },
        // next week
        {
            start: new Date(2024, 8, 9, 8, 0),
            durationInMin: 45,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ",
            groupSegmentation: 3,
            group: 0,
            classroom: "A2/6"
        },
        {
            start: new Date(2024, 8, 9, 8, 45),
            durationInMin: 45,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ",
            groupSegmentation: 3,
            group: 1,
            classroom: "A2/6"
        },
        {
            start: new Date(2024, 8, 9, 9, 30),
            durationInMin: 45,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ",
            groupSegmentation: 3,
            group: 2,
            classroom: "A2/6"
        },

        // Wtorek – segmentation 2
        {
            start: new Date(2024, 8, 10, 10, 0),
            durationInMin: 90,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ",
            groupSegmentation: 2,
            group: 0,
            classroom: "A2/6"
        },
        {
            start: new Date(2024, 8, 10, 11, 30),
            durationInMin: 90,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ",
            groupSegmentation: 2,
            group: 1,
            classroom: "A2/6"
        },

        // Środa – segmentation 1
        {
            start: new Date(2024, 8, 11, 9, 0),
            durationInMin: 60,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ",
            groupSegmentation: 1,
            group: 0,
            classroom: "A2/6"
        },

        // Czwartek – segmentation 3
        {
            start: new Date(2024, 8, 12, 8, 0),
            durationInMin: 60,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ",
            groupSegmentation: 3,
            group: 0,
            classroom: "A2/6"
        },
        {
            start: new Date(2024, 8, 12, 9, 0),
            durationInMin: 60,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ",
            groupSegmentation: 3,
            group: 1,
            classroom: "A2/6"
        },
        {
            start: new Date(2024, 8, 12, 10, 0),
            durationInMin: 60,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ",
            groupSegmentation: 3,
            group: 2,
            classroom: "A2/6"
        },

        // Piątek – segmentation 2
        {
            start: new Date(2024, 8, 13, 11, 0),
            durationInMin: 45,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ",
            groupSegmentation: 2,
            group: 0,
            classroom: "A2/6"
        },
        {
            start: new Date(2024, 8, 13, 11, 45),
            durationInMin: 45,
            lecturer: "J. Marchwicki",
            type: CalendarEventType.Laboratory,
            title: "PZ",
            groupSegmentation: 2,
            group: 1,
            classroom: "A2/6"
        }
    ];
    protected readonly CalendarDisplayMode = CalendarDisplayMode;
}
