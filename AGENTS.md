# Repository Guidelines

## Project Structure & Module Organization
- `app/` contains App Router sources; `app/page.tsx` is the interactive client page and `app/layout.tsx` provides the shell/metadata.
- Global styles live in `app/globals.css` with Tailwind v4 utilities and shared brutalist helpers.
- Backend routes reside in `functions/` as Cloudflare Pages Functions; shared data persists in Cloudflare KV.
- Static assets are in `public/`.
- Root config: `package.json`, `tsconfig.json`, `eslint.config.mjs`, `next.config.ts`, `postcss.config.mjs`.
- Tests: none yet. Add `*.test.ts(x)` beside code or under `__tests__/`.

## Build, Test, and Development Commands
- `npm install` — install dependencies.
- `npm run dev` — start the Turbopack dev server.
- `npm run build` — create the production build; follow with `npm start` to preview.
- `npm run lint` — run Next.js ESLint and resolve issues before PRs.
- Optional tests (when added): `npx vitest` (unit), `npx playwright test` (E2E).

## Coding Style & Naming Conventions
- TypeScript + React 19; prefer functional, client components unless clearly static.
- Use 2-space indentation. Favor Tailwind utilities and helpers from `app/globals.css` (e.g., `.brutal-card`, `.brutal-btn`).
- File names use kebab-case (e.g., `stake-picker.tsx`, `prediction-store.ts`). Keep Next.js reserved files: `page.tsx`, `layout.tsx`, bracketed routes like `[id]`.
- Constants and storage keys in `UPPER_SNAKE_CASE` (e.g., `LS_KEY`).

## Testing Guidelines
- Frameworks: Vitest + React Testing Library for units; Playwright for E2E.
- Name tests `*.test.ts(x)`; colocate or use `__tests__/`.
- Keep tests deterministic; mock network/Cloudflare bindings as needed. Client state is in-memory per session.

## Commit & Pull Request Guidelines
- Use Conventional Commits, e.g., `feat(app): add stake picker sheet`, `fix(ui): correct progress bar calc`.
- PRs include: concise description, linked issue, before/after UI screenshots, and confirmation that `npm run lint` passes.
- Keep diffs minimal; avoid renames unless necessary. Document new storage keys or persistence changes.

## Security & Configuration Tips
- Treat the app as static UI + Cloudflare Pages Functions; do not ship secrets to the client.
- Bind server-side secrets via Cloudflare environment bindings. Do not commit `.env*`.
- Configure bindings in `wrangler.toml` and the Cloudflare Pages dashboard.
- Avoid browser storage on the client (no `localStorage`, cookies, or IndexedDB). Remove any `.vercel/` artifacts.

