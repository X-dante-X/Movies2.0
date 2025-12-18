import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";
import { Header } from "@/components/header/Header";
import { Providers } from "./Providers";
import Link from "next/link";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={`${geistSans.variable} ${geistMono.variable} antialiased`}>
        <Providers>
          <Header />
          {children}
          <Link
            href="https://github.com/X-dante-X/Movies2.0/issues/new?template=bug-report.md"
            target="_blank"
            aria-label="Report a bug"
            className="fixed bottom-4 right-4 z-50 flex items-center justify-center w-12 h-12 bg-red-600 text-white rounded-full shadow-lg hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-red-300 transition">
            <span className="sr-only">Report a bug</span>
          </Link>
        </Providers>
      </body>
    </html>
  );
}
