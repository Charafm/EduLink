/// <reference types="vite/client" />

declare interface ImportMetaEnv {
  readonly VITE_EDULINK_API_BASE: string;
  readonly VITE_IDENTITY_URL: string;
    readonly VITE_BACKOFFICE_API_BASE: string;
  readonly DEV: boolean;
  readonly PROD: boolean;
  // tokens are obtained at runtime from login API, not via env vars
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}
