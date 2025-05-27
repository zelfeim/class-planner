import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: "translateStudyMode"
})
export class TranslateStudyModePipe implements PipeTransform {
    transform(isPartTime: boolean): string {
        return isPartTime ? "Niestacjonarne" : "Stacjonarne";
    }
}
