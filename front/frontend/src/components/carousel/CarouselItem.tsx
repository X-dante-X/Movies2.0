"use client";

import { AnimatePresence, m } from "framer-motion";
import Image from "next/image";
import { twMerge } from "tailwind-merge";

import { CarouselItemDetails } from "./CarouselItemDetails";
import { carouselItemAnimation } from "./animations/carousel-item.animation";
import { useCarouselItemAngle } from "./hooks/useCarouselItemAngle";
import { useCarouselItemZIndex } from "./hooks/useCarouselItemZIndex";
import { IMediaItem } from "@/types/movie.types";
import { useMainAnimationStore } from "@/stores/main-animation.store";
import { useCarouselStore } from "@/stores/carousel.store";
import { getServerMinIoUrl } from "@/utils/getMinIoUrl";

interface Props {
  index: number;
  item: IMediaItem;
  updateActiveCard: () => void;
}

export function CarouselItem({ item, index, updateActiveCard }: Props) {
  const { angle, radius } = useCarouselItemAngle({ index });

  const { isNewPageAnimation } = useMainAnimationStore();
  const { activeCardId } = useCarouselStore();

  const isActive = activeCardId === item.id;

  const { zIndex } = useCarouselItemZIndex({ index, isActive });

  return (
    <div
      className="absolute top-1/2 left-1/2"
      style={{
        transform: `translate(-50%, -50%) rotate(${angle}deg) translate(0, -${radius}px)`,
        zIndex,
        perspective: "1000px",
      }}>
      <m.button
        {...carouselItemAnimation(isActive, isNewPageAnimation)}
        className={twMerge("overflow-hidden rounded-xl transition will-change-transform", isActive && "shadow-lg")}
        style={{
          transformStyle: "preserve-3d",
        }}
        onClick={updateActiveCard}>
        <AnimatePresence>{isActive && <CarouselItemDetails item={item} />}</AnimatePresence>
        <Image
          width={252}
          height={378}
          src={getServerMinIoUrl(item.posterPath)}
          alt={item.title}
          draggable="false"
          className="will-change-transform"
          priority={index > 6 ? false : true}
        />
      </m.button>
    </div>
  );
}
