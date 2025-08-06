import { http, HttpResponse, delay } from 'msw';
import { mockSchools } from './mockData/schools';

import { mockParents } from './mockData/parents';
import { mockTeachers } from './mockData/teachers';
import { mockStaff } from './mockData/staff';
import { mockCourses } from './mockData/courses';
import { mockGrades } from './mockData/grades';
import { mockAttendance } from './mockData/attendance';
import { mockSchedule } from './mockData/schedule';
import { mockResources } from './mockData/resources';
import { mockEnrollmentRequests } from './mockData/enrollmentRequests';
import { mockTransferRequests } from './mockData/transferRequests';
import { PaginatedResponse, PageParams } from '../types';

// Helper to create paginated responses
function createPaginatedResponse<T>(
  items: T[],
  params: PageParams,
  search?: string
): PaginatedResponse<T> {
  const { page, size, sortBy, sortOrder } = params;

  // Filter items if search is provided
  let filteredItems = items;
  if (search) {
    const searchLower = search.toLowerCase();
    filteredItems = items.filter(item => {
      // You can customize this for each type of item
      return Object.values(item as Record<string, any>).some(value => 
        value && value.toString().toLowerCase().includes(searchLower)
      );
    });
  }

  // Sort items if sortBy is provided
  if (sortBy) {
    filteredItems = [...filteredItems].sort((a, b) => {
      const aValue = (a as Record<string, any>)[sortBy];
      const bValue = (b as Record<string, any>)[sortBy];
      
      if (sortOrder === 'asc') {
        return aValue > bValue ? 1 : -1;
      } else {
        return aValue < bValue ? 1 : -1;
      }
    });
  }

  // Paginate
  const totalCount = filteredItems.length;
  const startIndex = (page - 1) * size;
  const endIndex = Math.min(startIndex + size, totalCount);
  const paginatedItems = filteredItems.slice(startIndex, endIndex);

  return {
    items: paginatedItems,
    totalCount,
    page,
    pageSize: size,
    totalPages: Math.ceil(totalCount / size)
  };
}

export const handlers = [
  // Auth handler
  http.post('*/connect/token', async ({ request }) => {
    // Simulate API delay
    await delay(500);

    const data = await request.json();
    const { username, password, userType } = data;

    // Check credentials
    const validCredentials = (
      (username === 'admin' && password === 'password' && userType === 'SchoolAdmin') ||
      (username === 'teacher' && password === 'password' && userType === 'Staff') ||
      (username === 'staff' && password === 'password' && userType === 'Staff') ||
      (username === 'student' && password === 'password' && userType === 'Student') ||
      (username === 'parent' && password === 'password' && userType === 'Parent')
    );

    if (!validCredentials) {
      return new HttpResponse(
        JSON.stringify({ error: 'Invalid credentials' }),
        { status: 401 }
      );
    }

    return HttpResponse.json({
      token: `mock_token_${userType}_${username}_${Date.now()}`,
      refreshToken: `mock_refresh_${userType}_${username}_${Date.now()}`,
      userType,
      username,
    });
  }),

  // Refresh token handler
  http.post('*/connect/refresh', async () => {
    await delay(500);

    return HttpResponse.json({
      token: `mock_refreshed_token_${Date.now()}`,
      refreshToken: `mock_refreshed_refresh_${Date.now()}`,
    });
  }),

  // Schools API
  http.get('*/schools', async ({ request }) => {
    await delay(500);
    
    const url = new URL(request.url);
    const page = parseInt(url.searchParams.get('page') || '1', 10);
    const size = parseInt(url.searchParams.get('size') || '10', 10);
    const search = url.searchParams.get('search') || '';
    const sortBy = url.searchParams.get('sortBy') || '';
    const sortOrder = url.searchParams.get('sortOrder') as 'asc' | 'desc' || 'asc';
    
    const paginatedResponse = createPaginatedResponse(
      mockSchools,
      { page, size, search, sortBy, sortOrder },
      search
    );
    
    return HttpResponse.json(paginatedResponse);
  }),

  http.get('*/schools/:id/branches', async ({ params }) => {
    await delay(300);
    
    const schoolId = params.id as string;
    const branches = mockSchools.find(s => s.id === schoolId)?.branches || [];
    
    return HttpResponse.json(branches);
  }),

 
  

  // Teachers API
  http.get('*/teachers', async ({ request }) => {
    await delay(500);
    
    const url = new URL(request.url);
    const page = parseInt(url.searchParams.get('page') || '1', 10);
    const size = parseInt(url.searchParams.get('size') || '10', 10);
    const search = url.searchParams.get('search') || '';
    const sortBy = url.searchParams.get('sortBy') || '';
    const sortOrder = url.searchParams.get('sortOrder') as 'asc' | 'desc' || 'asc';
    
    const paginatedResponse = createPaginatedResponse(
      mockTeachers,
      { page, size, search, sortBy, sortOrder },
      search
    );
    
    return HttpResponse.json(paginatedResponse);
  }),

  // Staff API
  http.get('*/staff', async ({ request }) => {
    await delay(500);
    
    const url = new URL(request.url);
    const page = parseInt(url.searchParams.get('page') || '1', 10);
    const size = parseInt(url.searchParams.get('size') || '10', 10);
    const search = url.searchParams.get('search') || '';
    const sortBy = url.searchParams.get('sortBy') || '';
    const sortOrder = url.searchParams.get('sortOrder') as 'asc' | 'desc' || 'asc';
    
    const paginatedResponse = createPaginatedResponse(
      mockStaff,
      { page, size, search, sortBy, sortOrder },
      search
    );
    
    return HttpResponse.json(paginatedResponse);
  }),

  // Courses API
  http.get('*/courses', async ({ request }) => {
    await delay(500);
    
    const url = new URL(request.url);
    const page = parseInt(url.searchParams.get('page') || '1', 10);
    const size = parseInt(url.searchParams.get('size') || '10', 10);
    const search = url.searchParams.get('search') || '';
    const sortBy = url.searchParams.get('sortBy') || '';
    const sortOrder = url.searchParams.get('sortOrder') as 'asc' | 'desc' || 'asc';
    
    const paginatedResponse = createPaginatedResponse(
      mockCourses,
      { page, size, search, sortBy, sortOrder },
      search
    );
    
    return HttpResponse.json(paginatedResponse);
  }),

  // Grades API
  http.get('*/grades', async ({ request }) => {
    await delay(500);
    
    const url = new URL(request.url);
    const studentId = url.searchParams.get('studentId');
    const courseId = url.searchParams.get('courseId');
    
    let filteredGrades = mockGrades;
    
    if (studentId) {
      filteredGrades = filteredGrades.filter(g => g.studentId === studentId);
    }
    
    if (courseId) {
      filteredGrades = filteredGrades.filter(g => g.courseId === courseId);
    }
    
    return HttpResponse.json(filteredGrades);
  }),

  http.post('*/grade/bulkupdate', async () => {
    await delay(800);
    return HttpResponse.json({ success: true });
  }),

  // Attendance API
  http.get('*/attendance', async ({ request }) => {
    await delay(500);
    
    const url = new URL(request.url);
    const studentId = url.searchParams.get('studentId');
    const courseId = url.searchParams.get('courseId');
    const date = url.searchParams.get('date');
    
    let filteredAttendance = mockAttendance;
    
    if (studentId) {
      filteredAttendance = filteredAttendance.filter(a => a.studentId === studentId);
    }
    
    if (courseId) {
      filteredAttendance = filteredAttendance.filter(a => a.courseId === courseId);
    }
    
    if (date) {
      filteredAttendance = filteredAttendance.filter(a => a.date === date);
    }
    
    return HttpResponse.json(filteredAttendance);
  }),

  http.post('*/attendance/record', async () => {
    await delay(800);
    return HttpResponse.json({ success: true });
  }),

  // Schedule API
  http.get('*/schedule', async ({ request }) => {
    await delay(500);
    
    const url = new URL(request.url);
    const studentId = url.searchParams.get('studentId');
    const teacherId = url.searchParams.get('teacherId');
    
    let filteredSchedule = mockSchedule;
    
    if (studentId) {
      // In a real app, we would filter schedules by student enrollment
      // For simplicity, we'll just return a subset
      filteredSchedule = filteredSchedule.slice(0, 5);
    }
    
    if (teacherId) {
      filteredSchedule = filteredSchedule.filter(s => s.teacherId === teacherId);
    }
    
    return HttpResponse.json(filteredSchedule);
  }),

  http.post('*/schedule/create', async () => {
    await delay(800);
    return HttpResponse.json({ success: true });
  }),

  // Resources API
  http.get('*/resources', async ({ request }) => {
    await delay(500);
    
    const url = new URL(request.url);
    const type = url.searchParams.get('type');
    const courseId = url.searchParams.get('courseId');
    
    let filteredResources = mockResources;
    
    if (type) {
      filteredResources = filteredResources.filter(r => r.type === type);
    }
    
    if (courseId) {
      filteredResources = filteredResources.filter(r => r.courseId === courseId);
    }
    
    return HttpResponse.json(filteredResources);
  }),

  http.post('*/book/create', async () => {
    await delay(800);
    return HttpResponse.json({ success: true });
  }),

  http.post('*/supply/create', async () => {
    await delay(800);
    return HttpResponse.json({ success: true });
  }),

  // Enrollment API
  http.post('*/enrollment/uploadenrollmentdocument', async () => {
    await delay(1500);
    return HttpResponse.json({ 
      id: `doc-${Date.now()}`,
      url: 'https://example.com/document.pdf'
    });
  }),

  http.post('*/enrollment/submitenrollment', async () => {
    await delay(1500);
    return HttpResponse.json({ 
      id: `enr-${Date.now()}`,
      status: 'Pending',
      submissionDate: new Date().toISOString()
    });
  }),

  http.get('*/enrollment/requests', async ({ request }) => {
    await delay(500);
    
    const url = new URL(request.url);
    const page = parseInt(url.searchParams.get('page') || '1', 10);
    const size = parseInt(url.searchParams.get('size') || '10', 10);
    const search = url.searchParams.get('search') || '';
    const sortBy = url.searchParams.get('sortBy') || '';
    const sortOrder = url.searchParams.get('sortOrder') as 'asc' | 'desc' || 'asc';
    
    const paginatedResponse = createPaginatedResponse(
      mockEnrollmentRequests,
      { page, size, search, sortBy, sortOrder },
      search
    );
    
    return HttpResponse.json(paginatedResponse);
  }),

  http.get('*/enrollment/history', async () => {
    await delay(500);
    
    return HttpResponse.json([
      {
        date: new Date(Date.now() - 7 * 24 * 60 * 60 * 1000).toISOString(),
        status: 'Pending',
        comment: 'Enrollment request submitted'
      },
      {
        date: new Date(Date.now() - 5 * 24 * 60 * 60 * 1000).toISOString(),
        status: 'UnderReview',
        comment: 'Application is being reviewed by admissions'
      },
      {
        date: new Date(Date.now() - 2 * 24 * 60 * 60 * 1000).toISOString(),
        status: 'Approved',
        comment: 'Congratulations! Your enrollment has been approved'
      }
    ]);
  }),

  // Transfer API
  http.post('*/transfer/submittransferrequest', async () => {
    await delay(1500);
    return HttpResponse.json({ 
      id: `transfer-${Date.now()}`,
      status: 'Pending',
      submissionDate: new Date().toISOString()
    });
  }),

  http.get('*/transfer/requests', async ({ request }) => {
    await delay(500);
    
    const url = new URL(request.url);
    const page = parseInt(url.searchParams.get('page') || '1', 10);
    const size = parseInt(url.searchParams.get('size') || '10', 10);
    const search = url.searchParams.get('search') || '';
    const sortBy = url.searchParams.get('sortBy') || '';
    const sortOrder = url.searchParams.get('sortOrder') as 'asc' | 'desc' || 'asc';
    
    const paginatedResponse = createPaginatedResponse(
      mockTransferRequests,
      { page, size, search, sortBy, sortOrder },
      search
    );
    
    return HttpResponse.json(paginatedResponse);
  }),

  http.get('*/transfer/history', async () => {
    await delay(500);
    
    return HttpResponse.json([
      {
        date: new Date(Date.now() - 10 * 24 * 60 * 60 * 1000).toISOString(),
        status: 'Pending',
        comment: 'Transfer request submitted'
      },
      {
        date: new Date(Date.now() - 8 * 24 * 60 * 60 * 1000).toISOString(),
        status: 'UnderReview',
        comment: 'Application is being reviewed'
      },
      {
        date: new Date(Date.now() - 6 * 24 * 60 * 60 * 1000).toISOString(),
        status: 'Approved',
        comment: 'Transfer approved, waiting for current school release'
      }
    ]);
  }),

  // User API
  http.put('*/user/update', async () => {
    await delay(800);
    return HttpResponse.json({ success: true });
  }),
];