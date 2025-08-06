// BackOffice/StudentsManagement.tsx
import React, { useState } from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
import { Table, TableHeader, TableBody, TableRow, TableHead, TableCell } from '../../components/ui/Table';
import { Input } from '../../components/ui/Input';
import { Button } from '../../components/ui/Button';
import { Pagination } from '../../components/ui/Table';
import { Badge } from '../../components/ui/Badge';

const students = Array.from({ length: 45 }, (_, i) => ({
  id: `S${1000 + i}`,
  name: `Student ${i + 1}`,
  grade: `Grade ${9 + (i % 4)}`,
  status: i % 5 === 0 ? 'Inactive' : 'Active',
  enrollmentDate: `2023-0${(i % 9) + 1}-${15 + (i % 10)}`,
}));

export const StudentsManagement: React.FC = () => {
  usePageTitle('Students Management');
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <h1 className="text-2xl font-bold text-gray-900">Student Management</h1>
        <p className="mt-2 text-sm text-gray-500">Manage student records and enrollment status</p>
      </div>

      <Card>
        <CardHeader>
          <div className="flex items-center justify-between">
            <CardTitle>Student Directory</CardTitle>
            <div className="flex space-x-4">
              <Input placeholder="Search students..." className="w-64" />
              <Button>Add New Student</Button>
            </div>
          </div>
        </CardHeader>
        <CardBody>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Student ID</TableHead>
                <TableHead>Name</TableHead>
                <TableHead>Grade</TableHead>
                <TableHead>Enrollment Date</TableHead>
                <TableHead>Status</TableHead>
                <TableHead>Actions</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {students.slice((currentPage - 1) * pageSize, currentPage * pageSize).map((student) => (
                <TableRow key={student.id}>
                  <TableCell>{student.id}</TableCell>
                  <TableCell>{student.name}</TableCell>
                  <TableCell>{student.grade}</TableCell>
                  <TableCell>{student.enrollmentDate}</TableCell>
                  <TableCell>
                    <Badge variant={student.status === 'Active' ? 'success' : 'secondary'}>
                      {student.status}
                    </Badge>
                  </TableCell>
                  <TableCell>
                    <div className="flex space-x-2">
                      <Button variant="outline" size="sm">Edit</Button>
                      <Button variant="outline" size="sm">View</Button>
                    </div>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
          <Pagination
            currentPage={currentPage}
            pageSize={pageSize}
            totalItems={students.length}
            onPageChange={setCurrentPage}
            onPageSizeChange={setPageSize}
            className="mt-4"
          />
        </CardBody>
      </Card>
    </div>
  );
};