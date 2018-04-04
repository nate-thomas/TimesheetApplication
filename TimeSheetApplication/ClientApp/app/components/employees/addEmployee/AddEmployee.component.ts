﻿import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

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
    employees: Employee[] = new Array();
    employee: Employee = new Employee();

    constructor(private http: Http, private router: Router) { }

    /* Temporary method to clear the properties in the component */

    clearProperties() {
        this.employees = new Array();
        this.employee = new Employee();

        this.router.navigateByUrl('/employees');
    }

    /* Subscription methods to bind the response to a property (if applicable) */

    loadEmployees() {
        this.getEmployees()
            .subscribe(
                employees => this.employees = employees
            );
    }

    loadEmployee(employeeNumber: string) {
        this.getEmployee(employeeNumber)
            .subscribe(
                employee => this.employee = employee
            );
    }

    removeEmployee(employeeNumber: string) {
        this.deleteEmployee(employeeNumber)
            .subscribe(res => console.log("Response: " + JSON.stringify(res)));
    }

    addEmployee() {
        this.postEmployee(this.employee)
            .subscribe(res => console.log("Response: " + JSON.stringify(res)));
    }

    updateEmployee() {
        this.putEmployee(this.employee.employeeNumber, this.employee)
            .subscribe(res => console.log("Response: " + JSON.stringify(res)));
    }

    /* CRUD methods to make RESTful calls to the API */

    getEmployees(): Observable<Employee[]> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Employees/", options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }

    getEmployee(employeeNumber: string): Observable<Employee> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Employees/" + employeeNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }

    deleteEmployee(employeeNumber: string): Observable<Employee> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.delete(AppComponent.url + "/api/Employees/" + employeeNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }

    postEmployee(employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.post(AppComponent.url + "/api/Employees/", this.employee, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }

    putEmployee(employeeNumber: string, employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.put(AppComponent.url + "/api/Employees/" + employeeNumber, this.employee, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }
}