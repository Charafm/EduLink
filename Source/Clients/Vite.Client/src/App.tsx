import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import { ToastProvider } from './components/ui/Toast';
import { LoginPage } from './pages/auth/LoginPage';
import { MainLayout } from './components/layout/MainLayout';
import { useAuth } from './context/AuthContext';

// Import pages
import { FrontOfficeDashboard } from './pages/frontoffice/Dashboard';
import { EnrollmentWizard } from './pages/frontoffice/EnrollmentWizard';
import { EnrollmentStatus } from './pages/frontoffice/EnrollmentStatus';
import { TransferWizard } from './pages/frontoffice/TransferWizard';
import { TransferStatus } from './pages/frontoffice/TransferStatus';
import { FrontOfficeGrades } from './pages/frontoffice/Grades';
import { FrontOfficeAttendance } from './pages/frontoffice/Attendance';
import { FrontOfficeSchedule } from './pages/frontoffice/Schedule';
import { FrontOfficeResources } from './pages/frontoffice/Resources';
import { FrontOfficeProfile } from './pages/frontoffice/Profile';

import { BackOfficeDashboard } from './pages/backoffice/Dashboard';
import { EnrollmentRequests } from './pages/backoffice/EnrollmentRequests';
import { TransferRequests } from './pages/backoffice/TransferRequests';
import { StudentsManagement } from './pages/backoffice/StudentsManagement';
import  ParentsManagement  from './pages/backoffice/Parents';
import { TeachersManagement } from './pages/backoffice/TeachersManagement';
import  StaffManagement  from './pages/backoffice/Staff';
import  CoursesManagement from './pages/backoffice/Courses';
import  Gradebook  from './pages/backoffice/Gradebook';
import BackOfficeAttendance from './pages/backoffice/Attendance';
import { ScheduleBuilder } from './pages/backoffice/ScheduleBuilder';
import { BackOfficeResources } from './pages/backoffice/Resources';
import { Reports } from './pages/backoffice/Reports';
import { Settings } from './pages/backoffice/Settings';
import { BackOfficeProfile } from './pages/backoffice/Profile';
import Classes from './pages/backoffice/Classes';
import { MyChildren } from './pages/frontoffice/MyChild';

// Protected Route component
interface ProtectedRouteProps {
  children: React.ReactNode;
  userTypes?: string[];
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ 
  children, 
  userTypes = ['Student', 'Parent', 'Staff', 'SchoolAdmin'] 
}) => {
  const { isAuthenticated, userType } = useAuth();

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  if (userType && userTypes.length > 0 && !userTypes.includes(userType)) {
    // User is authenticated, but trying to access unauthorized portal
    if (userType === 'Parent') {
      return <Navigate to="/parent/dashboard" replace />;
    } else if (userType === 'Student') {
      return <Navigate to="/student/dashboard" replace />;
    } else {
      return <Navigate to="/backoffice/dashboard" replace />;
    }
  }

  // Authorized, render the page
  return <>{children}</>;
};

// App Router component
const AppRouter: React.FC = () => {
  return (
    <Routes>
      {/* Public routes */}
      <Route path="/login" element={<LoginPage />} />
      <Route path="/" element={<Navigate to="/login" replace />} />
      
       {/* Parent portal */}
    <Route
      path="/parent"
      element={
        <ProtectedRoute userTypes={['Parent']}>
          <MainLayout />
        </ProtectedRoute>
      }
    >
      <Route path="dashboard" element={<FrontOfficeDashboard />} />
      <Route path="enrollment" element={<EnrollmentWizard />} />
      <Route path="enrollment/status" element={<EnrollmentStatus />} />
      <Route path="transfer" element={<TransferWizard />} />
      <Route path="transfer/status" element={<TransferStatus />} />
      <Route path="mychild" element={<MyChildren />} />
      <Route path="resources" element={<FrontOfficeResources />} />
      <Route path="profile" element={<FrontOfficeProfile />} />
      <Route path="" element={<Navigate to="dashboard" replace />} />
    </Route>

    {/* Student portal */}
    <Route
      path="/student"
      element={
        <ProtectedRoute userTypes={['Student']}>
          <MainLayout />
        </ProtectedRoute>
      }
    >
      <Route path="dashboard" element={<FrontOfficeDashboard />} />
      <Route path="grades" element={<FrontOfficeGrades />} />
      <Route path="attendance" element={<FrontOfficeAttendance />} />
      <Route path="schedule" element={<FrontOfficeSchedule />} />
      <Route path="resources" element={<FrontOfficeResources />} />
      <Route path="profile" element={<FrontOfficeProfile />} />
      <Route path="" element={<Navigate to="dashboard" replace />} />
    </Route>
      {/* Back office protected routes */}
      <Route 
        path="/backoffice" 
        element={
          <ProtectedRoute userTypes={['Staff', 'SchoolAdmin']}>
            <MainLayout />
          </ProtectedRoute>
        }
      >
        <Route path="dashboard" element={<BackOfficeDashboard />} />
        <Route 
          path="enrollment-requests" 
          element={
            <ProtectedRoute userTypes={['SchoolAdmin']}>
              <EnrollmentRequests />
            </ProtectedRoute>
          } 
        />
        <Route 
          path="transfer-requests" 
          element={
            <ProtectedRoute userTypes={['SchoolAdmin']}>
              <TransferRequests />
            </ProtectedRoute>
          } 
        />
        <Route path="classes" element={<Classes />} />
        <Route path="students" element={<StudentsManagement />} />
        <Route path="parents" element={<ParentsManagement />} />
        <Route path="teachers" element={<TeachersManagement />} />
        <Route 
          path="staff" 
          element={
            <ProtectedRoute userTypes={['SchoolAdmin']}>
              <StaffManagement />
            </ProtectedRoute>
          } 
        />
        <Route path="courses" element={<CoursesManagement />} />
        <Route path="gradebook" element={<Gradebook />} />
        <Route path="attendance" element={<BackOfficeAttendance />} />
        <Route path="schedule" element={<ScheduleBuilder />} />
        <Route path="resources" element={<BackOfficeResources />} />
        <Route path="reports" element={<Reports />} />
        <Route 
          path="settings" 
          element={
            <ProtectedRoute userTypes={['SchoolAdmin']}>
              <Settings />
            </ProtectedRoute>
          } 
        />
        <Route path="profile" element={<BackOfficeProfile />} />
        <Route path="" element={<Navigate to="dashboard" replace />} />
      </Route>
      
      {/* Fallback route */}
      <Route path="*" element={<Navigate to="/login" replace />} />
    </Routes>
  );
};

function App() {
  return (
    <Router>
      <AuthProvider>
        <ToastProvider>
          <AppRouter />
        </ToastProvider>
      </AuthProvider>
    </Router>
  );
}

export default App;