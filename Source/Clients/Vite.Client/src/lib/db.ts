// src/lib/db.ts
import axios from 'axios';
import {
  useQuery,
  UseQueryOptions,
  QueryFunctionContext,
  QueryClient,
} from '@tanstack/react-query';

// --- ENV VARS (set in .env) ---
const API_BASE = import.meta.env.VITE_EDULINK_API_BASE as string;
if (!API_BASE) {
  throw new Error('VITE_EDULINK_API_BASE must be defined.');
}

// --- HELPER TO GET RAW TOKEN AT RUNTIME ---
function getRawToken(): string | null {
  return localStorage.getItem('edulink_token');
}

// --- AXIOS INSTANCE ---
const api = axios.create({
  baseURL: API_BASE,
});

// Attach Authorization header on each request
api.interceptors.request.use((config) => {
  const raw = getRawToken();
  if (raw) {
    const headerValue = raw.startsWith('Bearer ') ? raw : `Bearer ${raw}`;
    config.headers = config.headers || {};
    config.headers.Authorization = headerValue;
  }
  return config;
});

// --- CACHE KEYS ---
const CACHE_REGIONS_KEY = 'cache_regions';
const CACHE_CITIES_KEY_PREFIX = 'cache_cities_';

// --- DTO INTERFACES ---
export interface RegionDTO { id: string; nameFr: string; nameAr: string; nameEn: string; }
export interface CityDTO { id: string; nameFr: string; nameAr: string; nameEn: string; regionId: string; }
export interface SchoolMetadataDTO {
  id: string;
  nameFr: string;
  nameAr: string;
  nameEn: string;
  code: string;
  addressFr: string;
  addressAr: string;
  addressEn: string;
  regionId: string;
  regionNameFr: string;
  regionNameAr: string;
  regionNameEn: string;
  cityId: string;
  cityNameFr: string;
  cityNameAr: string;
  cityNameEn: string;
  useIsolatedDatabase: boolean;
  hasCustomConnectionString: boolean;
  logoUrl: string;
  timeZoneId: string;
}

// --- Error Handler ---
function handleApiError(error: unknown): never {
  console.error('API Error:', error);
  throw new Error('Failed to fetch data from server.');
}

// --- RAW FETCH FUNCTIONS WITH LOCALSTORAGE CACHE ---
export async function fetchRegions(): Promise<RegionDTO[]> {
  const cached = localStorage.getItem(CACHE_REGIONS_KEY);
  if (cached) {
    try { return JSON.parse(cached); } catch {    console.error('error fetching cities by region')}
  }
  try {
    const { data } = await api.get<RegionDTO[]>('/referential/regions');
    localStorage.setItem(CACHE_REGIONS_KEY, JSON.stringify(data));
    return data;
  } catch (err) {
    handleApiError(err);
  }
}

export async function fetchCitiesByRegion(
  ctx: QueryFunctionContext<['citiesByRegion', string]>
): Promise<CityDTO[]> {
  const regionId = ctx.queryKey[1];
  const cacheKey = CACHE_CITIES_KEY_PREFIX + regionId;
  const cached = localStorage.getItem(cacheKey);
  if (cached) {
    try { return JSON.parse(cached); } catch {
      console.error('error fetching cities by region')
    }
  }
  try {
    const { data } = await api.get<CityDTO[]>(`/referential/regions/${regionId}/cities`);
    localStorage.setItem(cacheKey, JSON.stringify(data));
    return data;
  } catch (err) {
    handleApiError(err);
  }
}

export async function fetchAllCities(): Promise<CityDTO[]> {
  return fetchRegions().then(regions => Promise.all(
    regions.map(r => fetchCitiesByRegion({
      queryKey: ['citiesByRegion', r.id],
      client: new QueryClient,
      signal: new AbortController().signal,
      meta: undefined
    }))
  ).then(arr => arr.flat()))
}

export async function fetchAllSchools(): Promise<SchoolMetadataDTO[]> {
  try {
    const { data } = await api.get<SchoolMetadataDTO[]>('/school');
    return data;
  } catch (err) {
    handleApiError(err);
  }
}

export async function fetchSchoolsByRegion(
  ctx: QueryFunctionContext<['schoolsByRegion', string]>
): Promise<SchoolMetadataDTO[]> {
  const regionId = ctx.queryKey[1];
  try {
    const { data } = await api.get<SchoolMetadataDTO[]>(`/school/byRegion/${regionId}`);
    return data;
  } catch (err) {
    handleApiError(err);
  }
}

export async function fetchSchoolsByCity(
  ctx: QueryFunctionContext<['schoolsByCity', string]>
): Promise<SchoolMetadataDTO[]> {
  const cityId = ctx.queryKey[1];
  try {
    const { data } = await api.get<SchoolMetadataDTO[]>(`/school/byCity/${cityId}`);
    return data;
  } catch (err) {
    handleApiError(err);
  }
}

// --- Caching Times ---
const STALE_TIME = 1000 * 60 * 30; // 30 minutes

type BaseOptions<TData, TQueryKey extends readonly unknown[]> =
  Omit<UseQueryOptions<TData, Error, TData, TQueryKey>, 'queryKey' | 'queryFn'>;

// --- REACT-QUERY HOOKS ---
export function useRegions(options?: BaseOptions<RegionDTO[], ['regions']>) {
  return useQuery({ queryKey: ['regions'], queryFn: fetchRegions, staleTime: STALE_TIME, ...options });
}

export function useCitiesByRegion(regionId: string, options?: BaseOptions<CityDTO[], ['citiesByRegion', string]>) {
  return useQuery({ queryKey: ['citiesByRegion', regionId], queryFn: fetchCitiesByRegion, enabled: !!regionId, staleTime: STALE_TIME, ...options });
}

export function useAllCities(options?: BaseOptions<CityDTO[], ['cities', 'all']>) {
  return useQuery({ queryKey: ['cities', 'all'], queryFn: fetchAllCities, staleTime: STALE_TIME, ...options });
}

export function useAllSchools(options?: BaseOptions<SchoolMetadataDTO[], ['schools', 'all']>) {
  return useQuery({ queryKey: ['schools', 'all'], queryFn: fetchAllSchools, staleTime: STALE_TIME, ...options });
}

export function useSchoolsByRegion(regionId: string, options?: BaseOptions<SchoolMetadataDTO[], ['schoolsByRegion', string]>) {
  return useQuery({ queryKey: ['schoolsByRegion', regionId], queryFn: fetchSchoolsByRegion, enabled: !!regionId, staleTime: STALE_TIME, ...options });
}

export function useSchoolsByCity(cityId: string, options?: BaseOptions<SchoolMetadataDTO[], ['schoolsByCity', string]>) {
  return useQuery({ queryKey: ['schoolsByCity', cityId], queryFn: fetchSchoolsByCity, enabled: !!cityId, staleTime: STALE_TIME, ...options });
}