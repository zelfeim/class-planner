import { CalendarDisplayMode } from "../enums/calendar-display-mode";
import { Day } from "../enums/day";

const CALENDAR_FIRST_VISIBLE_DAY: Record<CalendarDisplayMode, Day> = {
    [CalendarDisplayMode.ALL]: Day.Monday,
    [CalendarDisplayMode.WORKING_DAYS]: Day.Monday,
    // TODO: probably weekends should also have friday visible
    [CalendarDisplayMode.WEEKENDS]: Day.Saturday
};

export { CALENDAR_FIRST_VISIBLE_DAY };
