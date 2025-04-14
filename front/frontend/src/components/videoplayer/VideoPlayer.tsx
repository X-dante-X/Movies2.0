"use client";

import React, { useEffect, useRef, useState } from "react";
import Hls, { ManifestParsedData } from "hls.js";

interface VideoPlayerProps {
  src: string;
  poster?: string;
}

export function VideoPlayer({ src, poster }: VideoPlayerProps) {
  const videoRef = useRef<HTMLVideoElement>(null);
  const [qualityLevels, setQualityLevels] = useState<number[]>([]);
  const [selectedQuality, setSelectedQuality] = useState<number | "auto">("auto");
  const hlsRef = useRef<Hls | null>(null);

  useEffect(() => {
    if (videoRef.current) {
      if (Hls.isSupported()) {
        const hls = new Hls();
        hls.loadSource(src);
        hls.attachMedia(videoRef.current);

  hls.on(Hls.Events.MANIFEST_PARSED, (_: string, data: ManifestParsedData) => {
    const levels = data.levels.map((lvl) => lvl.height);
    setQualityLevels(levels);
  });

        hlsRef.current = hls;
      } else if (videoRef.current.canPlayType("application/vnd.apple.mpegurl")) {
        videoRef.current.src = src;
      }
    }

    return () => {
      hlsRef.current?.destroy();
    };
  }, [src]);

  const handleQualityChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const value = event.target.value;
    setSelectedQuality(value === "auto" ? "auto" : parseInt(value));

    if (hlsRef.current) {
      if (value === "auto") {
        hlsRef.current.currentLevel = -1;
      } else {
        const levelIndex = hlsRef.current.levels.findIndex((level) => level.height === parseInt(value));
        if (levelIndex !== -1) hlsRef.current.currentLevel = levelIndex;
      }
    }
  };

  return (
    <div className="relative w-full max-w-4xl mx-auto">
      <video
        ref={videoRef}
        className="w-full h-full rounded-xl shadow-xl object-cover"
        controls
        poster={poster}
      />
      {qualityLevels.length > 0 && (
        <div className="absolute top-2 right-2 z-10">
          <select
            className="bg-black/70 text-white text-sm px-2 py-1 rounded-md"
            value={selectedQuality}
            onChange={handleQualityChange}>
            <option value="auto">Auto</option>
            {qualityLevels.map((quality) => (
              <option
                key={quality}
                value={quality}>
                {quality}p
              </option>
            ))}
          </select>
        </div>
      )}
    </div>
  );
}
