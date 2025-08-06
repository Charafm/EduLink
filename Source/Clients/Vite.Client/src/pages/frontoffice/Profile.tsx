// FrontOffice/Profile.tsx
import React, { useEffect, useState } from 'react';
import { usePageTitle } from '../../hooks/usePageTitle';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { Card, CardBody, CardHeader, CardTitle } from '../../components/ui/Card';
import { Input } from '../../components/ui/Input';
import { Button } from '../../components/ui/Button';
import { useAuth } from '../../context/AuthContext';
import { ProfileClient } from '../../api/BO-api.service';

const profileClient = new ProfileClient(import.meta.env.VITE_FRONTOFFICE_API_URL);

const profileSchema = z.object({
  firstName: z.string().min(1, 'First name is required'),
  lastName: z.string().min(1, 'Last name is required'),
  email: z.string().email('Invalid email address'),
  phone: z.string().min(10, 'Phone number must be at least 10 digits'),
  address: z.string().min(5, 'Address is required'),
});

type ProfileFormData = z.infer<typeof profileSchema>;

export const FrontOfficeProfile: React.FC = () => {
  usePageTitle('Profile');
  const { user } = useAuth(); // Ensure 'user' exists in 'AuthContextType'
  const [profile, setProfile] = useState<ProfileDto | null>(null); // Updated type to 'ProfileDto'
  const { register, handleSubmit, formState: { errors } } = useForm<ProfileFormData>({
    resolver: zodResolver(profileSchema),
    defaultValues: {
      firstName: user?.firstName,
      lastName: user?.lastName,
      email: user?.email,
      phone: user?.phone,
      address: user?.address,
    }
  });

  const onSubmit = (data: ProfileFormData) => { // Replaced 'any' with a specific type
    // Handle form submission
  };

  const mockProfile = {
    firstName: 'John',
    lastName: 'Doe',
    email: 'john.doe@example.com',
    phone: '1234567890',
    address: '123 Main St',
  };

  useEffect(() => {
    setProfile(mockProfile);
  }, []);

  return (
    <div className="space-y-6">
      <div className="border-b border-gray-200 pb-4">
        <h1 className="text-2xl font-bold text-gray-900">My Profile</h1>
        <p className="mt-2 text-sm text-gray-500">Manage your personal information</p>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Personal Information</CardTitle>
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
                label="Phone"
                {...register('phone')}
                error={errors.phone?.message}
              />
              <Input
                label="Address"
                {...register('address')}
                error={errors.address?.message}
                className="md:col-span-2"
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