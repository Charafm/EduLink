// BackOffice/Resources.tsx
//import React, { useState } from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
import { Table, TableHeader, TableBody, TableRow, TableHead, TableCell } from '../../components/ui/Table';
import { Button } from '../../components/ui/Button';
import { FileUpload } from '../../components/ui/FileUpload';
import { Badge } from '../../components/ui/Badge';

const resources = [
  { name: 'Curriculum Guide', type: 'PDF', category: 'Academic', date: '2024-03-01', status: 'Published' },
  { name: 'Staff Handbook', type: 'DOCX', category: 'Administrative', date: '2024-02-15', status: 'Draft' },
  { name: 'Safety Protocol', type: 'PDF', category: 'Operations', date: '2024-03-10', status: 'Archived' },
];

export const BackOfficeResources: React.FC = () => {
  usePageTitle('Resource Management');
  //const [, setFiles] = useState<File[]>([]);

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <h1 className="text-2xl font-bold text-gray-900">Institutional Resources</h1>
        <p className="mt-2 text-sm text-gray-500">Manage official documents and resources</p>
      </div>

      <Card>
        <CardHeader>
          <div className="flex items-center justify-between">
            <CardTitle>Document Repository</CardTitle>
            <FileUpload
              //onUpload={(files: React.SetStateAction<File[]>) => setFiles(files)}
              multiple
              accept=".pdf,.docx,.xlsx"
              label="Upload New Document"
            />
          </div>
        </CardHeader>
        <CardBody>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Document Name</TableHead>
                <TableHead>Type</TableHead>
                <TableHead>Category</TableHead>
                <TableHead>Upload Date</TableHead>
                <TableHead>Status</TableHead>
                <TableHead>Actions</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {resources.map((resource, index) => (
                <TableRow key={index}>
                  <TableCell className="font-medium">{resource.name}</TableCell>
                  <TableCell>{resource.type}</TableCell>
                  <TableCell>{resource.category}</TableCell>
                  <TableCell>{resource.date}</TableCell>
                  <TableCell>
                    <Badge
                      variant={
                        resource.status === 'Published' ? 'success' :
                        resource.status === 'Draft' ? 'warning' : 'secondary'
                      }
                    >
                      {resource.status}
                    </Badge>
                  </TableCell>
                  <TableCell>
                    <div className="flex space-x-2">
                      <Button variant="outline" size="sm">Edit</Button>
                      <Button variant="outline" size="sm">Download</Button>
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