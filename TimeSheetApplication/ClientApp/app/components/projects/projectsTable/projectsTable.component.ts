import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Component } from '@angular/core';
import { Timesheet } from '../../timesheets/timesheetsTable/timesheets';


@Component({
    selector: 'projectsTable',
    styleUrls: ['./projectsTable.component.css'],
    templateUrl: './projectsTable.component.html'
})
export class ProjectsTableComponent {
    timesheet: Timesheet = new Timesheet();
}