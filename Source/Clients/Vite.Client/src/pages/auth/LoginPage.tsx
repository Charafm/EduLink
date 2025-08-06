import React, { useState } from 'react';
//mport { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { Button } from '../../components/ui/Button';
import { Input } from '../../components/ui/Input';
//import { Card, CardHeader, CardTitle, CardBody, CardFooter } from '../../components/ui/Card';
import { GraduationCap, Users, User, Building2 } from 'lucide-react';
import { UserType } from '../../types';
import { usePageTitle } from '../../hooks/usePageTitle';
import { useAuth } from '../../context/AuthContext'; // Updated import path for useAuth
import { useTranslation } from 'react-i18next';
import LanguageSwitcher from '../../components/ui/LanguageSwitcher';


// Define schema for form validation
const loginSchema = z.object({
  username: z.string().min(1, 'Username is required'),
  password: z.string().min(1, 'Password is required'),
  rememberMe: z.boolean().optional(),
});

type LoginFormValues = z.infer<typeof loginSchema>;

export const LoginPage: React.FC = () => {
  usePageTitle('Login');

  const { login, isLoading, error } = useAuth();
  //const navigate = useNavigate();
  const [selectedUserType, setSelectedUserType] = useState<UserType | null>(null);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormValues>({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      username: '',
      password: '',
      rememberMe: false,
    },
  });

  const onSubmit = async (data: LoginFormValues) => {
    if (!selectedUserType) return;

    try {
      await login(data.username, data.password, selectedUserType);
      // Navigation is handled in the auth context
    } catch {
      // Error handling is done in the auth context
    }
  };

  const userTypeCards = [
    {
      type: 'Student' as UserType,
      title: 'Student',
      description: 'Access your grades, schedule, and more',
      icon: <GraduationCap size={32} className="text-blue-600" />,
      bgClass: 'bg-blue-50',
    },
    {
      type: 'Parent' as UserType,
      title: 'Parent',
      description: 'View your child\'s academic progress',
      icon: <Users size={32} className="text-emerald-600" />,
      bgClass: 'bg-emerald-50',
    },
    {
      type: 'Staff' as UserType,
      title: 'Staff & Teachers',
      description: 'Manage classes, grades, and students',
      icon: <User size={32} className="text-amber-600" />,
      bgClass: 'bg-amber-50',
    },
    {
      type: 'SchoolAdmin' as UserType,
      title: 'Administrator',
      description: 'School administration and system settings',
      icon: <Building2 size={32} className="text-purple-600" />,
      bgClass: 'bg-purple-50',
    },
  ];
 const { t } = useTranslation();
  return (
    <div className="min-h-screen bg-gray-50 flex flex-col justify-center py-12 sm:px-6 lg:px-8">
       <div className="absolute top-4 right-4">
    <LanguageSwitcher />
  </div>
      <div className="sm:mx-auto sm:w-full sm:max-w-md">
        <div className="flex justify-center">
          <GraduationCap className="h-16 w-16 text-blue-600" />
        </div>
        <h1 className="mt-4 text-center text-3xl font-extrabold text-gray-900">
          {t('appName')}
        </h1>
        <h2 className="mt-2 text-center text-lg text-gray-600">
          {t('pageSubtitle')}
        </h2>
      </div>

      <div className="mt-8 sm:mx-auto sm:w-full sm:max-w-4xl px-4">
        <div className="bg-white py-8 px-4 shadow sm:rounded-lg sm:px-10">
          {!selectedUserType ? (
            <div>
              <h3 className="text-lg font-medium text-gray-900 mb-6 text-center">
                {t('selectUserType')}
              </h3>
              <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
                {userTypeCards.map((card) => (
                  <button
                    key={card.type}
                    className={`${card.bgClass} rounded-xl shadow-sm border border-gray-200 p-6 flex flex-col items-center text-center transition-transform duration-200 hover:scale-105 hover:shadow-md focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500`}
                    onClick={() => setSelectedUserType(card.type)}
                  >
                    {card.icon}
                    <h3 className="mt-4 text-lg font-medium text-gray-900">
                      {card.title}
                    </h3>
                    <p className="mt-2 text-sm text-gray-500">
                      {card.description}
                    </p>
                  </button>
                ))}
              </div>
              <p className="mt-6 text-center text-sm text-gray-500">
                {t('noAccount')}{' '}
                <a href="#" className="font-medium text-blue-600 hover:text-blue-500">
                  {t('contactAdmin')}
                </a>
              </p>
            </div>
          ) : (
            <div>
              <div className="flex items-center mb-6">
                <button
                  className="mr-4 text-gray-500 hover:text-gray-700"
                  onClick={() => setSelectedUserType(null)}
                >
                  &larr; {t('back')}
                </button>
                <h3 className="text-lg font-medium text-gray-900">
                  {t('signInAs', { userType: selectedUserType === 'SchoolAdmin' ? t('administrator') : selectedUserType })}
                </h3>
              </div>

              <form className="space-y-6" onSubmit={handleSubmit(onSubmit)}>
                <Input
                  label={t('username')}
                  {...register('username')}
                  error={errors.username?.message}
                  fullWidth
                />

                <Input
                  label={t('password')}
                  type="password"
                  {...register('password')}
                  error={errors.password?.message}
                  fullWidth
                />

                <div className="flex items-center justify-between">
                  <div className="flex items-center">
                    <input
                      id="remember-me"
                      type="checkbox"
                      className="h-4 w-4 text-blue-600 focus:ring-blue-500 border-gray-300 rounded"
                      {...register('rememberMe')}
                    />
                    <label
                      htmlFor="remember-me"
                      className="ml-2 block text-sm text-gray-900"
                    >
                      {t('rememberMe')}
                    </label>
                  </div>

                  <div className="text-sm">
                    <a
                      href="#"
                      className="font-medium text-blue-600 hover:text-blue-500"
                    >
                      {t('forgotPassword')}
                    </a>
                  </div>
                </div>

                {error && (
                  <div className="rounded-md bg-red-50 p-4">
                    <div className="flex">
                      <div className="ml-3">
                        <h3 className="text-sm font-medium text-red-800">
                          {t('error')}
                        </h3>
                        <div className="mt-2 text-sm text-red-700">
                          <p>{error}</p>
                        </div>
                      </div>
                    </div>
                  </div>
                )}

                <div>
                  <Button
                    type="submit"
                    variant="primary"
                    fullWidth
                    isLoading={isLoading}
                  >
                    {t('signIn')}
                  </Button>
                </div>
              </form>

              <div className="mt-6">
                <div className="relative">
                  <div className="absolute inset-0 flex items-center">
                    <div className="w-full border-t border-gray-300" />
                  </div>
                  <div className="relative flex justify-center text-sm">
                    <span className="px-2 bg-white text-gray-500">
                      {t('demoCredentials')}
                    </span>
                  </div>
                </div>

                <div className="mt-6">
                  <div className="rounded-md bg-blue-50 p-4">
                    <div className="flex">
                      <div className="ml-3">
                        <h3 className="text-sm font-medium text-blue-800">
                          {t('demoPurpose')}
                        </h3>
                        <div className="mt-2 text-sm text-blue-700">
                          <p>
                            {t('username')}: {selectedUserType === 'SchoolAdmin' ? 'admin' : selectedUserType?.toLowerCase()}
                            <br />
                            {t('password')}: password
                          </p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>

              </div>
            </div>
          )}
        </div>
      </div>
    </div>

  );
};