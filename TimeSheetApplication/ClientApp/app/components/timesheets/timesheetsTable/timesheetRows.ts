import { Timesheet } from './timesheets'

export class TimesheetRow {
    timesheetRowsId: string;
    employeeNumber: string;
    endDate: string;
    projectNumber: string;
    workPackageNumber: string;
    saturday: number = 0;
    sunday: number = 0;
    monday: number = 0;
    tuesday: number = 0;
    wednesday: number = 0;
    thursday: number = 0;
    friday: number = 0;

    constructor() { }
}