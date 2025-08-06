import React from 'react';

export const FrontOfficeAttendance: React.FC = () => {
  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold text-gray-900 mb-6">Attendance</h1>
      <div className="bg-white rounded-lg shadow p-6">
        <p className="text-gray-600">Your attendance records will be displayed here.</p>
      </div>
    </div>
  );
};

export default FrontOfficeAttendance;
