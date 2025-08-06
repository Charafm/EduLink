import React from 'react';
import { useAuth } from '../../context/AuthContext';
import { Card, CardBody, CardHeader, CardTitle } from '../../components/ui/Card';
import { GraduationCap, BookOpen, Bell, Calendar, Clock, Users, File, FileCheck } from 'lucide-react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { 
  ResponsiveContainer, 
  BarChart, 
  Bar, 
  XAxis, 
  YAxis, 
  CartesianGrid, 
  Tooltip, 
  LineChart, 
  Line 
} from 'recharts';
import { useTranslation } from 'react-i18next';

const attendanceData = [
  { month: 'Sep', present: 20, absent: 1, late: 1 },
  { month: 'Oct', present: 19, absent: 2, late: 1 },
  { month: 'Nov', present: 21, absent: 0, late: 1 },
  { month: 'Dec', present: 15, absent: 1, late: 0 },
  { month: 'Jan', present: 18, absent: 2, late: 2 },
  { month: 'Feb', present: 20, absent: 0, late: 0 }
];

const gradesData = [
  { subject: 'Math', grade: 85 },
  { subject: 'Science', grade: 92 },
  { subject: 'Language', grade: 78 },
  { subject: 'History', grade: 88 },
  { subject: 'Art', grade: 95 },
  { subject: 'PE', grade: 90 }
];

export const FrontOfficeDashboard: React.FC = () => {
  usePageTitle('Dashboard');
  const { username } = useAuth();
  const { t } = useTranslation();

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-3">
        <h1 className="text-2xl font-bold text-gray-900">{t('dashboardTitle')}</h1>
        <p className="text-sm text-gray-500">
          {t('dashboardWelcome', { username })}
        </p>
      </div>

      {/* Stats Cards */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        <Card className="bg-gradient-to-br from-blue-50 to-blue-100 border-blue-200">
          <CardBody className="flex items-center p-6">
            <div className="rounded-full bg-blue-500 p-3 mr-4">
              <BookOpen className="h-6 w-6 text-white" />
            </div>
            <div>
              <p className="text-sm font-medium text-blue-800">{t('currentGPA')}</p>
              <p className="text-2xl font-bold text-blue-900">19.23</p>
            </div>
          </CardBody>
        </Card>

        <Card className="bg-gradient-to-br from-green-50 to-green-100 border-green-200">
          <CardBody className="flex items-center p-6">
            <div className="rounded-full bg-green-500 p-3 mr-4">
              <Clock className="h-6 w-6 text-white" />
            </div>
            <div>
              <p className="text-sm font-medium text-green-800">{t('attendanceRate')}</p>
              <p className="text-2xl font-bold text-green-900">96%</p>
            </div>
          </CardBody>
        </Card>

        <Card className="bg-gradient-to-br from-amber-50 to-amber-100 border-amber-200">
          <CardBody className="flex items-center p-6">
            <div className="rounded-full bg-amber-500 p-3 mr-4">
              <Calendar className="h-6 w-6 text-white" />
            </div>
            <div>
              <p className="text-sm font-medium text-amber-800">{t('classesToday')}</p>
              <p className="text-2xl font-bold text-amber-900">5</p>
            </div>
          </CardBody>
        </Card>

        <Card className="bg-gradient-to-br from-purple-50 to-purple-100 border-purple-200">
          <CardBody className="flex items-center p-6">
            <div className="rounded-full bg-purple-500 p-3 mr-4">
              <Bell className="h-6 w-6 text-white" />
            </div>
            <div>
              <p className="text-sm font-medium text-purple-800">{t('pendingAssignments')}</p>
              <p className="text-2xl font-bold text-purple-900">3</p>
            </div>
          </CardBody>
        </Card>
      </div>

      {/* Charts */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <Card>
          <CardHeader>
            <CardTitle>{t('attendanceOverview')}</CardTitle>
          </CardHeader>
          <CardBody className="p-4">
            <div className="h-72">
              <ResponsiveContainer width="100%" height="100%">
                <BarChart data={attendanceData} margin={{ top: 20, right: 30, left: 0, bottom: 5 }}>
                  <CartesianGrid strokeDasharray="3 3" />
                  <XAxis dataKey="month" />
                  <YAxis />
                  <Tooltip />
                  <Bar dataKey="present" stackId="a" fill="#4ade80" name={t('present')} />
                  <Bar dataKey="late" stackId="a" fill="#facc15" name={t('late')} />
                  <Bar dataKey="absent" stackId="a" fill="#f87171" name={t('absent')} />
                </BarChart>
              </ResponsiveContainer>
            </div>
          </CardBody>
        </Card>

        <Card>
          <CardHeader>
            <CardTitle>{t('currentGrades')}</CardTitle>
          </CardHeader>
          <CardBody className="p-4">
            <div className="h-72">
              <ResponsiveContainer width="100%" height="100%">
                <LineChart data={gradesData} margin={{ top: 20, right: 30, left: 0, bottom: 5 }}>
                  <CartesianGrid strokeDasharray="3 3" />
                  <XAxis dataKey="subject" />
                  <YAxis domain={[0, 100]} />
                  <Tooltip />
                  <Line type="monotone" dataKey="grade" stroke="#3b82f6" strokeWidth={2} />
                </LineChart>
              </ResponsiveContainer>
            </div>
          </CardBody>
        </Card>
      </div>

      {/* Recent Activity */}
      <Card>
        <CardHeader>
          <CardTitle>{t('recentActivity')}</CardTitle>
        </CardHeader>
        <CardBody>
          <div className="space-y-4">
            <div className="flex items-start">
              <div className="flex-shrink-0 mt-1">
                <div className="bg-blue-100 rounded-full p-2">
                  <File className="h-4 w-4 text-blue-600" />
                </div>
              </div>
              <div className="ml-3">
                <p className="text-sm font-medium text-gray-900">{t('activityMathAssignmentGradedTitle')}</p>
                <p className="text-xs text-gray-500">Hier a 2:30 PM</p>
                <p className="mt-1 text-sm text-gray-600">{t('activityMathAssignmentGradedDescription')}</p>
              </div>
            </div>
            
            <div className="flex items-start">
              <div className="flex-shrink-0 mt-1">
                <div className="bg-green-100 rounded-full p-2">
                  <FileCheck className="h-4 w-4 text-green-600" />
                </div>
              </div>
              <div className="ml-3">
                <p className="text-sm font-medium text-gray-900">{t('activityScienceProjectSubmittedTitle')}</p>
                <p className="text-xs text-gray-500">2 days ago at 11:45 AM</p>
                <p className="mt-1 text-sm text-gray-600">{t('activityScienceProjectSubmittedDescription')}</p>
              </div>
            </div>
            
            <div className="flex items-start">
              <div className="flex-shrink-0 mt-1">
                <div className="bg-purple-100 rounded-full p-2">
                  <Users className="h-4 w-4 text-purple-600" />
                </div>
              </div>
              <div className="ml-3">
                <p className="text-sm font-medium text-gray-900">{t('activityCourseRegistrationOpenTitle')}</p>
                <p className="text-xs text-gray-500">3 days ago at 9:15 AM</p>
                <p className="mt-1 text-sm text-gray-600">{t('activityScienceProjectSubmittedDescription')}</p>
              </div>
            </div>
            
            <div className="flex items-start">
              <div className="flex-shrink-0 mt-1">
                <div className="bg-amber-100 rounded-full p-2">
                  <GraduationCap className="h-4 w-4 text-amber-600" />
                </div>
              </div>
              <div className="ml-3">
                <p className="text-sm font-medium text-gray-900">{t('activityCourseRegistrationOpenTitle')}</p>
                <p className="text-xs text-gray-500">1 week ago at 10:00 AM</p>
                <p className="mt-1 text-sm text-gray-600">{t('activityCourseRegistrationOpenDescription')}</p>
              </div>
            </div>
          </div>
        </CardBody>
      </Card>
    </div>
  );
};
