import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import { Input, Output, EventEmitter } from '@angular/core'

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from '../employees';
import { AppComponent } from '../../app/app.component'

@Component({
    selector: 'AddEmployee',
    styleUrls: ['./addEmployee.component.css'],
    templateUrl: './addEmployee.component.html'
})
export class AddEmployeeComponent {
    @Input()
    employee: Employee;
    @Input()
    employees: Employee[];
    @Input()
    supervisors: Employee[];
    @Output()
    employeesChange = new EventEmitter<Employee[]>();

    grades: Object[] = new Array();
    roles: String[] = new Array();

    constructor(private http: Http, private router: Router) { }

    /* Functions to be called when component is loaded */

    ngOnInit() {
        this.loadGrades();
        this.loadRoles();
    }

    /* Utility methods */

    validateInput(input: string) {
        if (input == undefined || input == null || input == "") {
            return 'invalid-input';
        } else {
            return '';
        }
    }

    /* Subscription methods to bind the response to a property (if applicable) */

    loadEmployees() {
        this.getEmployees()
            .subscribe(
                employees => {
                    this.employees = employees;
                    this.employeesChange.emit(this.employees);
                }
        );
    }

    addEmployee() {
        this.employee.password = "P@$$w0rd";
        this.employee.confirmPassword = "P@$$w0rd";

        if (this.employee.employeeNumber &&
            this.employee.firstName &&
            this.employee.lastName &&
            this.employee.supervisorNumber &&
            this.employee.employeeIntials &&
            this.employee.grade &&
            this.employee.role) {

            if (this.employee.employeeNumber.toString().length != 7) {
                alert("Employee number must be 7 digits!");
            } else {
                this.postEmployee(this.employee)
                    .subscribe(res => {
                        alert("Employee added!");
                    });
            }

        } else {
            alert("All fields are required!");
        }
        
    }

    loadGrades() {
        this.getGrades()
            .subscribe(
                (grades: any) => this.grades = grades
            );
    }

    loadSupervisors() {
        this.getSupervisors()
            .subscribe(
                (supervisors: any) => this.supervisors = supervisors
            );
    }

    loadRoles() {
        this.getRoles()
            .subscribe(
                (roles: any) => this.roles = roles
            );
    }

    /* CRUD methods to make RESTful calls to the API */

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

    postEmployee(employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.post(AppComponent.url + "/api/Employees/", this.employee, options)
            .map((res: Response) => res.json())
            .catch((err: any) => {
                alert(err._body);
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    getGrades(): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/LaborGrades/", options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    getSupervisors(): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/UsersInRoles/" + "Supervisor", options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    getRoles(): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Roles/", options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    putRole(employeeNumber: string, role: string): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        let formattedRole = role.replace(" ", "-");

        return this.http.put(AppComponent.url + "/api/Employees/" + employeeNumber + "/" + formattedRole, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }
}