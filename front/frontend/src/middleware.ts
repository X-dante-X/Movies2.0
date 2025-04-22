import { NextRequest, NextResponse } from 'next/server'
import { jwtVerify, JWTPayload } from 'jose'
import { EnumTokens } from './services/auth-token.service';

interface DecodedToken extends JWTPayload {
    IsAdmin: boolean;
}

const SECRET_KEY = new TextEncoder().encode(process.env.SECRET_KEY || "")

export async function middleware(request: NextRequest) {
    const { url, cookies } = request

    const token = cookies.get(EnumTokens.ACCESS_TOKEN)?.value
    
    const IsAdminPage = url.includes('/admin')
    const IsUserPage = url.includes('/user')

    if (IsUserPage && !token) {
        return NextResponse.redirect(new URL("/login", request.url));
    }
    if (IsAdminPage) {
        if (!token) {
            return NextResponse.redirect(new URL("/404", request.url));
        }

        try {
            const { payload } = await jwtVerify(token, SECRET_KEY);
            const decoded = payload as DecodedToken;

            const isAdmin = String(decoded.IsAdmin ?? "").toLowerCase() === "true";

            if (!isAdmin) {
                return NextResponse.redirect(new URL("/404", request.url));
            }
        }
        catch (error) {
            if (error instanceof Error) {
                console.log(`[MIDDLEWARE] Token Verification Error: ${error.message}`);
            }
            return NextResponse.redirect(new URL("/404", request.url));
        }
    }

    return NextResponse.next();
}

export const config = {
    matcher: ['/admin/:path*', '/user/:path*']
}
