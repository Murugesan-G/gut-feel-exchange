import { spawn } from "node:child_process";
import { mkdtemp, rm, writeFile } from "node:fs/promises";
import { tmpdir } from "node:os";
import { join } from "node:path";

import { DEFAULT_SEED_DATA_VERSION, PREDICTIONS_KV_KEY } from "../lib/constants";
import { buildSeedPredictions } from "../lib/model";

async function run() {
  const { flags, envName } = parseArgs(process.argv.slice(2));
  const version = process.env.SEED_DATA_VERSION ?? DEFAULT_SEED_DATA_VERSION;

  const storePayload = {
    version,
    predictions: buildSeedPredictions(),
  };

  const bulkEntries = [
    {
      key: PREDICTIONS_KV_KEY,
      value: JSON.stringify(storePayload),
    },
  ];

  const tempDir = await mkdtemp(join(tmpdir(), "cf-seed-"));
  const tempFile = join(tempDir, "kv-seed.json");
  await writeFile(tempFile, JSON.stringify(bulkEntries, null, 2), "utf8");

  const wranglerArgs = [
    "wrangler",
    "kv",
    "bulk",
    "put",
    tempFile,
    "--binding",
    "PREDICTIONS_KV",
  ];

  if (envName) {
    wranglerArgs.push("--env", envName);
  }

  if (flags.preview) {
    wranglerArgs.push("--preview");
  } else {
    wranglerArgs.push("--preview=false");
  }

  if (flags.remote) {
    wranglerArgs.push("--remote");
  }

  await execute("npx", wranglerArgs);

  console.log(
    `Seeded ${storePayload.predictions.length} predictions to KV key ${PREDICTIONS_KV_KEY} (version ${version}).`,
  );

  await rm(tempDir, { recursive: true, force: true });
}

type ParsedArgs = {
  flags: { preview: boolean; remote: boolean };
  envName: string | null;
};

function parseArgs(args: string[]): ParsedArgs {
  let preview = false;
  let remote = false;
  let envName: string | null = null;

  for (let i = 0; i < args.length; i += 1) {
    const current = args[i];
    if (current === "--preview") {
      preview = true;
      continue;
    }
    if (current === "--remote") {
      remote = true;
      continue;
    }
    if ((current === "--env" || current === "-e") && i + 1 < args.length) {
      envName = args[i + 1];
      i += 1;
      continue;
    }
  }

  return {
    flags: { preview, remote },
    envName,
  };
}

async function execute(command: string, args: string[]): Promise<void> {
  await new Promise<void>((resolve, reject) => {
    const child = spawn(command, args, {
      stdio: "inherit",
      shell: false,
    });
    child.on("error", reject);
    child.on("close", (code) => {
      if (code === 0) {
        resolve();
      } else {
        reject(new Error(`${command} ${args.join(" ")} exited with code ${code}`));
      }
    });
  });
}

run().catch((error) => {
  console.error("Failed to seed Cloudflare KV", error);
  process.exitCode = 1;
});
