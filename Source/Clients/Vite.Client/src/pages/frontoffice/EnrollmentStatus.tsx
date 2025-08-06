import React, { useEffect, useState } from 'react';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
import { Badge } from '../../components/ui/Badge';
import { usePageTitle } from '../../hooks/usePageTitle';
import { EnrollmentStatusEnum } from '../../api/BO-api.service';
import { useToast } from '../../components/ui/Toast';

interface StatusHistory {
  status: string;
  date: string;
}

export const EnrollmentStatus: React.FC = () => {
  usePageTitle('Enrollment Status');

  const { toast } = useToast();
  const [history, setHistory] = useState<StatusHistory[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  const mockEnrollmentHistory = [
    { status: EnrollmentStatusEnum.Pending, date: '2025-04-01' },
    { status: EnrollmentStatusEnum.Approved, date: '2025-04-15' },
  ];

  useEffect(() => {
    setHistory(mockEnrollmentHistory);
  }, []);

  const getStatusColor = (status: string): string => {
    switch (status) {
      case 'Approved':
        return 'success';
      case 'Rejected':
        return 'danger';
      case 'UnderReview':
        return 'warning';
      default:
        return 'info';
    }
  };

  if (isLoading) {
    return (
      <div className="animate-pulse">
        <div className="h-8 bg-gray-200 rounded w-1/4 mb-4"></div>
        <div className="space-y-4">
          {[1, 2, 3].map((i) => (
            <div key={i} className="h-24 bg-gray-200 rounded"></div>
          ))}
        </div>
      </div>
    );
  }

  const currentStatus = history[history.length - 1];

  return (
    <Card>
      <CardHeader>
        <CardTitle>Enrollment Status</CardTitle>
      </CardHeader>
      <CardBody>
        {history.map((item, index) => (
          <div key={index} className="flex items-center justify-between mb-4">
            <span>{item.date}</span>
            <Badge variant={getStatusColor(item.status as 'success' | 'danger' | 'warning' | 'info')}>{item.status}</Badge>
          </div>
        ))}
        {currentStatus && (
          <div className="mt-4">
            <h3>Current Status:</h3>
            <Badge variant={getStatusColor(currentStatus.status as 'success' | 'danger' | 'warning' | 'info')}>{currentStatus.status}</Badge>
          </div>
        )}
      </CardBody>
    </Card>
  );
};