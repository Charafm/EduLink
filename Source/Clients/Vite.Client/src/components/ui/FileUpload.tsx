import React, { useState } from 'react';
import { cn } from '../../utils/cn';
import { Button } from './Button';
import { Upload, X, Check, File } from 'lucide-react';

interface FileUploadProps {
  label?: string;
  accept?: string;
  helperText?: string;
  error?: string;
  onChange?: (file: File | null) => void;
  className?: string;
  maxSize?: number; // in MB
  multiple?: boolean;
}

export const FileUpload: React.FC<FileUploadProps> = ({
  label,
  accept,
  helperText,
  error,
  onChange,
  className,
  maxSize = 5, // Default 5MB
  multiple = false,
}) => {
  const [files, setFiles] = useState<File[]>([]);
  const [progress, setProgress] = useState<number>(0);
  const [isUploading, setIsUploading] = useState<boolean>(false);
  const [uploadError, setUploadError] = useState<string | null>(null);
  
  const inputId = `file-upload-${React.useId()}`;

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const selectedFiles = event.target.files;
    if (!selectedFiles?.length) return;
    
    setUploadError(null);
    
    // Check file size
    const newFiles: File[] = [];
    for (let i = 0; i < selectedFiles.length; i++) {
      const file = selectedFiles[i];
      if (file.size > maxSize * 1024 * 1024) {
        setUploadError(`File "${file.name}" exceeds the maximum size of ${maxSize}MB`);
        return;
      }
      newFiles.push(file);
    }
    
    setFiles(multiple ? [...files, ...newFiles] : newFiles);
    
    // Simulate upload process
    simulateUpload(newFiles);
    
    // Call onChange with the first file or all files based on multiple prop
    if (onChange) {
      onChange(multiple ? null : newFiles[0]);
    }
  };

  const simulateUpload = (newFiles: File[]) => {
    setIsUploading(true);
    setProgress(0);
    
    const interval = setInterval(() => {
      setProgress((prev) => {
        const nextProgress = prev + 10;
        if (nextProgress >= 100) {
          clearInterval(interval);
          setIsUploading(false);
          return 100;
        }
        return nextProgress;
      });
    }, 300);
  };

  const removeFile = (index: number) => {
    const newFiles = [...files];
    newFiles.splice(index, 1);
    setFiles(newFiles);
    
    if (onChange && !multiple) {
      onChange(newFiles.length ? newFiles[0] : null);
    }
  };

  return (
    <div className={cn('mb-4', className)}>
      {label && (
        <label 
          htmlFor={inputId}
          className="block text-sm font-medium text-gray-700 mb-1"
        >
          {label}
        </label>
      )}
      
      <div className={cn(
        'border-2 border-dashed rounded-md p-4',
        error || uploadError ? 'border-red-300 bg-red-50' : 'border-gray-300 bg-gray-50'
      )}>
        <div className="flex flex-col items-center justify-center py-4">
          <Upload className="h-10 w-10 text-gray-400 mb-2" />
          <p className="text-sm text-gray-500">
            <span className="font-medium text-blue-600 hover:text-blue-500">
              Click to upload
            </span>{' '}
            or drag and drop
          </p>
          <p className="text-xs text-gray-400 mt-1">
            {accept ? `${accept.split(',').join(', ')} • ` : ''}
            Max {maxSize}MB
          </p>
          <input
            id={inputId}
            type="file"
            className="hidden"
            accept={accept}
            onChange={handleFileChange}
            multiple={multiple}
          />
          <Button
            type="button"
            variant="outline"
            size="sm"
            className="mt-4"
            onClick={() => document.getElementById(inputId)?.click()}
          >
            Select file{multiple ? 's' : ''}
          </Button>
        </div>
        
        {/* File list */}
        {files.length > 0 && (
          <div className="mt-4 space-y-2">
            {files.map((file, index) => (
              <div key={index} className="bg-white rounded border border-gray-200 p-2 flex items-center justify-between">
                <div className="flex items-center space-x-2">
                  <File className="h-4 w-4 text-gray-400" />
                  <span className="text-sm truncate max-w-[200px]">{file.name}</span>
                  <span className="text-xs text-gray-500">
                    {(file.size / 1024).toFixed(0)} KB
                  </span>
                </div>
                <div className="flex items-center space-x-2">
                  {isUploading ? (
                    <div className="w-20 h-1 bg-gray-200 rounded-full overflow-hidden">
                      <div 
                        className="h-full bg-blue-500 rounded-full"
                        style={{ width: `${progress}%` }}
                      />
                    </div>
                  ) : (
                    <Check className="h-4 w-4 text-green-500" />
                  )}
                  <button 
                    type="button"
                    onClick={() => removeFile(index)}
                    className="text-gray-400 hover:text-gray-500"
                  >
                    <X className="h-4 w-4" />
                  </button>
                </div>
              </div>
            ))}
          </div>
        )}
        
        {(error || uploadError) && (
          <p className="mt-2 text-xs text-red-600">
            {error || uploadError}
          </p>
        )}
      </div>
      
      {helperText && !error && !uploadError && (
        <p className="mt-1 text-xs text-gray-500">
          {helperText}
        </p>
      )}
    </div>
  );
};