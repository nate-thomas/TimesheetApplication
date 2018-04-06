import { Timesheet } from '../timesheets/timesheetsTable/timesheets'
import { LaborGrade } from './laborGrades'

export class Employee {
    employeeNumber: string;
    employeeName: string;
    firstName: string;
    lastName: string;
    grade: string;
    employeeIntials: string;
    supervisorNumber: string;
    role: string;
    password: string;
    confirmPassword: string;

    constructor() { }
}