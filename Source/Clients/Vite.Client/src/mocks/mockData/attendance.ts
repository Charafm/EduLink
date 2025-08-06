import { AttendanceRecord } from '../../types';

export const mockAttendance: AttendanceRecord[] = [
  {
    id: '1',
    studentId: '1',
    studentName: 'Emma Johnson',
    courseId: '1',
    courseName: 'Algebra I',
    date: '2023-09-05',
    status: 'Present',
    notes: ''
  },
  {
    id: '2',
    studentId: '1',
    studentName: 'Emma Johnson',
    courseId: '1',
    courseName: 'Algebra I',
    date: '2023-09-06',
    status: 'Present',
    notes: ''
  },
  {
    id: '3',
    studentId: '1',
    studentName: 'Emma Johnson',
    courseId: '1',
    courseName: 'Algebra I',
    date: '2023-09-07',
    status: 'Absent',
    notes: 'Doctor appointment'
  },
  {
    id: '4',
    studentId: '1',
    studentName: 'Emma Johnson',
    courseId: '3',
    courseName: 'Biology',
    date: '2023-09-05',
    status: 'Present',
    notes: ''
  },
  {
    id: '5',
    studentId: '1',
    studentName: 'Emma Johnson',
    courseId: '3',
    courseName: 'Biology',
    date: '2023-09-07',
    status: 'Absent',
    notes: 'Doctor appointment'
  },
  {
    id: '6',
    studentId: '2',
    studentName: 'Liam Smith',
    courseId: '2',
    courseName: 'English Literature',
    date: '2023-09-05',
    status: 'Present',
    notes: ''
  },
  {
    id: '7',
    studentId: '2',
    studentName: 'Liam Smith',
    courseId: '2',
    courseName: 'English Literature',
    date: '2023-09-06',
    status: 'Late',
    notes: 'Bus delay - 10 minutes'
  },
  {
    id: '8',
    studentId: '2',
    studentName: 'Liam Smith',
    courseId: '4',
    courseName: 'American History',
    date: '2023-09-05',
    status: 'Present',
    notes: ''
  },
  {
    id: '9',
    studentId: '3',
    studentName: 'Olivia Williams',
    courseId: '3',
    courseName: 'Biology',
    date: '2023-09-05',
    status: 'Present',
    notes: ''
  },
  {
    id: '10',
    studentId: '3',
    studentName: 'Olivia Williams',
    courseId: '3',
    courseName: 'Biology',
    date: '2023-09-06',
    status: 'Present',
    notes: ''
  },
  {
    id: '11',
    studentId: '3',
    studentName: 'Olivia Williams',
    courseId: '6',
    courseName: 'Art Fundamentals',
    date: '2023-09-07',
    status: 'Present',
    notes: ''
  },
  {
    id: '12',
    studentId: '4',
    studentName: 'Noah Brown',
    courseId: '1',
    courseName: 'Algebra I',
    date: '2023-09-05',
    status: 'Present',
    notes: ''
  },
  {
    id: '13',
    studentId: '4',
    studentName: 'Noah Brown',
    courseId: '1',
    courseName: 'Algebra I',
    date: '2023-09-06',
    status: 'Excused',
    notes: 'Family emergency'
  },
  {
    id: '14',
    studentId: '4',
    studentName: 'Noah Brown',
    courseId: '7',
    courseName: 'Computer Science Principles',
    date: '2023-09-07',
    status: 'Present',
    notes: ''
  },
  {
    id: '15',
    studentId: '5',
    studentName: 'Ava Jones',
    courseId: '2',
    courseName: 'English Literature',
    date: '2023-09-05',
    status: 'Present',
    notes: ''
  }
];