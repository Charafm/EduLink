// BackOffice/TransferRequests.tsx
import React from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
import { Table, TableHeader, TableBody, TableRow, TableHead, TableCell } from '../../components/ui/Table';
import { Badge } from '../../components/ui/Badge';
import { Button } from '../../components/ui/Button';

const transferRequests = [
  { id: '#TR-3245', student: 'Emily Davis', fromSchool: 'Central High', toSchool: 'West Academy', date: '2024-03-15', status: 'Pending' },
  { id: '#TR-3244', student: 'James Wilson', fromSchool: 'North Prep', toSchool: 'South College', date: '2024-03-14', status: 'Approved' },
  { id: '#TR-3243', student: 'Olivia Brown', fromSchool: 'East Institute', toSchool: 'City Academy', date: '2024-03-13', status: 'Needs Info' },
];

export const TransferRequests: React.FC = () => {
  usePageTitle('Transfer Requests');

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <h1 className="text-2xl font-bold text-gray-900">Transfer Requests</h1>
        <p className="mt-2 text-sm text-gray-500">Manage student transfer applications between schools</p>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Active Transfer Requests</CardTitle>
        </CardHeader>
        <CardBody>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Request ID</TableHead>
                <TableHead>Student</TableHead>
                <TableHead>From School</TableHead>
                <TableHead>To School</TableHead>
                <TableHead>Date</TableHead>
                <TableHead>Status</TableHead>
                <TableHead>Actions</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {transferRequests.map((request) => (
                <TableRow key={request.id}>
                  <TableCell className="font-medium">{request.id}</TableCell>
                  <TableCell>{request.student}</TableCell>
                  <TableCell>{request.fromSchool}</TableCell>
                  <TableCell>{request.toSchool}</TableCell>
                  <TableCell>{request.date}</TableCell>
                  <TableCell>
                    <Badge variant={
                      request.status === 'Approved' ? 'success' :
                      request.status === 'Pending' ? 'warning' : 'secondary'
                    }>
                      {request.status}
                    </Badge>
                  </TableCell>
                  <TableCell>
                    <div className="flex space-x-2">
                      <Button variant="outline" size="sm">Review</Button>
                      <Button variant="outline" size="sm">Request Info</Button>
                    </div>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </CardBody>
      </Card>
    </div>
  );
};