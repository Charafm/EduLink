import React, { useEffect, useState } from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
import { Table, TableHeader, TableBody, TableRow, TableHead, TableCell } from '../../components/ui/Table';
import { Badge } from '../../components/ui/Badge';
import { GradeResourceDTO } from '../../types/types';
import { useParams } from 'react-router-dom';
import { useToast } from '../../components/ui/Toast';

// Mock data for grade resources
const mockResources: GradeResourceDTO[] = [
  { id: '1', gradeLevelId: 'g1', bookId: 'b1', schoolSupplyId: null, supplyQuantity: null, gradeLevelName: 'Grade 1', resourceTitle: 'Mathematics Textbook' },
  { id: '2', gradeLevelId: 'g1', bookId: 'b2', schoolSupplyId: null, supplyQuantity: null, gradeLevelName: 'Grade 1', resourceTitle: 'Science Textbook' },
  { id: '3', gradeLevelId: 'g1', bookId: null, schoolSupplyId: 's1', supplyQuantity: 2, gradeLevelName: 'Grade 1', resourceTitle: 'Notebook' },
  { id: '4', gradeLevelId: 'g1', bookId: null, schoolSupplyId: 's2', supplyQuantity: 1, gradeLevelName: 'Grade 1', resourceTitle: 'Scissors' },
  { id: '5', gradeLevelId: 'g2', bookId: 'b3', schoolSupplyId: null, supplyQuantity: null, gradeLevelName: 'Grade 2', resourceTitle: 'English Reader' },
  { id: '6', gradeLevelId: 'g2', bookId: null, schoolSupplyId: 's3', supplyQuantity: 3, gradeLevelName: 'Grade 2', resourceTitle: 'Colored Pencils Set' },
];

export const FrontOfficeResources: React.FC = () => {
  usePageTitle('Resources Needed');
  const { studentId } = useParams<{ studentId: string }>();
  const { toast } = useToast();
  const [resources, setResources] = useState<GradeResourceDTO[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!studentId) {
      // avoid state loop by toast only once
      toast({ title: 'Error', description: 'No student selected', variant: 'error' });
      setLoading(false);
      return;
    }
    const data = mockResources.filter(r => r.gradeLevelId === studentId);
    const timer = setTimeout(() => {
      setResources(data);
      setLoading(false);
    }, 300);
    return () => clearTimeout(timer);
  }, [studentId]);

  const books = resources.filter(r => r.bookId);
  const supplies = resources.filter(r => r.schoolSupplyId);

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <h1 className="text-2xl font-bold text-gray-900">Required Resources</h1>
        <p className="mt-2 text-sm text-gray-500">Materials for grade level based on selected child</p>
      </div>

      {loading ? (
        <p>Loading…</p>
      ) : (
        <>
          <Card>
            <CardHeader>
              <CardTitle>Books</CardTitle>
            </CardHeader>
            <CardBody>
              {books.length === 0 ? (
                <p className="text-gray-500">No books required for this grade.</p>
              ) : (
                <Table>
                  <TableHeader>
                    <TableRow>
                      <TableHead>Title</TableHead>
                      <TableHead>Grade Level</TableHead>
                    </TableRow>
                  </TableHeader>
                  <TableBody>
                    {books.map(b => (
                      <TableRow key={b.id}>
                        <TableCell className="font-medium">{b.resourceTitle}</TableCell>
                        <TableCell>{b.gradeLevelName}</TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              )}
            </CardBody>
          </Card>

          <Card>
            <CardHeader>
              <CardTitle>Supplies</CardTitle>
            </CardHeader>
            <CardBody>
              {supplies.length === 0 ? (
                <p className="text-gray-500">No supplies required for this grade.</p>
              ) : (
                <Table>
                  <TableHeader>
                    <TableRow>
                      <TableHead>Supply</TableHead>
                      <TableHead>Quantity</TableHead>
                      <TableHead>Grade Level</TableHead>
                    </TableRow>
                  </TableHeader>
                  <TableBody>
                    {supplies.map(s => (
                      <TableRow key={s.id}>
                        <TableCell className="font-medium">{s.resourceTitle}</TableCell>
                        <TableCell>{s.supplyQuantity}</TableCell>
                        <TableCell>{s.gradeLevelName}</TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              )}
            </CardBody>
          </Card>
        </>
      )}
    </div>
  );
};
