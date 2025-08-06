import { EnrollmentRequest } from '../../types';

export const mockEnrollmentRequests: EnrollmentRequest[] = [
  {
    id: '1',
    schoolId: '1',
    schoolName: 'Washington High School',
    branchId: '101',
    branchName: 'Main Campus',
    studentFirstName: 'Ryan',
    studentLastName: 'Johnson',
    studentDateOfBirth: '2010-03-15',
    studentGender: 'Male',
    studentGradeLevel: '7',
    parentFirstName: 'Amanda',
    parentLastName: 'Johnson',
    parentEmail: 'amanda.johnson@example.com',
    parentPhone: '(555) 123-4567',
    parentAddress: '123 Pine Street, Seattle, WA 98101',
    parentRelation: 'Mother',
    previousSchool: 'Westside Elementary',
    notes: 'Ryan enjoys math and science, and plays soccer.',
    documents: [
      {
        id: 'doc-1',
        name: 'Birth Certificate',
        type: 'BirthCertificate',
        url: 'https://example.com/documents/birth_certificate_1.pdf',
        uploadDate: '2023-10-15'
      },
      {
        id: 'doc-2',
        name: 'Transcripts',
        type: 'TranscriptOfRecords',
        url: 'https://example.com/documents/transcripts_1.pdf',
        uploadDate: '2023-10-15'
      }
    ],
    status: 'Pending',
    submissionDate: '2023-10-15',
    reviewDate: '',
    reviewerId: '',
    reviewerName: '',
    comments: ''
  },
  {
    id: '2',
    schoolId: '1',
    schoolName: 'Washington High School',
    branchId: '102',
    branchName: 'North Wing',
    studentFirstName: 'Emily',
    studentLastName: 'Brown',
    studentDateOfBirth: '2009-07-22',
    studentGender: 'Female',
    studentGradeLevel: '8',
    parentFirstName: 'Daniel',
    parentLastName: 'Brown',
    parentEmail: 'daniel.brown@example.com',
    parentPhone: '(555) 234-5678',
    parentAddress: '456 Oak Avenue, Seattle, WA 98102',
    parentRelation: 'Father',
    previousSchool: 'Eastside Middle School',
    notes: 'Emily is interested in the arts program and has played violin for 3 years.',
    documents: [
      {
        id: 'doc-3',
        name: 'Birth Certificate',
        type: 'BirthCertificate',
        url: 'https://example.com/documents/birth_certificate_2.pdf',
        uploadDate: '2023-10-10'
      },
      {
        id: 'doc-4',
        name: 'Transcripts',
        type: 'TranscriptOfRecords',
        url: 'https://example.com/documents/transcripts_2.pdf',
        uploadDate: '2023-10-10'
      },
      {
        id: 'doc-5',
        name: 'Medical Records',
        type: 'MedicalRecords',
        url: 'https://example.com/documents/medical_2.pdf',
        uploadDate: '2023-10-10'
      }
    ],
    status: 'UnderReview',
    submissionDate: '2023-10-10',
    reviewDate: '2023-10-17',
    reviewerId: '1',
    reviewerName: 'Patricia Adams',
    comments: 'Reviewing academic records and space availability.'
  },
  {
    id: '3',
    schoolId: '2',
    schoolName: 'Lincoln Elementary School',
    branchId: '201',
    branchName: 'Main Building',
    studentFirstName: 'Michael',
    studentLastName: 'Garcia',
    studentDateOfBirth: '2016-01-05',
    studentGender: 'Male',
    studentGradeLevel: '1',
    parentFirstName: 'Sofia',
    parentLastName: 'Garcia',
    parentEmail: 'sofia.garcia@example.com',
    parentPhone: '(555) 345-6789',
    parentAddress: '789 Maple Drive, Portland, OR 97205',
    parentRelation: 'Mother',
    previousSchool: 'Sunshine Preschool',
    notes: 'Michael is very energetic and social. He is beginning to read independently.',
    documents: [
      {
        id: 'doc-6',
        name: 'Birth Certificate',
        type: 'BirthCertificate',
        url: 'https://example.com/documents/birth_certificate_3.pdf',
        uploadDate: '2023-09-28'
      },
      {
        id: 'doc-7',
        name: 'Medical Records',
        type: 'MedicalRecords',
        url: 'https://example.com/documents/medical_3.pdf',
        uploadDate: '2023-09-28'
      }
    ],
    status: 'Approved',
    submissionDate: '2023-09-28',
    reviewDate: '2023-10-05',
    reviewerId: '2',
    reviewerName: 'Robert Campbell',
    comments: 'Approved enrollment for the upcoming term starting January 2024.'
  },
  {
    id: '4',
    schoolId: '3',
    schoolName: 'Jefferson Middle School',
    branchId: '301',
    branchName: 'Main Campus',
    studentFirstName: 'Olivia',
    studentLastName: 'Martinez',
    studentDateOfBirth: '2011-09-12',
    studentGender: 'Female',
    studentGradeLevel: '6',
    parentFirstName: 'Carlos',
    parentLastName: 'Martinez',
    parentEmail: 'carlos.martinez@example.com',
    parentPhone: '(555) 456-7890',
    parentAddress: '101 Cedar Street, San Francisco, CA 94107',
    parentRelation: 'Father',
    previousSchool: 'Golden Gate Elementary',
    notes: 'Olivia is fluent in both English and Spanish. She excels in mathematics.',
    documents: [
      {
        id: 'doc-8',
        name: 'Birth Certificate',
        type: 'BirthCertificate',
        url: 'https://example.com/documents/birth_certificate_4.pdf',
        uploadDate: '2023-10-05'
      },
      {
        id: 'doc-9',
        name: 'Transcripts',
        type: 'TranscriptOfRecords',
        url: 'https://example.com/documents/transcripts_4.pdf',
        uploadDate: '2023-10-05'
      },
      {
        id: 'doc-10',
        name: 'Proof of Residency',
        type: 'ProofOfResidency',
        url: 'https://example.com/documents/residency_4.pdf',
        uploadDate: '2023-10-05'
      }
    ],
    status: 'Rejected',
    submissionDate: '2023-10-05',
    reviewDate: '2023-10-12',
    reviewerId: '3',
    reviewerName: 'Susan Evans',
    comments: 'Outside of school district boundaries. Recommend applying to San Francisco Unified schools.'
  },
  {
    id: '5',
    schoolId: '4',
    schoolName: 'Roosevelt High School',
    branchId: '401',
    branchName: 'Main Building',
    studentFirstName: 'Ethan',
    studentLastName: 'Wilson',
    studentDateOfBirth: '2007-05-25',
    studentGender: 'Male',
    studentGradeLevel: '10',
    parentFirstName: 'Rebecca',
    parentLastName: 'Wilson',
    parentEmail: 'rebecca.wilson@example.com',
    parentPhone: '(555) 567-8901',
    parentAddress: '202 Elm Road, Chicago, IL 60601',
    parentRelation: 'Mother',
    previousSchool: 'Lincoln High School',
    notes: 'Ethan is transferring due to family relocation. He is interested in joining the robotics team.',
    documents: [
      {
        id: 'doc-11',
        name: 'Birth Certificate',
        type: 'BirthCertificate',
        url: 'https://example.com/documents/birth_certificate_5.pdf',
        uploadDate: '2023-10-02'
      },
      {
        id: 'doc-12',
        name: 'Transcripts',
        type: 'TranscriptOfRecords',
        url: 'https://example.com/documents/transcripts_5.pdf',
        uploadDate: '2023-10-02'
      },
      {
        id: 'doc-13',
        name: 'Medical Records',
        type: 'MedicalRecords',
        url: 'https://example.com/documents/medical_5.pdf',
        uploadDate: '2023-10-02'
      },
      {
        id: 'doc-14',
        name: 'Photo ID',
        type: 'PhotoId',
        url: 'https://example.com/documents/photo_id_5.pdf',
        uploadDate: '2023-10-02'
      }
    ],
    status: 'Pending',
    submissionDate: '2023-10-02',
    reviewDate: '',
    reviewerId: '',
    reviewerName: '',
    comments: ''
  }
];