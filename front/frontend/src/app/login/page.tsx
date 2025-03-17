
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
import { useAsync } from '../hooks/useAsync'
import { useLoginStore } from '../stores/userStore'
import { useRouter } from "next/navigation"; 
function dataTestAttr(id: string) {
  return { 'data-testid': id };
}

function LoginPage() {
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
                spacing={4} 
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