import React from 'react';
import { useAuth } from '../../context/AuthContext';
import { Card, CardBody, CardHeader, CardTitle } from '../../components/ui/Card';
import { 
  Users, 
  GraduationCap, 
  UserCheck, 
  BookOpen, 
  FileCheck, 
  Bell, 
  Calendar 
} from 'lucide-react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { 
  ResponsiveContainer, 
  BarChart, 
  Bar, 
  XAxis, 
  YAxis, 
  CartesianGrid, 
  Tooltip, 
  PieChart, 
  Pie, 
  Cell, 
  Legend 
} from 'recharts';

const enrollmentData = [
  { month: 'Aug', count: 45 },
  { month: 'Sep', count: 30 },
  { month: 'Oct', count: 20 },
  { month: 'Nov', count: 15 },
  { month: 'Dec', count: 10 },
  { month: 'Jan', count: 25 }
];

const gradeDistributionData = [
  { name: 'A', value: 35 },
  { name: 'B', value: 40 },
  { name: 'C', value: 15 },
  { name: 'D', value: 7 },
  { name: 'F', value: 3 }
];

const GRADE_COLORS = ['#4ade80', '#22c55e', '#facc15', '#f97316', '#ef4444'];

export const BackOfficeDashboard: React.FC = () => {
  usePageTitle('Dashboard');
  const { userType, username } = useAuth();
  
  const isAdmin = userType === 'SchoolAdmin';

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-3">
        <h1 className="text-2xl font-bold text-gray-900">Dashboard</h1>
        <p className="text-sm text-gray-500">
          Welcome back, {username}! Here's an overview of your school information.
        </p>
      </div>

      {/* Stats Cards */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        <Card className="bg-gradient-to-br from-blue-50 to-blue-100 border-blue-200">
          <CardBody className="flex items-center p-6">
            <div className="rounded-full bg-blue-500 p-3 mr-4">
              <GraduationCap className="h-6 w-6 text-white" />
            </div>
            <div>
              <p className="text-sm font-medium text-blue-800">Total Students</p>
              <p className="text-2xl font-bold text-blue-900">1,254</p>
            </div>
          </CardBody>
        </Card>

        {isAdmin && (
          <Card className="bg-gradient-to-br from-indigo-50 to-indigo-100 border-indigo-200">
            <CardBody className="flex items-center p-6">
              <div className="rounded-full bg-indigo-500 p-3 mr-4">
                <FileCheck className="h-6 w-6 text-white" />
              </div>
              <div>
                <p className="text-sm font-medium text-indigo-800">Pending Enrollments</p>
                <p className="text-2xl font-bold text-indigo-900">28</p>
              </div>
            </CardBody>
          </Card>
        )}

        <Card className="bg-gradient-to-br from-emerald-50 to-emerald-100 border-emerald-200">
          <CardBody className="flex items-center p-6">
            <div className="rounded-full bg-emerald-500 p-3 mr-4">
              <UserCheck className="h-6 w-6 text-white" />
            </div>
            <div>
              <p className="text-sm font-medium text-emerald-800">Teachers</p>
              <p className="text-2xl font-bold text-emerald-900">87</p>
            </div>
          </CardBody>
        </Card>

        <Card className="bg-gradient-to-br from-amber-50 to-amber-100 border-amber-200">
          <CardBody className="flex items-center p-6">
            <div className="rounded-full bg-amber-500 p-3 mr-4">
              <BookOpen className="h-6 w-6 text-white" />
            </div>
            <div>
              <p className="text-sm font-medium text-amber-800">Active Courses</p>
              <p className="text-2xl font-bold text-amber-900">142</p>
            </div>
          </CardBody>
        </Card>

        {isAdmin && (
          <Card className="bg-gradient-to-br from-rose-50 to-rose-100 border-rose-200">
            <CardBody className="flex items-center p-6">
              <div className="rounded-full bg-rose-500 p-3 mr-4">
                <FileCheck className="h-6 w-6 text-white" />
              </div>
              <div>
                <p className="text-sm font-medium text-rose-800">Transfer Requests</p>
                <p className="text-2xl font-bold text-rose-900">12</p>
              </div>
            </CardBody>
          </Card>
        )}
      </div>

      {/* Charts */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <Card>
          <CardHeader>
            <CardTitle>Enrollment Trends</CardTitle>
          </CardHeader>
          <CardBody className="p-4">
            <div className="h-72">
              <ResponsiveContainer width="100%" height="100%">
                <BarChart data={enrollmentData} margin={{ top: 20, right: 30, left: 0, bottom: 5 }}>
                  <CartesianGrid strokeDasharray="3 3" />
                  <XAxis dataKey="month" />
                  <YAxis />
                  <Tooltip />
                  <Bar dataKey="count" fill="#3b82f6" name="New Enrollments" />
                </BarChart>
              </ResponsiveContainer>
            </div>
          </CardBody>
        </Card>

        <Card>
          <CardHeader>
            <CardTitle>Grade Distribution</CardTitle>
          </CardHeader>
          <CardBody className="p-4">
            <div className="h-72">
              <ResponsiveContainer width="100%" height="100%">
                <PieChart>
                  <Pie
                    data={gradeDistributionData}
                    cx="50%"
                    cy="50%"
                    labelLine={false}
                    outerRadius={80}
                    fill="#8884d8"
                    dataKey="value"
                    label={({ name, percent }) => `${name} ${(percent * 100).toFixed(0)}%`}
                  >
                    {gradeDistributionData.map((entry, index) => (
                      <Cell key={`cell-${index}`} fill={GRADE_COLORS[index % GRADE_COLORS.length]} />
                    ))}
                  </Pie>
                  <Tooltip />
                  <Legend />
                </PieChart>
              </ResponsiveContainer>
            </div>
          </CardBody>
        </Card>
      </div>

      {/* Quick Actions */}
      <Card>
        <CardHeader>
          <CardTitle>Quick Actions</CardTitle>
        </CardHeader>
        <CardBody>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
            <button className="p-4 bg-blue-50 hover:bg-blue-100 transition-colors rounded-lg border border-blue-200 flex flex-col items-center justify-center">
              <BookOpen className="h-8 w-8 text-blue-600 mb-2" />
              <span className="text-sm font-medium text-blue-700">Record Grades</span>
            </button>
            
            <button className="p-4 bg-green-50 hover:bg-green-100 transition-colors rounded-lg border border-green-200 flex flex-col items-center justify-center">
              <Calendar className="h-8 w-8 text-green-600 mb-2" />
              <span className="text-sm font-medium text-green-700">Take Attendance</span>
            </button>
            
            <button className="p-4 bg-amber-50 hover:bg-amber-100 transition-colors rounded-lg border border-amber-200 flex flex-col items-center justify-center">
              <Users className="h-8 w-8 text-amber-600 mb-2" />
              <span className="text-sm font-medium text-amber-700">Manage Students</span>
            </button>
            
            <button className="p-4 bg-purple-50 hover:bg-purple-100 transition-colors rounded-lg border border-purple-200 flex flex-col items-center justify-center">
              <Bell className="h-8 w-8 text-purple-600 mb-2" />
              <span className="text-sm font-medium text-purple-700">Send Announcements</span>
            </button>
          </div>
        </CardBody>
      </Card>

      {/* Recent Activity */}
      <Card>
        <CardHeader>
          <CardTitle>Recent Activity</CardTitle>
        </CardHeader>
        <CardBody>
          <div className="space-y-4">
            <div className="flex items-start">
              <div className="flex-shrink-0 mt-1">
                <div className="bg-blue-100 rounded-full p-2">
                  <GraduationCap className="h-4 w-4 text-blue-600" />
                </div>
              </div>
              <div className="ml-3">
                <p className="text-sm font-medium text-gray-900">New Student Enrollment</p>
                <p className="text-xs text-gray-500">Today at 10:30 AM</p>
                <p className="mt-1 text-sm text-gray-600">A new student, Emma Johnson, has been enrolled in Grade 9.</p>
              </div>
            </div>
            
            <div className="flex items-start">
              <div className="flex-shrink-0 mt-1">
                <div className="bg-green-100 rounded-full p-2">
                  <BookOpen className="h-4 w-4 text-green-600" />
                </div>
              </div>
              <div className="ml-3">
                <p className="text-sm font-medium text-gray-900">Grades Updated</p>
                <p className="text-xs text-gray-500">Yesterday at 3:45 PM</p>
                <p className="mt-1 text-sm text-gray-600">Ms. Wilson updated grades for English Literature (ENG201).</p>
              </div>
            </div>
            
            <div className="flex items-start">
              <div className="flex-shrink-0 mt-1">
                <div className="bg-amber-100 rounded-full p-2">
                  <Calendar className="h-4 w-4 text-amber-600" />
                </div>
              </div>
              <div className="ml-3">
                <p className="text-sm font-medium text-gray-900">Schedule Updated</p>
                <p className="text-xs text-gray-500">2 days ago at 9:15 AM</p>
                <p className="mt-1 text-sm text-gray-600">The winter break schedule has been published.</p>
              </div>
            </div>
            
            <div className="flex items-start">
              <div className="flex-shrink-0 mt-1">
                <div className="bg-purple-100 rounded-full p-2">
                  <FileCheck className="h-4 w-4 text-purple-600" />
                </div>
              </div>
              <div className="ml-3">
                <p className="text-sm font-medium text-gray-900">Transfer Request Approved</p>
                <p className="text-xs text-gray-500">3 days ago at 2:00 PM</p>
                <p className="mt-1 text-sm text-gray-600">Transfer request for Noah Brown has been approved.</p>
              </div>
            </div>
          </div>
        </CardBody>
      </Card>
    </div>
  );
};