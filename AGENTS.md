# Repository Guidelines

## Project Structure & Module Organization
- `app/` contains App Router sources; `app/page.tsx` is the interactive client view and `app/layout.tsx` supplies shell and metadata.
- Global styles in `app/globals.css` (Tailwind v4 + brutalist helpers).
- Backend routes live in `functions/` (Cloudflare Pages Functions). Shared data persists in Cloudflare KV.
- Static assets: `public/`.
- Root config: `package.json`, `tsconfig.json`, `eslint.config.mjs`, `next.config.ts`, `postcss.config.mjs`.

## Build and Development Commands
- `npm install` — install dependencies.
- `npm run dev` — start the Turbopack dev server.
- `npm run build` — produce the production build.
- `npm start` — preview the built app locally.
- `npm run lint` — run ESLint; fix or inline-disable with rationale.

## Coding Style & Naming Conventions
- Tech: TypeScript + React 19, functional components. Prefer client components unless clearly static.
- Design: Brutalist. Use utilities and helpers from `app/globals.css` (e.g., `.brutal-card`, `.brutal-btn`) for thick borders, hard shadows, and high contrast.
- Indentation: 2 spaces. Favor Tailwind utilities over custom CSS.
- File names: kebab-case (e.g., `stake-picker.tsx`, `prediction-store.ts`); keep Next.js reserved files and route segments (`page.tsx`, `layout.tsx`, `[id]`).
- Constants/storage keys: `UPPER_SNAKE_CASE` (e.g., `LS_KEY`).

## Commit & Pull Request Guidelines
- Conventional Commits: `feat(app): add stake picker sheet`, `fix(ui): correct progress bar calc`.
- PRs must include: concise description, linked issue, before/after screenshots, and confirmation that `npm run lint` passes.
- Keep diffs minimal; avoid renames unless necessary. Document new KV namespaces, storage keys, or behavioral shifts.


## Security & Configuration Tips
- Treat the app as static UI + Cloudflare Pages Functions. Do not ship secrets to the client.
- Bind server-side secrets via Cloudflare environment bindings; never commit `.env*`.
- Configure bindings in `wrangler.toml` and Cloudflare Pages dashboard.
