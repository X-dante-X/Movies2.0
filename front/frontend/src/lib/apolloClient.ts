"use client";

import { EnumTokens } from "@/services/auth-token.service";
import { ApolloClient, InMemoryCache } from "@apollo/client";
import createUploadLink from 'apollo-upload-client/createUploadLink.mjs';
import Cookies from "js-cookie";

const token = Cookies.get(EnumTokens.ACCESS_TOKEN);

const httpLink = createUploadLink({
    uri: "http://localhost/movie",
    headers: {
        "GraphQL-Preflight": "1",
        "Authorization": token ? `Bearer ${token}` : "",
    },
});


export const apolloClient = new ApolloClient({
    link: httpLink,
    cache: new InMemoryCache(),
});

export const getClient = () =>
    new ApolloClient({
        uri: "http://localhost/movie",
        cache: new InMemoryCache(),
    });
