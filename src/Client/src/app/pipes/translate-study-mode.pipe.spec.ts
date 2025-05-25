import { TranslateStudyModePipe } from "./translate-study-mode.pipe";

describe("TranslateStudyModePipe", () => {
    it("creates an instance", () => {
        const pipe = new TranslateStudyModePipe();
        expect(pipe).toBeTruthy();
    });

    it("properly translates part time study mode", () => {
        const pipe = new TranslateStudyModePipe();
        expect(pipe.transform(true)).toEqual("Niestacjonarne");
    });

    it("properly translates full time study mode", () => {
        const pipe = new TranslateStudyModePipe();
        expect(pipe.transform(false)).toEqual("Stacjonarne");
    });
});
