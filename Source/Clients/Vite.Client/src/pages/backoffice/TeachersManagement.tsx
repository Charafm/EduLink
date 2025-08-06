// BackOffice/TeachersManagement.tsx
import React, { useState } from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
import { Table, TableHeader, TableBody, TableRow, TableHead, TableCell } from '../../components/ui/Table';
import { Button } from '../../components/ui/Button';
import { Select } from '../../components/ui/Select';
import { Badge } from '../../components/ui/Badge';
import { Modal } from '../../components/ui/Modal';
import { Input } from '../../components/ui/Input';

const subjects = ['Mathematics', 'Science', 'English', 'History', 'Art'];
const teachers = Array.from({ length: 20 }, (_, i) => ({
  id: `T${200 + i}`,
  name: `Teacher ${i + 1}`,
  subjects: [subjects[i % 5]], 
  status: i % 4 === 0 ? 'On Leave' : 'Active',
}));

export const TeachersManagement: React.FC = () => {
  usePageTitle('Teachers Management');
  const [isModalOpen, setIsModalOpen] = useState(false);

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <h1 className="text-2xl font-bold text-gray-900">Faculty Management</h1>
        <p className="mt-2 text-sm text-gray-500">Manage teaching staff and subject assignments</p>
      </div>

      <Card>
        <CardHeader>
          <div className="flex items-center justify-between">
            <CardTitle>Teaching Staff</CardTitle>
            <Button onClick={() => setIsModalOpen(true)}>Add Teacher</Button>
          </div>
        </CardHeader>
        <CardBody>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Teacher ID</TableHead>
                <TableHead>Name</TableHead>
                <TableHead>Subjects</TableHead>
                <TableHead>Status</TableHead>
                <TableHead>Actions</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {teachers.map((teacher) => (
                <TableRow key={teacher.id}>
                  <TableCell>{teacher.id}</TableCell>
                  <TableCell>{teacher.name}</TableCell>
                  <TableCell>
                    <div className="flex flex-wrap gap-2">
                      {teacher.subjects.map((subject) => (
                        <Badge key={subject} variant="secondary">{subject}</Badge>
                      ))}
                    </div>
                  </TableCell>
                  <TableCell>
                    <Badge variant={teacher.status === 'Active' ? 'success' : 'warning'}>
                      {teacher.status}
                    </Badge>
                  </TableCell>
                  <TableCell>
                    <div className="flex space-x-2">
                      <Button variant="outline" size="sm">Edit</Button>
                      <Button variant="outline" size="sm">Schedule</Button>
                    </div>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </CardBody>
      </Card>

      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        title="Add New Teacher"
        size="lg"
      >
        <div className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
            <Input label="First Name" required />
            <Input label="Last Name" required />
            <Input label="Email" type="email" required />
            <Input label="Phone Number" type="tel" />
          </div>
          <Select
            label="Subjects"
            options={subjects.map((s) => ({ value: s, label: s }))}
            multiple
          />
          <div className="flex justify-end space-x-4">
            <Button variant="outline" onClick={() => setIsModalOpen(false)}>
              Cancel
            </Button>
            <Button>Save Teacher</Button>
          </div>
        </div>
      </Modal>
    </div>
  );
};