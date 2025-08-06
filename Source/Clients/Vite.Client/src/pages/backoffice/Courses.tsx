import React, { useEffect, useState } from 'react';
import { Table } from '../../components/ui/Table';
import { CoursesClient, CourseDTO } from '../../api/BO-api.service';
import { useToast } from '../../components/ui/Toast';

const CoursesManagement: React.FC = () => {
  const [courses, setCourses] = useState<CourseDTO[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const { toast } = useToast();

  useEffect(() => {
    const fetchCourses = async () => {
      const coursesClient = new CoursesClient(import.meta.env.VITE_BACKOFFICE_API_URL);

      try {
        const response = await coursesClient.getPaginated(undefined, undefined, 1, 25);
        setCourses(response.results || []);
      } catch {
        toast({
          title: 'Error',
          description: 'Failed to fetch courses',
          variant: 'error',
        });
      } finally {
        setIsLoading(false);
      }
    };

    fetchCourses();
  }, [toast]);

  if (isLoading) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      <h1>Courses Management</h1>
      <Table
        headers={['Title (FR)', 'Title (AR)', 'Code', 'Description']} // Ensure 'headers' is a valid prop for Table component
        // Update TableProps interface if necessary
        rows={courses.map((course) => [
          course.titleFr || 'N/A',
          course.titleAr || 'N/A',
          course.code || 'N/A',
          course.description || 'N/A',
        ])}
      />
    </div>
  );
};

export default CoursesManagement;