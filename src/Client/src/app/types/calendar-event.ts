import { CalendarEventType } from "../enums/calendar-event-type";

interface CalendarEvent {
    start: Date;
    durationInMin: number;
    type: CalendarEventType;
    title: string;
    groupSegmentation: number;
    group: number;
    lecturer: string;
    classroom: string;
}

export { type CalendarEvent };
