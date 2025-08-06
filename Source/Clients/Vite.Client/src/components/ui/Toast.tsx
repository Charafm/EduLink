import React, { useState, useEffect } from 'react';
import { createPortal } from 'react-dom';
import { cn } from '../../utils/cn';
import { X, CheckCircle, AlertCircle, Info, AlertTriangle } from 'lucide-react';

type ToastVariant = 'success' | 'error' | 'info' | 'warning';

interface ToastProps {
  id: string;
  title: string;
  description?: string;
  variant?: ToastVariant;
  duration?: number;
  onClose: (id: string) => void;
}

const Toast: React.FC<ToastProps> = ({
  id,
  title,
  description,
  variant = 'info',
  duration = 5000,
  onClose,
}) => {
  const [isVisible, setIsVisible] = useState(true);
  const [isPaused, setIsPaused] = useState(false);
  const [progress, setProgress] = useState(100);
  
  useEffect(() => {
    if (duration === Infinity) return;
    
    const startTime = Date.now();
    const endTime = startTime + duration;
    
    const progressInterval = setInterval(() => {
      if (isPaused) return;
      
      const now = Date.now();
      const remainingTime = Math.max(0, endTime - now);
      const progressValue = (remainingTime / duration) * 100;
      
      setProgress(progressValue);
      
      if (progressValue <= 0) {
        clearInterval(progressInterval);
        setIsVisible(false);
        setTimeout(() => onClose(id), 300); // Allow time for exit animation
      }
    }, 10);
    
    return () => clearInterval(progressInterval);
  }, [duration, isPaused, id, onClose]);
  
  const variantStyles = {
    success: {
      bg: 'bg-green-50',
      border: 'border-green-200',
      icon: <CheckCircle className="h-5 w-5 text-green-500" />,
      progressBar: 'bg-green-500',
    },
    error: {
      bg: 'bg-red-50',
      border: 'border-red-200',
      icon: <AlertCircle className="h-5 w-5 text-red-500" />,
      progressBar: 'bg-red-500',
    },
    info: {
      bg: 'bg-blue-50',
      border: 'border-blue-200',
      icon: <Info className="h-5 w-5 text-blue-500" />,
      progressBar: 'bg-blue-500',
    },
    warning: {
      bg: 'bg-amber-50',
      border: 'border-amber-200',
      icon: <AlertTriangle className="h-5 w-5 text-amber-500" />,
      progressBar: 'bg-amber-500',
    },
  };
  
  const handleClose = () => {
    setIsVisible(false);
    setTimeout(() => onClose(id), 300); // Allow time for exit animation
  };
  
  return (
    <div
      className={cn(
        'max-w-sm w-full shadow-lg rounded-lg pointer-events-auto overflow-hidden',
        'transition-all duration-300 ease-in-out transform',
        variantStyles[variant].bg,
        'border',
        variantStyles[variant].border,
        isVisible ? 'translate-y-0 opacity-100' : 'translate-y-2 opacity-0'
      )}
      role="alert"
      aria-live="assertive"
      onMouseEnter={() => setIsPaused(true)}
      onMouseLeave={() => setIsPaused(false)}
    >
      <div className="relative">
        <div className="p-4">
          <div className="flex items-start">
            <div className="flex-shrink-0">
              {variantStyles[variant].icon}
            </div>
            <div className="ml-3 w-0 flex-1 pt-0.5">
              <p className="text-sm font-medium text-gray-900">{title}</p>
              {description && (
                <p className="mt-1 text-sm text-gray-500">{description}</p>
              )}
            </div>
            <div className="ml-4 flex-shrink-0 flex">
              <button
                type="button"
                className="bg-transparent rounded-md inline-flex text-gray-400 hover:text-gray-500 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
                onClick={handleClose}
              >
                <span className="sr-only">Close</span>
                <X className="h-5 w-5" />
              </button>
            </div>
          </div>
        </div>
        {/* Progress bar */}
        {duration !== Infinity && (
          <div
            className={cn(
              'h-1 w-full transition-all duration-150 ease-linear',
              variantStyles[variant].progressBar
            )}
            style={{ width: `${progress}%` }}
          />
        )}
      </div>
    </div>
  );
};

// Toast Manager
type ToastOptions = Omit<ToastProps, 'id' | 'onClose'>;

interface ToastContextValue {
  toast: (options: ToastOptions) => string;
  update: (id: string, options: Partial<ToastOptions>) => void;
  dismiss: (id: string) => void;
  dismissAll: () => void;
}

const ToastContext = React.createContext<ToastContextValue | undefined>(undefined);

export const useToast = () => {
  const context = React.useContext(ToastContext);
  if (!context) {
    throw new Error('useToast must be used within a ToastProvider');
  }
  return context;
};

export const ToastProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [toasts, setToasts] = useState<ToastProps[]>([]);
  
  const toast = (options: ToastOptions) => {
    const id = `toast-${Date.now()}-${Math.random().toString(36).substring(2, 9)}`;
    const newToast = { ...options, id, onClose: dismiss };
    setToasts((prev) => [...prev, newToast]);
    return id;
  };
  
  const update = (id: string, options: Partial<ToastOptions>) => {
    setToasts((prev) =>
      prev.map((t) => (t.id === id ? { ...t, ...options } : t))
    );
  };
  
  const dismiss = (id: string) => {
    setToasts((prev) => prev.filter((t) => t.id !== id));
  };
  
  const dismissAll = () => {
    setToasts([]);
  };
  
  // Create portal for toasts
  const [toastContainer, setToastContainer] = useState<HTMLElement | null>(null);
  
  useEffect(() => {
    let container = document.getElementById('toast-container');
    if (!container) {
      container = document.createElement('div');
      container.id = 'toast-container';
      container.className =
        'fixed top-4 right-4 z-50 flex flex-col items-end space-y-4 max-h-screen overflow-hidden pointer-events-none';
      document.body.appendChild(container);
    }
    setToastContainer(container);
    
    return () => {
      if (container && container.parentNode) {
        container.parentNode.removeChild(container);
      }
    };
  }, []);
  
  return (
    <ToastContext.Provider value={{ toast, update, dismiss, dismissAll }}>
      {children}
      {toastContainer &&
        createPortal(
          <div className="flex flex-col items-end space-y-4">
            {toasts.map((toast) => (
              <Toast key={toast.id} {...toast} />
            ))}
          </div>,
          toastContainer
        )}
    </ToastContext.Provider>
  );
};