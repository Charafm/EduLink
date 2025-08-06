// src/types.ts

// Existing enums
export enum EnrollmentStatusEnum {
  None = "None",
  Pending = "Pending",
  UnderReview = "UnderReview",
  Approved = "Approved",
  Rejected = "Rejected",
  Completed = "Completed",
  Enrolled = "Enrolled",
  NotEnrolled = 'NotEnrolled'
}

// New “scratch” status for drafts
export type EnrollmentStatusWithDraft = 
  | EnrollmentStatusEnum
  | "Scratch";

// New document enum
export enum DocumentTypeEnum {
  CIN = "CIN",
  BirthCertificate = "BirthCertificate",
  
  SchoolRecords = "SchoolRecords",
  TransferForm = "TransferForm",
  ParentAuthorization = "ParentAuthorization",
  
}
export enum TransferDocumentTypeEnum {
  CIN = "CIN",
  BirthCertificate = "BirthCertificate",
  TransferForm = "TransferForm",
  ParentAuthorization = "ParentAuthorization",
  SchoolRecords = "SchoolRecords",
  
}
// New document enum
export enum TransferRequestReasonEnum {
  
  MovedFromCity = "MovedFromCity",
  MovedFromArea = "MovedFromArea",
  Other  = "Other",
 
}
export enum TransferRequestStatus {
  
   None        = 'None',
  Pending     = 'Pending',
  UnderReview = 'UnderReview',
  Approved    = 'Approved',
  Rejected    = 'Rejected',
  Completed = 'Completed',
  Canceled = 'Canceled'
 
}
export enum StudentEnrollmentStatus  {
  
  Enrolled = 'Enrollment',
  NotEnrolled ='NotEnrollment'
 
}
export interface GradeResourceDTO{
  id: string;
  gradeLevelId: string;
  bookId: string | null; 
  schoolSupplyId: string | null;
  supplyQuantity: number | null;
  gradeLevelName: string;
  resourceTitle: string;
}
// Student DTO (CreateStudentDTO)
export interface CreateStudentDTO {
  id: string;
  firstNameFr: string;
  firstNameAr?: string;
  lastNameFr: string;
  lastNameAr?: string;
  dateOfBirth: string;          // ISO date
  gender: GenderEnum;
  email?: string;
  phone?: string;
  medicalInfo?: string;
  emergencyContact?: string;
  previousSchool?: string;
  additionalNotes?: string;
}
export interface StudentDTO {
  id: string;
  firstNameFr: string;
  firstNameAr?: string;
  lastNameFr: string;
  lastNameAr?: string;
  dateOfBirth: string;          // ISO date
  gender: GenderEnum;
  email?: string;
  phone?: string;
  medicalInfo?: string;
  emergencyContact?: string;
  previousSchool?: string;
  enrollmentStatus: EnrollmentStatusEnum;
  branchId: string;
  additionalNotes?: string;
}
// Gender
export enum GenderEnum {
  Male = "Male",
  Female = "Female",
  Other = "Other",
}

// Enrollment request DTO
export interface EnrollmentRequestDTO {
  id?: string;                              // for drafts
  studentId: string;
  schoolId: string;
  status: EnrollmentStatusWithDraft;
  submittedAt?: string;                     // ISO timestamp
  documents: {
    type: DocumentTypeEnum;
    file?: File;                            // for upload
    url?: string;                           // once uploaded
  }[];
}

// Pagination base
export interface IPagedResultBase {
  currentPage?: number;
  pageCount?: number;
  pageSize?: number;
  rowCount?: number;
  firstRowOnPage?: number;
  lastRowOnPage?: number;
}

// Paged enrollments
export interface IPagedResultOfEnrollmentRequestDTO extends IPagedResultBase {
  results?: EnrollmentRequestDTO[];
}
export interface TransferRequestDTO {
  id: string;
  code: string;                   // e.g. “TR-2025-00123”
  studentId: string;
  studentName: string;
  fromBranchId: string;
  fromBranchName: string;
  toBranchId?: string;
  toBranchName?: string;
  toSchoolId?: string;
  toSchoolName?: string;
  reasonEnum: TransferRequestReasonEnum;
  reason?: string;
  status: TransferRequestStatus;  // Pending, Approved, Rejected, etc.
  submittedAt: string;
}

  export interface StudentDTO {
  id: string;
  firstNameFr: string;
  lastNameFr: string;
  dateOfBirth: string;
  branchId: string;         // where they’re currently enrolled
  branchName: string;
  schoolId?: string;
  schoolName?: string;
}

export interface TransferDocumentUploadDTO {
  transferId: string;
  documentType: DocumentTypeEnum;   // e.g. TransferForm, ParentAuthorization, SchoolRecords
  fileName: string;
  contentType: string;              // e.g. "application/pdf"
  fileContent: string;              // base64 payload
}
export interface CreateTransferRequestDTO {
  studentId: string;
  fromBranchId: string;
  fromSchoolId?: string;
  toBranchId?: string;             // if inter-branch
  toSchoolId?: string;             // if inter-school
  reasonEnum: TransferRequestReasonEnum;
  reason?: string;                 // free text if “Other”
}
export interface TransferDocumentDTO {
  id: string;
  documentType: DocumentTypeEnum;
  filePath: string;
  uploadedAt: string;
  remarks?: string;
}

export interface ParentStudentDTO {
  id: string;
  firstNameFr: string;
  lastNameFr: string;
  dateOfBirth: Date;
  gender: GenderEnum;
  branchName?: string;
  enrollmentStatus: 'Enrolled' | 'NotEnrolled';
}