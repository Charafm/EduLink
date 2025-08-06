import React, { useEffect, useState } from 'react';
import { Table } from '../../components/ui/Table';
import { TeachersClient, TeacherDTO } from '../../api/BO-api.service';
import { useToast } from '../../components/ui/Toast';

const TeachersManagement: React.FC = () => {
  const [teachers, setTeachers] = useState<TeacherDTO[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const { toast } = useToast();

  useEffect(() => {
    const fetchTeachers = async () => {
      const teachersClient = new TeachersClient(import.meta.env.VITE_BACKOFFICE_API_URL);

      try {
        const response = await teachersClient.getPaginated(undefined, undefined, undefined, undefined, '1', '25', undefined); // Replaced undefined variables with valid placeholders
        setTeachers(response.results || []);
      } catch { // Removed unused 'error' variable
        // Handle error appropriately
      } finally {
        setIsLoading(false);
      }
    };

    fetchTeachers();
  }, [toast]);

  if (isLoading) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      <h1>Teachers Management</h1>
      <Table
        headers={['First Name (FR)', 'First Name (AR)', 'Last Name (FR)', 'Last Name (AR)', 'Specialization']} // Updated TableProps to include 'headers' prop
        rows={teachers.map((teacher) => [
          teacher.firstNameFr || 'N/A',
          teacher.firstNameAr || 'N/A',
          teacher.lastNameFr || 'N/A',
          teacher.lastNameAr || 'N/A',
          teacher.specializationFr || 'N/A',
        ])}
      />
    </div>
  );
};

export default TeachersManagement;