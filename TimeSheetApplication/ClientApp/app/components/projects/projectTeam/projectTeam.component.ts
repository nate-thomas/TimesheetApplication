import { Component, EventEmitter, Input, Output, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { Employee } from '../../employees/employees';
import { AppComponent } from '../../app/app.component';

@Component({
    selector: 'projectteam',
    templateUrl: './projectTeam.component.html',
    styleUrls: ['./projectTeam.component.css']
})
export class ProjectTeamComponent implements OnChanges {
    @Input() inputProjectNumber: string = 'WebPrj128';
    @Output() outputEmployee: EventEmitter<Employee> = new EventEmitter<Employee>();
    outputProjectNumber: string;
    selected: Employee;
    projectMembers: Employee[];
    emptyEmployee: Employee = new Employee();

    constructor(private http: Http) { }

    loadEmployees() {
        this.getEmployees(this.inputProjectNumber)
        .subscribe(
            employees => this.projectMembers = employees
        );
    }

    getEmployees(projectNumber: string): Observable<Employee[]> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + '/api/ProjectTeams/' + projectNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    deleteEmployeeFromProjectTeam(id: string): Promise<void> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        const url = AppComponent.url + '/api/ProjectTeams/' + id + '/' + this.inputProjectNumber;
        return this.http.delete(url, { headers: headers })
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    ngOnInit() {
        //this.loadEmployees();
    }

    ngOnChanges(changes: SimpleChanges) {

        this.outputProjectNumber = this.inputProjectNumber;
        
        if (changes['selectedProjectNumber'] !== undefined) {
            this.inputProjectNumber = changes['selectedProjectNumber'].currentValue;
        }

        if (this.inputProjectNumber !== undefined && this.inputProjectNumber != '') {
            //console.log("hi im in project team:" + this.inputProjectNumber + ".");
            this.loadEmployees();
        }
    }

    onSelect(member: Employee) {
        this.outputEmployee.emit(member);
        if (this.selected == member) {
            this.selected = this.emptyEmployee;
            this.outputEmployee.emit(undefined);
        }
        else
            this.selected = member;
    }

    remove(delMember: Employee) {
        this.deleteEmployeeFromProjectTeam(delMember.employeeNumber).then(() => {
            this.projectMembers = this.projectMembers.filter(e => e !== delMember);
            if (this.selected === delMember) { this.selected = this.emptyEmployee; }
        });
    }

    membersChange(event: any) {
        this.projectMembers = event;
    }

    checkSVRole() {
        return (localStorage.getItem("role") == "Supervisor" || localStorage.getItem("role") == "Administrator")
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}