import React from 'react';
import { cn } from '../../utils/cn';
import { ChevronLeft, ChevronRight, ChevronsLeft, ChevronsRight } from 'lucide-react';

interface TableProps extends React.TableHTMLAttributes<HTMLTableElement> {
  children: React.ReactNode;
}

export const Table: React.FC<TableProps> = ({
  className,
  children,
  ...props
}) => {
  return (
    <div className="overflow-x-auto">
      <table
        className={cn(
          'min-w-full divide-y divide-gray-200',
          className
        )}
        {...props}
      >
        {children}
      </table>
    </div>
  );
};

interface TableHeaderProps extends React.HTMLAttributes<HTMLTableSectionElement> {
  children: React.ReactNode;
}

export const TableHeader: React.FC<TableHeaderProps> = ({
  className,
  children,
  ...props
}) => {
  return (
    <thead
      className={cn(
        'bg-gray-50',
        className
      )}
      {...props}
    >
      {children}
    </thead>
  );
};

interface TableBodyProps extends React.HTMLAttributes<HTMLTableSectionElement> {
  children: React.ReactNode;
}

export const TableBody: React.FC<TableBodyProps> = ({
  className,
  children,
  ...props
}) => {
  return (
    <tbody
      className={cn(
        'bg-white divide-y divide-gray-200',
        className
      )}
      {...props}
    >
      {children}
    </tbody>
  );
};

interface TableRowProps extends React.HTMLAttributes<HTMLTableRowElement> {
  children: React.ReactNode;
}

export const TableRow: React.FC<TableRowProps> = ({
  className,
  children,
  ...props
}) => {
  return (
    <tr
      className={cn(
        'hover:bg-gray-50',
        className
      )}
      {...props}
    >
      {children}
    </tr>
  );
};

interface TableCellProps extends React.TdHTMLAttributes<HTMLTableCellElement> {
  children: React.ReactNode;
}

export const TableCell: React.FC<TableCellProps> = ({
  className,
  children,
  ...props
}) => {
  return (
    <td
      className={cn(
        'px-6 py-4 whitespace-nowrap text-sm text-gray-500',
        className
      )}
      {...props}
    >
      {children}
    </td>
  );
};

interface TableHeadProps extends React.ThHTMLAttributes<HTMLTableCellElement> {
  children: React.ReactNode;
  sortable?: boolean;
  sorted?: 'asc' | 'desc' | false;
  onSort?: () => void;
}

export const TableHead: React.FC<TableHeadProps> = ({
  className,
  children,
  sortable,
  sorted,
  onSort,
  ...props
}) => {
  return (
    <th
      className={cn(
        'px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider',
        sortable && 'cursor-pointer select-none',
        className
      )}
      onClick={sortable ? onSort : undefined}
      {...props}
    >
      <div className="flex items-center space-x-1">
        <span>{children}</span>
        {sortable && (
          <span className="inline-flex flex-col">
            <span className={cn(
              'w-2 h-2 transform rotate-180',
              sorted === 'asc' ? 'text-gray-900' : 'text-gray-400'
            )}>▴</span>
            <span className={cn(
              'w-2 h-2',
              sorted === 'desc' ? 'text-gray-900' : 'text-gray-400'
            )}>▴</span>
          </span>
        )}
      </div>
    </th>
  );
};

interface PaginationProps {
  currentPage: number;
  pageSize: number;
  totalItems: number;
  onPageChange: (page: number) => void;
  onPageSizeChange?: (pageSize: number) => void;
  pageSizeOptions?: number[];
  className?: string;
}

export const Pagination: React.FC<PaginationProps> = ({
  currentPage,
  pageSize,
  totalItems,
  onPageChange,
  onPageSizeChange,
  pageSizeOptions = [10, 25, 50, 100],
  className,
}) => {
  const totalPages = Math.ceil(totalItems / pageSize);
  
  // Calculate start and end items for display
  const startItem = (currentPage - 1) * pageSize + 1;
  const endItem = Math.min(currentPage * pageSize, totalItems);
  
  // Generate page buttons (show 5 pages at most)
  const generatePageButtons = () => {
    const buttons = [];
    const maxPageButtons = 5;
    
    let startPage = Math.max(1, currentPage - Math.floor(maxPageButtons / 2));
    let endPage = startPage + maxPageButtons - 1;
    
    if (endPage > totalPages) {
      endPage = totalPages;
      startPage = Math.max(1, endPage - maxPageButtons + 1);
    }
    
    for (let i = startPage; i <= endPage; i++) {
      buttons.push(
        <button
          key={i}
          className={cn(
            'relative inline-flex items-center px-4 py-2 text-sm font-medium',
            currentPage === i
              ? 'z-10 bg-blue-600 text-white focus:z-20 focus:outline-offset-0'
              : 'text-gray-500 bg-white hover:bg-gray-50 focus:z-20'
          )}
          onClick={() => onPageChange(i)}
        >
          {i}
        </button>
      );
    }
    
    return buttons;
  };
  
  if (totalItems === 0) {
    return null;
  }
  
  return (
    <div className={cn(
      'flex items-center justify-between border-t border-gray-200 bg-white px-4 py-3 sm:px-6',
      className
    )}>
      <div className="flex flex-1 justify-between sm:hidden">
        <button
          onClick={() => onPageChange(Math.max(1, currentPage - 1))}
          disabled={currentPage === 1}
          className={cn(
            'relative inline-flex items-center rounded-md px-4 py-2 text-sm font-medium',
            currentPage === 1
              ? 'text-gray-300 cursor-not-allowed'
              : 'text-gray-700 bg-white hover:bg-gray-50'
          )}
        >
          Previous
        </button>
        <button
          onClick={() => onPageChange(Math.min(totalPages, currentPage + 1))}
          disabled={currentPage === totalPages}
          className={cn(
            'relative ml-3 inline-flex items-center rounded-md px-4 py-2 text-sm font-medium',
            currentPage === totalPages
              ? 'text-gray-300 cursor-not-allowed'
              : 'text-gray-700 bg-white hover:bg-gray-50'
          )}
        >
          Next
        </button>
      </div>
      
      <div className="hidden sm:flex sm:flex-1 sm:items-center sm:justify-between">
        <div>
          <p className="text-sm text-gray-700">
            Showing <span className="font-medium">{startItem}</span> to <span className="font-medium">{endItem}</span> of{' '}
            <span className="font-medium">{totalItems}</span> results
          </p>
        </div>
        
        <div className="flex items-center space-x-6">
          {onPageSizeChange && (
            <div className="flex items-center space-x-2">
              <label htmlFor="pageSize" className="text-sm text-gray-700">
                Show
              </label>
              <select
                id="pageSize"
                value={pageSize}
                onChange={(e) => onPageSizeChange(Number(e.target.value))}
                className="rounded border-gray-300 text-sm focus:ring-blue-500 focus:border-blue-500"
              >
                {pageSizeOptions.map((size) => (
                  <option key={size} value={size}>
                    {size}
                  </option>
                ))}
              </select>
            </div>
          )}
          
          <nav className="inline-flex -space-x-px rounded-md shadow-sm" aria-label="Pagination">
            <button
              onClick={() => onPageChange(1)}
              disabled={currentPage === 1}
              className={cn(
                'relative inline-flex items-center rounded-l-md px-2 py-2 text-gray-400',
                currentPage === 1
                  ? 'cursor-not-allowed'
                  : 'hover:bg-gray-50'
              )}
            >
              <span className="sr-only">First</span>
              <ChevronsLeft className="h-5 w-5" />
            </button>
            
            <button
              onClick={() => onPageChange(Math.max(1, currentPage - 1))}
              disabled={currentPage === 1}
              className={cn(
                'relative inline-flex items-center px-2 py-2 text-gray-400',
                currentPage === 1
                  ? 'cursor-not-allowed'
                  : 'hover:bg-gray-50'
              )}
            >
              <span className="sr-only">Previous</span>
              <ChevronLeft className="h-5 w-5" />
            </button>
            
            {generatePageButtons()}
            
            <button
              onClick={() => onPageChange(Math.min(totalPages, currentPage + 1))}
              disabled={currentPage === totalPages}
              className={cn(
                'relative inline-flex items-center px-2 py-2 text-gray-400',
                currentPage === totalPages
                  ? 'cursor-not-allowed'
                  : 'hover:bg-gray-50'
              )}
            >
              <span className="sr-only">Next</span>
              <ChevronRight className="h-5 w-5" />
            </button>
            
            <button
              onClick={() => onPageChange(totalPages)}
              disabled={currentPage === totalPages}
              className={cn(
                'relative inline-flex items-center rounded-r-md px-2 py-2 text-gray-400',
                currentPage === totalPages
                  ? 'cursor-not-allowed'
                  : 'hover:bg-gray-50'
              )}
            >
              <span className="sr-only">Last</span>
              <ChevronsRight className="h-5 w-5" />
            </button>
          </nav>
        </div>
      </div>
    </div>
  );
};