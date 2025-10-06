"use client"
import React from 'react';
import { CheckCircle, Film, ArrowRight } from 'lucide-react';

export default function EmailVerified() {
  return (
    <div className="min-h-screen bg-gray-50 flex items-center justify-center p-4">
      <div className="max-w-md w-full text-center">
        <div className="flex items-center justify-center mb-8">
          <Film className="w-8 h-8 text-purple-600 mr-2" />
          <h1 className="text-2xl font-bold text-gray-800">Movies<span className="text-purple-600">2.0</span></h1>
        </div>

        <div className="mb-6 flex justify-center">
          <CheckCircle className="w-20 h-20 text-green-600" strokeWidth={2} />
        </div>

        <h2 className="text-2xl font-bold text-gray-800 mb-3">
          Email Verified Successfully!
        </h2>
        
        <p className="text-gray-600 mb-8">
          Your account has been verified. You&apos;re all set to watch movies and shows on Movies2.0!
        </p>
        <button 
          onClick={() => window.location.href = '/'}
          className="bg-purple-600 hover:bg-purple-700 text-white font-semibold py-3 px-8 rounded-lg transition-colors flex items-center justify-center mx-auto"
        >
          <span>Continue to Movies2.0</span>
          <ArrowRight className="ml-2 w-5 h-5" />
        </button>
      </div>
    </div>
  );
}