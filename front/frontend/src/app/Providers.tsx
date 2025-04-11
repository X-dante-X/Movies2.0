"use client";

import { apolloClient } from "@/lib/apolloClient";
import { ApolloProvider } from "@apollo/client";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { LazyMotion, domAnimation } from "framer-motion";
import { PropsWithChildren, useState } from "react";

export function Providers({ children }: PropsWithChildren) {
    const [queryClient] = useState(() => new QueryClient());
    return (
      <LazyMotion features={domAnimation}>
        <QueryClientProvider client={queryClient}>
          <ApolloProvider client={apolloClient}>{children}</ApolloProvider>
        </QueryClientProvider>
      </LazyMotion>
    );
}
