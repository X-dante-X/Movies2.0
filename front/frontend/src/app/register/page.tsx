"use client";

import React, { useState } from "react";
import { useRouter } from "next/navigation";
import { IAuthRegisterForm } from "@/types/auth.types";
import { authService } from "@/services/auth.service";
import { useMutation } from "@tanstack/react-query";
import { Input } from "@/components/ui/Input";
import { Button, ButtonIcon } from "@/components/ui/Button";
import { FaFacebookF, FaGoogle, FaMicrosoft } from "react-icons/fa";

function RegisterPage() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [credentialsError, setCredentialsError] = useState<string | undefined>(undefined);
  const router = useRouter();

  const { mutate, isPending } = useMutation({
    mutationKey: ["auth"],
    mutationFn: (data: IAuthRegisterForm) => authService.register(data),
    onSuccess: () => router.push("/"),
    onError: (err) => {
      setCredentialsError(err instanceof Error ? err.message : "An error occurred during registration");
    },
  });

  function onSubmit() {
    setCredentialsError(undefined);

    if (password !== confirmPassword) {
      setCredentialsError("Passwords do not match");
      return;
    }

    mutate({ username, email, password });
  }

  function handleOAuthLogin(provider: "google" | "microsoft" | "facebook") {
    console.log(`Logging in with ${provider}`);
  }

  return (
    <div className="flex items-center justify-center -mt-20 h-screen bg-gradient-to-br from-gray-900 to-black">
      <div className="w-full max-w-md p-8 rounded-2xl bg-white/5 backdrop-blur-md border border-white/10 shadow-lg">
        <h2 className="text-3xl font-bold text-white text-center mb-6">Register</h2>
        <div className="space-y-4">
          <Input
            label="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            error={credentialsError}
            disabled={isPending}
            placeholder="Enter your username"
          />
          <Input
            label="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            error={credentialsError}
            disabled={isPending}
            placeholder="Enter your email"
            type="email"
          />
          <Input
            label="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            error={credentialsError}
            disabled={isPending}
            placeholder="Enter your password"
            type="password"
          />
          <Input
            label="Confirm Password"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            error={credentialsError}
            disabled={isPending}
            placeholder="Repeat your password"
            type="password"
          />
          <div className="pb-4"></div>
          <div className="flex justify-center">
            <Button
              onClick={onSubmit}
              disabled={isPending}
              data-testid="register-register-button">
              {isPending ? "Registering..." : "Register"}
            </Button>
          </div>
          {credentialsError && <p className="text-red-500 text-sm text-center">{credentialsError}</p>}
          <div className="mt-8">
            <p className="text-white text-center mb-2">Or sign in with</p>
            <div className="flex w-full justify-evenly">
              <ButtonIcon onClick={() => handleOAuthLogin("google")}>
                <FaGoogle />
              </ButtonIcon>
              <ButtonIcon onClick={() => handleOAuthLogin("microsoft")}>
                <FaMicrosoft />
              </ButtonIcon>
              <ButtonIcon onClick={() => handleOAuthLogin("facebook")}>
                <FaFacebookF />
              </ButtonIcon>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default RegisterPage;
