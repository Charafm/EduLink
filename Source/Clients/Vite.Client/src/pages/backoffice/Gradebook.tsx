// BackOffice/Gradebook.tsx
import React, { useEffect, useState } from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
import { Table, TableHeader, TableBody, TableRow, TableHead, TableCell } from '../../components/ui/Table';
import { Input } from '../../components/ui/Input';
import { Button } from '../../components/ui/Button';
import { Select } from '../../components/ui/Select';
import { Badge } from '../../components/ui/Badge';
import { GradesClient, GradeDTO, GradeFilterDTO } from '../../api/BO-api.service';
import { useToast } from '../../components/ui/Toast';

const Gradebook: React.FC = () => {
  usePageTitle('Gradebook');
  const [grades, setGrades] = useState<GradeDTO[]>([]);
  const [selectedClass, setSelectedClass] = useState('Mathematics 101');
  const [isLoading, setIsLoading] = useState(true);
  const { toast } = useToast();

  useEffect(() => {
    const fetchGrades = async () => {
      const gradesClient = new GradesClient(import.meta.env.VITE_BACKOFFICE_API_URL);
      const filter = new GradeFilterDTO();
      filter.courseId = selectedClass;
      filter.pageNumber = 1;
      filter.pageSize = 25;

      try {
        const response = await gradesClient.getStatistics(filter, arg2, arg3, arg4, arg5, arg6, arg7); // Adjusted arguments
        setGrades(response.results || []); // Ensure 'results' exists in 'GradeStatisticsDTO'
      } catch { // Removed unused 'error' variable
        // Handle error appropriately
      } finally {
        setIsLoading(false);
      }
    };

    fetchGrades();
  }, [selectedClass, toast]);

  if (isLoading) {
    return <p>Loading...</p>;
  }

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Gradebook</h1>
            <p className="mt-2 text-sm text-gray-500">Manage student grades and assignments</p>
          </div>
          <Select
            options={[
              { label: 'Mathematics 101', value: 'Mathematics 101' },
              { label: 'Science 201', value: 'Science 201' },
              { label: 'English 301', value: 'English 301' },
            ]}
            value={selectedClass}
            onChange={setSelectedClass}
            className="w-48"
          />
        </div>
      </div>

      <Card>
        <CardHeader>
          <div className="flex items-center justify-between">
            <CardTitle>{selectedClass} Grades</CardTitle>
            <Button>Bulk Update</Button>
          </div>
        </CardHeader>
        <CardBody>
          <div className="overflow-x-auto">
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>Student</TableHead>
                  <TableHead>Grade</TableHead>
                  <TableHead>Average</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {grades.map((grade) => (
                  <TableRow key={grade.id}>
                    <TableCell className="font-medium">{grade.studentId || 'N/A'}</TableCell>
                    <TableCell>
                      <Input
                        type="number"
                        min="0"
                        max="100"
                        value={grade.score || 0}
                        className="w-20"
                      />
                    </TableCell>
                    <TableCell>
                      <Badge variant="secondary">{grade.score || 0}%</Badge>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </div>
        </CardBody>
      </Card>
    </div>
  );
};

export default Gradebook;