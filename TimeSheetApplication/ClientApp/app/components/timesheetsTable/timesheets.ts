import { Employee } from '../employees/employees'
import { TimesheetRow } from './timesheetRows'

export class Timesheet {
    employeeNumber: string;
    endDate: string;
    employee: Employee;
    timesheetRows: TimesheetRow[];

    constructor() { }
}