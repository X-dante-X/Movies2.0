import React from "react";

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  children: React.ReactNode;
}

export function Button({ children, ...props }: ButtonProps) {
  return (
    <button
      {...props}
      className="px-12 py-2 rounded-lg bg-white/10 backdrop-blur-md text-white font-semibold hover:bg-white/20 transition disabled:opacity-50 disabled:cursor-not-allowed">
      {children}
    </button>
  );
}

export function ButtonIcon({ children, ...props }: ButtonProps) {
  return (
    <button
      {...props}
      className="px-12 py-2 rounded-lg bg-white/10 backdrop-blur-md text-white font-semibold hover:bg-white/20 transition disabled:opacity-50 disabled:cursor-not-allowed">
      {children}
    </button>
  );
}