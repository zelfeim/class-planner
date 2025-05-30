import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: "formatHourPipe",
    standalone: true
})
export class FormatHourPipe implements PipeTransform {
    transform(hour: number): string {
        return `${hour}:00`;
    }
}
