import { Timesheet } from './timesheets'

export class TimesheetRow {
    employeeNumber: string = localStorage.getItem("employeeNumber") || "";
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