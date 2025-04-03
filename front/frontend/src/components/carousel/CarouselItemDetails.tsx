import { IMediaItem } from "@/types/movie.types";
import { m } from "framer-motion";
import { Star, TvMinimal } from "lucide-react";

interface Props {
  item: IMediaItem;
}

const animation = {
  initial: { opacity: 0, scale: 0 },
  animate: { opacity: 1, scale: 1 },
  exit: { opacity: 0, scale: 0 },
  transition: { duration: 0.6 },
};

export function CarouselItemDetails({ item }: Props) {
  const date = new Date(item.releaseDate);
  return (
    <div className="absolute inset-0 z-2 flex h-full w-full flex-col justify-between">
      <div className="absolute top-2 left-2 flex w-[calc(100%-1rem)] items-center justify-between">
        <m.div
          className="bg-yellow-400 flex items-center gap-1.5 rounded px-2 py-0.5 text-xs text-black"
          {...animation}>
          <Star size={14} /> {item.popularity.toFixed(1)}
        </m.div>

        <m.div
          className="flex items-center gap-1.5 rounded bg-black/50 px-2 py-0.5 text-xs text-white"
          {...animation}>
          <TvMinimal size={14} /> Movie
        </m.div>
      </div>
      <m.div
        initial={{
          opacity: 1,
        }}
        exit={{
          opacity: 0,
        }}
        animate={{
          opacity: 1,
        }}
        className="absolute bottom-0 left-0 w-full bg-gradient-to-t from-black to-black/0 p-3 text-center transition-all">
        <m.div {...animation}>
          <h2 className="mb-0.5 font-medium text-white">{item.title}</h2>
          <div className="flex items-center justify-center gap-1 text-xs text-white/50">
            <span>{date.getFullYear()}</span>
          </div>
        </m.div>
      </m.div>
    </div>
  );
}
