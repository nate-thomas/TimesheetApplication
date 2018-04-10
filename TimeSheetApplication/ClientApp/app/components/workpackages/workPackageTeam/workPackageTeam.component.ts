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
    workPackageMembers: Employee[];
    emptyEmployee: Employee = new Employee();

    constructor(private http: Http) { }

    loadEmployees() {
        this.getEmployees(this.inputProjectNumber, this.inputWorkPackageNumber)
            .subscribe(
                employees => this.workPackageMembers = employees
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

    deleteEmployeeFromWorkPackageTeam(id: string): Promise<void> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        const url = AppComponent.url + '/api/WPassignments/' + id + '/' + this.inputProjectNumber + '/' + this.inputWorkPackageNumber;
        return this.http.delete(url, { headers: headers })
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    postEmployeeToWorkPackageTeam(id: string): Promise<void> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        const url = AppComponent.url + '/api/WPassignments/';
        return this.http.post(url,
            {
                employeeNumber: id,
                projectNumber: this.inputProjectNumber,
                workPackageNumber: this.inputWorkPackageNumber
            },
            { headers: headers })
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
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

    remove(delMember: Employee) {
        this.deleteEmployeeFromWorkPackageTeam(delMember.employeeNumber).then(() => {
            this.workPackageMembers = this.workPackageMembers.filter(e => e !== delMember);
            if (this.selected === delMember) { this.selected = this.emptyEmployee; }
        });
    }

    add() {
        let addMember = this.inputMember;
        this.postEmployeeToWorkPackageTeam(addMember.employeeNumber).then(() => {
            this.workPackageMembers.push(addMember);
        });
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}