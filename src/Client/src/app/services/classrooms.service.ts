import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { Classroom } from "../types/classrooms";

// TODO: mock
const classrooms: Classroom[] = [
    {
        id: 1,
        sala: "A2/15"

    },
    {
        id: 2,
        sala: "A2/15"
    },
    {
        id: 3,
        sala: "A3/7"
    },
    {
        id: 4,
        sala: "D2/5"
    },
    {
        id: 5,
        sala: "E1/10"
    }
];

@Injectable({
    providedIn: "root"
})
export class ClassroomsService {
    public get(id: number): Observable<Classroom | null> {
        return of(classrooms.find((classroom: Classroom): boolean => classroom.id === id) || null);
    }

    public getAll(): Observable<Classroom[]> {
        return of([...classrooms]);
    }
}
