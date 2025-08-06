// src/api/enrollmentClient.ts
import {
  IEnrollmentClient,
  EnrollmentDTO,
  EnrollmentStatusUpdateDTO,
  CreateStudentDTO,
  ParsedStudentDto,
  CreateEnrollmentRequestDTO,
  EnrollmentRequestDTO,
  UpdateEnrollmentRequestDTO,
  EnrollmentRequestDetailDTO,
  EnrollmentRequestStatusEnum,
  PagedResultOfEnrollmentDTO,
  PagedResultOfEnrollmentRequestDTO,
  EnrollmentDetailDTO,
  EnrollmentMetricsDTO,
  EnrollmentStatusEnum
} from "./BO-api.service"

const API = import.meta.env.VITE_BACKOFFICE_API_URL
const ENROLL = `${API}/Enrollment`

export class EnrollmentClient implements IEnrollmentClient {
  private token = localStorage.getItem("token") || ""
  private parentId = localStorage.getItem("parentId") || ""

  private headers(json = true) {
    const h: Record<string,string> = {
      "Authorization": `Bearer ${this.token}`
    }
    if (json) h["Content-Type"] = "application/json"
    return h
  }

  private async request<T>(url:string, opts:RequestInit):Promise<T> {
    const resp = await fetch(url, opts)
    if (!resp.ok) throw new Error(resp.statusText)
    return resp.json()
  }

  async submitEnrollment(dto: EnrollmentDTO): Promise<boolean> {
    const resp = await fetch(`${ENROLL}`, {
      method:"POST",
      headers:this.headers(),
      body: JSON.stringify(dto)
    })
    return resp.ok
  }

  async getPaginated(
    branchId?: string | null, status?: EnrollmentStatusEnum | null,
    createdFrom?: Date | null, createdTo?: Date | null,
    searchTerm?: string, pageNumber: number = 1, pageSize: number = 10
  ): Promise<PagedResultOfEnrollmentDTO> {
    const qp = new URLSearchParams({
      branchId: branchId || "",
      status: status ? status.toString() : "",
      createdFrom: createdFrom?.toISOString() || "",
      createdTo: createdTo?.toISOString() || "",
      searchTerm: searchTerm || "",
      pageNumber: pageNumber.toString(),
      pageSize: pageSize.toString()
    });
    return this.request(`${ENROLL}?${qp}`, {
      headers: this.headers(false)
    });
  }

  async getEnrollment(id:string): Promise<EnrollmentDetailDTO> {
    return this.request(`${ENROLL}/${id}`, {
      headers: this.headers(false)
    })
  }

  async updateStatus(id:string, dto:EnrollmentStatusUpdateDTO):Promise<boolean> {
    const resp = await fetch(`${ENROLL}/Status/${id}`, {
      method:"PUT",
      headers:this.headers(),
      body: JSON.stringify(dto)
    })
    return resp.ok
  }

  async createStudent(dto:CreateStudentDTO):Promise<ParsedStudentDto> {
    return this.request(`${API}/Student`, {
      method:"POST",
      headers:this.headers(),
      body: JSON.stringify(dto)
    })
  }

  async getAssociatedStudents(
    pageNumber=1, pageSize=20
  ):Promise<PagedResultOfEnrollmentDTO & { results: ParsedStudentDto[] }> {
    // note: your controller for parent service is named differently. adjust URL accordingly
    const qp = new URLSearchParams({
      parentId: this.parentId,
      pageNumber: pageNumber.toString(),
      pageSize: pageSize.toString()
    })
    return this.request(`${API}/Parent/GetAssociatedStudents?${qp}`, {
      headers: this.headers(false)
    })
  }

  async createRequest(dto:CreateEnrollmentRequestDTO):Promise<string> {
    const resp = await fetch(`${ENROLL}/CreateRequest`, {
      method:"POST",
      headers:this.headers(),
      body: JSON.stringify(dto)
    })
    if (!resp.ok) throw new Error(resp.statusText)
    return resp.text()
  }

  async getEnrollmentRequests(
    status: EnrollmentRequestStatusEnum | null | undefined,
    branchId: string | null | undefined,
    parentId: string | null | undefined,
    searchTerm: string | null | undefined,
    page: number | undefined,
    pageSize: number | undefined
  ): Promise<PagedResultOfEnrollmentRequestDTO> {
    const qp = new URLSearchParams({
      status: status || "",
      branchId: branchId || "",
      parentId: parentId || "",
      searchTerm: searchTerm || "",
      page: page?.toString() || "1",
      pageSize: pageSize?.toString() || "20"
    });
    return this.request(`${ENROLL}/GetEnrollmentRequests?${qp}`, {
      headers: this.headers(false)
    });
  }

  async getEnrollmentRequestById(id:string):Promise<EnrollmentRequestDetailDTO> {
    return this.request(`${ENROLL}/GetEnrollmentRequestById/${id}`, {
      headers: this.headers(false)
    })
  }

  async updateRequest(requestId:string, dto:UpdateEnrollmentRequestDTO):Promise<boolean> {
    const resp = await fetch(`${ENROLL}/UpdateRequest/${requestId}`, {
      method:"PUT",
      headers:this.headers(),
      body: JSON.stringify(dto)
    })
    return resp.ok
  }

  async submitEnrollmentRequest(requestId:string):Promise<boolean> {
    const resp = await fetch(`${ENROLL}/SubmitEnrollmentRequest`, {
      method:"POST",
      headers:this.headers(),
      body: JSON.stringify(requestId)
    })
    return resp.ok
  }

  async approveEnrollmentRequest(requestId:string, adminUserId:string):Promise<boolean> {
    const resp = await fetch(`${ENROLL}/ApproveEnrollmentRequest`, {
      method:"POST",
      headers:this.headers(),
      body: JSON.stringify({ requestId, adminUserId })
    })
    return resp.ok
  }

  async bulkApproveEnrollmentRequest(adminUserId: string | undefined, requestIds: string[]): Promise<boolean> {
    const resp = await fetch(`${ENROLL}/BULKApproveEnrollmentRequest`, {
      method:"POST",
      headers:this.headers(),
      body: JSON.stringify({ requestIds, adminUserId })
    })
    return resp.ok
  }

  async saveRequest(request:EnrollmentRequestDTO):Promise<boolean> {
    const resp = await fetch(`${ENROLL}/SaveRequest`, {
      method:"POST",
      headers:this.headers(),
      body: JSON.stringify(request)
    })
    return resp.ok
  }

  async getDashboardMetrics(startDate?:Date, endDate?:Date):Promise<EnrollmentMetricsDTO> {
    const qp = new URLSearchParams({
      startDate: startDate?.toISOString()||"",
      endDate: endDate?.toISOString()||""
    })
    return this.request(`${ENROLL}/Dashboard?${qp}`, {
      headers:this.headers(false)
    })
  }
}
