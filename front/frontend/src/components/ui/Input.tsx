import React from "react";

interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label: string;
  error?: string;
}

export function Input({ label, error, ...props }: InputProps) {
  return (
    <div className="w-full flex flex-col gap-1">
      <label className="text-white text-sm font-medium">{label}</label>
      <input
        {...props}
        className={`px-4 py-2 rounded-lg bg-white/10 backdrop-blur-md border ${
          error ? "border-red-500" : "border-white/20"
        } text-white placeholder-white/60 focus:outline-none focus:ring-2 focus:ring-white/30`}
      />
      {error && <span className="text-red-500 text-xs mt-1">{error}</span>}
    </div>
  );
}
