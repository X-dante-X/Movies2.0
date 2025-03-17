"use client"
import {
    Button,
    CircularProgress,
    Grid,  // Using regular Grid
    TextField,
    Typography,
    useTheme,
} from '@mui/material'
import { useSnackbar } from 'notistack'
import * as React from 'react'
import { useState } from 'react'
import { useAsync } from '../hooks/useAsync'
import { useLoginStore } from '../stores/userStore'
import { routes} from '../routes'
import { useRouter } from "next/navigation"; 
function dataTestAttr(id: string) {
  return { 'data-testid': id };
}

function dataTestInputProp(id: string) {
  return { inputProps: { 'data-testid': id } };
}

function LoginPage() {
    const theme = useTheme()
    const loginStore = useLoginStore()
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [credentialsError, setCredentialsError] = useState<
        string | undefined
    >(undefined)
    const { call: logIn, loading, error } = useAsync(loginStore.logIn)
    const { enqueueSnackbar } = useSnackbar()
    const router = useRouter();
    
    async function onSubmit() {
        const success = await logIn(email, password)
        console.log(success)
        if (success === undefined) return

        if (success) {
            enqueueSnackbar('Login successful', { variant: 'success' })
            router.push("/")
        } else {
            setCredentialsError('Wrong credentials')
        }
    }

    // Using regular Grid but ignoring the deprecation warning
    // @ts-ignore
    return (
        <Grid
            container
            direction="column"
            spacing={4}
            alignItems="center"
            justifyContent="center"
            sx={{ minHeight: '100vh' }}  // Using sx prop instead of style
        >
            <Grid item>
                <TextField
                    label="Email"
                    value={email}
                    error={!!credentialsError}
                    helperText={credentialsError}
                    onChange={e => setEmail(e.target.value)}
                />
            </Grid>
            <Grid item>
                <TextField
                    label="Password"
                    value={password}
                    type="password"
                    error={!!credentialsError}
                    helperText={credentialsError}
                    onChange={e => setPassword(e.target.value)}
                />
            </Grid>
            <Grid
                item
                container
                direction="row"
                justifyContent="center"
                spacing={4}  // Using spacing prop instead of gap
            >
                <Grid item>
                    
                </Grid>
                <Grid item>
                    <Button
                        variant="contained"
                        size="large"
                        onClick={onSubmit}
                        {...dataTestAttr('login-login-button')}
                    >
                        Log in
                    </Button>
                </Grid>
            </Grid>
            {loading && (
                <Grid item>
                    <CircularProgress />
                </Grid>
            )}
            {error && (
                <Grid item>
                    <Typography color="error">
                        {error.toString()}
                    </Typography>
                </Grid>
            )}
        </Grid>
    )
}

export default LoginPage