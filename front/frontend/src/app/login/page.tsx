"use client";

import { Button, CircularProgress, Grid, TextField, Typography } from "@mui/material";
import * as React from "react";
import { useState } from "react";
import { useRouter } from "next/navigation";
import { useMutation } from "@tanstack/react-query";
import { authService } from "@/services/auth.service";
import { IAuthLoginForm } from "@/types/auth.types";

function dataTestAttr(id: string) {
  return { "data-testid": id };
}

function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState<string | undefined>(undefined);
  const router = useRouter();

  const { mutate, isPending, isError } = useMutation({
    mutationKey: ["auth"],
    mutationFn: (data: IAuthLoginForm) => authService.login(data),
    onSuccess() {
      router.push("/");
    },
    onError(err) {
      setErrorMessage(err instanceof Error ? err.message : "An error occurred during login");
    },
  });

  function onSubmit() {
    setErrorMessage(undefined);
    mutate({ email, password } as IAuthLoginForm);
  }

  return (
    <Grid
      container
      direction="column"
      spacing={4}
      alignItems="center"
      justifyContent="center"
      sx={{ minHeight: "100vh" }}>
      <Grid item>
        <TextField
          label="Email"
          value={email}
          error={!!errorMessage}
          helperText={errorMessage}
          onChange={(e) => setEmail(e.target.value)}
          disabled={isPending}
        />
      </Grid>
      <Grid item>
        <TextField
          label="Password"
          value={password}
          type="password"
          error={!!errorMessage}
          helperText={errorMessage}
          onChange={(e) => setPassword(e.target.value)}
          disabled={isPending}
        />
      </Grid>
      <Grid
        item
        container
        direction="row"
        justifyContent="center"
        spacing={4}>
        <Grid item>
          <Button
            variant="contained"
            size="large"
            onClick={onSubmit}
            disabled={isPending}
            {...dataTestAttr("login-login-button")}>
            Log in
          </Button>
        </Grid>
      </Grid>
      {isPending && (
        <Grid item>
          <CircularProgress />
        </Grid>
      )}
      {isError && (
        <Grid item>
          <Typography color="error">{errorMessage}</Typography>
        </Grid>
      )}
    </Grid>
  );
}

export default LoginPage;
