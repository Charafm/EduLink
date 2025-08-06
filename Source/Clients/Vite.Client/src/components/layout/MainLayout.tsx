import React from 'react';
import { Outlet } from 'react-router-dom';
import { Sidebar } from './Sidebar';
import { Topbar } from './Topbar';



import { UserType } from '../../types';
import { useAuth } from '../../context/AuthContext';

export const MainLayout: React.FC = () => {
  const { userType } = useAuth();

  return (
    <div className="flex h-screen bg-gray-50">
      <Sidebar userType={userType as UserType | null} />
      <div className="flex-1 flex flex-col overflow-hidden">
        <Topbar />
        
        <main className="flex-1 overflow-y-auto p-4 sm:p-6 lg:p-8">
          <Outlet />
        </main>
      </div>
    </div>
  );
};