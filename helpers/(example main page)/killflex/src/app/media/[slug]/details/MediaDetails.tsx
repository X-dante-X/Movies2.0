import { m } from 'framer-motion'

import { IMediaItem } from '@/media/media.types'

interface Props {
	mediaItem: IMediaItem
}

export function MediaDetails({ mediaItem }: Props) {
	return (
		<div>
			<m.div
				initial={{ opacity: 0, y: 10 }}
				animate={{ opacity: 1, y: 0 }}
				transition={{ duration: 0.6, delay: 0.8 }}
				className="mb-2 flex items-center gap-3"
			>
				{mediaItem.genres.map(genre => (
					<div
						key={genre}
						className="rounded bg-neutral-900/50 px-2 py-1 text-xs text-white shadow-lg"
					>
						{genre}
					</div>
				))}
			</m.div>
			<m.h1
				initial={{ opacity: 0, y: 25 }}
				animate={{ opacity: 1, y: 0 }}
				transition={{ duration: 0.7, delay: 0.8 }}
				className="mb-3.5 text-6xl font-bold text-white"
				style={{ textShadow: '1px 1px 3px rgba(0, 0, 0, 0.3)' }}
			>
				{mediaItem.title}
			</m.h1>
			<m.div
				initial={{ opacity: 0, y: 25 }}
				animate={{ opacity: 1, y: 0 }}
				transition={{ duration: 0.7, delay: 0.9 }}
				className="flex items-center gap-6"
			>
				<div
					className="text-2xl font-semibold text-white"
					style={{ textShadow: '2px 2px 4px rgba(0, 0, 0, 0.5)' }}
				>
					KillFlex
				</div>
				<div className="flex items-center gap-2">
					<div className="bg-secondary rounded px-2 py-0.5 text-sm font-semibold text-black">
						iMDb
					</div>
					<div className="text-white">{mediaItem.rating.toFixed(1)}/10</div>
				</div>
			</m.div>
		</div>
	)
}
