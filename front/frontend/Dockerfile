FROM oven/bun:latest AS builder

WORKDIR /app

COPY package.json bun.lock ./

RUN bun install --frozen-lockfile

COPY . .

# COPY .env .env

RUN bun run build

FROM oven/bun:latest AS runner

WORKDIR /app

COPY --from=builder /app/.next .next
COPY --from=builder /app/node_modules node_modules
COPY --from=builder /app/package.json package.json
COPY --from=builder /app/.env .env

CMD ["bun", "run", "start"]
