import React, { useEffect, useState } from 'react';
import { Table } from '../../components/ui/Table';
import { StudentsClient, StudentDTO } from '../../api/BO-api.service';
import { useToast } from '../../components/ui/Toast';

const StudentsManagement: React.FC = () => {
  const [students, setStudents] = useState<StudentDTO[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const { toast } = useToast();

  useEffect(() => {
    const fetchStudents = async () => {
      const studentsClient = new StudentsClient(import.meta.env.VITE_BACKOFFICE_API_URL);

      try {
        const response = await studentsClient.getPaginated(arg1, arg2, arg3, arg4, 1, 25, arg7); // Adjusted arguments
        setStudents(response.results || []);
      } catch { // Removed unused 'error' variable
        // Handle error appropriately
      } finally {
        setIsLoading(false);
      }
    };

    fetchStudents();
  }, [toast]);

  if (isLoading) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      <h1>Students Management</h1>
      <Table
        headers={['First Name (FR)', 'First Name (AR)', 'Last Name (FR)', 'Last Name (AR)', 'Enrollment Date']} // Ensure 'headers' is a valid prop for Table component
        rows={students.map((student) => [
          student.firstNameFr || 'N/A',
          student.firstNameAr || 'N/A',
          student.lastNameFr || 'N/A',
          student.lastNameAr || 'N/A',
          student.enrollmentDate ? new Date(student.enrollmentDate).toLocaleDateString() : 'N/A',
        ])}
      />
    </div>
  );
};

export default StudentsManagement;