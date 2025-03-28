"use client";

import Link from "next/link";
import { routes } from "../../src/app/routes";
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
          className="text-xl font-semibold hover:underline">
          Admin
        </Link>
      )}
      <button
        onClick={handleLogout}
        className="text-xl font-semibold hover:underline">
        Logout
      </button>
    </>
  ) : (
    <>
      <Link
        href={routes.login.pattern}
        className="text-xl font-semibold hover:underline">
        Login
      </Link>
      <Link
        href={routes.register.pattern}
        className="text-xl font-semibold hover:underline">
        Register
      </Link>
    </>
  );
}
