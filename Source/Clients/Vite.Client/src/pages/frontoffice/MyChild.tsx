import React, { useEffect, useState } from 'react'
import {
  Table,
  TableHeader,
  TableRow,
  TableHead,
  TableBody,
  TableCell,
} from '../../components/ui/Table'
import { Button } from '../../components/ui/Button'
import {
  StudentDTO,
  EnrollmentStatusEnum,
  GenderEnum,
} from '../../types/types'
import { useNavigate } from 'react-router-dom'
import { useToast } from '../../components/ui/Toast'

// — Mock API — replace with real hook/fetch
const mockParentStudents: StudentDTO[] = [
  {
    id: 'c1',
    firstNameFr: 'Alice',
    lastNameFr: 'Martin',
    dateOfBirth: '2012-05-10',
    gender: GenderEnum.Female,
    branchName: 'Rabat Branch',
    enrollmentStatus: EnrollmentStatusEnum.Enrolled,
    branchId: '',
  },
  {
    id: 'c2',
    firstNameFr: 'Youssef',
    lastNameFr: 'El Idrissi',
    dateOfBirth: '2010-11-23',
    gender: GenderEnum.Male,
    branchName: '',
    enrollmentStatus: EnrollmentStatusEnum.NotEnrolled,
    branchId: '',
  },
]

function fetchParentStudents(): Promise<StudentDTO[]> {
  return new Promise((res) => setTimeout(() => res(mockParentStudents), 300))
}

export const MyChildren: React.FC = () => {
  const { toast } = useToast()
  const navigate = useNavigate()
  const [children, setChildren] = useState<StudentDTO[]>([])
  const [loading, setLoading] = useState(true)
  const [selectedChild, setSelectedChild] = useState<StudentDTO | null>(null)

  useEffect(() => {
    fetchParentStudents()
      .then(setChildren)
      .catch(() =>
        toast({
          title: 'Error',
          description: 'Failed to load children',
          variant: 'error',
        })
      )
      .finally(() => setLoading(false))
  }, [toast])

  const enrolled = children.filter(
    (c) => c.enrollmentStatus === EnrollmentStatusEnum.Enrolled
  )
  const notEnrolled = children.filter(
    (c) => c.enrollmentStatus !== EnrollmentStatusEnum.Enrolled
  )

  return (
    <div className="max-w-4xl mx-auto p-6 space-y-8">
      <h1 className="text-3xl font-bold">My Children</h1>
      {loading && <p>Loading…</p>}
      {!loading && (
        <>
          {/* Enrolled Students */}
          <section className="space-y-4">
            <h2 className="text-2xl font-semibold">Enrolled Students</h2>
            {enrolled.length === 0 ? (
              <p className="text-gray-500">No enrolled students found.</p>
            ) : (
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableHead>Name</TableHead>
                    <TableHead>DOB</TableHead>
                    <TableHead>Gender</TableHead>
                    <TableHead>Branch</TableHead>
                    <TableHead>Status</TableHead>
                    <TableHead>Actions</TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {enrolled.map((child) => (
                    <TableRow key={child.id}>
                      <TableCell>
                        {child.firstNameFr} {child.lastNameFr}
                      </TableCell>
                      <TableCell>{child.dateOfBirth}</TableCell>
                      <TableCell>{child.gender}</TableCell>
                      <TableCell>{child.branchName || '—'}</TableCell>
                      <TableCell>Enrolled</TableCell>
                      <TableCell>
                        <Button
                          size="sm"
                          onClick={() => setSelectedChild(child)}
                        >
                          View
                        </Button>
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            )}
          </section>

          {/* Not Enrolled Students */}
          <section className="space-y-4">
            <h2 className="text-2xl font-semibold">Not Enrolled Yet</h2>
            {notEnrolled.length === 0 ? (
              <p className="text-gray-500">No pending children.</p>
            ) : (
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableHead>Name</TableHead>
                    <TableHead>DOB</TableHead>
                    <TableHead>Gender</TableHead>
                    <TableHead>Branch</TableHead>
                    <TableHead>Status</TableHead>
                    <TableHead>Actions</TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {notEnrolled.map((child) => (
                    <TableRow key={child.id}>
                      <TableCell>
                        {child.firstNameFr} {child.lastNameFr}
                      </TableCell>
                      <TableCell>{child.dateOfBirth}</TableCell>
                      <TableCell>{child.gender}</TableCell>
                      <TableCell>{child.branchName || '—'}</TableCell>
                      <TableCell>Not Enrolled</TableCell>
                      <TableCell>
                        <Button
                          size="sm"
                          variant="outline"
                          onClick={() => navigate('/parent/enrollment')}
                        >
                          Enroll
                        </Button>
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            )}
          </section>

          {/* Modal for Student Details */}
          {selectedChild && (
            <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
              <div className="bg-white rounded-lg shadow-lg p-6 w-96">
                <h3 className="text-xl font-semibold mb-4">Student Details</h3>
                <p><strong>Name:</strong> {selectedChild.firstNameFr} {selectedChild.lastNameFr}</p>
                <p><strong>DOB:</strong> {selectedChild.dateOfBirth}</p>
                <p><strong>Gender:</strong> {selectedChild.gender}</p>
                <p><strong>Branch:</strong> {selectedChild.branchName || '—'}</p>
                <div className="mt-6 text-right">
                  <Button size="sm" onClick={() => setSelectedChild(null)}>
                    Close
                  </Button>
                </div>
              </div>
            </div>
          )}
        </>
      )}
    </div>
  )
}
