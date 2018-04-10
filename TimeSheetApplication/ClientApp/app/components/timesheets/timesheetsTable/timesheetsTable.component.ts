import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Timesheet } from './timesheets'
import { TimesheetRow } from './timesheetRows'
import { Project } from '../../projects/projects'
import { WorkPackage } from '../../workpackages/workPackages'
import { AppComponent } from '../../app/app.component'

@Component({
    selector: 'timesheetsTable',
    styleUrls: ['./timesheetsTable.component.css'],
    templateUrl: './timesheetsTable.component.html'
})
export class TimesheetsTableComponent {
    timesheets: Timesheet[] = new Array();
    timesheet: Timesheet = new Timesheet();
    endDate: string = this.formatDate(new Date);
    weekNumber: number = this.getWeekNumber(this.endDate);
    projects: Project[] = new Array();
    workPackages: WorkPackage[] = new Array();
    employeeNumber: string = localStorage.getItem("employeeNumber") || "";
    projectWorkPackageMap: { [index: string]: any } = {};

    constructor(private http: Http) { }

    testFunction() {
        console.log("called");
    }

    /* Functions to be called when component is loaded */

    ngOnInit() {
        this.loadTimesheet();
        this.loadProjects();
        this.loadWorkPackages();
    }

    /* Utility methods */

    setTimesheet(timesheet: Timesheet) {
        this.timesheet = timesheet;
        this.endDate = timesheet.endDate.substring(0, 10);
        this.weekNumber = this.getWeekNumber(this.endDate);
        this.employeeNumber = timesheet.employeeNumber;
    }

    addTimesheetRow() {
        let row = new TimesheetRow();

        row.endDate = this.endDate;
        this.timesheet.endDate = this.endDate;

        this.timesheet.timesheetRows.push(row);
    }

    deleteTimesheetRow(index: number) {
        this.timesheet.timesheetRows.splice(index, 1);
    }

    getWeekNumber(endDate: string) {
        var onejan = new Date((new Date).getFullYear(), 0, 1);
        var today = new Date(endDate);
        var dayOfYear = ((today.getTime() - onejan.getTime() + 1) / 86400000);
        return Math.ceil(dayOfYear / 7);
    }

    formatDate(date: Date) {
        return date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
    }

    checkEndDate() {
        var dayOfWeek = 5;
        var currentDate = new Date(this.endDate);

        if (currentDate.toString() == "Invalid Date") {
            currentDate = new Date();
        }

        currentDate.setDate(currentDate.getDate() + (dayOfWeek + 7 - currentDate.getDay()) % 7);
        this.endDate = this.formatDate(currentDate);
    }

    checkTimesheetStatus() {
        if (this.timesheet.statusName == "Draft" || this.timesheet.statusName == "Rejected") {
            return true;
        } else {
            return false;
        }
    }

    checkStatusRoleAndNumber() {
        if (!this.checkTimesheetStatus() &&
            localStorage.getItem("role") == "Supervisor" &&
            this.timesheet.employeeNumber != localStorage.getItem("employeeNumber")) {

            return true;
        } else {
            return false;
        }
    }

    validateInput(input: string) {
        if (this.timesheet.statusName == "Draft" || this.timesheet.statusName == "Rejected") {
            if (input == undefined || input == null || input == "") {
                return 'timesheet-input invalid-input';
            } else {
                return 'timesheet-input';
            }
        } else {
            return 'timesheet-input disabled-input';
        }
    }

    validateDailyHours(hour: number) {
        if (this.timesheet.statusName == "Draft" || this.timesheet.statusName == "Rejected") {
            if (hour < 0 || hour > 24) {
                return 'timesheet-input invalid-input';
            } else {
                return 'timesheet-input';
            }
        } else {
            return 'timesheet-input disabled-input';
        }
    }

    validateTotalHours() {
        let totalHours = 0;
        let requiredHours = 40;
        
        for (let timesheetRow of this.timesheet.timesheetRows) {
            if (timesheetRow.saturday < 0 || timesheetRow.saturday > 24 ||
                timesheetRow.sunday < 0 || timesheetRow.sunday > 24 ||
                timesheetRow.monday < 0 || timesheetRow.monday > 24 ||
                timesheetRow.tuesday < 0 || timesheetRow.tuesday > 24 ||
                timesheetRow.wednesday < 0 || timesheetRow.wednesday > 24 ||
                timesheetRow.thursday < 0 || timesheetRow.thursday > 24 ||
                timesheetRow.friday < 0 || timesheetRow.friday > 24) {

                return false;
            }

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

    calculateTimesheetHours(timesheetRow: TimesheetRow) {
        return timesheetRow.saturday +
               timesheetRow.sunday +
               timesheetRow.monday +
               timesheetRow.tuesday +
               timesheetRow.wednesday +
               timesheetRow.thursday +
               timesheetRow.friday;
    }

    calculateTotalHours() {
        let totalHours = 0;
        for (let i = 0; i < this.timesheet.timesheetRows.length; i++) {
            totalHours += Number((document.getElementById("totalInput" + i) as HTMLInputElement).value);
        }
        return totalHours;
    }

    generateProjectWorkPackageMap() {
        for (let workPackage of this.workPackages) {
            if (this.projectWorkPackageMap[workPackage.projectNumber]) {
                this.projectWorkPackageMap[workPackage.projectNumber].push(workPackage.workPackageNumber);
            } else {
                this.projectWorkPackageMap[workPackage.projectNumber] = [];
                this.projectWorkPackageMap[workPackage.projectNumber].push(workPackage.workPackageNumber);
            }
        }
    }

    /* Subscription methods to bind the response to a property (if applicable) */

    loadTimesheets() {
        this.getTimesheets()
            .subscribe(
                (timesheets: any) => this.timesheets = timesheets
            );
    }

    loadTimesheet() {
        this.checkEndDate();
        this.employeeNumber = localStorage.getItem("employeeNumber") || "";
        this.getTimesheet(localStorage.getItem("employeeNumber") || "", this.endDate)
            .subscribe(
                timesheet => {
                    this.timesheet = timesheet;
                }
            );
        this.weekNumber = this.getWeekNumber(this.endDate);
    }

    updateTimesheet(status: string) {
        if (this.validateTotalHours()) {
            if ((new Date(this.timesheet.endDate)).getDay() != 5) {
                alert("You can only update a timesheet on a Friday!");
            } else {
                let alertMessage = "";

                switch (status) {
                    case "Draft":
                        alertMessage = "Timesheet saved!";
                        break;
                    case "Submitted":
                        alertMessage = "Timesheet submitted!";
                        break;
                    case "Approved":
                        alertMessage = "Timesheet approved!";
                        break;
                    case "Rejected":
                        alertMessage = "Timesheet rejected!";
                        break;
                    default:
                        alertMessage = "Timesheet updated!";
                }

                this.timesheet.statusName = status;

                this.putTimesheetRows(this.employeeNumber, this.endDate, this.timesheet)
                    .subscribe(res => {
                        if (status == "Approved" || status == "Rejected") {
                            this.endDate = this.formatDate(new Date);
                            this.loadTimesheet();
                        }

                        alert(alertMessage);
                    });
            }
        } else {
            alert("Total timesheet hours must add up to 40 and each day's total hours must be between 0 and 24.");
        }
    }

    loadProjects() {
        this.getProjects()
            .subscribe(
                projects => this.projects = projects
            );
    }

    loadWorkPackages() {
        this.getWorkPackages()
            .subscribe(
                workPackages => { 
                    this.workPackages = workPackages;
                    this.generateProjectWorkPackageMap();
                }
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

    getTimesheet(employeeNumber: string, endDate: string): Observable<Timesheet> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Timesheets/" + employeeNumber + "/" + endDate, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                this.timesheet = new Timesheet();
                this.addTimesheetRow();

                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    putTimesheetRows(employeeNumber: string, endDate: string, timesheet: Timesheet): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.put(AppComponent.url + "/api/Timesheets/" + employeeNumber + "/" + endDate, this.timesheet, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    getProjects(): Observable<Project[]> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Projects/", options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });

    }

    getWorkPackages(): Observable<WorkPackage[]> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/WorkPackages/", options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });

    }
}
