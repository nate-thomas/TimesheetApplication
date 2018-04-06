﻿import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from '../employees/employees';
import { LaborGrade } from '../employees/laborGrades'
import { AppComponent } from '../app/app.component'

@Component({
    selector: 'user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent {
    employee: Employee = new Employee();
    laborGrades: LaborGrade[] = new Array();

    constructor(private http: Http, private router: Router) { }

    /* Functions to be called when component is loaded */

    ngOnInit() {
        this.employee = new Employee();
        this.loadLaborGrades();
    }

    /* Utility methods */

    validateInput(input: string) {
        if (input == undefined || input == null || input == "") {
            return 'invalid-input';
        } else {
            return '';
        }
    }

    validatePasswords(password: string, confirmPassword: string) {
        if (password == confirmPassword) {
            return true;
        } else {
            alert("Passwords do not match!");
            return false;
        }
    }

    /* Subscription methods to bind the response to a property (if applicable) */

    updateEmployee() {
        this.putEmployee(this.employee.employeeNumber, this.employee)
            .subscribe(res => {
                if (this.validatePasswords(this.employee.password, this.employee.confirmPassword)) {
                    this.postPassword(this.employee.employeeNumber, this.employee.password, this.employee.confirmPassword)
                        .subscribe(res => alert("Employee updated!"));
                }
            });        
    }

    loadLaborGrades() {
        this.getLaborGrades()
            .subscribe(
            (laborGrades: any) => this.laborGrades = laborGrades
            );
    }

    /* CRUD methods to make RESTful calls to the API */

    putEmployee(employeeNumber: string, employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.put(AppComponent.url + "/api/Employees/" + employeeNumber, this.employee, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json());
                return Observable.throw(new Error(err.json().error));
            });
    }

    postPassword(employeeNumber: string, password: string, confirmPassword: string) {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let body = { "password": password, "confirmPassword": confirmPassword };
        let options = new RequestOptions({ headers: headers });

        return this.http.put(AppComponent.url + "/api/ApplicationUserApi/" + employeeNumber, body, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert("Password change failed!");
                return Observable.throw(new Error(err.json().error));
            });
    }

    getLaborGrades(): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/LaborGrades/", options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json());
                return Observable.throw(new Error(err.json().error));
            });
    }
}