// src/pages/frontoffice/EnrollmentWizard.tsx
import React, { useState, useRef, useEffect } from "react"
import { CardBody } from "../../components/ui/Card"
import { Button } from "../../components/ui/Button"
import { Select, SelectOption } from "../../components/ui/Select"
import { Input } from "../../components/ui/Input"
import { useToast } from "../../components/ui/Toast"
import { usePageTitle } from "../../hooks/usePageTitle"
import {
  CreateStudentDTO,
  ParsedStudentDto,
  EnrollmentRequestDTO,
  EnrollmentStatusEnum,
  DocumentTypeEnum,
  GenderEnum,

  EnrollmentRequestStatusEnum,
  StudentsClient,
  EnrollmentClient,
  ParentsClient,
  CreateEnrollmentRequestDTO,
} from "../../api/BO-api.service"
import { useRegions, useCitiesByRegion, useSchoolsByCity } from "../../lib/db"
import axios from "axios"

type Step = 1 | 2 | 3 | 4

interface ModalProps {
  onClose(): void
  children: React.ReactNode
}
const Modal: React.FC<ModalProps> = ({ onClose, children }) => (
  <div className="fixed inset-0 flex items-center justify-center z-50">
    <div className="absolute inset-0 bg-black/40" onClick={onClose} />
    <div className="relative bg-white rounded-lg shadow p-6 w-11/12 max-w-lg">
      {children}
      <div className="mt-4 text-right">
        <Button onClick={onClose}>Close</Button>
      </div>
    </div>
  </div>
)

async function toBase64(file: File): Promise<string> {
  return new Promise((resolve, reject) => {
    const reader = new FileReader()
    reader.onload = () => {
      const dataUrl = reader.result as string
      const idx = dataUrl.indexOf(",")
      resolve(idx >= 0 ? dataUrl.slice(idx + 1) : dataUrl)
    }
    reader.onerror = () => reject(reader.error)
    reader.readAsDataURL(file)
  })
}

export const EnrollmentWizard: React.FC = () => {
  usePageTitle("Enrollment Requests")
  const { toast } = useToast()

  // Auth setup
  const token    = localStorage.getItem("edulink_token")  ?? ""
  const parentId = localStorage.getItem("edulink_parentId") ?? ""
  const authAxios = axios.create({
    baseURL: import.meta.env.VITE_BACKOFFICE_API_URL,
    headers: { Authorization: token ? `Bearer ${token}` : undefined },
  })
  const studentsClient = new StudentsClient(undefined, authAxios)
  const enrollClient   = new EnrollmentClient(undefined, authAxios)
  const parentClient   = new ParentsClient(undefined, authAxios)

  // OVERVIEW
  const [view, setView] = useState<"overview"|"wizard">("overview")
  const [students, setStudents]       = useState<ParsedStudentDto[]>([])
  const [enrollments, setEnrollments] = useState<EnrollmentRequestDTO[]>([])
  const [detailsModal, setDetailsModal] = useState<EnrollmentRequestDTO|null>(null)

  // WIZARD
  const [completed, setCompleted] = useState<Record<Step,boolean>>({
    1: false, 2: false, 3: false, 4: false,
  })
  const [selectedStudent, setSelectedStudent] = useState<ParsedStudentDto|null>(null)
  const [newStudent, setNewStudent]           = useState<Partial<CreateStudentDTO>>({})
  const [addingStudent, setAddingStudent]     = useState(false)
  const [region, setRegion] = useState("")
  const [city,   setCity]   = useState("")
  const [school, setSchool] = useState("")

  const [documents, setDocuments] = useState<Record<DocumentTypeEnum,File|null>>(
    Object.values(DocumentTypeEnum).reduce((acc, dt) => {
      acc[dt] = null
      return acc
    }, {} as Record<DocumentTypeEnum,File|null>)
  )

  const { data: regions } = useRegions()
  const { data: cities }  = useCitiesByRegion(region)
  const { data: schools } = useSchoolsByCity(city)
  const stepRefs = useRef<Record<Step,HTMLDetailsElement|null>>({
    1: null, 2: null, 3: null, 4: null,
  })

  // load overview + your associated students
  useEffect(() => {
    ;(async () => {
      try {
       const eRes = await enrollClient.getEnrollmentRequests(
        null,
        null,
        parentId,
        null,
        1,
        20
      )
      setEnrollments(eRes.results ?? [])
      } catch {
        toast({ title:"Error", description:"Cannot load enrollment requests", variant:"error" })
      }
     try {
        const sRes = await parentClient.getAssociatedStudents(parentId, 1, 20)
        setStudents(sRes.results ?? [])
      } catch {
        toast({ title:"Error", description:"Cannot load your students", variant:"error" })
      }
    })()
  }, [parentId])

  // auto-scroll
  useEffect(() => {
    for (const step of [1,2,3,4] as Step[]) {
      if (!completed[step]) {
        stepRefs.current[step]?.scrollIntoView({ behavior:"smooth" })
        break
      }
    }
  }, [completed])

  // STEP 1: create or pick
  const saveStudent = async () => {
    // basic client-side validation
    for (const f of ["firstNameFr","lastNameFr","dateOfBirth","gender"] as const) {
      if (!(newStudent as any)[f]) {
        return toast({ title:"Missing", description:`${f} is required`, variant:"error" })
      }
    }

    // build DTO—no stub methods
    const dto: CreateStudentDTO = {
      parentId:      parentId,
      firstNameFr:     newStudent.firstNameFr!,
      lastNameFr:      newStudent.lastNameFr!,
      firstNameAr:     newStudent.firstNameAr ?? newStudent.firstNameFr,
      lastNameAr:      newStudent.lastNameAr ?? newStudent.lastNameFr,
      dateOfBirth:     newStudent.dateOfBirth!,
      gender:          newStudent.gender!,
      status:          "Active",
      emergencyContact:newStudent.emergencyContact ?? "",
      email:           newStudent.email            ?? `${newStudent.firstNameFr}.${newStudent.lastNameFr}@Edulink.ma`,
      phone:           newStudent.phone            ?? "0800808080808",
    }

    console.log("Creating student:", dto)
    try {
      const created = await studentsClient.create(dto)
      console.log("CreateStudent response:", created)
      setStudents((s) => [...s, created])
      setSelectedStudent(created)
      setCompleted((c) => ({ ...c, 1: true }))
      setAddingStudent(false)
      toast({ title:"Student Created", description:"Your child has been added", variant:"success" })
    } catch (err) {
      console.error("CreateStudent error:", err)
      toast({ title:"Error", description:"Failed to create student", variant:"error" })
    }
  }
  const pickStudent = (id: string) => {
    const found = students.find((s) => s.id === id)
    if (found) {
      setSelectedStudent(found)
      setCompleted((c) => ({ ...c, 1: true }))
    }
  }

  const completeStep2 = () => {
    if (!school) return toast({ title:"Missing", description:"Choose a school", variant:"error" })
    setCompleted((c) => ({ ...c, 2: true }))
  }
  const completeStep3 = () => {
    if (!documents[DocumentTypeEnum.CIN] || !documents[DocumentTypeEnum.BirthCertificate]) {
      return toast({ title:"Missing", description:"Upload both documents", variant:"error" })
    }
    setCompleted((c) => ({ ...c, 3: true }))
  }

  // submit all
  const submitAll = async () => {
    if (!selectedStudent || !school) {
      return toast({ title:"Incomplete", description:"Please finish all steps", variant:"error" })
    }
     let reqId: string
  try {
    // createEnrollmentRequest returns an EnrollmentRequestDTO
    const dto: CreateEnrollmentRequestDTO = {
        studentId: selectedStudent.id!,
        branchId: 'FE5CBD73-C197-4A12-89B1-030F73035A0C',
        
        
        documents: [],
       
      }
    const createdReq = await enrollClient.createRequest(dto)
    console.log("createEnrollmentRequest response:", createdReq)
    
    reqId = createdReq!
  } catch (err) {
    console.error("createEnrollmentRequest failed:", err)
    return toast({ title:"Error", description:"Could not create request", variant:"error" })
  }

    // upload each doc
    for (const dt of Object.values(DocumentTypeEnum)) {
      const file = documents[dt]
      if (!file) continue
      const content = await toBase64(file)
      const dto: CreateEnrollmentRequestDTO = {
        studentId: selectedStudent.id!,
        branchId: crypto.randomUUID(),
        
        
        documents: undefined,
      }
      await enrollClient.saveRequest(dto)
    }

    await enrollClient.submitEnrollmentRequest(reqId)
    toast({ title:"Success", description:"Enrollment submitted", variant:"success" })
    setView("overview")

    // reload
    try {
      const eRes = await enrollClient.getEnrollmentRequests(null,null,parentId,null,1,20)
      setEnrollments(eRes.results ?? [])
    } catch {
      /* ignore */
    }
  }

  // ---- RENDER: OVERVIEW ----
  if (view === "overview") {
    const byStatus = (sts: EnrollmentStatusEnum[]) =>
      enrollments.filter((e) => sts.includes(e.status as any))

    return (
      <div className="space-y-6">
        <header className="flex justify-between items-center">
          <h2 className="text-2xl font-semibold">Enrollment Requests</h2>
          <Button onClick={() => setView("wizard")}>+ New Enrollment</Button>
        </header>

        {[
          { title:"Pending",     sts:[EnrollmentStatusEnum.Pending,  EnrollmentRequestStatusEnum.Draft]      },
          { title:"Under Review", sts:[EnrollmentStatusEnum.UnderReview]   },
          { title:"Done / Rejected",
            sts:[EnrollmentStatusEnum.Approved,EnrollmentStatusEnum.Rejected]
          },
        ].map(({title,sts}) => {
          const list = byStatus(sts)
          return (
            <div key={title} className="border rounded overflow-hidden">
              <div className="bg-gray-100 px-4 py-2 font-medium">
                {title} ({list.length})
              </div>
              <table className="w-full text-left">
                <thead>
                  <tr>
                    <th className="px-4 py-2">Code</th>
                    <th className="px-4 py-2">Date</th>
                    <th className="px-4 py-2">Status</th>
                    <th className="px-4 py-2">Action</th>
                  </tr>
                </thead>
                <tbody>
                  {list.length > 0 ? list.map((e) => {
                    
                    return (
                      <tr key={e.id} className="border-t">
                        <td className="px-4 py-2">
                         {e.requestCode}
                        </td>
                        <td className="px-4 py-2">
                          {e.submittedAt?.toISOString().slice(0,10) || "–"}
                        </td>
                        <td className="px-4 py-2">{e.status}</td>
                        <td className="px-4 py-2">
                          <Button size="sm" onClick={() => setDetailsModal(e)}>
                            View
                          </Button>
                        </td>
                      </tr>
                    )
                  }) : (
                    <tr>
                      <td colSpan={4} className="px-4 py-8 text-center text-gray-500">
                        No requests
                      </td>
                    </tr>
                  )}
                </tbody>
              </table>
            </div>
          )
        })}

        {detailsModal && (
          <Modal onClose={() => setDetailsModal(null)}>
            <h3 className="text-lg font-bold mb-2">Details</h3>
            <pre className="bg-gray-100 p-4 rounded">
              {JSON.stringify(detailsModal, null, 2)}
            </pre>
          </Modal>
        )}
      </div>
    )
  }

  // ---- RENDER: WIZARD ----
  const available = students.filter((s) =>
    !enrollments.some((e) => e.studentId === s.id)
  )

  return (
    <div className="space-y-6">
      {([1,2,3,4] as Step[]).map((step) => (
        <details
          key={step}
          ref={(el) => (stepRefs.current[step] = el)}
          open={step === 1 ? true : completed[(step-1) as Step]}
          className="border rounded"
        >
          <summary className="px-4 py-2 bg-blue-50 cursor-pointer">
            Step {step} {completed[step] && "✅"}
          </summary>
          <CardBody>
            {/* STEP 1 */}
            {step === 1 && (
              <>
                <div className="flex justify-end mb-2">
                  {!addingStudent && (
                    <Button size="sm" onClick={() => setAddingStudent(true)}>
                      + Add Student
                    </Button>
                  )}
                </div>
                {addingStudent ? (
                  <div className="space-y-4">
                    <Input
                      label="First Name (FR)*"
                      value={newStudent.firstNameFr||""}
                      onChange={(e) => setNewStudent((n) => ({...n,firstNameFr:e.target.value}))}
                    />
                    <Input
                      label="Last Name (FR)*"
                      value={newStudent.lastNameFr||""}
                      onChange={(e) => setNewStudent((n) => ({...n,lastNameFr:e.target.value}))}
                    />
                    <Input
                      label="Date of Birth*"
                      type="date"
                      value={newStudent.dateOfBirth
                        ? newStudent.dateOfBirth.toISOString().split("T")[0]
                        : ""}
                      onChange={(e) => setNewStudent((n) => ({...n,dateOfBirth:new Date(e.target.value)}))}
                    />
                    <Select
                      label="Gender*"
                      options={[
                        { label:"— Select —", value:"" },
                        ...Object.values(GenderEnum).map((g) => ({ label:g, value:g })),
                      ]}
                      value={newStudent.gender||""}
                      onChange={(v) => setNewStudent((n) => ({...n,gender:v as GenderEnum}))}
                    />
                    <div className="flex justify-between">
                      <Button variant="outline" onClick={() => setAddingStudent(false)}>
                        Cancel
                      </Button>
                      <Button onClick={saveStudent}>Save & Continue</Button>
                    </div>
                  </div>
                ) : selectedStudent ? (
                  <p className="px-4">
                    Selected: {selectedStudent.firstNameFr} {selectedStudent.lastNameFr}
                  </p>
                ) : (
                  <Select
                    label="Pick Your Child"
                    options={[
                      { label:"— Select —", value:"" },
                      ...available.map((s) => ({ label:`${s.firstNameFr} ${s.lastNameFr}`, value:s.id! })),
                    ]}
                    value={selectedStudent?.id||""}
                    onChange={pickStudent}
                  />
                )}
              </>
            )}
            {/* STEP 2 */}
            {step === 2 && completed[1] && (
              <div className="space-y-4 px-4">
                <Select
                  label="Region*"
                  options={[
                    { label:"— Select —", value:"" },
                    ...(regions||[]).map((r) => ({ label:r.nameFr, value:r.id })),
                  ]}
                  value={region}
                  onChange={(v) => { setRegion(v); setCity(""); setSchool("") }}
                />
                <Select
                  label="City*"
                  disabled={!region}
                  options={[
                    { label:"— Select —", value:"" },
                    ...(cities||[]).map((c) => ({ label:c.nameFr, value:c.id })),
                  ]}
                  value={city}
                  onChange={(v) => { setCity(v); setSchool("") }}
                />
                <Select
                  label="School*"
                  disabled={!city}
                  options={[
                    { label:"— Select —", value:"" },
                    ...(schools||[]).map((s) => ({ label:s.nameFr, value:s.id })),
                  ]}
                  value={school}
                  onChange={(v) => setSchool(v)}
                />
                <div className="flex justify-between">
                  <Button variant="outline" onClick={() => setCompleted({1:false,2:false,3:false,4:false})}>
                    Back
                  </Button>
                  <Button onClick={completeStep2}>Next</Button>
                </div>
              </div>
            )}
            {/* STEP 3 */}
            {step === 3 && completed[2] && (
              <div className="space-y-4 px-4">
                {Object.values(DocumentTypeEnum).map((dt) => (
                  <div key={dt}>
                    <label className="block">{dt}*</label>
                    <Input
                      type="file"
                      onChange={(e) => {
                        const f = e.target.files?.[0]
                        if (f) setDocuments((d) => ({ ...d, [dt]: f }))
                      }}
                    />
                  </div>
                ))}
                <div className="flex justify-between">
                  <Button variant="outline" onClick={() => setCompleted({1:true,2:false,3:false,4:false})}>
                    Back
                  </Button>
                  <Button onClick={completeStep3}>Next</Button>
                </div>
              </div>
            )}
            {/* STEP 4 */}
            {step === 4 && completed[3] && (
              <div className="space-y-4 p-4">
                <h3 className="text-lg font-bold mb-2">Review & Submit</h3>
                <div className="flex justify-between mt-4">
                  <Button variant="outline" onClick={() => setView("overview")}>
                    Cancel
                  </Button>
                  <Button onClick={submitAll}>Submit</Button>
                </div>
              </div>
            )}
          </CardBody>
        </details>
      ))}
    </div>
  )
}
