import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  output: "export",
  outputFileTracingRoot: process.cwd(),
  async rewrites() {
    // In development, proxy API requests to Wrangler Pages dev (functions)
    if (process.env.NODE_ENV === "development") {
      return [
        {
          source: "/api/:path*",
          destination: "http://127.0.0.1:8788/api/:path*",
        },
      ];
    }
    return [];
  },
};

export default nextConfig;
