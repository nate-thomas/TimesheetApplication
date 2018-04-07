import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

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
    timesheets: Timesheet[] = new Array();

    constructor(private http: Http, private router: Router) { }

    /* Functions to be called when component is loaded */

    ngOnInit() {
        this.loadTimesheets();
    }

    /* Subscription methods to bind the response to a property (if applicable) */

    loadTimesheets() {
        this.getTimesheets()
            .subscribe(
                (timesheets: any) => this.timesheets = timesheets
            );
    }

    /* CRUD methods to make RESTful calls to the API */

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
}