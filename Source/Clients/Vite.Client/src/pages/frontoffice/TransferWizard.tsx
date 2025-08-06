// src/pages/frontoffice/TransferWizard.tsx
import React, { useState } from 'react'
import { Button } from '../../components/ui/Button'
import { Select } from '../../components/ui/Select'
import { Input } from '../../components/ui/Input'
import { useToast } from '../../components/ui/Toast'
import {
  StudentDTO,
  StudentEnrollmentStatus,
  TransferDocumentTypeEnum,
  TransferRequestReasonEnum,
} from '../../types/types'
import { GenderEnum } from '../../types'

// ——— mock dashboard data ———
interface TransferSummary {
  id: string
  studentName: string
  submittedAt: string
  status: 'Pending' | 'UnderReview' | 'Approved' | 'Rejected'
}
const mockDashboard: TransferSummary[] = [
  { id: 't1', studentName: 'Alice Martin', submittedAt: '2025-01-15', status: 'Pending' },
  { id: 't2', studentName: 'Youssef El Idrissi', submittedAt: '2025-02-05', status: 'UnderReview' },
  { id: 't3', studentName: 'Karim Benali', submittedAt: '2025-01-28', status: 'Approved' },
  { id: 't4', studentName: 'Leila Haddad', submittedAt: '2025-02-10', status: 'Rejected' },
]

// ——— mock children & branches/schools ———
const mockChildren: StudentDTO[] = [
  {
    id: 'c1',
    firstNameFr: 'Alice',
    lastNameFr: 'Martin',
    dateOfBirth: '2012-05-10',
    branchId: 'b1',
    gender: GenderEnum.Female,
    branchName: 'Rabat Branch',
    enrollmentStatus: StudentEnrollmentStatus.Enrolled,
  },
  {
    id: 'c2',
    firstNameFr: 'Youssef',
    lastNameFr: 'El Idrissi',
    dateOfBirth: '2010-11-23',
    branchId: 'b2',
    gender: GenderEnum.Male,
    branchName: 'Casablanca Branch',
    enrollmentStatus: StudentEnrollmentStatus.Enrolled,
  },
]
const mockBranches = [
  { id: 'b1', name: 'Rabat Branch' },
  { id: 'b2', name: 'Casablanca Branch' },
]
const mockSchools = [
  { id: 's1', name: 'Lycée Français' },
  { id: 's2', name: 'École Martyr' },
]

type TransferType = 'branch' | 'school'

export const TransferWizard: React.FC = () => {
  const { toast } = useToast()

  // overall mode
  const [mode, setMode] = useState<'dashboard' | 'wizard'>('dashboard')

  // wizard steps
  const [step, setStep] = useState(1)
  const next = () => setStep((s) => Math.min(s + 1, 4))
  const back = () => setStep((s) => Math.max(s - 1, 1))

  // step 1
  const [selectedChild, setSelectedChild] = useState<StudentDTO | null>(null)

  // step 2
  const [type, setType] = useState<TransferType | null>(null)
  const [showComingSoon, setShowComingSoon] = useState(false)
  const [toBranchId, setToBranchId] = useState('')
  const [toSchoolId, setToSchoolId] = useState('')

  // step 3
  const [reasonEnum, setReasonEnum] = useState<TransferRequestReasonEnum | ''>('')
  const [reasonText, setReasonText] = useState('')
  const [docs, setDocs] = useState<Record<TransferDocumentTypeEnum, File | null>>({
    [TransferDocumentTypeEnum.TransferForm]: null,
    [TransferDocumentTypeEnum.ParentAuthorization]: null,
    [TransferDocumentTypeEnum.SchoolRecords]: null,
    [TransferDocumentTypeEnum.CIN]: null,
    [TransferDocumentTypeEnum.BirthCertificate]: null,
  })

  // review
  const review = {
    student: selectedChild,
    destination:
      type === 'branch'
        ? mockBranches.find((b) => b.id === toBranchId)
        : mockSchools.find((s) => s.id === toSchoolId),
    reasonEnum,
    reasonText,
    docs,
  }

  // wizard validation
  const canNext = () => {
    if (step === 1) return selectedChild !== null
    if (step === 2) return type === 'branch' ? !!toBranchId : type === 'school' ? !!toSchoolId : false
    if (step === 3) return !!reasonEnum
    return true
  }

  // final submit
  const handleSubmit = () => {
    if (!selectedChild) {
      toast({ title: 'Error', description: 'Pick a student', variant: 'error' })
      return
    }
    if (type === 'branch' && !toBranchId) {
      toast({ title: 'Error', description: 'Select a branch', variant: 'error' })
      return
    }
    if (type === 'school' && !toSchoolId) {
      toast({ title: 'Error', description: 'Select a school', variant: 'error' })
      return
    }
    if (!reasonEnum) {
      toast({ title: 'Error', description: 'Select a reason', variant: 'error' })
      return
    }

    // TODO: call your API…
    toast({ title: 'Success', description: 'Transfer request submitted', variant: 'success' })

    // reset & back to dash
    setStep(1)
    setSelectedChild(null)
    setType(null)
    setToBranchId('')
    setToSchoolId('')
    setReasonEnum('')
    setReasonText('')
    setDocs({
      [TransferDocumentTypeEnum.TransferForm]: null,
      [TransferDocumentTypeEnum.ParentAuthorization]: null,
      [TransferDocumentTypeEnum.SchoolRecords]: null,
      [TransferDocumentTypeEnum.CIN]: null,
      [TransferDocumentTypeEnum.BirthCertificate]: null,
    })
    setMode('dashboard')
  }

  // card definitions
  const typeCards = [
    {
      type: 'branch' as TransferType,
      title: 'Inter-Branch',
      description: 'Within your network',
      disabled: false,
      bgClass: 'bg-green-50',
    },
    {
      type: 'school' as TransferType,
      title: 'Inter-School',
      description: 'Different school (soon)',
      disabled: true,
      bgClass: 'bg-gray-100',
    },
  ]

  // ——— DASHBOARD ———
  if (mode === 'dashboard') {
    const statuses: TransferSummary['status'][] = [
      'Pending',
      'UnderReview',
      'Approved',
      'Rejected',
    ]
    return (
      <div className="max-w-4xl mx-auto p-6 space-y-8">
        <header className="flex justify-between items-center">
          <h1 className="text-3xl font-bold">My Transfer Requests</h1>
          <Button onClick={() => setMode('wizard')}>+ New Transfer</Button>
        </header>

        {statuses.map((status) => (
          <section key={status} className="space-y-2">
            <h2 className="text-xl font-semibold">{status}</h2>
            <table className="w-full border">
              <thead className="bg-gray-100">
                <tr>
                  <th className="px-4 py-2">Student</th>
                  <th className="px-4 py-2">Submitted</th>
                </tr>
              </thead>
              <tbody>
                {mockDashboard.filter((r) => r.status === status).map((r) => (
                  <tr key={r.id} className="border-t">
                    <td className="px-4 py-2">{r.studentName}</td>
                    <td className="px-4 py-2">{r.submittedAt}</td>
                  </tr>
                ))}
                {mockDashboard.filter((r) => r.status === status).length === 0 && (
                  <tr>
                    <td colSpan={2} className="px-4 py-2 text-center text-gray-500">
                      — none —
                    </td>
                  </tr>
                )}
              </tbody>
            </table>
          </section>
        ))}
      </div>
    )
  }

  // ——— WIZARD ———
  return (
    <div className="max-w-3xl mx-auto p-6 space-y-8">
      <div className="flex items-center space-x-4">
        <Button variant="outline" onClick={() => setMode('dashboard')}>
          ← Dashboard
        </Button>
        <h1 className="text-3xl font-bold">New Transfer</h1>
      </div>

      {/* step indicator */}
      <div className="flex space-x-4">
        {[1, 2, 3, 4].map((s) => (
          <div
            key={s}
            className={`flex-1 text-center py-2 rounded ${
              step === s ? 'bg-blue-600 text-white' : 'bg-gray-100'
            }`}
          >
            Step {s}
          </div>
        ))}
      </div>

      {/* STEP PANELS */}
      {step === 1 && (
        <section className="space-y-4">
          <h2 className="text-xl font-semibold">1. Select Your Child</h2>
          <Select
            label="Child"
            options={[
              { label: '— Select —', value: '' },
              ...mockChildren.map((c) => ({
                label: `${c.firstNameFr} ${c.lastNameFr}`,
                value: c.id,
              })),
            ]}
            value={selectedChild?.id || ''}
            onChange={(v) =>
              setSelectedChild(mockChildren.find((x) => x.id === v) || null)
            }
          />
        </section>
      )}

      {step === 2 && (
        <section className="space-y-4">
          <h2 className="text-xl font-semibold">2. Type & Destination</h2>
          <div className="grid grid-cols-2 gap-4">
            {typeCards.map((card) => (
              <button
                key={card.type}
                className={`${card.bgClass} rounded-xl p-6 text-center ${
                  card.disabled ? 'opacity-50' : 'hover:scale-105'
                } ${type === card.type ? 'ring-2 ring-blue-500' : ''}`}
                disabled={card.disabled}
                onClick={() => {
                  if (card.disabled) {
                    setShowComingSoon(true)
                  } else {
                    setType(card.type)
                  }
                }}
              >
                <h3 className="font-medium">{card.title}</h3>
                <p className="text-sm text-gray-600">{card.description}</p>
              </button>
            ))}
          </div>

          {type === 'branch' && (
            <Select
              label="To Branch"
              options={[
                { label: '— Select —', value: '' },
                ...mockBranches.map((b) => ({ label: b.name, value: b.id })),
              ]}
              value={toBranchId}
              onChange={setToBranchId}
            />
          )}
          {type === 'school' && (
            <Select
              label="To School"
              options={[
                { label: '— Select —', value: '' },
                ...mockSchools.map((s) => ({ label: s.name, value: s.id })),
              ]}
              value={toSchoolId}
              onChange={setToSchoolId}
            />
          )}
        </section>
      )}

      {step === 3 && (
        <section className="space-y-4">
          <h2 className="text-xl font-semibold">3. Reason & Documents</h2>

          <Select
            label="Reason"
            options={[
              { label: '— Select —', value: '' },
              ...Object.values(TransferRequestReasonEnum).map((r) => ({
                label: r,
                value: r,
              })),
            ]}
            value={reasonEnum}
            onChange={(v) => setReasonEnum(v as TransferRequestReasonEnum)}
          />
          {reasonEnum === TransferRequestReasonEnum.Other && (
            <Input
              label="Other Reason"
              value={reasonText}
              onChange={(e) => setReasonText(e.target.value)}
            />
          )}

          {Object.entries(docs).map(([t, f]) => (
            <div key={t}>
              <label className="block font-medium">{t}</label>
              <Input
                type="file"
                onChange={(e) => {
                  const file = e.target.files?.[0] || null
                  setDocs((d) => ({ ...d, [t]: file }))
                }}
              />
              {f && <p className="text-sm text-gray-600">{f.name}</p>}
            </div>
          ))}
        </section>
      )}

      {step === 4 && (
        <section className="space-y-6">
          <h2 className="text-xl font-semibold">4. Review & Submit</h2>

          <div className="bg-gray-50 p-4 rounded">
            <h3 className="font-medium mb-2">Student</h3>
            <p>
              {review.student?.firstNameFr} {review.student?.lastNameFr}
            </p>
            <p>DOB: {review.student?.dateOfBirth}</p>
          </div>

          <div className="bg-gray-50 p-4 rounded">
            <h3 className="font-medium mb-2">Details</h3>
            <p>Type: {type === 'branch' ? 'Inter-Branch' : 'Inter-School'}</p>
            <p>Destination: {review.destination?.name}</p>
            <p>
              Reason: {reasonEnum}
              {reasonEnum === TransferRequestReasonEnum.Other
                ? `: ${reasonText}`
                : ''}
            </p>
          </div>

          <div className="bg-gray-50 p-4 rounded">
            <h3 className="font-medium mb-2">Documents</h3>
            <ul className="list-disc list-inside">
              {Object.entries(review.docs).map(([t, f]) =>
                f ? (
                  <li key={t}>
                    {t}: {f.name}
                  </li>
                ) : null
              )}
            </ul>
          </div>
        </section>
      )}

      {/* Coming Soon popup */}
      {showComingSoon && (
        <div className="fixed inset-0 flex items-center justify-center z-50">
          <div
            className="absolute inset-0 bg-black/40"
            onClick={() => setShowComingSoon(false)}
          />
          <div className="bg-white p-6 rounded shadow-lg z-10">
            <h3 className="text-lg font-bold">Inter-School Transfer</h3>
            <p className="my-4">This feature is under construction.</p>
            <Button onClick={() => setShowComingSoon(false)}>Close</Button>
          </div>
        </div>
      )}

      {/* Navigation */}
      <div className="flex justify-between mt-6">
        {step > 1 ? (
          <Button variant="outline" onClick={back}>
            Back
          </Button>
        ) : (
          <div />
        )}

        {step < 4 ? (
          <Button onClick={next} disabled={!canNext()}>
            Next
          </Button>
        ) : (
          <div className="space-x-2">
            <Button variant="outline" onClick={() => setMode('dashboard')}>
              Save & Exit
            </Button>
            <Button onClick={handleSubmit}>Submit Transfer</Button>
          </div>
        )}
      </div>
    </div>
  )
}
