// BackOffice/ScheduleBuilder.tsx
import React, { useState } from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
//import { Button } from '../../components/ui/Button';
import { Select } from '../../components/ui/Select';

const timeSlots = ['08:00', '09:00', '10:00', '11:00', '12:00', '13:00', '14:00', '15:00'];
const days = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];

export const ScheduleBuilder: React.FC = () => {
  usePageTitle('Schedule Builder');
  const [schedule, setSchedule] = useState(() =>
    days.map(() => 
      timeSlots.map(time => ({
        time,
        class: '',
        teacher: '',
        room: ''
      }))
    )
  );

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <h1 className="text-2xl font-bold text-gray-900">Schedule Builder</h1>
        <p className="mt-2 text-sm text-gray-500">Create and manage class schedules</p>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Weekly Schedule Grid</CardTitle>
        </CardHeader>
        <CardBody>
          <div className="overflow-x-auto">
            <table className="min-w-full">
              <thead>
                <tr>
                  <th className="w-32"></th>
                  {days.map(day => (
                    <th key={day} className="px-4 py-2 text-left border-b">{day}</th>
                  ))}
                </tr>
              </thead>
              <tbody>
                {timeSlots.map((time, timeIndex) => (
                  <tr key={time}>
                    <td className="p-2 border-r">{time}</td>
                    {days.map((day, dayIndex) => (
                      <td key={day} className="p-2 border">
                        <div className="space-y-2">
                          <Select
                            options={['Math 101', 'Science 201', 'English 301'].map(option => ({ label: option, value: option }))}
                            value={schedule[dayIndex][timeIndex].class}
                            onChange={(value) => {
                              const newSchedule = [...schedule];
                              newSchedule[dayIndex][timeIndex].class = value;
                              setSchedule(newSchedule);
                            }}
                          />
                          <Select
                            options={['Room 101', 'Room 202', 'Lab 301'].map(option => ({ label: option, value: option }))}
                            value={schedule[dayIndex][timeIndex].room}
                            onChange={(value) => {
                              const newSchedule = [...schedule];
                              newSchedule[dayIndex][timeIndex].room = value;
                              setSchedule(newSchedule);
                            }}
                          />
                        </div>
                      </td>
                    ))}
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </CardBody>
      </Card>
    </div>
  );
};