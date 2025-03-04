'use client'

import { AnimatePresence, m } from 'framer-motion'
import { CSSProperties } from 'react'

import { IMediaItem } from '@/media/media.types'

import { useVideoPlayerStore } from '@/store/video-player.store'

import { backdropAnimation } from './animation'
import { MediaDetails } from './details/MediaDetails'
import { Episodes } from './episodes/Episodes'
import { Products } from './products/Products'
import { useMediaBackdrop } from './useMediaBackdrop'
import { VideoPlayer } from './video-player/VideoPlayer'

interface Props {
	mediaItem: IMediaItem
}

export function MediaPage({ mediaItem }: Props) {
	const { style } = useMediaBackdrop(mediaItem.backdrop)
	const { videoUrl } = useVideoPlayerStore()

	const styleWhenOverlayOpened: CSSProperties = videoUrl
		? {
				position: 'relative',
				zIndex: 2
			}
		: ({} as CSSProperties)

	return (
		<div
			style={{
				perspective: '1500px',
				...styleWhenOverlayOpened
			}}
		>
			<AnimatePresence>{videoUrl && <VideoPlayer />}</AnimatePresence>
			<m.div
				{...backdropAnimation}
				style={style}
				className="relative left-0 z-0 -mt-25 bg-cover bg-no-repeat"
			>
				<div className="absolute bottom-0 left-0 z-1 flex w-full items-end justify-between p-8">
					<MediaDetails mediaItem={mediaItem} />
					<Products />
				</div>
			</m.div>
			<Episodes mediaItem={mediaItem} />
		</div>
	)
}
