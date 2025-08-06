import React from 'react';
import { cn } from '../../utils/cn';

interface CardProps {
  className?: string;
  children: React.ReactNode;
}

export const Card: React.FC<CardProps> = ({ className, children }) => {
  return (
    <div
      className={cn(
        'bg-white rounded-lg shadow-md overflow-hidden',
        className
      )}
    >
      {children}
    </div>
  );
};

interface CardHeaderProps {
  className?: string;
  children: React.ReactNode;
}

export const CardHeader: React.FC<CardHeaderProps> = ({ className, children }) => {
  return (
    <div
      className={cn(
        'px-6 py-4 border-b border-gray-200',
        className
      )}
    >
      {children}
    </div>
  );
};

interface CardTitleProps {
  className?: string;
  children: React.ReactNode;
}

export const CardTitle: React.FC<CardTitleProps> = ({ className, children }) => {
  return (
    <h3
      className={cn(
        'text-lg font-semibold text-gray-900',
        className
      )}
    >
      {children}
    </h3>
  );
};

interface CardDescriptionProps {
  className?: string;
  children: React.ReactNode;
}

export const CardDescription: React.FC<CardDescriptionProps> = ({ className, children }) => {
  return (
    <p
      className={cn(
        'mt-1 text-sm text-gray-500',
        className
      )}
    >
      {children}
    </p>
  );
};

interface CardBodyProps {
  className?: string;
  children: React.ReactNode;
}

export const CardBody: React.FC<CardBodyProps> = ({ className, children }) => {
  return (
    <div
      className={cn(
        'px-6 py-4',
        className
      )}
    >
      {children}
    </div>
  );
};

interface CardFooterProps {
  className?: string;
  children: React.ReactNode;
}

export const CardFooter: React.FC<CardFooterProps> = ({ className, children }) => {
  return (
    <div
      className={cn(
        'px-6 py-4 border-t border-gray-200 bg-gray-50',
        className
      )}
    >
      {children}
    </div>
  );
};