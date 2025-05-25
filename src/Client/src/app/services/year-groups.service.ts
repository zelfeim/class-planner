import { Injectable } from "@angular/core";
import { YearGroup } from "../types/year-group";
import { Observable, of } from "rxjs";

const yearGroups: YearGroup[] = [
    {
        id: 1,
        name: "NS 2016",
        isPartTime: true
    },
    {
        id: 1,
        name: "NS 2017",
        isPartTime: true
    },
    {
        id: 1,
        name: "S 2020",
        isPartTime: false
    },
    {
        id: 1,
        name: "S 2021",
        isPartTime: false
    }
];

@Injectable({
    providedIn: "root"
})
export class YearGroupsService {
    public getAll(): Observable<YearGroup[]> {
        return of([...yearGroups]);
    }
}
