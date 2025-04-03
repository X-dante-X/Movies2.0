import Link from 'next/link'
import { menuData } from './menu.data'

export function Menu() {
	return (
		<ul className="flex items-center gap-10 flex-wrap">
			{menuData.map((item, index) => (
				<li key={index}>
					<Link
						className="transition-colors group hover:text-primary relative"
						href={item.url}
					>
						{item.name}

						<span className="absolute left-0 w-full h-0.5 top-8 transition-colors bg-transparent group-hover:bg-primary" />
					</Link>
				</li>
			))}
		</ul>
	)
}
