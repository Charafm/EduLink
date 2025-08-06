import React, { useState, useRef, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import { Bell, ChevronDown, Search, UserCircle, LogOut, Settings } from 'lucide-react';
import LanguageSwitcher from '../ui/LanguageSwitcher';

export const Topbar: React.FC = () => {
  const { userType, username, logout } = useAuth();
  const [isProfileMenuOpen, setIsProfileMenuOpen] = useState(false);
  const [isNotificationsOpen, setIsNotificationsOpen] = useState(false);
  const profileMenuRef = useRef<HTMLDivElement>(null);
  const notificationsRef = useRef<HTMLDivElement>(null);

  // Handle clicks outside the dropdown
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (
        profileMenuRef.current &&
        !profileMenuRef.current.contains(event.target as Node)
      ) {
        setIsProfileMenuOpen(false);
      }
      
      if (
        notificationsRef.current &&
        !notificationsRef.current.contains(event.target as Node)
      ) {
        setIsNotificationsOpen(false);
      }
    };

    document.addEventListener('mousedown', handleClickOutside);
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);

  // Determine user's profile path
  const profilePath = userType === 'Student' || userType === 'Parent'
    ? '/frontoffice/profile'
    : '/backoffice/profile';

  return (
    <header className="sticky top-0 z-30 bg-white border-b border-gray-200 shadow-sm">
      <div className="px-4 sm:px-6 lg:px-8">
        <div className="flex h-16 items-center justify-between">
          {/* Left side - Branding (visible only on mobile) */}
          <div className="flex items-center md:hidden">
            <Link to="/" className="flex items-center">
              <UserCircle className="h-8 w-8 text-blue-600" />
              <span className="ml-2 text-xl font-bold text-gray-900">EduLink</span>
            </Link>
          </div>

          {/* Center - Search */}
          <div className="flex-1 flex items-center justify-center px-2 lg:ml-6 lg:justify-end">
            <div className="max-w-lg w-full">
              <label htmlFor="search" className="sr-only">
                Search
              </label>
              <div className="relative">
                <div className="pointer-events-none absolute inset-y-0 left-0 pl-3 flex items-center">
                  <Search className="h-5 w-5 text-gray-400" />
                </div>
                <input
                  id="search"
                  name="search"
                  className="block w-full rounded-md border border-gray-300 bg-white py-2 pl-10 pr-3 text-sm placeholder-gray-500 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500"
                  placeholder="Search"
                  type="search"
                />
              </div>
            </div>
          </div>

          {/* Right side - User menu */}
          <div className="flex items-center space-x-4">
            <LanguageSwitcher /> {/* Add the LanguageSwitcher to the top navigation */}
            {/* Notifications */}
            <div className="relative" ref={notificationsRef}>
              <button
                type="button"
                className="rounded-full bg-white p-1 text-gray-400 hover:text-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                onClick={() => setIsNotificationsOpen(!isNotificationsOpen)}
              >
                <span className="sr-only">View notifications</span>
                <Bell className="h-6 w-6" />
              </button>
              
              {/* Notification indicator */}
              <span className="absolute top-0 right-0 block h-2 w-2 rounded-full bg-red-500 ring-2 ring-white" />

              {/* Notifications dropdown */}
              {isNotificationsOpen && (
                <div className="absolute right-0 mt-2 w-80 origin-top-right rounded-md bg-white py-1 shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none z-50">
                  <div className="px-4 py-2 border-b border-gray-100">
                    <h3 className="text-sm font-medium">Notifications</h3>
                  </div>
                  <div className="max-h-60 overflow-y-auto">
                    {[1, 2, 3].map((item) => (
                      <a
                        key={item}
                        href="#"
                        className="block px-4 py-2 hover:bg-gray-50"
                        onClick={(e) => {
                          e.preventDefault();
                          setIsNotificationsOpen(false);
                        }}
                      >
                        <p className="text-sm font-medium text-gray-900">
                          Notification {item}
                        </p>
                        <p className="text-xs text-gray-500 mt-1">
                          This is a sample notification message.
                        </p>
                      </a>
                    ))}
                  </div>
                  <div className="border-t border-gray-100 px-4 py-2">
                    <a
                      href="#"
                      className="text-xs text-blue-600 hover:text-blue-500"
                      onClick={(e) => {
                        e.preventDefault();
                        setIsNotificationsOpen(false);
                      }}
                    >
                      View all notifications
                    </a>
                  </div>
                </div>
              )}
            </div>

            {/* Profile dropdown */}
            <div className="relative ml-3" ref={profileMenuRef}>
              <div>
                <button
                  type="button"
                  className="flex items-center rounded-full bg-white text-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                  onClick={() => setIsProfileMenuOpen(!isProfileMenuOpen)}
                >
                  <span className="sr-only">Open user menu</span>
                  <div className="h-8 w-8 rounded-full bg-gray-200 flex items-center justify-center">
                    <UserCircle className="h-6 w-6 text-gray-600" />
                  </div>
                  <span className="hidden md:flex md:items-center ml-2">
                    <span className="text-sm font-medium text-gray-700">
                      {username || 'User'}
                    </span>
                    <ChevronDown className="ml-1 h-4 w-4 text-gray-400" />
                  </span>
                </button>
              </div>

              {/* Profile dropdown menu */}
              {isProfileMenuOpen && (
                <div className="absolute right-0 mt-2 w-48 origin-top-right rounded-md bg-white py-1 shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none z-50">
                  <div className="border-b border-gray-100 px-4 py-2">
                    <p className="text-sm font-medium text-gray-900">
                      {username || 'User'}
                    </p>
                    <p className="text-xs text-gray-500">
                      {userType}
                    </p>
                  </div>
                  
                  <Link
                    to={profilePath}
                    className="flex items-center px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                    onClick={() => setIsProfileMenuOpen(false)}
                  >
                    <UserCircle className="mr-2 h-4 w-4" />
                    Your Profile
                  </Link>
                  
                  {userType === 'SchoolAdmin' && (
                    <Link
                      to="/backoffice/settings"
                      className="flex items-center px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                      onClick={() => setIsProfileMenuOpen(false)}
                    >
                      <Settings className="mr-2 h-4 w-4" />
                      Settings
                    </Link>
                  )}
                  
                  <button
                    className="flex w-full items-center px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                    onClick={() => {
                      setIsProfileMenuOpen(false);
                      logout();
                    }}
                  >
                    <LogOut className="mr-2 h-4 w-4" />
                    Sign out
                  </button>
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
    </header>
  );
};