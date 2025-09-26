# Brutalist Predictions (Now With 1000% More Vibes)
Live demo: https://prediction-app.pages.dev

A mobile-first prediction toy where you bet fake dollars on extremely serious questions like “Will the printer jam while the boss is watching?” The borders are thick. The colors are loud. The confidence is imaginary. The data lives in a communal cloud brain. What could possibly go wrong?

No client storage is used (we ate all the cookies). Your “last stake” lives only in memory until refresh, like a goldfish with a to-do list.

## Features (Absurdly Honest Edition)
- Weighted voting ($1 / $10 / $100 / custom) — because nothing says “statistical rigor” like integers and vibes.
- “Trending” tab — sorted by total votes a.k.a. “people clicked this a lot”.
- Add your own predictions — one emoji per prediction to prevent emoji monopolies.
- Cloudflare KV persistence — a shared chalkboard in the sky so everyone sees the same list.
- Brutalist UI — borders so chunky they have their own gravity.

## Tech Stack (AKA The Ingredients)
- Next.js 15 (App Router) with `output: "export"` → static site in `out/`.
- React 19 + TypeScript 5 — modern enough to feel slightly dangerous.
- Tailwind CSS v4 via `@tailwindcss/postcss` — see `app/globals.css` for brutalist helpers.
- Cloudflare Pages + Pages Functions — API lives with the static site; data in KV.

## How It Works (Tiny Tour)
- UI ships as static files from Cloudflare Pages.
- Pages Functions expose:
  - `GET /api/predictions` — fetch every prediction.
  - `POST /api/predictions` — add a new prediction (emoji, category, question).
  - `POST /api/predictions/:id/vote` — record a vote with a stake.
- Data is stored under the KV key `predictions/all` (see `lib/predictions.ts`). New predictions start 50/50 to keep the peace.

## Quick Start (3 Steps, Zero Drama)
Prereqs: Node.js LTS and npm.

```bash
npm install
npm run dev
```

Visit http://localhost:3000 and make irresponsible prophecies responsibly.

## Scripts (Greatest Hits)
- `npm run dev` — Start the dev server (Turbopack).
- `npm run build` — Produce static export into `out/`.
- `npm start` — Preview the production build locally.
- `npm run lint` — Run ESLint; fix things before opening a PR.
- `npm run cf:build` — Same as `npm run build`.
- `npm run cf:deploy` — Build then `wrangler pages deploy ./out --project-name=prediction-app`.
- `npm run cf:seed` — Seed KV with some delightful nonsense.

## Project Map (Where Stuff Lives)
- `app/page.tsx` — UI, voting logic, “new prediction” sheet.
- `app/layout.tsx` — Shell + metadata.
- `app/globals.css` — Tailwind v4 + brutalist helpers (`.brutal-card`, `.brutal-btn`, `.chip`).
- `functions/` — Cloudflare Pages Functions for `/api/predictions`.
- `lib/` — Data model and utilities.
- `public/` — Static assets.
- `next.config.ts`, `wrangler.toml` — Build and deployment settings.

## Cloudflare Setup (Deploy Without Tears)
1. Create a KV namespace and bind it as `PREDICTIONS_KV` in `wrangler.toml`.
   - Tip: `npx wrangler kv namespace create PREDICTIONS_KV`
2. Optional but recommended: seed your namespace.
   - `npm run cf:seed`
3. Build and deploy to Pages.
   - `npm run cf:deploy` → runs build, then `wrangler pages deploy ./out`.
4. In the Pages project, set the build output directory to `./out` and point backend routes at `functions/`.

### Local End‑to‑End Preview
After `npm run build`, you can run:

```bash
wrangler pages dev ./out --kv PREDICTIONS_KV=<namespace-id>
```

## Notes & Limitations (Disclaimer Zone)
- Static export only: no SSR/ISR/Middleware/Image Optimization. All beef, no server.
- API requests go through Pages Functions; configure bindings in `wrangler.toml`.
- This is a workshop/demo project. Production hardening (auth, quotas, input validation that scowls at you) is still pending.

## FAQ (Frequently Anticipated Quibbles)
- Is this gambling?
  - No. It’s a vibes-based scoreboard with play money.
- Why no login or persistent state?
  - The client intentionally stores nothing. Each visit is a fresh start; it’s minimalist… or forgetful.
- Why “Brutalist”?
  - Because these borders will survive the heat death of the universe.
- Dark mode?
  - Light mode only, but the borders are basically sunglasses.

## Contributing (Bring Your Own Jokes)
- Use Conventional Commits: `feat(app): add stake picker`.
- Run `npm run lint` and keep diffs focused.
- Keep components client-side unless obviously static.
- Don’t ship secrets to the client; configure Cloudflare bindings instead. No `.env*` files in git, pretty please.

## License & Safety
- No real money. No financial advice. Predict responsibly.
- License: if absent, assume “look but don’t sue”; otherwise see `LICENSE`.
