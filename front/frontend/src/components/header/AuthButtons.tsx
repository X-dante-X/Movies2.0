"use client";

import Link from "next/link";
import { routes } from "../../app/routes";
import { useRouter } from "next/navigation";
import { authService } from "@/services/auth.service";
import { useMutation } from "@tanstack/react-query";
import { useAuthStore } from "@/stores/useAuthStore";
import { useEffect, useState } from "react";

export function AuthButtons() {
  const router = useRouter();
  const { user } = useAuthStore();
  const [loading, setLoading] = useState(true);

  const { mutate } = useMutation({
    mutationKey: ["auth"],
    mutationFn: () => authService.logout(),
    onSuccess() {
      router.push("/");
    },
    onError(err) {
      console.error(err);
    },
  });

  const handleLogout = () => {
    mutate();
  };

  useEffect(() => {
    setLoading(false);
  }, [user]);

  if (loading) {
    return (
      <div className="flex space-x-4">
        <div className="w-24 h-8 bg-gray-300 animate-pulse rounded-md"></div>
        <div className="w-24 h-8 bg-gray-300 animate-pulse rounded-md"></div>
      </div>
    );
  }

  return user ? (
    <>
      {user.isAdmin && (
        <Link
          href="/admin"
          className="transition-colors group hover:text-primary relative">
          Admin
          <span className="absolute left-0 w-full h-0.5 top-8 transition-colors bg-transparent group-hover:bg-primary" />
        </Link>
      )}
      <button
        onClick={handleLogout}
        className="transition-colors group hover:text-primary relative">
        Logout
        <span className="absolute left-0 w-full h-0.5 top-8 transition-colors bg-transparent group-hover:bg-primary" />
      </button>
    </>
  ) : (
    <>
      <Link
        href={routes.login.pattern}
        className="transition-colors group hover:text-primary relative">
        Login
        <span className="absolute left-0 w-full h-0.5 top-8 transition-colors bg-transparent group-hover:bg-primary" />
      </Link>
      <Link
        href={routes.register.pattern}
        className="transition-colors group hover:text-primary relative">
        Register
        <span className="absolute left-0 w-full h-0.5 top-8 transition-colors bg-transparent group-hover:bg-primary" />
      </Link>
    </>
  );
}
