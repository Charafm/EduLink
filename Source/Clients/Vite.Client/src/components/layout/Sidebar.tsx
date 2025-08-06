import React, { useState } from 'react';
import { NavLink } from 'react-router-dom';
import { cn } from '../../utils/cn';
import { UserType } from '../../types';
import { 
  LayoutDashboard, 
  Users, 
  GraduationCap, 
  UserCheck, 
  BookOpen, 
  FileCheck, 
  Calendar, 
  Package, 
  BarChart4, 
  Settings, 
  UserCircle, 
  Menu, 
  X, 
  ChevronRight,
  Book,
  Clock
} from 'lucide-react';
import { useTranslation } from 'react-i18next';

interface NavigationItem {
  name: string;
  href: string;
  icon: React.ReactNode;
  userTypes: UserType[];
}

interface SidebarProps {
  userType: UserType | null;
}

export const Sidebar: React.FC<SidebarProps> = ({ userType }) => {
  const [isCollapsed, setIsCollapsed] = useState(false);
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
    const { t } = useTranslation();
    const parentNavigation: NavigationItem[] = [
    { 
      name: t('dashboardTitle'), // Use the translated value
      href: '/parent/dashboard', 
      icon: <LayoutDashboard size={20} />, 
      userTypes: ['Parent'] 
    },
    { 
      name: t('enrollmentWizard'), 
      href: '/parent/enrollment', 
      icon: <FileCheck size={20} />, 
      userTypes: ['Parent'] 
    },
    { 
      name: t('transferWizard'), 
      href: '/parent/transfer', 
      icon: <FileCheck size={20} />, 
      userTypes: ['Parent'] 
    },
    { 
      name: t('myChildren'), 
      href: '/parent/mychild', 
      icon: <Users size={20} />, 
      userTypes: ['Parent'] 
    },
    { 
      name: t('requiredResources'), 
      href: '/parent/resources', 
      icon: <Book size={20} />, 
      userTypes: ['Parent'] 
    },
    { 
      name: t('profile'), 
      href: '/parent/profile', 
      icon: <UserCircle size={20} />, 
      userTypes: ['Parent'] 
    },
  ];

  const studentNavigation: NavigationItem[] = [
    { 
      name: t('dashboardTitle'), 
      href: '/student/dashboard', 
      icon: <LayoutDashboard size={20} />, 
      userTypes: ['Student'] 
    },
    { 
      name: t('myGrades'), 
      href: '/student/grades', 
      icon: <BookOpen size={20} />, 
      userTypes: ['Student'] 
    },
    { 
      name: t('myAttendance'), 
      href: '/student/attendance', 
      icon: <Clock size={20} />, 
      userTypes: ['Student'] 
    },
    { 
      name: t('mySchedule'), 
      href: '/student/schedule', 
      icon: <Calendar size={20} />, 
      userTypes: ['Student'] 
    },
    { 
      name: t('resourcesNeeded'), 
      href: '/student/resources', 
      icon: <Book size={20} />, 
      userTypes: ['Student'] 
    },
    { 
      name: t('profile'), 
      href: '/student/profile', 
      icon: <UserCircle size={20} />, 
      userTypes: ['Student'] 
    },
  ];

  const backOfficeNavigation: NavigationItem[] = [
    { 
      name: t('dashboardTitle'), 
      href: '/backoffice/dashboard', 
      icon: <LayoutDashboard size={20} />, 
      userTypes: ['Staff', 'SchoolAdmin'] 
    },
    { 
      name: t('enrollmentRequests'), 
      href: '/backoffice/enrollment-requests', 
      icon: <FileCheck size={20} />, 
      userTypes: ['SchoolAdmin'] 
    },
    { 
      name: t('transferRequests'), 
      href: '/backoffice/transfer-requests', 
      icon: <FileCheck size={20} />, 
      userTypes: ['SchoolAdmin'] 
    },
    { 
      name: t('myClasses'), 
      href: '/backoffice/classes', 
      icon: <BookOpen size={20} />, 
      userTypes: ['Staff'] 
    },
    { 
      name: t('students'), 
      href: '/backoffice/students', 
      icon: <GraduationCap size={20} />, 
      userTypes: ['Staff', 'SchoolAdmin'] 
    },
    { 
      name: t('parents'), 
      href: '/backoffice/parents', 
      icon: <Users size={20} />, 
      userTypes: ['Staff', 'SchoolAdmin'] 
    },
    { 
      name: t('teachers'), 
      href: '/backoffice/teachers', 
      icon: <UserCheck size={20} />, 
      userTypes: ['Staff', 'SchoolAdmin'] 
    },
    { 
      name: t('staff'), 
      href: '/backoffice/staff', 
      icon: <Users size={20} />, 
      userTypes: ['SchoolAdmin'] 
    },
    { 
      name: t('courses'), 
      href: '/backoffice/courses', 
      icon: <BookOpen size={20} />, 
      userTypes: ['Staff', 'SchoolAdmin'] 
    },
    { 
      name: t('gradebook'), 
      href: '/backoffice/gradebook', 
      icon: <BookOpen size={20} />, 
      userTypes: ['Staff', 'SchoolAdmin'] 
    },
    { 
      name: t('attendance'), 
      href: '/backoffice/attendance', 
      icon: <Clock size={20} />, 
      userTypes: ['Staff', 'SchoolAdmin'] 
    },
    { 
      name: t('scheduleBuilder'), 
      href: '/backoffice/schedule', 
      icon: <Calendar size={20} />, 
      userTypes: ['Staff', 'SchoolAdmin'] 
    },
    { 
      name: t('resources'), 
      href: '/backoffice/resources', 
      icon: <Package size={20} />, 
      userTypes: ['Staff', 'SchoolAdmin'] 
    },
    { 
      name: t('reports'), 
      href: '/backoffice/reports', 
      icon: <BarChart4 size={20} />, 
      userTypes: ['Staff', 'SchoolAdmin'] 
    },
    { 
      name: t('settings'), 
      href: '/backoffice/settings', 
      icon: <Settings size={20} />, 
      userTypes: ['SchoolAdmin'] 
    },
    { 
      name: t('profile'), 
      href: '/backoffice/profile', 
      icon: <UserCircle size={20} />, 
      userTypes: ['Staff', 'SchoolAdmin'] 
    },
  ];

  // Determine which navigation to show based on user type
  let navigation: NavigationItem[] = [];
  if (userType === 'Parent') {
    navigation = parentNavigation;
  } else if (userType === 'Student') {
    navigation = studentNavigation;
  } else if (userType === 'Staff' || userType === 'SchoolAdmin') {
    navigation = backOfficeNavigation;
  }

  // Filter navigation based on user type
  const filteredNavigation = navigation.filter(item => 
    item.userTypes.includes(userType as UserType)
  );

  const toggleSidebar = () => {
    setIsCollapsed(!isCollapsed);
  };

  const toggleMobileMenu = () => {
    setIsMobileMenuOpen(!isMobileMenuOpen);
  };

  return (
    <>
      {/* Mobile menu button */}
      <button
        className="fixed bottom-4 right-4 z-50 md:hidden bg-blue-600 text-white rounded-full p-3 shadow-lg"
        onClick={toggleMobileMenu}
      >
        {isMobileMenuOpen ? <X /> : <Menu />}
      </button>

      {/* Desktop sidebar */}
      <aside
        className={cn(
          'hidden md:flex md:flex-col h-screen sticky top-0 bg-white border-r border-gray-200 transition-all duration-300 ease-in-out',
          isCollapsed ? 'w-20' : 'w-64'
        )}
      >
        {/* Logo */}
        <div className="flex items-center p-4 h-16 border-b border-gray-200">
          {!isCollapsed ? (
            <div className="flex items-center">
              <GraduationCap className="h-8 w-8 text-blue-600" />
              <span className="ml-2 text-xl font-bold text-gray-900">EduLink</span>
            </div>
          ) : (
            <GraduationCap className="h-8 w-8 text-blue-600 mx-auto" />
          )}
        </div>

        {/* Navigation */}
        <nav className="flex-1 overflow-y-auto py-4">
          <ul className="space-y-1 px-2">
            {filteredNavigation.map((item) => (
              <li key={item.name}>
                <NavLink
                  to={item.href}
                  className={({ isActive }) =>
                    cn(
                      'flex items-center py-2 px-3 rounded-md transition-colors',
                      isActive
                        ? 'bg-blue-50 text-blue-700'
                        : 'text-gray-600 hover:bg-gray-100 hover:text-gray-900',
                      isCollapsed ? 'justify-center' : ''
                    )
                  }
                >
                  <span className="flex-shrink-0">{item.icon}</span>
                  {!isCollapsed && <span className="ml-3">{item.name}</span>}
                </NavLink>
              </li>
            ))}
          </ul>
        </nav>

        {/* Collapse button */}
        <div className="p-4 border-t border-gray-200">
          <button
            className={cn(
              'flex items-center justify-center w-full py-2 text-sm text-gray-500 hover:text-gray-700 transition-colors',
              isCollapsed ? 'px-0' : 'px-4'
            )}
            onClick={toggleSidebar}
          >
            <ChevronRight
              className={cn(
                'h-5 w-5 transition-transform',
                isCollapsed ? 'rotate-180' : ''
              )}
            />
            {!isCollapsed && <span className="ml-2">Collapse</span>}
          </button>
        </div>
      </aside>

      {/* Mobile sidebar */}
      <div
        className={cn(
          'fixed inset-0 z-40 md:hidden bg-black bg-opacity-50 transition-opacity',
          isMobileMenuOpen ? 'opacity-100' : 'opacity-0 pointer-events-none'
        )}
        onClick={toggleMobileMenu}
      />

      <aside
        className={cn(
          'fixed left-0 top-0 z-40 h-screen w-64 bg-white md:hidden transition-transform transform',
          isMobileMenuOpen ? 'translate-x-0' : '-translate-x-full'
        )}
      >
        <div className="flex items-center p-4 h-16 border-b border-gray-200">
          <div className="flex items-center">
            <GraduationCap className="h-8 w-8 text-blue-600" />
            <span className="ml-2 text-xl font-bold text-gray-900">EduLink</span>
          </div>
        </div>

        <nav className="flex-1 overflow-y-auto py-4">
          <ul className="space-y-1 px-2">
            {filteredNavigation.map((item) => (
              <li key={item.name}>
                <NavLink
                  to={item.href}
                  className={({ isActive }) =>
                    cn(
                      'flex items-center py-2 px-3 rounded-md transition-colors',
                      isActive
                        ? 'bg-blue-50 text-blue-700'
                        : 'text-gray-600 hover:bg-gray-100 hover:text-gray-900'
                    )
                  }
                  onClick={toggleMobileMenu}
                >
                  <span className="flex-shrink-0">{item.icon}</span>
                  <span className="ml-3">{item.name}</span>
                </NavLink>
              </li>
            ))}
          </ul>
        </nav>
      </aside>
    </>
  );
};