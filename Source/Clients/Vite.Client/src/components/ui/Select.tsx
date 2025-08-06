import React from 'react';
import { cn } from '../../utils/cn';

interface SelectOption {
  value: string;
  label: string;
}

interface SelectProps extends Omit<React.SelectHTMLAttributes<HTMLSelectElement>, 'onChange'> {
  label?: string;
  options: SelectOption[];
  helperText?: string;
  error?: string;
  fullWidth?: boolean;
  onChange?: (value: string) => void;
}

export const Select = React.forwardRef<HTMLSelectElement, SelectProps>(
  (
    {
      className,
      label,
      options,
      helperText,
      error,
      fullWidth = false,
      id,
      onChange,
      ...props
    },
    ref
  ) => {
    // Generate a unique ID if none is provided
    const selectId = id || `select-${React.useId()}`;

    const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
      if (onChange) {
        onChange(e.target.value);
      }
    };

    return (
      <div className={cn('mb-4', fullWidth ? 'w-full' : '', className)}>
        {label && (
          <label
            htmlFor={selectId}
            className="block text-sm font-medium text-gray-700 mb-1"
          >
            {label}
          </label>
        )}
        <select
          ref={ref}
          id={selectId}
          className={cn(
            'block w-full rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm',
            'border-gray-300 py-2 pl-3 pr-10',
            error
              ? 'border-red-300 text-red-900 focus:ring-red-500 focus:border-red-500'
              : 'border-gray-300',
            props.disabled && 'bg-gray-100 cursor-not-allowed opacity-75'
          )}
          aria-invalid={error ? 'true' : 'false'}
          aria-describedby={
            error ? `${selectId}-error` : helperText ? `${selectId}-description` : undefined
          }
          onChange={handleChange}
          {...props}
        >
          {props.placeholder && (
            <option value="" disabled>
              {props.placeholder}
            </option>
          )}
          {options.map((option) => (
            <option key={option.value} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>
        {helperText && !error && (
          <p
            id={`${selectId}-description`}
            className="mt-1 text-xs text-gray-500"
          >
            {helperText}
          </p>
        )}
        {error && (
          <p
            id={`${selectId}-error`}
            className="mt-1 text-xs text-red-600"
          >
            {error}
          </p>
        )}
      </div>
    );
  }
);

Select.displayName = 'Select';