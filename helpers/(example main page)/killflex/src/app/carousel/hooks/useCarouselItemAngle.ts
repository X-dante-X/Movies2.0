import { mediaData } from '@/media/media.data'

export function useCarouselItemAngle({ index }: { index: number }) {
	const angleStep = 360 / mediaData.length
	const angle = -90 + angleStep * index

	const radius = 430

	return { angle, radius }
}
