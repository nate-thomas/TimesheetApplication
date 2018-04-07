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
    employeeNumber: string = localStorage.getItem("username") || "";
    endDate: string = (new Date()).getFullYear() + "-" + ((new Date()).getMonth() + 1) + "-" + (new Date()).getDate();
    weekNumber: number = this.getWeekNumber(this.endDate);
    flextime: number = 0;
    overtime: number = 0;

    constructor(private http: Http) { }

    /* Temporary method to clear the properties in the component */

    clearProperties() {
        this.timesheet = new Array();
    }

    /* Temporary method to display the Timesheet object in the browser console */

    printProperties() {
        console.log(JSON.stringify(this.timesheet));
    }

    /* Functions to be called when component is loaded */

    ngOnInit() {
        this.loadTimesheet();
    }

    /* Utility methods */

    addTimesheetRow() {
        let row = new TimesheetRow();
        row.employeeNumber = this.employeeNumber;
        row.endDate = this.endDate;
        this.timesheet.push(row);
    }

    deleteTimesheetRow(index: number) {
        this.timesheet.splice(index, 1);
    }

    getWeekNumber(endDate: string) {
        var onejan = new Date((new Date).getFullYear(), 0, 1);
        var today = new Date(endDate);
        var dayOfYear = ((today.getTime() - onejan.getTime() + 1) / 86400000);
        return Math.ceil(dayOfYear / 7);
    }

    validateHour(hour: number) {
        if (hour < 0 || hour > 24) {
            return 'invalid-hour';
        } else {
            return '';
        }
    }

    validateHours() {
        let totalHours = 0;
        let requiredHours = 40;
        for (let timesheetRow of this.timesheet) {
            totalHours += timesheetRow.saturday +
                timesheetRow.sunday +
                timesheetRow.monday +
                timesheetRow.tuesday +
                timesheetRow.wednesday +
                timesheetRow.thursday +
                timesheetRow.friday;
        }
        if (totalHours == requiredHours) {
            return true;
        } else {
            return false;
        }
    }

    /* Subscription methods to bind the response to a property (if applicable) */

    loadTimesheet() {
        this.getTimesheetRows(this.employeeNumber, this.endDate)
            .subscribe(
                timesheet => this.timesheet = timesheet
        );
        this.weekNumber = this.getWeekNumber(this.endDate);
    }

    removeTimesheet() {
        this.deleteTimesheetRows(this.employeeNumber, this.endDate)
            .subscribe(res => { alert("Deletion successful") });
        this.clearProperties();
    }

    addTimesheet() {
        if (this.validateHours()) {
            this.postTimesheetRows(this.employeeNumber, this.endDate, this.timesheet)
                .subscribe(res => { alert("Creation successful") });
        } else {
            alert("Total timesheet hours must add up to 40.");
        }
    }

    updateTimesheet() {
        if (this.validateHours()) {
            this.putTimesheetRows(this.employeeNumber, this.endDate, this.timesheet)
                .subscribe(res => { alert("Update successful") });
        } else {
            alert("Total timesheet hours must add up to 40.");
        }
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
