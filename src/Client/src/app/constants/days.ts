import { Day } from "../enums/day";

const DAYS: Day[] = [Day.Monday, Day.Tuesday, Day.Wednesday, Day.Thursday, Day.Friday, Day.Saturday, Day.Sunday];
const DAY_NAMES: Record<Day, string> = {
    [Day.Sunday]: "Niedziela",
    [Day.Monday]: "Poniedziałek",
    [Day.Tuesday]: "Wtorek",
    [Day.Wednesday]: "Środa",
    [Day.Thursday]: "Czwartek",
    [Day.Friday]: "Piątek",
    [Day.Saturday]: "Sobota"
};

export { DAYS, DAY_NAMES };
