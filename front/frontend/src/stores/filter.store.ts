import { TFilter } from '@/components/filters/filters.data'
import { create } from 'zustand'

export interface IFilterStore {
	currentFilter: TFilter
	setCurrentFilter: (filter: TFilter) => void
}
// Creates Zustand for the filter storage.
export const useFilterStore = create<IFilterStore>(set => ({
	currentFilter: 'Popular',
	setCurrentFilter: filter => set({ currentFilter: filter })
}))
