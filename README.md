# Brutalist Predictions
Live demo: https://prediction-app.pages.dev

A mobile-first prediction voting app with chunky borders, loud colors, and weighted yes/no voting. The UI ships as a static Next.js export served from Cloudflare Pages, while Pages Functions talk to Cloudflare KV for shared prediction data. There is no client-side persistence (no `localStorage`, cookies, or IndexedDB).

## Features
- Weighted voting with stake picks ($1 / $10 / $100 / custom)
- “Trending” view (sorted by total votes) and category filters
- Add your own predictions with an emoji icon and category
- Cloudflare KV-backed persistence so every visitor sees the same list
- Brutalist UI: big buttons, thick borders, playful colors

## Tech Stack
- Next.js 15 (App Router) exported statically to `out/`
- React 19, TypeScript 5
- Tailwind CSS v4 (via `@tailwindcss/postcss`) in `app/globals.css`
- Cloudflare Pages + Pages Functions (Workers runtime with KV binding)

## Quick Start
Prereqs: Node.js LTS and npm.

```bash
npm install
npm run dev
```

Visit http://localhost:3000.

## Scripts
- `npm run dev` — Start the dev server (Turbopack)
- `npm run build` — Production static export to `out/`
- `npm start` — Preview the build locally with Next.js
- `npm run lint` — Lint with ESLint
- `npm run cf:build` — Alias for `npm run build`
- `npm run cf:deploy` — Build then `wrangler pages deploy ./out --project-name=prediction-app`
- `npm run cf:seed` — Seed the Cloudflare KV namespace with starter predictions

## Project Structure
- `app/page.tsx` — Main client component: UI, voting logic, add-prediction sheet
- `app/layout.tsx` — App shell, metadata, font setup
- `app/globals.css` — Tailwind v4 + brutalist theme helpers
- `functions/` — Cloudflare Pages Functions powering `/api/predictions`
- `lib/` — Shared domain helpers and data model
- `public/` — Static assets
- `next.config.ts`, `wrangler.toml` — App + deployment configuration

## Data Flow
- Pages Functions expose `GET /api/predictions` and `POST /api/predictions/:id/vote`, persisting to the `PREDICTIONS_KV` namespace.
- On first run, KV is seeded with playful predictions via `npm run cf:seed` (or automatically when missing).
- The client fetches predictions on load, applies filtering, and keeps the last chosen stake only in memory for the current session (no browser storage).

## Deployment
### Cloudflare Pages
1. Ensure `wrangler.toml` contains your `PREDICTIONS_KV` namespace IDs.
2. Run `npm run cf:seed` if deploying to a fresh namespace.
3. Deploy with `npm run cf:deploy` (builds and runs `wrangler pages deploy ./out --project-name=prediction-app`).
4. Configure the Pages project to use the `./out` directory and the `functions/` folder for backend routes.

### Local Preview
Use `wrangler pages dev ./out --kv PREDICTIONS_KV=<namespace-id>` after `npm run build` for an end-to-end preview with Functions.

## Notes & Limitations
- Static export means no Next.js SSR, ISR, Middleware, or built-in image optimization.
- API traffic goes through Cloudflare Pages Functions; bind KV/Durable Objects in `wrangler.toml` as needed.
- Suitable for workshops and demos; production hardening (auth, quotas, validation) remains to be done.
