import { TFilter } from '@/app/filters/filters.data'
import { create } from 'zustand'

export interface IStore {
	currentFilter: TFilter
	setCurrentFilter: (filter: TFilter) => void
}

export const useFilterStore = create<IStore>(set => ({
	currentFilter: 'Popular',
	setCurrentFilter: filter => set({ currentFilter: filter })
}))
