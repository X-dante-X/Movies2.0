import { useRouter } from 'next/navigation'
import { Dispatch, SetStateAction, useEffect, useLayoutEffect } from 'react'

import { mediaData } from '@/media/media.data'

import { useCarouselStore } from '@/store/carousel.store'
import { useMainAnimationStore } from '@/store/main-animation.store'

import pageConfig from '@/config/page.config'

interface Props {
	setRotateAngle: Dispatch<SetStateAction<number>>
}

const getCardIndex = (cardId: number) =>
	mediaData.findIndex(media => media.id === cardId)

export function useCarousel({ setRotateAngle }: Props) {
	const { activeCardId, setActiveCardId } = useCarouselStore()
	const { changeState, resetState, isHideOtherCards } = useMainAnimationStore()

	useLayoutEffect(() => {
		resetState()
		setActiveCardId(4)
	}, [])

	const router = useRouter()
	useEffect(() => {
		mediaData.forEach(media => {
			router.prefetch(pageConfig.MEDIA(media.slug))
		})
	}, [])

	const updateActiveCard = (id: number) => {
		if (activeCardId === id) {
			const url = pageConfig.MEDIA(mediaData[getCardIndex(id)].slug)

			changeState('isNewPageAnimation', true)
			changeState('isHideHeading', true)
			changeState('isHideOtherCards', true)

			setTimeout(() => {
				router.push(url)
			}, 1300)

			return
		}

		const totalCards = mediaData.length
		const oldIndex = getCardIndex(activeCardId)
		const newIndex = getCardIndex(id)

		let diff = newIndex - oldIndex

		if (diff > totalCards / 2) {
			diff -= totalCards
		} else if (diff < -totalCards / 2) {
			diff += totalCards
		}

		let newRotateAngle = -(diff * 30)

		setRotateAngle(prev => prev + newRotateAngle)
		setActiveCardId(id)
	}

	return { updateActiveCard, isHideOtherCards }
}
