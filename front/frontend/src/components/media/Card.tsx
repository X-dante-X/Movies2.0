import { Star, TvMinimal } from "lucide-react";
import Image from "next/image";
import { getServerMinIoUrl } from "@/utils/getMinIoUrl";
import { m } from "framer-motion";
import Link from "next/link";

interface VideoCardProps {
  movieId: number;
  title: string;
  ReleaseDate: string;
  popularity: number;
  posterFile?: string;
}

export function Card({ movieId, title, posterFile, ReleaseDate, popularity }: VideoCardProps) {
  const posterUrl = getServerMinIoUrl(posterFile);
  const date = new Date(ReleaseDate);

  return (
    <Link href={`/movies/${movieId}`}>
      <m.div
        className="relative w-[252px] h-[378px] rounded-lg overflow-hidden shadow-lg hover:shadow-xl transition-transform duration-300"
        whileHover={{ scale: 1.05 }}
        whileTap={{ scale: 0.95 }}>
        <Image
          src={posterUrl}
          alt={`Poster for ${title}`}
          width={252}
          height={378}
          className="object-cover w-full h-full"
        />

        <div className="absolute inset-0 flex flex-col justify-between p-3 bg-black/30">
          <div className="flex justify-between">
            <div className="bg-yellow-400 flex items-center gap-1.5 rounded px-2 py-0.5 text-xs text-black">
              <Star size={14} /> {popularity.toFixed(1)}
            </div>

            <div className="flex items-center gap-1.5 rounded bg-black/50 px-2 py-0.5 text-xs text-white">
              <TvMinimal size={14} /> Movie
            </div>
          </div>

          <div className="absolute text-center bottom-0 left-0 w-full bg-gradient-to-t from-black/80 to-transparent px-3 py-4">
            <h3 className="text-lg font-semibold text-white truncate">{title}</h3>
            <p className="text-xs text-white/70">{date.getFullYear()}</p>
          </div>
        </div>
      </m.div>
    </Link>
  );
}
