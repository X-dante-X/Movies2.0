export function useCarouselItemAngle({ index }: { index: number }) {
	const angleStep = 360 / 12
	const angle = -90 + angleStep * index

	const radius = 430

	return { angle, radius }
}
