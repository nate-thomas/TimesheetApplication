import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { TimesheetRow } from './timesheetRows'

@Component({
    selector: 'timesheets',
    templateUrl: './timesheets.component.html'
})
export class TimesheetsComponent {
    timesheet: TimesheetRow[] = new Array();

    constructor(private http: Http) { }

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

    getTimesheetRows(employeeNumber: string, endDate:string): Observable<TimesheetRow[]> {
        return this.http.get("http://localhost:61150/api/EmployeesAPI/" + employeeNumber + "/" + endDate)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    deleteTimesheetRows(employeeNumber: string, endDate: string): Observable<TimesheetRow[]> {
        return this.http.delete("http://localhost:61150/api/EmployeesAPI/" + employeeNumber + "/" + endDate)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    postTimesheetRows(employeeNumber: string, endDate: string, timesheet: TimesheetRow[]): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post("http://localhost:61150/api/EmployeesAPI/" + employeeNumber + "/" + endDate, this.timesheet, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    putTimesheetRows(employeeNumber: string, endDate: string, timesheet: TimesheetRow[]): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.put("http://localhost:61150/api/EmployeesAPI/" + employeeNumber + "/" + endDate, this.timesheet, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }
}
