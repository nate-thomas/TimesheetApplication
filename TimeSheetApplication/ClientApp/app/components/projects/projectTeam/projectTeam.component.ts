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
    selected: Employee;
    projectMembers: Employee[];

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

        return this.http.get(AppComponent.url + "/api/ProjectTeams/" + projectNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    ngOnInit() {
        
        //this.loademployees();
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes['selectedProjectNumber'] !== undefined)
            this.inputProjectNumber = changes['selectedProjectNumber'].currentValue;
        if (this.inputProjectNumber !== undefined && this.inputProjectNumber != '') {
            console.log("hi im in project team:" + this.inputProjectNumber + ".");
            this.loadEmployees();
        }
    }

    onSelect(member: Employee) {
        this.outputEmployee.emit(member);
        this.selected = member;
    }

    removeEmployeeFromProject() { }

    addEmployeeToProject() { }
}