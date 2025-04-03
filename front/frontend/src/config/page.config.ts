class PageConfig {
	HOME = '/'

	MEDIA(slug?: string) {
		return `/movies${slug ? '/' + slug : ''}`
	}

	MOVIES = '/movies'
	TV_SHOWS = '/tv-shows'
	WATCHLIST = '/watchlist'
}

const pageConfigInstance = new PageConfig()
export default pageConfigInstance
