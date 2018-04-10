import { Component, EventEmitter, Input, Output, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { Employee } from '../../employees/employees';
import { AppComponent } from '../../app/app.component';

@Component({
    selector: 'workpackageteam',
    templateUrl: './workPackageTeam.component.html',
    styleUrls: ['./workPackageTeam.component.css']
})
export class WorkPackageTeamComponent implements OnChanges {
    @Input() inputProjectNumber: string = 'WebPrj128';
    @Input() inputWorkPackageNumber: string = 'A2';
    @Input() inputMember: Employee = new Employee();
    selected: Employee;
    projectMembers: Employee[];

    constructor(private http: Http) { }

    loadEmployees() {
        this.getEmployees(this.inputProjectNumber, this.inputWorkPackageNumber)
            .subscribe(
            employees => this.projectMembers = employees
            );
    }

    getEmployees(projectNumber: string, workPackageNumber: string): Observable<Employee[]> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + '/api/WPAssignments/Employees/' + projectNumber + '/'+ workPackageNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    ngOnInit() {
        //this.loadEmployees();
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes['selectedProjectNumber'] !== undefined) {
            this.inputProjectNumber = changes['selectedProjectNumber'].currentValue;
            this.inputWorkPackageNumber = '';
        }
        if (changes['selectedWorkPackageNumber'] !== undefined)
            this.inputWorkPackageNumber = changes['selectedWorkPackageNumber'].currentValue;
        if (this.inputProjectNumber !== undefined && this.inputProjectNumber != '' && this.inputWorkPackageNumber !== undefined && this.inputWorkPackageNumber != '') {

            console.log("hi im in wp team:" + this.inputProjectNumber + "." + this.inputWorkPackageNumber + ".");
            this.loadEmployees();
        }
    }

    onSelect(member: Employee) {
        this.selected = member;
    }

    removeEmployeeFromWorkPackage() { }

    addEmployeeToWorkPackage() { }
}