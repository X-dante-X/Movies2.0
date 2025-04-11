import { Movie } from "@/types/movie.types";
import { m } from "framer-motion";
import Link from "next/link";

interface Props {
  mediaItem: Movie;
}

const getPegiColor = (pegi: string) => {
  const pegiNumber = parseInt(pegi.replace(/\D/g, ""), 10) || 0;

  if (pegiNumber >= 21) return "bg-red-700";
  if (pegiNumber >= 18) return "bg-red-500";
  if (pegiNumber >= 16) return "bg-oragne-500";
  if (pegiNumber >= 12) return "bg-yellow-500";
  if (pegiNumber >= 7) return "bg-green-500";
  if (pegiNumber >= 3) return "bg-green-300";
  return "bg-green-300";
};

export function MediaDetails({ mediaItem }: Props) {
  return (
    <div className="z-10">
      <m.div
        initial={{ opacity: 0, y: 10 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.6, delay: 1.2 }}
        className="mb-2 flex items-center gap-3">
        {mediaItem.genre.map((genre) => (
          <div
            key={genre.genreId}
            className="rounded bg-neutral-900/50 px-2 py-1 text-s text-white shadow-lg">
            {genre.genreName}
          </div>
        ))}
      </m.div>
      <m.div
        initial={{ opacity: 0, y: 10 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.6, delay: 1.2 }}
        className="mb-2 flex items-center gap-3">
        {mediaItem.tags.map((tag) => (
          <div
            key={tag.tagId}
            className="rounded bg-neutral-900/50 px-2 py-1 text-xs text-white shadow-lg">
            {tag.tagName}
          </div>
        ))}
      </m.div>
      <m.h1
        initial={{ opacity: 0, y: 25 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.7, delay: 1.2 }}
        className="mb-3.5 text-6xl font-bold text-white"
        style={{ textShadow: "1px 1px 3px rgba(0, 0, 0, 0.3)" }}>
        {mediaItem.title}
      </m.h1>
      <m.div
        initial={{ opacity: 0, y: 25 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.7, delay: 1.3 }}
        className="flex items-center gap-6">
        <div className="flex items-center gap-4">
          <div className="bg-secondary rounded px-2 py-0.5 text-sm font-semibold text-black">
            <Link href={`/company/${mediaItem.productionCompany.companyId}`}>{mediaItem.productionCompany.companyName}</Link>
          </div>
          <div className="bg-secondary rounded px-2 py-0.5 text-sm font-semibold text-black">{mediaItem.popularity.toFixed(1)}/10</div>
          <div className={`${getPegiColor(mediaItem.pegi)} rounded px-2 py-0.5 text-sm font-semibold text-black`}>
            {parseInt(mediaItem.pegi.replace(/\D/g, ""), 10) || 0}+
          </div>
        </div>
      </m.div>
    </div>
  );
}
