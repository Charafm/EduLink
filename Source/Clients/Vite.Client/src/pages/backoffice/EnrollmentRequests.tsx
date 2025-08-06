// BackOffice/EnrollmentRequests.tsx
import React, { useEffect, useState, useMemo } from "react";
import { usePageTitle } from "../../hooks/usePageTitle";
import {
  Card,
  CardHeader,
  CardTitle,
  CardBody,
} from "../../components/ui/Card";
import {
  Table,
  TableHeader,
  TableBody,
  TableRow,
  TableHead,
  TableCell,
} from "../../components/ui/Table";
import { Badge } from "../../components/ui/Badge";
import { Button } from "../../components/ui/Button";
import axios from "axios";
import { EnrollmentClient } from "../../api/BO-api.service";
import {
  EnrollmentRequestDTO,
  EnrollmentRequestStatusEnum,
} from "../../api/BO-api.service";

export const EnrollmentRequests: React.FC = () => {
  usePageTitle("Enrollment Requests");

  const [requests, setRequests] = useState<EnrollmentRequestDTO[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [selected, setSelected] = useState<EnrollmentRequestDTO | null>(null);
  const [actionLoading, setActionLoading] = useState(false);

  // grab token once
  const token = localStorage.getItem("edulink_token") ?? "";
 const userAdminId = localStorage.getItem("edulink_staffId") ;
  // memoize axios instance so it's stable across renders
  const authAxios = useMemo(() => {
    const inst = axios.create({
      baseURL: import.meta.env.VITE_BACKOFFICE_API_URL,
    });
    inst.interceptors.request.use((config) => {
      config.headers.set('Authorization', token ? `Bearer ${token}` : "");
      return config;
    });
    return inst;
  }, [token]);

  // memoize client
  const client = useMemo(
    () => new EnrollmentClient(undefined, authAxios),
    [authAxios]
  );

  const BRANCH_ID = "FE5CBD73-C197-4A12-89B1-030F73035A0C";

  // fetch list
  const loadRequests = () => {
    setLoading(true);
    client
      .getEnrollmentRequests(
        null, // status
        BRANCH_ID,
        null, // parentId
        null, // searchTerm
        1,
        100
      )
      .then((res) => setRequests(res.results ?? []))
      .catch(() => setError("Failed to load enrollment requests"))
      .finally(() => setLoading(false));
  };

  useEffect(() => {
    loadRequests();
  }, [client]);

  const renderBadge = (status: EnrollmentRequestStatusEnum) => {
    switch (status) {
      case EnrollmentRequestStatusEnum.Approved:
        return <Badge variant="success">{status}</Badge>;
      case EnrollmentRequestStatusEnum.Rejected:
        return <Badge variant="danger">{status}</Badge>;
      case EnrollmentRequestStatusEnum.Draft:
        return <Badge variant="warning">{status}</Badge>;
      case EnrollmentRequestStatusEnum.Submitted:
        return <Badge variant="secondary">{status}</Badge>;
      default:
        return <Badge variant="secondary">{status}</Badge>;
    }
  };

  // approve and refresh
  const handleApprove = async () => {
    if (!selected) return;
    setActionLoading(true);
    try {
      await client.approveEnrollmentRequest(userAdminId || "", selected.id || "", undefined);
      setSelected(null);
      loadRequests();
    } catch {
      setError("Approval failed");
    } finally {
      setActionLoading(false);
    }
  };

  // cancel review
  const handleCancel = () => setSelected(null);

  // if a request is selected, show detail view
  if (selected) {
    return (
      <div className="space-y-4">
        <Button variant="outline" onClick={handleCancel}>
          ← Back to list
        </Button>
        <Card>
          <CardHeader>
            <CardTitle>Review Enrollment Request</CardTitle>
          </CardHeader>
          <CardBody>
            <dl className="grid grid-cols-2 gap-x-4 gap-y-2">
              <dt className="font-semibold">Request ID</dt>
              <dd>{selected.id}</dd>
              <dt className="font-semibold">Request Code</dt>
              <dd>{selected.requestCode}</dd>
              <dt className="font-semibold">Student ID</dt>
              <dd>{selected.studentId}</dd>
              <dt className="font-semibold">Student Name</dt>
              <dd>{selected.requestedByName}</dd>
              <dt className="font-semibold">School</dt>
              <dd>{selected.schoolName}</dd>
              <dt className="font-semibold">Status</dt>
              <dd>{renderBadge(selected.status as any)}</dd>
              <dt className="font-semibold">Created At</dt>
              <dd>{selected.createdAt ? new Date(selected.createdAt).toLocaleString() : "–"}</dd>
              <dt className="font-semibold">Submitted At</dt>
              <dd>
                {selected.submittedAt
                  ? new Date(selected.submittedAt).toLocaleString()
                  : "–"}
              </dd>
              {/* add any other fields you need */}
            </dl>
            <div className="mt-4 flex space-x-2">
              <Button
                onClick={handleApprove}
                disabled={actionLoading}
                variant="primary"
              >
                {actionLoading ? "Approving…" : "Approve"}
              </Button>
              <Button onClick={handleCancel} variant="outline">
                Cancel
              </Button>
            </div>
          </CardBody>
        </Card>
      </div>
    );
  }

  // otherwise show list
  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <h1 className="text-2xl font-bold text-gray-900">
          Enrollment Requests
        </h1>
        <p className="mt-2 text-sm text-gray-500">
          Manage student enrollment applications
        </p>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Applications</CardTitle>
        </CardHeader>
        <CardBody>
          {loading && <div>Loading…</div>}
          {error && <div className="text-red-600">{error}</div>}
          {!loading && !error && (
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>Request ID</TableHead>
                  <TableHead>Request Code</TableHead>
                  <TableHead>Student</TableHead>
                  <TableHead>Submission Date</TableHead>
                  <TableHead>Status</TableHead>
                  <TableHead>Actions</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {requests.length > 0 ? (
                  requests.map((r) => (
                    <TableRow key={r.id}>
                      <TableCell className="font-medium">{r.id}</TableCell>
                      <TableCell>{r.requestCode}</TableCell>
                      <TableCell>{r.requestedByName}</TableCell>
                      <TableCell>
                        {r.submittedAt
                          ? new Date(r.submittedAt).toLocaleDateString()
                          : "–"}
                      </TableCell>
                      <TableCell>{renderBadge(r.status as any)}</TableCell>
                      <TableCell>
                        <Button
                          variant="outline"
                          size="sm"
                          onClick={() => setSelected(r)}
                        >
                          Review
                        </Button>
                      </TableCell>
                    </TableRow>
                  ))
                ) : (
                  <TableRow>
                    <TableCell colSpan={6} className="text-center py-8">
                      No requests found
                    </TableCell>
                  </TableRow>
                )}
              </TableBody>
            </Table>
          )}
        </CardBody>
      </Card>
    </div>
  );
};
