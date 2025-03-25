import { NextRequest, NextResponse } from 'next/server'
import { jwtVerify, JWTPayload} from 'jose'

interface DecodedToken extends JWTPayload {
    IsAdmin: boolean;
}
const SECRET_KEY = new TextEncoder().encode("YourSuperSecretKeyHereMakeItLongEnoughAtLeast32BytesLong1234567890")

export async function middleware(request: NextRequest) {
    const {url } = request;
    const IsAdminPage = url.includes('/admin')
    const token = request.cookies.get("token")?.value;

    if (!token) {
        return NextResponse.redirect(new URL("/login", request.url));
    }
    try {
        const { payload } = await jwtVerify(token, SECRET_KEY);
        const decoded = payload as DecodedToken;

        const isAdmin = String(decoded.IsAdmin ?? "").toLowerCase() === "true";

        if (IsAdminPage && !isAdmin) {
            return NextResponse.redirect(new URL("/404", request.url));
        }
        return NextResponse.next();
    } catch (error) {
        if (error instanceof Error) {
            console.log(`[MIDDLEWARE] Token Verification Error: ${error.message}`);
        }
        return NextResponse.redirect(new URL("/login", request.url));
    }
}

export const config = {
    matcher: ['/admin/:path*']
}