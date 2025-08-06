import React from 'react';
import { Card } from '../../components/ui/Card';

export const TransferStatus: React.FC = () => {
  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold text-gray-900 mb-6">Transfer Status</h1>
      
      <Card className="bg-white shadow-sm">
        <div className="p-6">
          <div className="space-y-4">
            <div className="flex items-center justify-between">
              <h2 className="text-lg font-semibold text-gray-800">Transfer Request Status</h2>
              <span className="px-3 py-1 text-sm font-medium rounded-full bg-blue-100 text-blue-800">
                Pending
              </span>
            </div>
            
            <div className="space-y-3">
              <div className="grid grid-cols-2 gap-4 text-sm">
                <div className="text-gray-600">Request Date</div>
                <div className="text-gray-900">{new Date().toLocaleDateString()}</div>
                
                <div className="text-gray-600">Current School</div>
                <div className="text-gray-900">Current School Name</div>
                
                <div className="text-gray-600">Requested School</div>
                <div className="text-gray-900">Requested School Name</div>
              </div>
            </div>
            
            <div className="mt-6 pt-6 border-t border-gray-200">
              <h3 className="text-sm font-medium text-gray-900 mb-2">Status Timeline</h3>
              <div className="space-y-3">
                <div className="flex items-start gap-3">
                  <div className="w-2 h-2 mt-2 rounded-full bg-blue-600"></div>
                  <div>
                    <p className="text-sm font-medium text-gray-900">Request Submitted</p>
                    <p className="text-sm text-gray-500">{new Date().toLocaleDateString()}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </Card>
    </div>
  );
};