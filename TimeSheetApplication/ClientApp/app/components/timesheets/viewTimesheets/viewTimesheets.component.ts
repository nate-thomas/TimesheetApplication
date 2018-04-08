import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import { Input, Output, EventEmitter } from '@angular/core'

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Timesheet } from '../timesheetsTable/timesheets'
import { AppComponent } from '../../app/app.component'

@Component({
    selector: 'viewTimesheets',
    styleUrls: ['./viewTimesheets.component.css'],
    templateUrl: './viewTimesheets.component.html'
})
export class ViewTimesheetsComponent {
    @Input()
    timesheet: Timesheet;
    @Output()
    timesheetChange = new EventEmitter<Timesheet>();

    timesheets: Object[] = new Array();

    constructor(private http: Http, private router: Router) { }

    /* Functions to be called when component is loaded */

    ngOnInit() {
        this.loadTimesheets();
    }

    /* Utiilty methods */

    validateSupervisorRole() {
        if (localStorage.getItem("role") == "Supervisor") {
            return true;
        } else {
            return false;
        }
    }

    applyStatusStyling(statusName: string) {
        if (statusName == "Submitted") {
            return "submitted-styling";
        } else if (statusName == "Approved") {
            return "approved-styling";
        } else if (statusName == "Rejected") {
            return "rejected-styling";
        }
    }

    formatDate(endDate: string) {
        return endDate.substring(0, 10);
    }

    /* Subscription methods to bind the response to a property (if applicable) */

    loadTimesheet(employeeNumber: string, endDate: string) {
        this.getTimesheet(employeeNumber, this.formatDate(endDate))
            .subscribe(
                timesheet => {
                    this.timesheet = timesheet;
                    this.timesheetChange.emit(this.timesheet);
                }
            );
    }

    loadTimesheets() {
        this.getTimesheets()
            .subscribe(
                (timesheets: any) => this.timesheets = timesheets
            );
    }

    loadSupervisorTimesheets() {
        this.getSupervisorTimesheets()
            .subscribe(
                (timesheets: any) => this.timesheets = timesheets
            );
    }

    /* CRUD methods to make RESTful calls to the API */

    getTimesheet(employeeNumber: string, endDate: string): Observable<Timesheet> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Timesheets/" + employeeNumber + "/" + endDate, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    getTimesheets() {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Timesheets/" + localStorage.getItem("employeeNumber"), options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    getSupervisorTimesheets() {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Timesheets/Supervisor/" + localStorage.getItem("employeeNumber"), options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }
}