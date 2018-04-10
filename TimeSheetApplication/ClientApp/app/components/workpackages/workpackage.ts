import { Timesheet } from '../timesheets/timesheetsTable/timesheets'
import { TimesheetRow } from '../timesheets/timesheetsTable/timesheetRows'

export class Workpackage {
    projectNumber: string;
    workpackageNumber: string;
    description: string;
    project: Object;
    timesheetRows: TimesheetRow[];

    constructor() { }
}