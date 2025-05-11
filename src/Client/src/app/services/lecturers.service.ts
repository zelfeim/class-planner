import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { Lecturer } from "../types/lecturers";

// TODO: mock
const lecturers: Lecturer[] = [
    {
        id: 1,
        name: "John",
        lastName: "Doe",
        email: "john.doe@example.com"
    },
    {
        id: 2,
        name: "John",
        lastName: "Cena",
        email: "john.cena@example.com"
    },
    {
        id: 3,
        name: "Joe",
        lastName: "Doe",
        email: "joe.doe@example.com"
    },
    {
        id: 4,
        name: "John",
        lastName: "Does",
        email: "john.does@example.com"
    },
    {
        id: 5,
        name: "John",
        lastName: "Dont",
        email: "john.dont@example.com"
    }
];

@Injectable({
    providedIn: "root"
})
export class LecturersService {
    public get(id: number): Observable<Lecturer | null> {
        return of(lecturers.find((lecturer: Lecturer): boolean => lecturer.id === id) || null);
    }

    public getAll(): Observable<Lecturer[]> {
        return of([...lecturers]);
    }
}
