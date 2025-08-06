import { TransferRequest } from '../../types';

export const mockTransferRequests: TransferRequest[] = [
  {
    id: '1',
    studentId: '1',
    studentName: 'Emma Johnson',
    currentSchoolId: '1',
    currentSchoolName: 'Washington High School',
    destinationSchoolId: '4',
    destinationSchoolName: 'Roosevelt High School',
    destinationBranchId: '401',
    destinationBranchName: 'Main Building',
    reason: 'Family relocation to Chicago due to parental job change.',
    documents: [
      {
        id: 'tdoc-1',
        name: 'Proof of Address',
        type: 'ProofOfResidency',
        url: 'https://example.com/documents/transfer/address_1.pdf',
        uploadDate: '2023-10-12'
      },
      {
        id: 'tdoc-2',
        name: 'Parent Employment Verification',
        type: 'OtherDocuments',
        url: 'https://example.com/documents/transfer/employment_1.pdf',
        uploadDate: '2023-10-12'
      }
    ],
    status: 'Pending',
    submissionDate: '2023-10-12',
    reviewDate: '',
    reviewerId: '',
    reviewerName: '',
    comments: ''
  },
  {
    id: '2',
    studentId: '3',
    studentName: 'Olivia Williams',
    currentSchoolId: '1',
    currentSchoolName: 'Washington High School',
    destinationSchoolId: '3',
    destinationSchoolName: 'Jefferson Middle School',
    destinationBranchId: '302',
    destinationBranchName: 'Arts Wing',
    reason: 'Student wishes to enroll in specialized arts program not available at current school.',
    documents: [
      {
        id: 'tdoc-3',
        name: 'Portfolio Samples',
        type: 'OtherDocuments',
        url: 'https://example.com/documents/transfer/portfolio_2.pdf',
        uploadDate: '2023-10-05'
      },
      {
        id: 'tdoc-4',
        name: 'Art Teacher Recommendation',
        type: 'OtherDocuments',
        url: 'https://example.com/documents/transfer/recommendation_2.pdf',
        uploadDate: '2023-10-05'
      }
    ],
    status: 'UnderReview',
    submissionDate: '2023-10-05',
    reviewDate: '2023-10-10',
    reviewerId: '1',
    reviewerName: 'Patricia Adams',
    comments: 'Reviewing space availability in the arts program.'
  },
  {
    id: '3',
    studentId: '5',
    studentName: 'Ava Jones',
    currentSchoolId: '2',
    currentSchoolName: 'Lincoln Elementary School',
    destinationSchoolId: '5',
    destinationSchoolName: 'Franklin Academy',
    destinationBranchId: '502',
    destinationBranchName: 'Upper School',
    reason: 'Seeking more challenging academic environment with advanced placement options.',
    documents: [
      {
        id: 'tdoc-5',
        name: 'Academic Transcripts',
        type: 'TranscriptOfRecords',
        url: 'https://example.com/documents/transfer/transcripts_3.pdf',
        uploadDate: '2023-09-28'
      },
      {
        id: 'tdoc-6',
        name: 'Teacher Recommendations',
        type: 'OtherDocuments',
        url: 'https://example.com/documents/transfer/recommendations_3.pdf',
        uploadDate: '2023-09-28'
      },
      {
        id: 'tdoc-7',
        name: 'Test Scores',
        type: 'OtherDocuments',
        url: 'https://example.com/documents/transfer/scores_3.pdf',
        uploadDate: '2023-09-28'
      }
    ],
    status: 'Approved',
    submissionDate: '2023-09-28',
    reviewDate: '2023-10-03',
    reviewerId: '2',
    reviewerName: 'Robert Campbell',
    comments: 'Approved for transfer to advanced program based on excellent academic record.'
  },
  {
    id: '4',
    studentId: '7',
    studentName: 'Lucas Davis',
    currentSchoolId: '4',
    currentSchoolName: 'Roosevelt High School',
    destinationSchoolId: '1',
    destinationSchoolName: 'Washington High School',
    destinationBranchId: '101',
    destinationBranchName: 'Main Campus',
    reason: 'Family moving back to Seattle area after temporary relocation.',
    documents: [
      {
        id: 'tdoc-8',
        name: 'Proof of Address',
        type: 'ProofOfResidency',
        url: 'https://example.com/documents/transfer/address_4.pdf',
        uploadDate: '2023-10-08'
      },
      {
        id: 'tdoc-9',
        name: 'Transcripts',
        type: 'TranscriptOfRecords',
        url: 'https://example.com/documents/transfer/transcripts_4.pdf',
        uploadDate: '2023-10-08'
      }
    ],
    status: 'Rejected',
    submissionDate: '2023-10-08',
    reviewDate: '2023-10-15',
    reviewerId: '3',
    reviewerName: 'Susan Evans',
    comments: 'Rejected due to lack of space in requested grade level. Recommended alternative schools in the district.'
  },
  {
    id: '5',
    studentId: '9',
    studentName: 'Ethan Rodriguez',
    currentSchoolId: '3',
    currentSchoolName: 'Jefferson Middle School',
    destinationSchoolId: '4',
    destinationSchoolName: 'Roosevelt High School',
    destinationBranchId: '402',
    destinationBranchName: 'Science Wing',
    reason: 'Seeking enrollment in specialized STEM program for advanced science students.',
    documents: [
      {
        id: 'tdoc-10',
        name: 'Science Competition Awards',
        type: 'OtherDocuments',
        url: 'https://example.com/documents/transfer/awards_5.pdf',
        uploadDate: '2023-10-01'
      },
      {
        id: 'tdoc-11',
        name: 'Science Teacher Recommendation',
        type: 'OtherDocuments',
        url: 'https://example.com/documents/transfer/recommendation_5.pdf',
        uploadDate: '2023-10-01'
      },
      {
        id: 'tdoc-12',
        name: 'Academic Transcripts',
        type: 'TranscriptOfRecords',
        url: 'https://example.com/documents/transfer/transcripts_5.pdf',
        uploadDate: '2023-10-01'
      }
    ],
    status: 'Pending',
    submissionDate: '2023-10-01',
    reviewDate: '',
    reviewerId: '',
    reviewerName: '',
    comments: ''
  }
];