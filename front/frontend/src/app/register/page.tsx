"use client";

import { Button, CircularProgress, Grid, TextField, Typography } from "@mui/material";
import * as React from "react";
import { useState } from "react";
import { useRouter } from "next/navigation";
import { IAuthRegisterForm } from "@/types/auth.types";
import { authService } from "@/services/auth.service";
import { useMutation } from "@tanstack/react-query";

function dataTestAttr(id: string) {
  return { "data-testid": id };
}

function RegisterPage() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [credentialsError, setCredentialsError] = useState<string | undefined>(undefined);
  const router = useRouter();

  const { mutate, isPending, isError } = useMutation({
    mutationKey: ["auth"],
    mutationFn: (data: IAuthRegisterForm) => authService.register(data),
    onSuccess() {
      router.push("/");
    },
    onError(err) {
      setCredentialsError(err instanceof Error ? err.message : "An error occurred during login");
    },
  });

  function onSubmit() {
    mutate({ username, email, password } as IAuthRegisterForm);
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
          label="Username"
          value={username}
          error={!!credentialsError}
          helperText={credentialsError}
          onChange={(e) => setUsername(e.target.value)}
          disabled={isPending}
        />
      </Grid>
      <Grid item>
        <TextField
          label="Email"
          value={email}
          error={!!credentialsError}
          helperText={credentialsError}
          onChange={(e) => setEmail(e.target.value)}
          disabled={isPending}
        />
      </Grid>
      <Grid item>
        <TextField
          label="Password"
          value={password}
          type="password"
          error={!!credentialsError}
          helperText={credentialsError}
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
            {...dataTestAttr("register-register-button")}>
            Register
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
          <Typography color="error">{credentialsError}</Typography>
        </Grid>
      )}
    </Grid>
  );
}

export default RegisterPage;
