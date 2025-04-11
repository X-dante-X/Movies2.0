"use client";

import { Grip, Search } from "lucide-react";

import { Menu } from "./Menu";
import { AuthButtons } from "./AuthButtons";

export function Header() {
  return (
    <header className="relative z-1 flex items-center justify-between p-7">
      <div className="flex items-center gap-6">
        <Grip className="hover:text-primary transition-colors" />
        <Menu />
      </div>

      <div className="flex items-center gap-6">
        <Search className="hover:text-primary transition-colors" />
        <AuthButtons />
      </div>
    </header>
  );
}
