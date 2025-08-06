import { Resource } from '../../types';

export const mockResources: Resource[] = [
  {
    id: '1',
    name: 'Algebra Textbook',
    type: 'Book',
    description: 'Standard textbook for Algebra I',
    quantity: 120,
    price: 75.99,
    required: true,
    gradeLevel: '9',
    courseId: '1',
    courseName: 'Algebra I'
  },
  {
    id: '2',
    name: 'English Literature Anthology',
    type: 'Book',
    description: 'Collection of classic literature for English class',
    quantity: 95,
    price: 89.50,
    required: true,
    gradeLevel: '10',
    courseId: '2',
    courseName: 'English Literature'
  },
  {
    id: '3',
    name: 'Biology Textbook',
    type: 'Book',
    description: 'Comprehensive biology textbook with online resources',
    quantity: 105,
    price: 95.75,
    required: true,
    gradeLevel: '9',
    courseId: '3',
    courseName: 'Biology'
  },
  {
    id: '4',
    name: 'American History Textbook',
    type: 'Book',
    description: 'Complete American history from pre-colonial to modern era',
    quantity: 90,
    price: 85.25,
    required: true,
    gradeLevel: '10',
    courseId: '4',
    courseName: 'American History'
  },
  {
    id: '5',
    name: 'Scientific Calculator',
    type: 'Supply',
    description: 'TI-84 or equivalent scientific calculator',
    quantity: 50,
    price: 115.99,
    required: true,
    gradeLevel: '9-12',
    courseId: '1',
    courseName: 'Algebra I'
  },
  {
    id: '6',
    name: 'Art Supply Kit',
    type: 'Supply',
    description: 'Complete kit with sketchpad, pencils, and basic paints',
    quantity: 45,
    price: 42.50,
    required: true,
    gradeLevel: 'All',
    courseId: '6',
    courseName: 'Art Fundamentals'
  },
  {
    id: '7',
    name: 'Lab Notebook',
    type: 'Supply',
    description: 'Specialized notebook for biology lab experiments',
    quantity: 110,
    price: 8.99,
    required: true,
    gradeLevel: '9',
    courseId: '3',
    courseName: 'Biology'
  },
  {
    id: '8',
    name: 'Gym Uniform',
    type: 'Supply',
    description: 'School PE uniform (shirt and shorts)',
    quantity: 150,
    price: 25.00,
    required: true,
    gradeLevel: 'All',
    courseId: '5',
    courseName: 'Physical Education'
  },
  {
    id: '9',
    name: 'Computer Science Handbook',
    type: 'Book',
    description: 'Reference book for computer science concepts',
    quantity: 65,
    price: 45.75,
    required: false,
    gradeLevel: 'All',
    courseId: '7',
    courseName: 'Computer Science Principles'
  },
  {
    id: '10',
    name: 'USB Flash Drive',
    type: 'Supply',
    description: '16GB minimum storage for computer class',
    quantity: 80,
    price: 12.99,
    required: true,
    gradeLevel: 'All',
    courseId: '7',
    courseName: 'Computer Science Principles'
  }
];