// BackOffice/Reports.tsx
import React, { useState } from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
import { Select } from '../../components/ui/Select';
import { Button } from '../../components/ui/Button';
import { ResponsiveContainer, BarChart, Bar, XAxis, YAxis, Tooltip, PieChart, Pie, Cell } from 'recharts';

const enrollmentData = [
  { name: 'Grade 9', students: 245 },
  { name: 'Grade 10', students: 198 },
  { name: 'Grade 11', students: 176 },
  { name: 'Grade 12', students: 152 },
];

const COLORS = ['#3b82f6', '#10b981', '#8b5cf6', '#ef4444'];

export const Reports: React.FC = () => {
  usePageTitle('Analytics Reports');
  const [reportType, setReportType] = useState('enrollment');
  const [timeRange, setTimeRange] = useState('currentYear');

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Analytical Reports</h1>
            <p className="mt-2 text-sm text-gray-500">Generate and export institutional reports</p>
          </div>
          <div className="flex space-x-4">
            <Select
              options={[
                { value: 'enrollment', label: 'Enrollment' },
                { value: 'attendance', label: 'Attendance' },
                { value: 'academic', label: 'Academic' },
              ]}
              value={reportType}
              onChange={setReportType}
              className="w-48"
            />
            <Select
              options={[
                { value: 'currentYear', label: 'Current Year' },
                { value: 'lastYear', label: 'Last Year' },
                { value: 'last5Years', label: 'Last 5 Years' },
              ]}
              value={timeRange}
              onChange={setTimeRange}
              className="w-48"
            />
            <Button variant="outline">Export PDF</Button>
          </div>
        </div>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <Card>
          <CardHeader>
            <CardTitle>Enrollment Distribution</CardTitle>
          </CardHeader>
          <CardBody>
            <div className="h-80">
              <ResponsiveContainer width="100%" height="100%">
                <BarChart data={enrollmentData}>
                  <XAxis dataKey="name" />
                  <YAxis />
                  <Tooltip />
                  <Bar dataKey="students" fill="#3b82f6" radius={[4, 4, 0, 0]} />
                </BarChart>
              </ResponsiveContainer>
            </div>
          </CardBody>
        </Card>

        <Card>
          <CardHeader>
            <CardTitle>Grade Level Distribution</CardTitle>
          </CardHeader>
          <CardBody>
            <div className="h-80">
              <ResponsiveContainer width="100%" height="100%">
                <PieChart>
                  <Pie
                    data={enrollmentData}
                    cx="50%"
                    cy="50%"
                    innerRadius={60}
                    outerRadius={80}
                    paddingAngle={5}
                    dataKey="students"
                  >
                    {enrollmentData.map((entry, index) => (
                      <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                    ))}
                  </Pie>
                  <Tooltip />
                </PieChart>
              </ResponsiveContainer>
            </div>
          </CardBody>
        </Card>
      </div>
    </div>
  );
};