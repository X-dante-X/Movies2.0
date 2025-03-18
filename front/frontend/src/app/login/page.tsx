"use client"

import {
    Button,
    CircularProgress,
    Grid,  
    TextField,
    Typography,
} from '@mui/material'
import { useSnackbar } from 'notistack'
import * as React from 'react'
import { useState } from 'react'
import { useRouter } from "next/navigation"
import { apiClient } from '../api/index' 
import { useLogin } from '../stores/userStore'

function dataTestAttr(id: string) {
  return { 'data-testid': id };
}

function LoginPage() {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [credentialsError, setCredentialsError] = useState<string | undefined>(undefined)
    const { enqueueSnackbar } = useSnackbar()
    const router = useRouter()
    
    const loginMutation = useLogin(apiClient)
    
    async function onSubmit() {
        setCredentialsError(undefined)
        
        try {
            await loginMutation.mutateAsync({ email, password })
            enqueueSnackbar('Login successful', { variant: 'success' })
            router.push("/")
        } catch (error) {
            console.error('Login error:', error)
            setCredentialsError('Wrong credentials')
        }
    }

    return (
        <Grid
            container
            direction="column"
            spacing={4}
            alignItems="center"
            justifyContent="center"
            sx={{ minHeight: '100vh' }} 
        >
            <Grid item>
                <TextField
                    label="Email"
                    value={email}
                    error={!!credentialsError}
                    helperText={credentialsError}
                    onChange={e => setEmail(e.target.value)}
                    disabled={loginMutation.isPending}
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
                    disabled={loginMutation.isPending}
                />
            </Grid>
            <Grid
                item
                container
                direction="row"
                justifyContent="center"
                spacing={4} 
            >
                <Grid item>
                    
                </Grid>
                <Grid item>
                    <Button
                        variant="contained"
                        size="large"
                        onClick={onSubmit}
                        disabled={loginMutation.isPending}
                        {...dataTestAttr('login-login-button')}
                    >
                        Log in
                    </Button>
                </Grid>
            </Grid>
            {loginMutation.isPending && (
                <Grid item>
                    <CircularProgress />
                </Grid>
            )}
            {loginMutation.isError && (
                <Grid item>
                    <Typography color="error">
                        {loginMutation.error instanceof Error 
                            ? loginMutation.error.message 
                            : 'An error occurred during login'}
                    </Typography>
                </Grid>
            )}
        </Grid>
    )
}

export default LoginPage