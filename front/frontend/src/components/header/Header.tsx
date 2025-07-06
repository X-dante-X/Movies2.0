"use client";

import { Grip } from "lucide-react";
import { Menu } from "./Menu";
import { AuthButtons } from "./AuthButtons";
import { SearchButton } from "./SearchButton";

export function Header() {
  return (
    <header className="relative z-1 flex items-center justify-between p-7">
      <div className="flex items-center gap-6">
        <Grip className="hover:text-primary transition-colors" />
        <Menu />
      </div>

      <div className="flex items-center gap-6">
        <SearchButton />
        <AuthButtons />
      </div>
    </header>
  );
}
  