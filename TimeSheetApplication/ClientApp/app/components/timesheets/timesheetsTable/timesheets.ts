import { Employee } from '../../employees/employees'
import { TimesheetRow } from './timesheetRows'
import { TimesheetStatus } from './timesheetStatus'

export class Timesheet {
    employeeNumber: string;
    statusName: string;
    endDate: string;
    employee: Employee;
    timesheetStatus: TimesheetStatus;
    timesheetRows: TimesheetRow[];

    constructor() { }
}