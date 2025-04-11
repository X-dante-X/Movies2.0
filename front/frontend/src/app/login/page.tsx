"use client";

import React, { useState } from "react";
import { useRouter } from "next/navigation";
import { useMutation } from "@tanstack/react-query";
import { authService } from "@/services/auth.service";
import { IAuthLoginForm } from "@/types/auth.types";
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";
import { FaGoogle, FaMicrosoft, FaFacebookF } from "react-icons/fa";

function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState<string | undefined>(undefined);
  const router = useRouter();

  const { mutate, isPending } = useMutation({
    mutationKey: ["auth"],
    mutationFn: (data: IAuthLoginForm) => authService.login(data),
    onSuccess: () => router.push("/"),
    onError: (err) => {
      setErrorMessage(err instanceof Error ? err.message : "An error occurred during login");
    },
  });

  function onSubmit() {
    setErrorMessage(undefined);
    mutate({ email, password });
  }

  function handleOAuthLogin(provider: "google" | "microsoft" | "facebook") {
    console.log(`Logging in with ${provider}`);
  }

  return (
    <div className="flex items-center justify-center -mt-20 h-screen bg-gradient-to-br from-gray-900 to-black">
      <div className="w-full max-w-md p-8 rounded-2xl bg-white/5 backdrop-blur-md border border-white/10 shadow-lg">
        <h2 className="text-3xl font-bold text-white text-center mb-6">Login</h2>
        <div className="space-y-4">
          <Input
            label="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            error={errorMessage}
            disabled={isPending}
            placeholder="Enter your email"
            type="email"
          />
          <Input
            label="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            error={errorMessage}
            disabled={isPending}
            placeholder="Enter your password"
            type="password"
          />
          <div className="pb-4"></div>
          <div className="flex justify-center">
            <Button
              onClick={onSubmit}
              disabled={isPending}
              data-testid="login-login-button">
              {isPending ? "Logging in..." : "Log In"}
            </Button>
          </div>
          {errorMessage && <p className="text-red-500 text-sm text-center">{errorMessage}</p>}
          <div className="mt-8">
            <p className="text-white text-center mb-2">Or sign in with</p>
            <div className="flex gap-4 justify-center">
              <Button onClick={() => handleOAuthLogin("google")}>
                <FaGoogle />
              </Button>
              <Button onClick={() => handleOAuthLogin("microsoft")}>
                <FaMicrosoft />
              </Button>
              <Button onClick={() => handleOAuthLogin("facebook")}>
                <FaFacebookF />
              </Button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default LoginPage;
