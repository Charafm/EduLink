// BackOffice/Attendance.tsx
import React, { useEffect, useState } from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
import { Button } from '../../components/ui/Button';
import { Badge } from '../../components/ui/Badge';
import { Table } from '../../components/ui/Table';
import { TableBody, TableCell, TableHead, TableHeader, TableRow } from '../../components/ui/Table';
import { Input } from '../../components/ui/Input';
import { AttendanceClient, AttendanceDTO, AttendanceEnum } from '../../api/BO-api.service';
import { useToast } from '../../components/ui/Toast';

const BackOfficeAttendance: React.FC = () => {
  usePageTitle('Attendance Management');
  const [attendanceRecords, setAttendanceRecords] = useState<AttendanceDTO[]>([]);
  const [selectedDate, setSelectedDate] = useState('2025-04-27');
  const [isLoading, setIsLoading] = useState(true);
  const { toast } = useToast();

  useEffect(() => {
    const fetchAttendance = async () => {
      const attendanceClient = new AttendanceClient(import.meta.env.VITE_BACKOFFICE_API_URL);

      try {
        const response = await attendanceClient.getPaginatedRecords(undefined, undefined, new Date(selectedDate), new Date(selectedDate), undefined, 1, 25);
        setAttendanceRecords(response.results || []);
      } catch {
        // Handle error appropriately
      } finally {
        setIsLoading(false);
      }
    };

    fetchAttendance();
  }, [selectedDate, toast]);

  if (isLoading) {
    return <p>Loading...</p>;
  }

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <h1 className="text-2xl font-bold text-gray-900">Attendance Tracking</h1>
        <p className="mt-2 text-sm text-gray-500">Daily attendance records and reporting</p>
      </div>

      <Card>
        <CardHeader>
          <div className="flex items-center justify-between">
            <CardTitle>Daily Attendance</CardTitle>
            <div className="flex items-center space-x-4">
              <Input
                type="date"
                value={selectedDate}
                onChange={(e) => setSelectedDate(e.target.value)}
                className="w-48"
              />
              <Button variant="outline">Export CSV</Button>
            </div>
          </div>
        </CardHeader>
        <CardBody>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Student</TableHead>
                <TableHead>Status</TableHead>
                <TableHead>Actions</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {attendanceRecords.map((record) => (
                <TableRow key={record.studentId}>
                  <TableCell>{record.studentId || 'N/A'}</TableCell>
                  <TableCell>
                    <Badge
                      variant={
                        record.status === AttendanceEnum.Present ? 'success' :
                        record.status === AttendanceEnum.Absent ? 'danger' : 'warning'
                      }
                    >
                      {record.status}
                    </Badge>
                  </TableCell>
                  <TableCell>
                    <div className="flex space-x-2">
                      <Button variant="outline" size="sm">Present</Button>
                      <Button variant="outline" size="sm">Absent</Button>
                      <Button variant="outline" size="sm">Late</Button>
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

export default BackOfficeAttendance;