import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { YearGroup } from "../types/year_groups";

// Dane testowe (mock)
const mockYearGroups: YearGroup[] = [
    {
        id: 1,
        rocznik: "2015/2016",
        opiekunRoku: "Jan Kowalski",
        trybStudiow: "dzienny"
    },
    {
        id: 2,
        rocznik: "2016/2017",
        opiekunRoku: "Mirosława Kownacka",
        trybStudiow: "dzienny"

    },
    {
        id: 3,
        rocznik: "2017/2018",
        opiekunRoku: "Andrzej Nowak",
        trybStudiow: "dzienny"

    },
    {
        id: 4,
        rocznik: "2018/2019",
        opiekunRoku: "Anna Zielińska",
        trybStudiow: "dzienny"

    },
    {
        id: 5,
        rocznik: "2019/2020",
        opiekunRoku: "Piotr Mazur",
        trybStudiow: "dzienny"

    }
];

@Injectable({
    providedIn: "root"
})
export class YearGroupsService {
    public get(id: number): Observable<YearGroup | null> {
        const found = mockYearGroups.find((group) => group.id === id) || null;
        return of(found);
    }

    public getAll(): Observable<YearGroup[]> {
        return of([...mockYearGroups]);
    }
}
