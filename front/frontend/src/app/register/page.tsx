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
import { useRegister } from '../stores/userStore'
import { EmailUsedError } from '../api/index' 

function dataTestAttr(id: string) {
  return { 'data-testid': id };
}

function RegisterPage() {
    const [username, setUsername] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [credentialsError, setCredentialsError] = useState<string | undefined>(undefined)
    const { enqueueSnackbar } = useSnackbar()
    const router = useRouter()
    
    
    // Use the register mutation hook
    const registerMutation = useRegister(apiClient)
    
    async function onSubmit() {
        // Reset any previous credential errors
        setCredentialsError(undefined)
        
        const userStatus = 0
        
        try {
           const success = await registerMutation.mutateAsync({ 
                username, 
                email, 
                password, 
                userstatus: userStatus 
            })
            console.log(success)
            enqueueSnackbar('Registration successful', { variant: 'success' })
            router.push("/")
        } catch (error) {
            console.error('Registration error:', error)
            
            if (error instanceof EmailUsedError) {
                setCredentialsError('Email already in use')
            } else {
                setCredentialsError('Something went wrong')
            }
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
                    label="Username"
                    value={username}
                    error={!!credentialsError}
                    helperText={credentialsError}
                    onChange={e => setUsername(e.target.value)}
                    disabled={registerMutation.isPending}
                />
            </Grid>
            <Grid item>
                <TextField
                    label="Email"
                    value={email}
                    error={!!credentialsError}
                    helperText={credentialsError}
                    onChange={e => setEmail(e.target.value)}
                    disabled={registerMutation.isPending}
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
                    disabled={registerMutation.isPending}
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
                    <Button
                        variant="contained"
                        size="large"
                        onClick={onSubmit}
                        disabled={registerMutation.isPending}
                        {...dataTestAttr('register-register-button')}
                    >
                        Register
                    </Button>
                </Grid>
            </Grid>
            {registerMutation.isPending && (
                <Grid item>
                    <CircularProgress />
                </Grid>
            )}
            {registerMutation.isError && (
                <Grid item>
                    <Typography color="error">
                        {registerMutation.error instanceof Error 
                            ? registerMutation.error.message 
                            : 'An error occurred during registration'}
                    </Typography>
                </Grid>
            )}
        </Grid>
    )
}

export default RegisterPage