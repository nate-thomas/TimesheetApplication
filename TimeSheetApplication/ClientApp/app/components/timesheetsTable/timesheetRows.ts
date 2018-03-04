import { Timesheet } from './timesheets'

export class TimesheetRow {
    timesheetRowsId: string;
    employeeNumber: string;
    endDate: string;
    projectNumber: string;
    workPackageNumber: string;
    saturday: number;
    sunday: number;
    monday: number;
    tuesday: number;
    wednesday: number;
    thursday: number;
    friday: number;
    workPackage: Object;
    timesheet: Timesheet;

    constructor() { }
}