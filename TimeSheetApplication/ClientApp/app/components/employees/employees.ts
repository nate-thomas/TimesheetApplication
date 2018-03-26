import { Timesheet } from '../timesheets/timesheetsTable/timesheets'

export class Employee {
    employeeNumber: string;
    firstName: string;
    lastName: string;
    laborGrade: Object;
    grade: string;
    employeeInitials: string;
    supervisor: Object;
    supervisorNumber: string;
    authCode: string;
    authorizationCode: Object;
    timesheets: Timesheet[];

    constructor() { }
}