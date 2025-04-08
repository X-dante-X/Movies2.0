import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  /* config options here */
  images: {
    remotePatterns: [
      {
        protocol: "http",
        hostname: "localhost",
        pathname: "/minio/uploads/**",
      },
      {
        protocol: "http",
        hostname: "nginx",
        pathname: "/minio/uploads/**",
      },
    ],
  },
};

export default nextConfig;
