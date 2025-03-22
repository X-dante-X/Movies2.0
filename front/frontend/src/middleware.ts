import { NextRequest, NextResponse } from 'next/server'
import jwt from 'jsonwebtoken' // Import JWT for decoding tokens
import { DecodedToken } from './app/models/responses';

const SECRET_KEY = "YourSuperSecretKeyHereMakeItLongEnoughAtLeast32BytesLong1234567890"

export async function middleware(request: NextRequest) {
	const { url, cookies } = request

	const refreshToken = cookies.get("token")?.value;
	console.log(refreshToken);
	const isAuthPage = url.includes('/login')
	const isMoviesPage = url.includes('/movies')

	if (isAuthPage && refreshToken) {
		return NextResponse.redirect(new URL("/", url))
	}

	if (isAuthPage) {
		return NextResponse.next()
	}

	if (!refreshToken) {
		return NextResponse.redirect(new URL('/login', request.url))
	}

	try {
		const decodedToken  = jwt.verify(refreshToken, SECRET_KEY) as DecodedToken
		const userRole = decodedToken?.IsAdmin;
		console.log(userRole)
		if (isMoviesPage && !userRole) {
			return NextResponse.redirect(new URL("/", url))
		}
	} catch (error) {
		console.log(error)
		return NextResponse.redirect(new URL('/login', request.url))
	}

	return NextResponse.next()
}

export const config = {
	matcher: ['/movies/:path*', '/login/:path', '/i/:path*']
}
