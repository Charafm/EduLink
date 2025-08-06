import React, { useState } from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
import { Table, TableHeader, TableBody, TableRow, TableHead, TableCell } from '../../components/ui/Table';
import { Select } from '../../components/ui/Select';
import { Badge } from '../../components/ui/Badge';
import { BookOpen, TrendingUp, Award } from 'lucide-react';
import { ResponsiveContainer, LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip } from 'recharts';

const gradeData = [
  { subject: 'Mathematics', current: 92, midterm: 88, assignments: 94, participation: 90, final: null },
  { subject: 'English', current: 88, midterm: 85, assignments: 90, participation: 88, final: null },
  { subject: 'Science', current: 95, midterm: 92, assignments: 96, participation: 94, final: null },
  { subject: 'History', current: 87, midterm: 84, assignments: 89, participation: 86, final: null },
  { subject: 'Art', current: 94, midterm: 92, assignments: 95, participation: 96, final: null }
];

const progressData = [
  { month: 'Sep', Mathematics: 88, English: 85, Science: 92 },
  { month: 'Oct', Mathematics: 90, English: 87, Science: 94 },
  { month: 'Nov', Mathematics: 92, English: 88, Science: 95 },
  { month: 'Dec', Mathematics: 91, English: 86, Science: 93 }
];

const getGradeColor = (grade: number) => {
  if (grade >= 90) return 'text-green-600';
  if (grade >= 80) return 'text-blue-600';
  if (grade >= 70) return 'text-yellow-600';
  return 'text-red-600';
};

const getLetterGrade = (grade: number) => {
  if (grade >= 90) return 'A';
  if (grade >= 80) return 'B';
  if (grade >= 70) return 'C';
  if (grade >= 60) return 'D';
  return 'F';
};

export const FrontOfficeGrades: React.FC = () => {
  usePageTitle('My Grades');
  const [selectedTerm, setSelectedTerm] = useState('Fall 2023');

  const terms = [
    { value: 'Fall 2023', label: 'Fall 2023' },
    { value: 'Spring 2023', label: 'Spring 2023' },
    { value: 'Fall 2022', label: 'Fall 2022' }
  ];

  const currentGPA = 3.8;

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <div>
          <h1 className="text-2xl font-bold text-gray-900">My Grades</h1>
          <p className="text-sm text-gray-500">View your academic performance and progress</p>
        </div>
        <Select
          options={terms}
          value={selectedTerm}
          onChange={(value) => setSelectedTerm(value)}
          className="w-48"
        />
      </div>

      {/* Summary Cards */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        <Card className="bg-gradient-to-br from-blue-50 to-blue-100">
          <CardBody className="flex items-center p-6">
            <div className="rounded-full bg-blue-500 p-3 mr-4">
              <BookOpen className="h-6 w-6 text-white" />
            </div>
            <div>
              <p className="text-sm font-medium text-blue-800">Current GPA</p>
              <p className="text-2xl font-bold text-blue-900">{currentGPA.toFixed(1)}</p>
            </div>
          </CardBody>
        </Card>

        <Card className="bg-gradient-to-br from-green-50 to-green-100">
          <CardBody className="flex items-center p-6">
            <div className="rounded-full bg-green-500 p-3 mr-4">
              <TrendingUp className="h-6 w-6 text-white" />
            </div>
            <div>
              <p className="text-sm font-medium text-green-800">Class Rank</p>
              <p className="text-2xl font-bold text-green-900">12 of 156</p>
            </div>
          </CardBody>
        </Card>

        <Card className="bg-gradient-to-br from-purple-50 to-purple-100">
          <CardBody className="flex items-center p-6">
            <div className="rounded-full bg-purple-500 p-3 mr-4">
              <Award className="h-6 w-6 text-white" />
            </div>
            <div>
              <p className="text-sm font-medium text-purple-800">Honor Roll Status</p>
              <Badge variant="success" size="sm">High Honors</Badge>
            </div>
          </CardBody>
        </Card>
      </div>

      {/* Grade Table */}
      <Card>
        <CardHeader>
          <CardTitle>Grade Overview</CardTitle>
        </CardHeader>
        <CardBody>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Subject</TableHead>
                <TableHead>Current Grade</TableHead>
                <TableHead>Midterm</TableHead>
                <TableHead>Assignments</TableHead>
                <TableHead>Participation</TableHead>
                <TableHead>Final</TableHead>
                <TableHead>Letter Grade</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {gradeData.map((row) => (
                <TableRow key={row.subject}>
                  <TableCell className="font-medium">{row.subject}</TableCell>
                  <TableCell className={getGradeColor(row.current)}>{row.current}%</TableCell>
                  <TableCell>{row.midterm}%</TableCell>
                  <TableCell>{row.assignments}%</TableCell>
                  <TableCell>{row.participation}%</TableCell>
                  <TableCell>{row.final || 'N/A'}</TableCell>
                  <TableCell>
                    <Badge 
                      variant={row.current >= 80 ? 'success' : row.current >= 70 ? 'warning' : 'danger'}
                    >
                      {getLetterGrade(row.current)}
                    </Badge>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </CardBody>
      </Card>

      {/* Progress Chart */}
      <Card>
        <CardHeader>
          <CardTitle>Grade Progress</CardTitle>
        </CardHeader>
        <CardBody>
          <div className="h-80">
            <ResponsiveContainer width="100%" height="100%">
              <LineChart data={progressData}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="month" />
                <YAxis domain={[60, 100]} />
                <Tooltip />
                <Line type="monotone" dataKey="Mathematics" stroke="#3b82f6" strokeWidth={2} />
                <Line type="monotone" dataKey="English" stroke="#10b981" strokeWidth={2} />
                <Line type="monotone" dataKey="Science" stroke="#8b5cf6" strokeWidth={2} />
              </LineChart>
            </ResponsiveContainer>
          </div>
        </CardBody>
      </Card>
    </div>
  );
};