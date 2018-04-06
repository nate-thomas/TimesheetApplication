import { Employee } from '../../employees/employees'
import { TimesheetRow } from './timesheetRows'

export class Timesheet {
    employeeNumber: string;
    statusName: string;
    endDate: string;
    timesheetRows: TimesheetRow[];

    constructor() { }
}