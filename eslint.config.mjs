import { dirname } from "path";
import { fileURLToPath } from "url";
import { FlatCompat } from "@eslint/eslintrc";

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

const compat = new FlatCompat({
  baseDirectory: __dirname,
});

const eslintConfig = [
  ...compat.extends("next/core-web-vitals", "next/typescript"),
  {
    ignores: [
      "node_modules/**",
      ".next/**",
      ".wrangler/**",
      "out/**",
      "build/**",
      "next-env.d.ts",
    ],
  },
  {
    files: ["app/**/page.tsx", "app/**/page.ts"],
    rules: {
      // Enforce alias imports in pages: use "@/…" instead of relative paths
      "no-restricted-imports": [
        "error",
        {
          patterns: [
            {
              group: ["./*", "../*", "../../*", "../../../*"],
              message: "Use '@/…' alias for internal imports in page files.",
            },
          ],
        },
      ],
    },
  },
];

export default eslintConfig;
