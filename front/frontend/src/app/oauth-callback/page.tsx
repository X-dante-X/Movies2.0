"use client";

import React, { useState, useEffect, useCallback, Suspense } from "react";
import { useRouter, useSearchParams } from "next/navigation";

export const dynamic = 'force-dynamic';

function OAuthCallbackContent() {
  const router = useRouter();
  const searchParams = useSearchParams();
  const [status, setStatus] = useState<'processing' | 'success' | 'error'>('processing');
  const [errorMessage, setErrorMessage] = useState<string>('');

  const handleOAuthCallback = useCallback(() => {
    try {
      const accessToken = searchParams.get('accessToken');
      const refreshToken = searchParams.get('refreshToken');
      const username = searchParams.get('username');
      const email = searchParams.get('email');
      const isAdmin = searchParams.get('isAdmin');
      const error = searchParams.get('error');

      if (error) {
        console.error('OAuth login failed:', error);
        setErrorMessage('Login failed. Please try again.');
        setStatus('error');
        return;
      }

      if (!accessToken || !email) {
        console.error('Missing required OAuth parameters');
        setErrorMessage('Login incomplete. Please try again.');
        setStatus('error');
        return;
      }

      const authData = {
        accessToken,
        refreshToken: refreshToken || '',
        username: username || '',
        email,
        isAdmin: isAdmin === 'true'
      };

      localStorage.setItem('accessToken', authData.accessToken);
      localStorage.setItem('refreshToken', authData.refreshToken);
      localStorage.setItem('userEmail', authData.email);
      localStorage.setItem('username', authData.username);
      localStorage.setItem('isAdmin', authData.isAdmin.toString());

      console.log('OAuth login successful:', authData);
      setStatus('success');

      setTimeout(() => {
        const redirectUrl = localStorage.getItem('oauth_redirect_url') || '/';
        localStorage.removeItem('oauth_redirect_url');
        router.push(redirectUrl);
      }, 1500);

    } catch (error) {
      console.error('Error processing OAuth callback:', error);
      setErrorMessage('An unexpected error occurred.');
      setStatus('error');
    }
  }, [searchParams, router]);

  useEffect(() => {
    handleOAuthCallback();
  }, [handleOAuthCallback]);

  if (status === 'error') {
    return (
      <div className="flex items-center justify-center h-screen bg-gradient-to-br from-gray-900 to-black">
        <div className="w-full max-w-md p-8 rounded-2xl bg-white/5 backdrop-blur-md border border-white/10 shadow-lg text-center">
          <div className="text-red-500 text-6xl mb-4">⚠️</div>
          <h2 className="text-2xl font-bold text-white mb-4">Login Failed</h2>
          <p className="text-white/70 mb-6">{errorMessage}</p>
          <button
            onClick={() => router.push('/login')}
            className="px-6 py-2 bg-blue-600 hover:bg-blue-700 text-white rounded-lg transition-colors"
          >
            Try Again
          </button>
        </div>
      </div>
    );
  }

  if (status === 'success') {
    return (
      <div className="flex items-center justify-center h-screen bg-gradient-to-br from-gray-900 to-black">
        <div className="w-full max-w-md p-8 rounded-2xl bg-white/5 backdrop-blur-md border border-white/10 shadow-lg text-center">
          <h2 className="text-2xl font-bold text-white mb-4">Login Successful!</h2>
          <p className="text-white/70 mb-6">Redirecting you to the application...</p>
          <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-white mx-auto"></div>
        </div>
      </div>
    );
  }

  return (
    <div className="flex items-center justify-center h-screen bg-gradient-to-br from-gray-900 to-black">
      <div className="w-full max-w-md p-8 rounded-2xl bg-white/5 backdrop-blur-md border border-white/10 shadow-lg text-center">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-white mx-auto mb-4"></div>
        <h2 className="text-2xl font-bold text-white mb-4">Processing Login</h2>
        <p className="text-white/70">Please wait while we complete your authentication...</p>
      </div>
    </div>
  );
}

function LoadingFallback() {
  return (
    <div className="flex items-center justify-center h-screen bg-gradient-to-br from-gray-900 to-black">
      <div className="w-full max-w-md p-8 rounded-2xl bg-white/5 backdrop-blur-md border border-white/10 shadow-lg text-center">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-white mx-auto mb-4"></div>
        <h2 className="text-2xl font-bold text-white mb-4">Loading...</h2>
        <p className="text-white/70">Preparing OAuth callback...</p>
      </div>
    </div>
  );
}

function OAuthCallback() {
  return (
    <Suspense fallback={<LoadingFallback />}>
      <OAuthCallbackContent />
    </Suspense>
  );
}

export default OAuthCallback;