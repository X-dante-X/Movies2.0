'use client'

import { m } from 'framer-motion'
import { ChevronLeft } from 'lucide-react'
import ReactPlayer from 'react-player'

import { useVideoPlayerStore } from '@/store/video-player.store'

export function VideoPlayer() {
	const { videoUrl, setVideoUrl } = useVideoPlayerStore()

	return (
		<m.div
			initial={{ opacity: 0 }}
			animate={{ opacity: 1 }}
			exit={{ opacity: 0 }}
			transition={{ duration: 0.4 }}
			className="absolute top-0 left-0 z-10 flex h-full w-full items-center justify-center bg-[#222436] backdrop-blur-lg"
		>
			<div className="relative aspect-video h-[85%] w-max overflow-hidden rounded-xl shadow-lg">
				<m.button
					initial={{
						opacity: 0,
						y: -15
					}}
					animate={{
						opacity: 1,
						y: 0
					}}
					exit={{
						opacity: 0,
						y: -15
					}}
					transition={{
						duration: 0.3,
						delay: 0.5
					}}
					onClick={() => setVideoUrl('')}
					className="absolute top-7 left-7 z-20 cursor-pointer text-white"
				>
					<ChevronLeft />
				</m.button>
				<m.div
					initial={{ opacity: 0, scale: 0.8 }}
					animate={{ opacity: 1, scale: 1 }}
					exit={{ opacity: 0, scale: 0.8 }}
					transition={{ duration: 0.4 }}
				>
					<ReactPlayer
						url={videoUrl || ''}
						controls
						width={'100%'}
						height={'100%'}
					/>
				</m.div>
			</div>
		</m.div>
	)
}
