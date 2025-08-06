// BackOffice/Settings.tsx
import React from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
import { Input } from '../../components/ui/Input';
import { Button } from '../../components/ui/Button';
import { Select } from '../../components/ui/Select';

const settingsSchema = z.object({
  schoolName: z.string().min(1, 'School name is required'),
  academicYear: z.string().min(9, 'Valid academic year required'),
  gradeLevels: z.array(z.string()),
  defaultLanguage: z.string().min(1),
});

export const Settings: React.FC = () => {
  usePageTitle('System Settings');
  const { register, handleSubmit, formState: { errors } } = useForm({
    resolver: zodResolver(settingsSchema),
    defaultValues: {
      schoolName: 'Greenwood High School',
      academicYear: '2023-2024',
      gradeLevels: ['9', '10', '11', '12'],
      defaultLanguage: 'en'
    }
  });

  const onSubmit = (data: unknown) => {
    console.log('Settings updated:', data);
  };

  const { setValue } = useForm({
    resolver: zodResolver(settingsSchema),
    defaultValues: {
      schoolName: 'Greenwood High School',
      academicYear: '2023-2024',
      gradeLevels: ['9', '10', '11', '12'],
      defaultLanguage: 'en'
    }
  });

  const watch = useForm().watch;

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <h1 className="text-2xl font-bold text-gray-900">System Configuration</h1>
        <p className="mt-2 text-sm text-gray-500">Manage institutional settings and preferences</p>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>General Settings</CardTitle>
        </CardHeader>
        <CardBody>
          <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <Input
                label="School Name"
                {...register('schoolName')}
                error={errors.schoolName?.message}
              />
              <Input
                label="Academic Year"
                {...register('academicYear')}
                error={errors.academicYear?.message}
              />
              <Select
                label="Grade Levels"
                options={[
                  { value: '9', label: '9' },
                  { value: '10', label: '10' },
                  { value: '11', label: '11' },
                  { value: '12', label: '12' },
                ]}
                multiple
                onChange={(value) => setValue('gradeLevels', Array.isArray(value) ? value : [value])}
                value={watch('gradeLevels')}
              />
              <Select
                label="Default Language"
                options={[
                  { value: 'en', label: 'English' },
                  { value: 'ar', label: 'العربية' },
                  { value: 'fr', label: 'Français' },
                ]}
                value={watch('defaultLanguage')}
                onChange={(value) => setValue('defaultLanguage', value)}
              />
            </div>
            <div className="flex justify-end">
              <Button type="submit">Save Changes</Button>
            </div>
          </form>
        </CardBody>
      </Card>
    </div>
  );
};