export type UserType = 'Student' | 'Parent' | 'Staff' | 'SchoolAdmin' | 'Teacher';

export type AuthState = {
  isAuthenticated: boolean; 
  userType: UserType | null;
  username: string | null;
  token: string | null;
  refreshToken: string | null;
};

export interface School {
  id: string;
  name: string;
  address: string;
  city: string;
  state: string;
  zipCode: string;
  phone: string;
  email: string;
  website: string;
  principalName: string;
  districtName: string;
}

export interface Branch {
  id: string;
  name: string;
  address: string;
  schoolId: string;
}

export interface Student {
  id: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  gender: 'Male' | 'Female' | 'Other';
  gradeLevel: string;
  email: string;
  phone: string;
  address: string;
  parentId: string;
  enrollmentDate: string;
  status: 'Active' | 'Inactive' | 'Suspended' | 'Transferred';
}

export interface Parent {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  address: string;
  relation: 'Father' | 'Mother' | 'Guardian' | 'Other';
  students: string[];
}

export interface Teacher {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  address: string;
  subjects: string[];
  hireDate: string;
  status: 'Active' | 'Inactive';
}

export interface Staff {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  address: string;
  position: string;
  department: string;
  hireDate: string;
  status: 'Active' | 'Inactive';
}

export interface Course {
  id: string;
  name: string;
  code: string;
  description: string;
  credits: number;
  gradeLevel: string;
  teacherId: string;
  teacherName: string;
  semester: 'Fall' | 'Spring' | 'Summer';
  year: number;
  status: 'Active' | 'Inactive';
}

export interface Grade {
  id: string;
  studentId: string;
  studentName: string;
  courseId: string;
  courseName: string;
  assignmentId: string;
  assignmentName: string;
  score: number;
  maxScore: number;
  gradeDate: string;
  comments: string;
}

export interface Assignment {
  id: string;
  name: string;
  description: string;
  courseId: string;
  dueDate: string;
  maxScore: number;
  weight: number;
  type: 'Homework' | 'Quiz' | 'Test' | 'Project' | 'Final';
}

export interface AttendanceRecord {
  id: string;
  studentId: string;
  studentName: string;
  courseId: string;
  courseName: string;
  date: string;
  status: 'Present' | 'Absent' | 'Late' | 'Excused';
  notes: string;
}

export interface Schedule {
  id: string;
  courseId: string;
  courseName: string;
  teacherId: string;
  teacherName: string;
  roomNumber: string;
  day: 'Monday' | 'Tuesday' | 'Wednesday' | 'Thursday' | 'Friday';
  startTime: string;
  endTime: string;
  semester: 'Fall' | 'Spring' | 'Summer';
  year: number;
}

export interface Resource {
  id: string;
  name: string;
  type: 'Book' | 'Supply';
  description: string;
  quantity: number;
  price: number;
  required: boolean;
  gradeLevel: string;
  courseId: string;
  courseName: string;
}

export interface EnrollmentRequest {
  id: string;
  schoolId: string;
  schoolName: string;
  branchId: string;
  branchName: string;
  studentFirstName: string;
  studentLastName: string;
  studentDateOfBirth: string;
  studentGender: 'Male' | 'Female' | 'Other';
  studentGradeLevel: string;
  parentFirstName: string;
  parentLastName: string;
  parentEmail: string;
  parentPhone: string;
  parentAddress: string;
  parentRelation: 'Father' | 'Mother' | 'Guardian' | 'Other';
  previousSchool: string;
  notes: string;
  documents: Document[];
  status: 'Pending' | 'UnderReview' | 'Approved' | 'Rejected';
  submissionDate: string;
  reviewDate: string;
  reviewerId: string;
  reviewerName: string;
  comments: string;
}

export interface TransferRequest {
  id: string;
  studentId: string;
  studentName: string;
  currentSchoolId: string;
  currentSchoolName: string;
  destinationSchoolId: string;
  destinationSchoolName: string;
  destinationBranchId: string;
  destinationBranchName: string;
  reason: string;
  documents: Document[];
  status: 'Pending' | 'UnderReview' | 'Approved' | 'Rejected';
  submissionDate: string;
  reviewDate: string;
  reviewerId: string;
  reviewerName: string;
  comments: string;
}

export interface Document {
  id: string;
  name: string;
  type: DocumentType;
  url: string;
  uploadDate: string;
}

export type DocumentType = 
  | 'BirthCertificate'
  | 'TranscriptOfRecords'
  | 'MedicalRecords'
  | 'ProofOfResidency'
  | 'PhotoId'
  | 'OtherDocuments';

export interface PaginatedResponse<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

export interface PageParams {
  page: number;
  size: number;
  search?: string;
  sortBy?: string;
  sortOrder?: 'asc' | 'desc';
}

// src/types/index.ts

/** Gender options for students/parents */
export enum GenderEnum {
  Male   = 'Male',
  Female = 'Female',
  Other  = 'Other',
}

/** Enrollment status stages */
export enum EnrollmentStatusEnum {
  None        = 'None',
  Pending     = 'Pending',
  UnderReview = 'UnderReview',
  Approved    = 'Approved',
  Rejected    = 'Rejected',
  Completed   = 'Completed',
}

/** Mirror of your back-end CreateStudentDTO */
export interface CreateStudentDTO {
  branchId?: string;
  firstNameFr: string;
  firstNameAr?: string;
  lastNameFr: string;
  lastNameAr?: string;
  dateOfBirth: string;            // ISO date string
  gender: GenderEnum;
  enrollmentDate?: string;        // ISO date string
  status?: EnrollmentStatusEnum;
  medicalInfo?: string;
  emergencyContact?: string;
  previousSchool?: string;
  additionalNotes?: string;
  email?: string;
  phone?: string;
}

/** Mirror of your back-end EnrollmentDTO */
export interface EnrollmentDTO {
  studentId?: string;
  branchId?: string;
  status?: EnrollmentStatusEnum;
  submittedAt?: string;           // ISO date string
  adminComment?: string;
}

/** (Optional) Utility type for paginated lists */
export interface PaginatedResult<T> {
  currentPage: number;
  pageSize: number;
  pageCount: number;
  rowCount: number;
  results: T[];
}
