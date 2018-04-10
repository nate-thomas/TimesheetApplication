import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Project } from '../projects';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AppComponent } from '../../app/app.component';
import { Employee } from '../../employees/employees';

@Component({
    selector: 'addtoprojectteam',
    styleUrls: ['./addToProjectTeam.component.css'],
    templateUrl: './addToProjectTeam.component.html'
})
export class AddToProjectTeamComponent {

    employees: Employee[] = [];
    newMember: Employee = new Employee();

    @Input()
    currentMembers: Employee[];

    @Input()
    project: string;


    @Output()
    projectTeamChange = new EventEmitter<Employee[]>();

    constructor(private http: Http, private router: Router) { }

    loadEmployees() {
        this.getEmployees()
            .subscribe(
            employees => this.employees = employees
            );
    }

    getEmployees(): Observable<Employee[]> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Employees/", options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

 
    postToProjectTeam(): Promise<void | null> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });


        const url = AppComponent.url + '/api/ProjectTeams/';
        return this.http.post(url,
            { "projectNumber": this.project, "employeeNumber": this.newMember.employeeNumber },
            { headers: headers })
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    ngOnInit() {
        this.loadEmployees();
    }


    addToProjectTeam() {
        console.log("child:" + this.project + " " + this.newMember.employeeNumber);
        this.postToProjectTeam()
            .then(() => this.currentMembers.push(this.newMember));
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }

    loadProjectTeam() {
        this.projectTeamChange.emit(this.currentMembers);
    }
}