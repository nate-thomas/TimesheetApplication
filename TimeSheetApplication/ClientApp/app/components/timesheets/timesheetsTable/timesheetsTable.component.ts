import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Timesheet } from './timesheets'
import { TimesheetRow } from './timesheetRows'
import { Project } from '../../projects/projects'
import { WorkPackage } from '../../workpackages/workPackages'
import { WPassignment } from '../../workpackages/wpAssignments'
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
    employeeNumber: string = localStorage.getItem("employeeNumber") || "";
    employeeProjectsWPMap: { [index: string]: any } = {};
    employeeProjects: string[] = new Array();

    constructor(private http: Http) { }

    testFunction() {
        console.log("called");
    }

    /* Functions to be called when component is loaded */

    ngOnInit() {
        this.loadTimesheet();
        this.loadWPassignments();
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


    validateInputHours(hour: number) {
        if (this.timesheet.statusName == "Draft" || this.timesheet.statusName == "Rejected") {
            if (hour == null || hour < 0 || hour > 24) {
                return 'timesheet-input invalid-input';
            } else {
                return 'timesheet-input';
            }
        } else {
            return 'timesheet-input disabled-input';
        }
    }

    validateSelectedPWP() {
        for (let timesheetRow of this.timesheet.timesheetRows) {
            if (timesheetRow.projectNumber == null || timesheetRow.workPackageNumber == null) {
                return false;
            }
        }

        return true;
    }

    validateDailyHours() {
        let totalSatHours = 0;
        let totalSunHours = 0;
        let totalMonHours = 0;
        let totalTueHours = 0;
        let totalWedHours = 0;
        let totalThuHours = 0;
        let totalFriHours = 0;

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

            totalSatHours += timesheetRow.saturday;
            totalSunHours += timesheetRow.sunday
            totalMonHours += timesheetRow.monday;
            totalTueHours += timesheetRow.tuesday;
            totalWedHours += timesheetRow.wednesday;
            totalThuHours += timesheetRow.thursday;
            totalFriHours += timesheetRow.friday;
        }

        if (totalSatHours > 24 ||
            totalSunHours > 24 ||
            totalMonHours > 24 ||
            totalTueHours > 24 ||
            totalWedHours > 24 ||
            totalThuHours > 24 ||
            totalFriHours > 24) {

            return false;
        } else {
            return true;
        }
    }

    validateTotalHours(status: string) {
        let totalHours = 0;

        for (let timesheetRow of this.timesheet.timesheetRows) {
            if (timesheetRow.saturday == null || timesheetRow.saturday < 0 || timesheetRow.saturday > 24 ||
                timesheetRow.sunday == null || timesheetRow.sunday < 0 || timesheetRow.sunday > 24 ||
                timesheetRow.monday == null || timesheetRow.monday < 0 || timesheetRow.monday > 24 ||
                timesheetRow.tuesday == null || timesheetRow.tuesday < 0 || timesheetRow.tuesday > 24 ||
                timesheetRow.wednesday == null || timesheetRow.wednesday < 0 || timesheetRow.wednesday > 24 ||
                timesheetRow.thursday == null || timesheetRow.thursday < 0 || timesheetRow.thursday > 24 ||
                timesheetRow.friday == null || timesheetRow.friday < 0 || timesheetRow.friday > 24) {

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

        if (status == "Draft" || totalHours == 40) {
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

    generateEmployeeProjectsWPMap(wpAssignments: WPassignment[]) {
        for (let wpAssignment of wpAssignments) {
            if (this.employeeProjectsWPMap[wpAssignment.projectNumber]) {
                this.employeeProjectsWPMap[wpAssignment.projectNumber].push(wpAssignment.workPackageNumber);
            } else {
                this.employeeProjects.push(wpAssignment.projectNumber);
                this.employeeProjectsWPMap[wpAssignment.projectNumber] = [];
                this.employeeProjectsWPMap[wpAssignment.projectNumber].push(wpAssignment.workPackageNumber);
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
        if (this.validateSelectedPWP() && this.validateTotalHours(status) && this.validateDailyHours()) {
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
            alert("All fields are required and total timesheet hours must be 40 and each day's total hours must be between 0 and 24.");
        }
    }

    loadWPassignments() {
        this.getWPassignments()
            .subscribe(
            wpAssignments => {
                    this.generateEmployeeProjectsWPMap(wpAssignments);
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

                if (err.status == 404) {
                    return Observable.throw(new Error("Timesheet does not exist for the employee at the specified end-date."))
                } else {
                    return Observable.throw(new Error(JSON.stringify(err)));
                }
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

    getWPassignments(): Observable<WPassignment[]> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/WPassignments/All/" + localStorage.getItem("employeeNumber"), options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }
}
