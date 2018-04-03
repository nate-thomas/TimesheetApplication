import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { TimesheetRow } from './timesheetRows'
import { AppComponent } from '../../app/app.component'

@Component({
    selector: 'timesheetsTable',
    styleUrls: ['./timesheetsTable.component.css'],
    templateUrl: './timesheetsTable.component.html'
})
export class TimesheetsTableComponent {
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
                timesheet => this.timesheet = timesheet
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
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/TimesheetRows/" + employeeNumber + "/" + endDate, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }

    deleteTimesheetRows(employeeNumber: string, endDate: string): Observable<TimesheetRow[]> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.delete(AppComponent.url + "/api/TimesheetRows/" + employeeNumber + "/" + endDate, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }

    postTimesheetRows(employeeNumber: string, endDate: string, timesheet: TimesheetRow[]): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.post(AppComponent.url + "/api/TimesheetRows/" + employeeNumber + "/" + endDate, this.timesheet, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }

    putTimesheetRows(employeeNumber: string, endDate: string, timesheet: TimesheetRow[]): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.put(AppComponent.url + "/api/TimesheetRows/" + employeeNumber + "/" + endDate, this.timesheet, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }
}
