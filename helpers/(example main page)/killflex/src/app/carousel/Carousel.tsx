'use client'

import { m } from 'framer-motion'
import { useState } from 'react'

import { mediaData } from '@/media/media.data'

import { CarouselItem } from './CarouselItem'
import { useCarousel } from './hooks/useCarousel'

export function Carousel() {
	const [rotateAngle, setRotateAngle] = useState(0)
	const { isHideOtherCards, updateActiveCard } = useCarousel({ setRotateAngle })

	return (
		<m.div
			className="relative mx-auto mt-60 h-[952px] w-[952px] will-change-transform"
			initial={{
				rotate: 0
			}}
			animate={{
				rotate: rotateAngle ? `${rotateAngle}deg` : 0,
				translateY: isHideOtherCards ? 200 : 0
			}}
			transition={{
				type: 'keyframes',
				ease: 'easeInOut',
				duration: isHideOtherCards ? 1.8 : 1
			}}
		>
			{mediaData.map((media, index) => (
				<CarouselItem
					key={media.id}
					item={media}
					index={index}
					updateActiveCard={updateActiveCard.bind(null, media.id)}
				/>
			))}
		</m.div>
	)
}
