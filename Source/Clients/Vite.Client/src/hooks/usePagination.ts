import { useState } from 'react';
import { PageParams } from '../types';

export function usePagination(initialParams: Partial<PageParams> = {}) {
  const [params, setParams] = useState<PageParams>({
    page: initialParams.page || 1,
    size: initialParams.size || 10,
    search: initialParams.search || '',
    sortBy: initialParams.sortBy,
    sortOrder: initialParams.sortOrder,
  });

  const setPage = (page: number) => {
    setParams((prev) => ({ ...prev, page }));
  };

  const setPageSize = (size: number) => {
    setParams((prev) => ({ ...prev, size, page: 1 }));
  };

  const setSearch = (search: string) => {
    setParams((prev) => ({ ...prev, search, page: 1 }));
  };

  const setSort = (sortBy: string) => {
    setParams((prev) => ({
      ...prev,
      sortBy,
      sortOrder:
        prev.sortBy === sortBy && prev.sortOrder === 'asc' ? 'desc' : 'asc',
      page: 1,
    }));
  };

  return {
    params,
    setPage,
    setPageSize,
    setSearch,
    setSort,
  };
}