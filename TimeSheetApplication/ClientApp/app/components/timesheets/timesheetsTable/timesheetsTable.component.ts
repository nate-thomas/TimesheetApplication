import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { TimesheetRow } from './timesheetRows'

@Component({
    selector: 'timesheetsTable',
    styleUrls: ['./timesheetsTable.component.css'],
    templateUrl: './timesheetsTable.component.html'
})
export class TimesheetsTableComponent {
    url: string = "http://localhost:58911";

    timesheet: TimesheetRow[] = new Array();

    constructor(private http: Http) { }

    /* Temporary method to clear the properties in the component */

    clearProperties() {
        this.timesheet = new Array();
    }

    /* Temporary method to display the Timesheet object in the browser console */

    printProperties() {
        console.log(JSON.stringify(this.timesheet));
    }

    /* Utility methods */

    addTimesheetRow(employeeNumber: string, endDate: string) {
        let row = new TimesheetRow();
        row.employeeNumber = employeeNumber;
        row.endDate = endDate;
        this.timesheet.push(row);
    }

    /* Subscription methods to bind the response to a property (if applicable) */

    loadTimesheet(employeeNumber: string, endDate: string) {
        this.getTimesheetRows(employeeNumber, endDate)
            .subscribe(
                timesheet => this.timesheet = timesheet,
                errors => {
                    console.log(errors)
                }
            );
    }

    removeTimesheet(employeeNumber: string, endDate: string) {
        this.deleteTimesheetRows(employeeNumber, endDate)
            .subscribe(res => console.log("Response: " + res));
        this.clearProperties();
    }

    addTimesheet(employeeNumber: string, endDate: string) {
        this.postTimesheetRows(employeeNumber, endDate, this.timesheet)
            .subscribe(res => console.log("Response: " + res));
    }

    updateTimesheet() {
        this.putTimesheetRows(this.timesheet[0].employeeNumber, this.timesheet[0].endDate, this.timesheet)
            .subscribe(res => console.log("Response: " + res));
    }

    /* CRUD methods to make RESTful calls to the API */

    getTimesheetRows(employeeNumber: string, endDate: string): Observable<TimesheetRow[]> {
        return this.http.get(this.url + "/api/TimesheetRows/" + employeeNumber + "/" + endDate)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    deleteTimesheetRows(employeeNumber: string, endDate: string): Observable<TimesheetRow[]> {
        return this.http.delete(this.url + "/api/TimesheetRows/" + employeeNumber + "/" + endDate)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    postTimesheetRows(employeeNumber: string, endDate: string, timesheet: TimesheetRow[]): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.url + "/api/TimesheetRows/" + employeeNumber + "/" + endDate, this.timesheet, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    putTimesheetRows(employeeNumber: string, endDate: string, timesheet: TimesheetRow[]): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.put(this.url + "/api/TimesheetRows/" + employeeNumber + "/" + endDate, this.timesheet, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }
}
