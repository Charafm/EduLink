import { useEffect } from 'react';

export function usePageTitle(title: string) {
  useEffect(() => {
    // Update the page title
    const previousTitle = document.title;
    document.title = `${title} | EduLink SIS`;

    // Restore previous title on unmount
    return () => {
      document.title = previousTitle;
    };
  }, [title]);
}