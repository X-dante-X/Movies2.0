import { AnimatePresence, m } from 'framer-motion'
import Image from 'next/image'
import { useState } from 'react'

import { IVideo } from '@/media/media.types'

import { useVideoPlayerStore } from '@/store/video-player.store'

import { episodeAnimation } from '../animation'

interface Props {
	episode: IVideo
}

export function EpisodeItem({ episode }: Props) {
	const { setVideoUrl } = useVideoPlayerStore()
	const [isWhiteOverlay, setIsWhiteOverlay] = useState(false)

	const clickHandler = (videoUrl: string) => {
		setIsWhiteOverlay(true)
		setVideoUrl(videoUrl)

		setTimeout(() => {
			setIsWhiteOverlay(false)
		}, 250)
	}

	return (
		<m.button
			variants={episodeAnimation}
			transition={{
				duration: 1.5,
				type: 'spring',
				bounce: 0.27
			}}
			onClick={() => clickHandler(episode.videoUrl)}
		>
			<AnimatePresence>
				{isWhiteOverlay && (
					<m.div
						initial={{ opacity: 0 }}
						animate={{ opacity: 1 }}
						exit={{ opacity: 0 }}
						transition={{ duration: 0.4 }}
						className="absolute h-[136] w-[243] overflow-hidden rounded-lg bg-white/35"
					/>
				)}
			</AnimatePresence>
			<Image
				src={episode.poster}
				alt={episode.title}
				width={243}
				height={136}
				className="rounded-lg"
				draggable={false}
			/>
			<div className="mt-2 flex items-center gap-2 text-xs">
				<div>{episode.title}</div>
				<div className="opacity-50">• {episode.duration}m</div>
			</div>
		</m.button>
	)
}
