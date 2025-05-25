import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { Classroom } from "../types/classroom";

const classrooms: Classroom[] = [
    {
        id: 1,
        name: "A2/6"
    },
    {
        id: 2,
        name: "A2/8"
    },
    {
        id: 3,
        name: "A2/9"
    },
    {
        id: 4,
        name: "A1/2"
    }
];

@Injectable({
    providedIn: "root"
})
export class ClassroomsService {
    public getAll(): Observable<Classroom[]> {
        return of([...classrooms]);
    }
}
