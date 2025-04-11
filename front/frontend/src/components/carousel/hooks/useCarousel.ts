import pageConfig from '@/config/page.config'
import { useCarouselStore } from '@/stores/carousel.store'
import { useMainAnimationStore } from '@/stores/main-animation.store'
import { useMediaStore } from '@/stores/mediaData.store'
import { useRouter } from 'next/navigation'
import { Dispatch, SetStateAction, useLayoutEffect } from 'react'

interface MediaItem {
	id: number
	movieId: number
}

interface Props {
	setRotateAngle: Dispatch<SetStateAction<number>>
}

export function useCarousel({ setRotateAngle }: Props) {
	const { activeCardId, setActiveCardId } = useCarouselStore()
	const { mediaItems } = useMediaStore() as { mediaItems: MediaItem[] }
	const { changeState, resetState, isHideOtherCards } = useMainAnimationStore()
	const router = useRouter()

	const getCardIndex = (cardId: number) =>
		mediaItems.findIndex(media => media.id === cardId)

	useLayoutEffect(() => {
		resetState()
		setActiveCardId(4)
	}, [resetState, setActiveCardId])

	// useEffect(() => {
	// 	mediaItems.forEach(media => {
	// 		router.prefetch(pageConfig.MEDIA(media.movieId.toString()))
	// 	})
	// }, [mediaItems, router])

	const updateActiveCard = (id: number) => {
		if (activeCardId === id) {
			const url = pageConfig.MEDIA(mediaItems[getCardIndex(id)].movieId.toString())

			changeState('isNewPageAnimation', true)
			changeState('isHideHeading', true)
			changeState('isHideOtherCards', true)

			setTimeout(() => {
				router.push(url)
			}, 1300)

			return
		}

		const totalCards = mediaItems.length
		const oldIndex = getCardIndex(activeCardId)
		const newIndex = getCardIndex(id)

		let diff = newIndex - oldIndex

		if (diff > totalCards / 2) {
			diff -= totalCards
		} else if (diff < -totalCards / 2) {
			diff += totalCards
		}

		const newRotateAngle = -(diff * 30)

		setRotateAngle(prev => prev + newRotateAngle)
		setActiveCardId(id)
	}

	return { updateActiveCard, isHideOtherCards }
}
