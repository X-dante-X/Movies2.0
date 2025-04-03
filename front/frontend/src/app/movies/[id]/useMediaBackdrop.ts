import { getBackgroundImage, getClientMinIoUrl } from '@/utils/getMinIoUrl'
import { getImageProps } from 'next/image'
import { CSSProperties } from 'react'

export function useMediaBackdrop(backdropPath: string) {

	const fullPath = getClientMinIoUrl(backdropPath);
	console.log(fullPath);

	const {
		props: { srcSet }
	} = getImageProps({
		alt: '',
		width: 1643,
		height: 692,
		src: fullPath,
		priority: true,
		quality: 100
	})
	const backgroundImage = getBackgroundImage(srcSet)
	const style: CSSProperties = {
		height: 540,
		width: '100%',
		backgroundImage,
		transformStyle: 'preserve-3d'
	}

	return {
		style
	}
}
