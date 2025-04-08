declare module 'color-thief-browser' {
    class ColorThief {
        getPalette(image: HTMLImageElement, colorCount: number): number[][];
        getColor(image: HTMLImageElement): number[];
    }
    export = ColorThief;
}
