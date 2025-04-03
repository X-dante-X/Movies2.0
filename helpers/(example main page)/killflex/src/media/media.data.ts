import { IMediaItem } from './media.types'
import { seasons } from './seasons.data'

export const mediaData: IMediaItem[] = [
	{
		id: 1,
		title: 'Loki',
		slug: 'loki',
		year: 2021,
		rating: 8.2,
		poster: '/posters/1.jpg',
		genres: ['Action', 'Adventure', 'Fantasy'],
		backdrop: '/backdrops/backdrop-1.jpg',
		seasons
	},
	{
		id: 2,
		title: 'The morning show',
		slug: 'the-morning-show',
		year: 2019,
		rating: 8.4,
		poster: '/posters/2.jpg',
		genres: ['Drama'],
		backdrop: '/backdrops/backdrop-2.jpg',
		seasons
	},
	{
		id: 3,
		title: 'You',
		slug: 'you',
		year: 2018,
		rating: 7.7,
		poster: '/posters/3.jpg',
		genres: ['Crime', 'Drama', 'Romance'],
		backdrop: '/backdrops/backdrop-3.jpg',
		seasons
	},
	{
		id: 4,
		title: 'Stranger Things',
		slug: 'stranger-things',
		year: 2016,
		rating: 8.7,
		poster: '/posters/4.jpg',
		genres: ['Drama', 'Fantasy', 'Horror'],
		backdrop: '/backdrops/backdrop-4.jpg',
		seasons
	},
	{
		id: 5,
		title: 'Bridgerton',
		slug: 'bridgerton',
		year: 2020,
		rating: 7.3,
		poster: '/posters/5.jpg',
		genres: ['Drama', 'Romance'],
		backdrop: '/backdrops/backdrop-5.jpg',
		seasons
	},
	{
		id: 6,
		title: 'Emily in Paris',
		slug: 'emily-in-paris',
		year: 2020,
		rating: 7.1,
		poster: '/posters/6.jpg',
		genres: ['Comedy', 'Drama', 'Romance'],
		backdrop: '/backdrops/backdrop-6.jpg',
		seasons
	},
	{
		id: 7,
		title: 'The Boys',
		slug: 'the-boyes',
		year: 2019,
		rating: 8.7,
		poster: '/posters/7.jpg',
		genres: ['Action', 'Comedy', 'Crime'],
		backdrop: '/backdrops/backdrop-7.jpg',
		seasons
	},
	{
		id: 8,
		title: 'The Mandalorian',
		slug: 'the-mandalorian',
		year: 2019,
		rating: 8.8,
		poster: '/posters/8.jpg',
		genres: ['Action', 'Adventure', 'Fantasy'],
		backdrop: '/backdrops/backdrop-8.jpg',
		seasons
	},
	{
		id: 9,
		title: 'Picky Blinders',
		slug: 'picky-blinders',
		year: 2013,
		rating: 8.8,
		poster: '/posters/9.jpg',
		genres: ['Crime', 'Drama'],
		backdrop: '/backdrops/backdrop-9.jpg',
		seasons
	},
	{
		id: 10,
		title: 'Reacher',
		slug: 'reacher',
		year: 2022,
		rating: 8.2,
		poster: '/posters/10.jpg',
		genres: ['Action', 'Adventure', 'Crime'],
		backdrop: '/backdrops/backdrop-10.jpg',
		seasons
	},
	{
		id: 11,
		title: 'Breaking Bad',
		slug: 'breaking-bad',
		year: 2008,
		rating: 9.4,
		poster: '/posters/11.jpg',
		genres: ['Crime', 'Drama', 'Thriller'],
		backdrop: '/backdrops/backdrop-11.jpg',
		seasons
	},
	{
		id: 12,
		title: 'Westworld',
		slug: 'westworld',
		year: 2016,
		rating: 8.6,
		poster: '/posters/12.jpg',
		genres: ['Drama', 'Mystery', 'Sci-Fi'],
		backdrop: '/backdrops/backdrop-12.jpg',
		seasons
	}
]
