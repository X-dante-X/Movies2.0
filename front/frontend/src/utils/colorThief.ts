// import { useState, useEffect } from 'react';

// export function useColorThief() {
//     // eslint-disable-next-line @typescript-eslint/no-explicit-any
//     const [ColorThief, setColorThief] = useState<any>(null);  // Type should be improved later

//     useEffect(() => {
//         if (typeof window !== 'undefined') {
//             import('color-thief-browser').then((module) => {
//                 setColorThief(module.default);
//             });
//         }
//     }, []);

//     return ColorThief;
// }

// // eslint-disable-next-line @typescript-eslint/no-explicit-any
// export function getMainColors(imageUrl: string, ColorThief: any) {
//     return new Promise<string[]>((resolve, reject) => {
//         if (!ColorThief) {
//             reject('ColorThief is not loaded yet.');
//             return;
//         }
//         const colorThief = new ColorThief();
//         const img = new Image();
//         img.crossOrigin = 'Anonymous'; // Handle cross-origin images
//         img.onload = () => {
//             const colors = colorThief.getPalette(img, 5);
//             resolve(colors.map((color: number[]) => `rgb(${color.join(', ')})`));
//         };
//         img.onerror = reject;
//         img.src = imageUrl;
//     });
// }
