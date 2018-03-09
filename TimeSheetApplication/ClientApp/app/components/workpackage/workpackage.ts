import { Timesheet } from '../timesheetsTable/timesheets'
import { TimesheetRow } from '../timesheetsTable/timesheetRows'

export class Workpackage {
    projectNumber: string;
    workpackageNumber: string;
    description: string;
    project: Object;
    timesheetRows: TimesheetRow[];

    constructor() { }
}