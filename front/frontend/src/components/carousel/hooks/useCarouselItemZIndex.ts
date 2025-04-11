import { useCarouselStore } from '@/stores/carousel.store'
import { useMediaStore } from '@/stores/mediaData.store'

export function useCarouselItemZIndex({
	index,
	isActive
}: {
	index: number
	isActive: boolean
}) {
	const { mediaItems } = useMediaStore();
	const { activeCardId } = useCarouselStore()
	const activeIndex = mediaItems.findIndex(media => media.id === activeCardId)
	const distanceFromActive = index - activeIndex
	const zIndex = isActive ? 20 : 12 - Math.abs(distanceFromActive)

	return {
		zIndex
	}
}
