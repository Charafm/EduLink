// import React, { useState } from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
import { Clock, Calendar } from 'lucide-react';

export const FrontOfficeSchedule: React.FC = () => {
  usePageTitle('Class Schedule');
  
  // Removed unused currentWeek state
  
  const schedule = [
    {
      day: 'Monday',
      classes: [
        { time: '08:00 - 09:30', course: 'Mathematics', room: '101', teacher: 'Mr. Anderson' },
        { time: '09:45 - 11:15', course: 'English Literature', room: '203', teacher: 'Ms. Wilson' },
        { time: '11:30 - 13:00', course: 'Physics', room: '301', teacher: 'Dr. Thompson' }
      ]
    },
    {
      day: 'Tuesday',
      classes: [
        { time: '08:00 - 09:30', course: 'Biology', room: '302', teacher: 'Mrs. Davis' },
        { time: '09:45 - 11:15', course: 'History', room: '204', teacher: 'Mr. Roberts' },
        { time: '11:30 - 13:00', course: 'Art', room: 'A101', teacher: 'Ms. Martinez' }
      ]
    },
    {
      day: 'Wednesday',
      classes: [
        { time: '08:00 - 09:30', course: 'Mathematics', room: '101', teacher: 'Mr. Anderson' },
        { time: '09:45 - 11:15', course: 'Chemistry', room: '303', teacher: 'Dr. White' },
        { time: '11:30 - 13:00', course: 'Physical Education', room: 'GYM', teacher: 'Mr. Johnson' }
      ]
    },
    {
      day: 'Thursday',
      classes: [
        { time: '08:00 - 09:30', course: 'English Literature', room: '203', teacher: 'Ms. Wilson' },
        { time: '09:45 - 11:15', course: 'Computer Science', room: '401', teacher: 'Mrs. Brown' },
        { time: '11:30 - 13:00', course: 'Music', room: 'M101', teacher: 'Mr. Garcia' }
      ]
    },
    {
      day: 'Friday',
      classes: [
        { time: '08:00 - 09:30', course: 'Physics', room: '301', teacher: 'Dr. Thompson' },
        { time: '09:45 - 11:15', course: 'History', room: '204', teacher: 'Mr. Roberts' },
        { time: '11:30 - 13:00', course: 'Spanish', room: '102', teacher: 'Ms. Rodriguez' }
      ]
    }
  ];

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <h1 className="text-2xl font-bold text-gray-900">Class Schedule</h1>
        <p className="mt-2 text-sm text-gray-500">
          Your weekly class schedule and room assignments
        </p>
      </div>

      {/* Quick Stats */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <Card>
          <CardBody className="flex items-center space-x-4">
            <div className="p-3 bg-blue-100 rounded-lg">
              <Clock className="h-6 w-6 text-blue-600" />
            </div>
            <div>
              <p className="text-sm font-medium text-gray-500">Today's Classes</p>
              <p className="text-2xl font-bold text-gray-900">3</p>
            </div>
          </CardBody>
        </Card>

        <Card>
          <CardBody className="flex items-center space-x-4">
            <div className="p-3 bg-green-100 rounded-lg">
              <Calendar className="h-6 w-6 text-green-600" />
            </div>
            <div>
              <p className="text-sm font-medium text-gray-500">Total Hours/Week</p>
              <p className="text-2xl font-bold text-gray-900">15</p>
            </div>
          </CardBody>
        </Card>
      </div>

      {/* Weekly Schedule */}
      <Card>
        <CardHeader>
          <CardTitle>Weekly Schedule</CardTitle>
        </CardHeader>
        <CardBody>
          <div className="space-y-6">
            {schedule.map((day) => (
              <div key={day.day} className="space-y-4">
                <h3 className="text-lg font-medium text-gray-900">{day.day}</h3>
                <div className="grid gap-4">
                  {day.classes.map((classItem, index) => (
                    <div
                      key={index}
                      className="bg-gray-50 p-4 rounded-lg border border-gray-200 hover:border-blue-500 transition-colors"
                    >
                      <div className="flex items-center justify-between">
                        <div>
                          <p className="font-medium text-gray-900">{classItem.course}</p>
                          <p className="text-sm text-gray-500">{classItem.teacher}</p>
                        </div>
                        <div className="text-right">
                          <p className="text-sm font-medium text-gray-900">{classItem.time}</p>
                          <p className="text-sm text-gray-500">Room {classItem.room}</p>
                        </div>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            ))}
          </div>
        </CardBody>
      </Card>
    </div>
  );
};

export default FrontOfficeSchedule;