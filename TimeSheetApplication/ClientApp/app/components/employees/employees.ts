import { Timesheet } from '../timesheets/timesheetsTable/timesheets'
import { LaborGrade } from './laborGrades'

export class Employee {
    employeeNumber: string;
    firstName: string;
    lastName: string;
    laborGrade: LaborGrade;
    grade: string;
    employeeInitials: string;
    supervisor: Object;
    supervisorNumber: string;
    authCode: string;
    authorizationCode: Object;
    timesheets: Timesheet[];

    constructor() { }
}