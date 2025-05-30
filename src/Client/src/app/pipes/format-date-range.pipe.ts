import { Pipe, PipeTransform } from "@angular/core";
import { format } from "date-fns";

@Pipe({
    name: "formatDateRangePipe",
    standalone: true
})
export class FormatDateRangePipe implements PipeTransform {
    transform(from: Date, to: Date, dateFormat: string = "dd-MM-yyyy"): string {
        return `${format(from, dateFormat)} - ${format(to, dateFormat)}`;
    }
}
