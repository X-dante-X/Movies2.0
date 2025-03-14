import { ApolloClient, InMemoryCache } from "@apollo/client";

export const apolloClient = new ApolloClient({
    uri: "http://localhost/movie",
    cache: new InMemoryCache(),
});
