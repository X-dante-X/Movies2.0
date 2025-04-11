"use client";

import { Carousel } from "@/components/carousel/Carousel";
import { Filters } from "@/components/filters/Filters";
import { useMainAnimationStore } from "@/stores/main-animation.store";
import { m } from "framer-motion";
import { useState, useEffect } from "react";

export default function Page() {
  const { isHideHeading } = useMainAnimationStore();
  const [isCarouselVisible, setIsCarouselVisible] = useState(true);
  const [isFiltersChanged, setIsFiltersChanged] = useState(false);

  useEffect(() => {
    if (isFiltersChanged) {
      setIsCarouselVisible(false);

      const timeout = setTimeout(() => {
        setIsFiltersChanged(false);
        setIsCarouselVisible(true);
      }, 500);

      return () => clearTimeout(timeout);
    }
  }, [isFiltersChanged]);

  return (
    <>
      <div className="absolute w-full z-0 -mt-20 h-screen bg-gradient-to-br from-gray-900/20 via-indigo-900/20 to-purple-900/20 bg-fixed" />
      <div className="relative mt-8 z-1 h-[87.5vh] overflow-y-hidden">
        <m.div
          initial={{
            opacity: 0,
          }}
          animate={{
            opacity: isHideHeading ? 0 : 1,
            translateY: isHideHeading ? -100 : 0,
          }}
          transition={{
            duration: 1.8,
            type: "keyframes",
            ease: "easeInOut",
          }}>
          <h1 className="text-center text-5xl font-bold">Discover Unlimited Content</h1>

          <Filters onChange={() => setIsFiltersChanged(true)} />
        </m.div>

        <m.div
          initial={{ opacity: 0 }}
          animate={{
            opacity: isCarouselVisible ? 1 : 0,
          }}
          exit={{ opacity: 0 }}
          transition={{
            duration: 1.2,
            ease: "easeInOut",
          }}>
          <Carousel />
        </m.div>
      </div>
    </>
  );
}
