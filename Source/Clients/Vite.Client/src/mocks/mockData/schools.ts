import { School, Branch } from '../../types';

export const mockSchools: (School & { branches: Branch[] })[] = [
  {
    id: '1',
    name: 'Washington High School',
    address: '123 Main St',
    city: 'Seattle',
    state: 'WA',
    zipCode: '98101',
    phone: '(206) 555-1234',
    email: 'info@washingtonhs.edu',
    website: 'www.washingtonhs.edu',
    principalName: 'Dr. Jane Thompson',
    districtName: 'Seattle School District',
    branches: [
      {
        id: '101',
        name: 'Main Campus',
        address: '123 Main St, Seattle, WA 98101',
        schoolId: '1'
      },
      {
        id: '102',
        name: 'North Wing',
        address: '125 Main St, Seattle, WA 98101',
        schoolId: '1'
      }
    ]
  },
  {
    id: '2',
    name: 'Lincoln Elementary School',
    address: '456 Oak Ave',
    city: 'Portland',
    state: 'OR',
    zipCode: '97205',
    phone: '(503) 555-5678',
    email: 'office@lincolnes.edu',
    website: 'www.lincolnes.edu',
    principalName: 'Ms. Sarah Johnson',
    districtName: 'Portland Public Schools',
    branches: [
      {
        id: '201',
        name: 'Main Building',
        address: '456 Oak Ave, Portland, OR 97205',
        schoolId: '2'
      }
    ]
  },
  {
    id: '3',
    name: 'Jefferson Middle School',
    address: '789 Pine St',
    city: 'San Francisco',
    state: 'CA',
    zipCode: '94107',
    phone: '(415) 555-9012',
    email: 'info@jeffersonms.edu',
    website: 'www.jeffersonms.edu',
    principalName: 'Mr. Robert Chen',
    districtName: 'San Francisco Unified',
    branches: [
      {
        id: '301',
        name: 'Main Campus',
        address: '789 Pine St, San Francisco, CA 94107',
        schoolId: '3'
      },
      {
        id: '302',
        name: 'Arts Wing',
        address: '790 Pine St, San Francisco, CA 94107',
        schoolId: '3'
      }
    ]
  },
  {
    id: '4',
    name: 'Roosevelt High School',
    address: '101 Cedar Blvd',
    city: 'Chicago',
    state: 'IL',
    zipCode: '60601',
    phone: '(312) 555-3456',
    email: 'admin@roosevelths.edu',
    website: 'www.roosevelths.edu',
    principalName: 'Dr. Michael Williams',
    districtName: 'Chicago Public Schools',
    branches: [
      {
        id: '401',
        name: 'Main Building',
        address: '101 Cedar Blvd, Chicago, IL 60601',
        schoolId: '4'
      },
      {
        id: '402',
        name: 'Science Wing',
        address: '102 Cedar Blvd, Chicago, IL 60601',
        schoolId: '4'
      },
      {
        id: '403',
        name: 'Athletics Complex',
        address: '105 Cedar Blvd, Chicago, IL 60601',
        schoolId: '4'
      }
    ]
  },
  {
    id: '5',
    name: 'Franklin Academy',
    address: '222 Maple Dr',
    city: 'New York',
    state: 'NY',
    zipCode: '10001',
    phone: '(212) 555-7890',
    email: 'info@franklinacademy.edu',
    website: 'www.franklinacademy.edu',
    principalName: 'Ms. Elizabeth Taylor',
    districtName: 'NYC Department of Education',
    branches: [
      {
        id: '501',
        name: 'Lower School',
        address: '222 Maple Dr, New York, NY 10001',
        schoolId: '5'
      },
      {
        id: '502',
        name: 'Upper School',
        address: '224 Maple Dr, New York, NY 10001',
        schoolId: '5'
      }
    ]
  }
];