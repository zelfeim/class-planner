import { CalendarDisplayMode } from "../enums/calendar-display-mode";
import { Day } from "../enums/day";
import { DAYS } from "./days";

const CALENDAR_DISPLAYED_DAYS: Record<CalendarDisplayMode, Day[]> = {
    [CalendarDisplayMode.WORKING_DAYS]: [Day.Monday, Day.Tuesday, Day.Wednesday, Day.Thursday, Day.Friday],
    [CalendarDisplayMode.WEEKENDS]: [Day.Saturday, Day.Sunday],
    [CalendarDisplayMode.ALL]: DAYS
};

export { CALENDAR_DISPLAYED_DAYS };
