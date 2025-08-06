import React, { useEffect, useRef } from 'react';
import { createPortal } from 'react-dom';
import { cn } from '../../utils/cn';
import { X } from 'lucide-react';

interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  title?: string;
  description?: string;
  children: React.ReactNode;
  footer?: React.ReactNode;
  size?: 'sm' | 'md' | 'lg' | 'xl' | 'full';
  closeOnOutsideClick?: boolean;
  showCloseButton?: boolean;
  className?: string;
}

export const Modal: React.FC<ModalProps> = ({
  isOpen,
  onClose,
  title,
  description,
  children,
  footer,
  size = 'md',
  closeOnOutsideClick = true,
  showCloseButton = true,
  className,
}) => {
  const modalRef = useRef<HTMLDivElement>(null);
  
  useEffect(() => {
    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.key === 'Escape' && isOpen) {
        onClose();
      }
    };
    
    const handleOutsideClick = (event: MouseEvent) => {
      if (
        closeOnOutsideClick &&
        modalRef.current &&
        !modalRef.current.contains(event.target as Node)
      ) {
        onClose();
      }
    };
    
    if (isOpen) {
      document.addEventListener('keydown', handleKeyDown);
      document.addEventListener('mousedown', handleOutsideClick);
      document.body.style.overflow = 'hidden';
    }
    
    return () => {
      document.removeEventListener('keydown', handleKeyDown);
      document.removeEventListener('mousedown', handleOutsideClick);
      document.body.style.overflow = '';
    };
  }, [isOpen, onClose, closeOnOutsideClick]);
  
  const sizeClasses = {
    sm: 'max-w-sm',
    md: 'max-w-md',
    lg: 'max-w-lg',
    xl: 'max-w-xl',
    full: 'max-w-4xl',
  };
  
  if (!isOpen) return null;
  
  const modalContent = (
    <div
      className="fixed inset-0 z-50 overflow-y-auto"
      aria-labelledby={title ? 'modal-title' : undefined}
      role="dialog"
      aria-modal="true"
    >
      <div className="flex min-h-screen items-center justify-center p-4 text-center">
        {/* Backdrop */}
        <div
          className={cn(
            'fixed inset-0 bg-black bg-opacity-50 transition-opacity',
            isOpen ? 'opacity-100' : 'opacity-0'
          )}
          aria-hidden="true"
        />
        
        {/* Modal panel */}
        <div
          ref={modalRef}
          className={cn(
            'inline-block w-full transform overflow-hidden rounded-lg bg-white text-left align-middle shadow-xl transition-all',
            sizeClasses[size],
            isOpen ? 'opacity-100 scale-100' : 'opacity-0 scale-95',
            className
          )}
        >
          {/* Header */}
          {(title || showCloseButton) && (
            <div className="px-6 py-4 border-b border-gray-200">
              <div className="flex items-center justify-between">
                {title && (
                  <div>
                    <h3
                      id="modal-title"
                      className="text-lg font-medium leading-6 text-gray-900"
                    >
                      {title}
                    </h3>
                    {description && (
                      <p className="mt-1 text-sm text-gray-500">
                        {description}
                      </p>
                    )}
                  </div>
                )}
                {showCloseButton && (
                  <button
                    type="button"
                    className="rounded-md bg-white text-gray-400 hover:text-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                    onClick={onClose}
                  >
                    <span className="sr-only">Close</span>
                    <X className="h-6 w-6" />
                  </button>
                )}
              </div>
            </div>
          )}
          
          {/* Body */}
          <div className="px-6 py-4">{children}</div>
          
          {/* Footer */}
          {footer && (
            <div className="px-6 py-4 bg-gray-50 border-t border-gray-200">
              {footer}
            </div>
          )}
        </div>
      </div>
    </div>
  );
  
  return createPortal(modalContent, document.body);
};