import { Parent } from '../../types';

export const mockParents: Parent[] = [
  {
    id: '1',
    firstName: 'Robert',
    lastName: 'Johnson',
    email: 'robert.johnson@example.com',
    phone: '(555) 123-4567',
    address: '123 Oak Street, Anytown, USA',
    relation: 'Father',
    students: ['1', '6']
  },
  {
    id: '2',
    firstName: 'Jennifer',
    lastName: 'Smith',
    email: 'jennifer.smith@example.com',
    phone: '(555) 234-5678',
    address: '456 Maple Avenue, Somewhere, USA',
    relation: 'Mother',
    students: ['2', '7']
  },
  {
    id: '3',
    firstName: 'Michael',
    lastName: 'Williams',
    email: 'michael.williams@example.com',
    phone: '(555) 345-6789',
    address: '789 Pine Road, Elsewhere, USA',
    relation: 'Father',
    students: ['3', '8']
  },
  {
    id: '4',
    firstName: 'Sarah',
    lastName: 'Brown',
    email: 'sarah.brown@example.com',
    phone: '(555) 456-7890',
    address: '101 Cedar Lane, Nowhere, USA',
    relation: 'Mother',
    students: ['4', '9']
  },
  {
    id: '5',
    firstName: 'David',
    lastName: 'Jones',
    email: 'david.jones@example.com',
    phone: '(555) 567-8901',
    address: '202 Birch Boulevard, Anywhere, USA',
    relation: 'Father',
    students: ['5', '10']
  },
  {
    id: '6',
    firstName: 'Lisa',
    lastName: 'Garcia',
    email: 'lisa.garcia@example.com',
    phone: '(555) 678-9012',
    address: '303 Elm Street, Someplace, USA',
    relation: 'Mother',
    students: []
  },
  {
    id: '7',
    firstName: 'James',
    lastName: 'Martinez',
    email: 'james.martinez@example.com',
    phone: '(555) 789-0123',
    address: '404 Walnut Drive, Otherplace, USA',
    relation: 'Guardian',
    students: []
  },
  {
    id: '8',
    firstName: 'Patricia',
    lastName: 'Robinson',
    email: 'patricia.robinson@example.com',
    phone: '(555) 890-1234',
    address: '505 Spruce Circle, Thatplace, USA',
    relation: 'Mother',
    students: []
  },
  {
    id: '9',
    firstName: 'Thomas',
    lastName: 'Clark',
    email: 'thomas.clark@example.com',
    phone: '(555) 901-2345',
    address: '606 Aspen Court, Thisplace, USA',
    relation: 'Father',
    students: []
  },
  {
    id: '10',
    firstName: 'Nancy',
    lastName: 'Rodriguez',
    email: 'nancy.rodriguez@example.com',
    phone: '(555) 012-3456',
    address: '707 Willow Way, Whereville, USA',
    relation: 'Guardian',
    students: []
  }
];