"use client";

import { Carousel } from "@/components/carousel/Carousel";
import { Filters } from "@/components/filters/Filters";
import { useMainAnimationStore } from "@/stores/main-animation.store";
import { m } from "framer-motion";

export default function Page() {
  const { isHideHeading } = useMainAnimationStore();

  return (
    <div className="mt-8 h-[85.5vh] overflow-y-hidden">
      <m.div
        initial={{
          opacity: 0,
        }}
        animate={{
          opacity: isHideHeading ? 0 : 1,
          translateY: isHideHeading ? -100 : 0,
        }}
        transition={
          isHideHeading
            ? {
                duration: 1.8,
                type: "keyframes",
                ease: "easeInOut",
              }
            : {}
        }>
        <h1 className="text-center text-5xl font-bold">Discover Unlimited Content</h1>

        <Filters />
      </m.div>
      <Carousel />
    </div>
  );
}
