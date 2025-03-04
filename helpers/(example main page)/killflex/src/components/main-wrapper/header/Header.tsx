'use client'

import { Bell, Grip, Search } from 'lucide-react'
import Image from 'next/image'
import { usePathname } from 'next/navigation'
import { useEffect, useState } from 'react'

import { Menu } from './Menu'

const checkMediaPath = (pathname: string | null) => {
	return !pathname?.includes('/media/')
}

export function Header({ pathname }: { pathname: string | null }) {
	const [isShowMenu, setIsShowMenu] = useState(checkMediaPath(pathname))

	const clientPathName = usePathname()

	useEffect(() => {
		setIsShowMenu(checkMediaPath(clientPathName))
	}, [clientPathName])

	return (
		<header className="relative z-1 flex items-center justify-between p-7">
			<div className="flex items-center gap-6">
				<Grip className="hover:text-primary transition-colors" />
				{isShowMenu && <Menu />}
			</div>

			<div className="flex items-center gap-6">
				<Search className="hover:text-primary transition-colors" />
				<Bell className="hover:text-primary transition-colors" />
				<Image
					src="/avatar-killflex.jpg"
					alt="Profile picture"
					height={40}
					width={40}
					className="rounded-full"
				/>
			</div>
		</header>
	)
}
