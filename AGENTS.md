# Repository Guidelines

## Project Structure & Module Organization
- `app/` houses the App Router sources; `app/page.tsx` is the interactive client page and `app/layout.tsx` provides the shell and metadata.
- Global styling lives in `app/globals.css`, including Tailwind v4 utilities and brutalist helpers reused across components.
- Static assets sit in `public/`, and build configuration lives at the root (`package.json`, `tsconfig.json`, `eslint.config.mjs`, `next.config.ts`, `postcss.config.mjs`).
- Backend routes live in `functions/` as Cloudflare Pages Functions; shared data persists in Cloudflare KV. The client does not use browser storage (no `localStorage`, cookies, or IndexedDB).

## Build, Test, and Development Commands
- `npm install` installs dependencies.
- `npm run dev` starts the Turbopack-powered dev server.
- `npm run build` creates the production build; follow with `npm start` to preview the build.
- `npm run lint` runs the Next.js ESLint config; resolve warnings before opening a PR.

## Coding Style & Naming Conventions
- TypeScript + React 19 with functional components; keep components client-side unless clearly static.
- Use 2-space indentation and Tailwind utilities; prefer helpers from `app/globals.css` like `.brutal-card`, `.brutal-btn`, and `.chip`.
- Use dash-case (`kebab-case`) for all file names (components, pages, hooks, and utilities), e.g., `stake-picker.tsx`, `prediction-store.ts`. Keep Next.js reserved files as-is (`page.tsx`, `layout.tsx`) and bracketed route segments (`[id]`). Constants and storage keys stay in `UPPER_SNAKE_CASE` (e.g., `LS_KEY`).

## Testing Guidelines
- No tests exist yet; if adding them, prefer Vitest + React Testing Library for units and Playwright for E2E.
- Name tests `*.test.ts(x)` and co-locate with the implementation or use a `__tests__/` directory.
- If testing client state, avoid browser storage mocks; state is in-memory per session.

## Commit & Pull Request Guidelines
- Use Conventional Commits, e.g., `feat(app): add stake picker sheet` or `fix(ui): correct progress bar calc`.
- PRs should include a concise description, linked issue (if any), before/after UI screenshots, and confirmation that `npm run lint` passes.
- Keep diffs minimal and focused; avoid renames unless necessary and document any new storage keys or major behavioral shifts.

## Security & Configuration Tips
- Treat the app as static UI + Cloudflare Pages Functions. Do not ship secrets to the client; bind any server-side secrets via Cloudflare environment bindings.
 - Do not commit `.env*` files. Configure bindings in `wrangler.toml` and the Cloudflare Pages dashboard. Remove any `.vercel/` artifacts.
- When altering persistence behavior, update documentation so newcomers understand breaking changes.
