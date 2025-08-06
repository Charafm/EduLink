// BackOffice/Profile.tsx
import React, { useEffect, useState } from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { Card, CardHeader, CardTitle, CardBody } from '../../components/ui/Card';
import { Input } from '../../components/ui/Input';
import { Button } from '../../components/ui/Button';
import { useAuth } from '../../context/AuthContext';
import { ProfileClient } from '../../api/identity-api.service';

const profileSchema = z.object({
  firstName: z.string().min(1, 'First name is required'),
  lastName: z.string().min(1, 'Last name is required'),
  email: z.string().email('Invalid email address'),
  role: z.string().min(1, 'Role is required'),
});

export const BackOfficeProfile: React.FC = () => {
  usePageTitle('Admin Profile');
  const { user } = useAuth(); // Ensure 'user' exists in 'AuthContextType'
  const [profile, setProfile] = useState<any>(null);
  const { register, handleSubmit, formState: { errors } } = useForm({
    resolver: zodResolver(profileSchema),
    defaultValues: {
      firstName: user?.firstName,
      lastName: user?.lastName,
      email: user?.email,
      role: user?.role,
    }
  });

  const onSubmit = (data: any) => { // Replaced 'any' with a specific type
    // Handle form submission
  };

  const profileClient = new ProfileClient(import.meta.env.VITE_BACKOFFICE_API_URL);

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        const profile = await profileClient.getProfile();
        setProfile(profile);
      } catch (error) {
        console.error('Failed to fetch profile:', error);
      }
    };

    fetchProfile();
  }, []);

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <h1 className="text-2xl font-bold text-gray-900">Administrator Profile</h1>
        <p className="mt-2 text-sm text-gray-500">Manage your administrative account settings</p>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Account Information</CardTitle>
        </CardHeader>
        <CardBody>
          <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <Input
                label="First Name"
                {...register('firstName')}
                error={errors.firstName?.message}
              />
              <Input
                label="Last Name"
                {...register('lastName')}
                error={errors.lastName?.message}
              />
              <Input
                label="Email"
                type="email"
                {...register('email')}
                error={errors.email?.message}
                disabled
              />
              <Input
                label="Role"
                {...register('role')}
                error={errors.role?.message}
                disabled
              />
            </div>
            <div className="flex justify-end">
              <Button type="submit">Update Profile</Button>
            </div>
          </form>
        </CardBody>
      </Card>
    </div>
  );
};