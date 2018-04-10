import { Employee } from '../../employees/employees'
import { TimesheetRow } from './timesheetRows'

export class Timesheet {
    employeeNumber: string = localStorage.getItem("employeeNumber") || "";;
    statusName: string = "Draft";
    endDate: string;
    timesheetRows: TimesheetRow[] = new Array();

    constructor() { }
}