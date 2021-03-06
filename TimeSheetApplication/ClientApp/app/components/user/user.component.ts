﻿import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from '../employees/employees';
import { AppComponent } from '../app/app.component'

@Component({
    selector: 'user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent {
    employee: Employee = new Employee();

    constructor(private http: Http, private router: Router) { }

    /* Functions to be called when component is loaded */

    ngOnInit() {
        this.setEmployee();
    }

    /* Utility methods */

    setEmployee() {
        this.employee.employeeNumber = localStorage.getItem("employeeNumber") || "";
        this.employee.firstName = localStorage.getItem("firstName") || "";
        this.employee.lastName = localStorage.getItem("lastName") || "";
        this.employee.employeeIntials = localStorage.getItem("employeeIntials") || "";
    }

    validateInput(input: string) {
        if (input == undefined || input == null || input == "") {
            return 'invalid-input';
        } else {
            return '';
        }
    }

    validatePasswords(password: string, confirmPassword: string) {
        if (password == confirmPassword) {
            if (password == "") {
                alert("Password cannot be empty!");
                return false;
            } else {
                return true;
            }
        } else {
            alert("Passwords do not match!");
            return false;
        }
    }

    /* Subscription methods to bind the response to a property (if applicable) */

    updateEmployee() {
        if (this.employee.firstName == "") {
            this.employee.firstName = localStorage.getItem("firstName") || "";
        }

        if (this.employee.lastName == "") {
            this.employee.lastName = localStorage.getItem("lastName") || "";
        }

        if (this.employee.employeeIntials == "") {
            this.employee.employeeIntials = localStorage.getItem("employeeIntials") || "";
        }

        this.employee.grade = localStorage.getItem("grade") || "";
        this.employee.supervisorNumber = localStorage.getItem("supervisorNumber") || "";
        this.employee.role = localStorage.getItem("role") || "";

        this.putEmployee(this.employee.employeeNumber, this.employee)
            .subscribe(res => {
                localStorage.setItem("firstName", this.employee.firstName);
                localStorage.setItem("employeeIntials", this.employee.employeeIntials);
                localStorage.setItem("lastName", this.employee.lastName);
                alert("Employee updated!");
            });        
    }

    updatePassword() {
        if (this.validatePasswords(this.employee.password, this.employee.confirmPassword)) {
            this.putPassword(this.employee.employeeNumber, this.employee.password, this.employee.confirmPassword)
                .subscribe(res => alert("Password changed!"));
        }
    }

    /* CRUD methods to make RESTful calls to the API */

    putEmployee(employeeNumber: string, employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.put(AppComponent.url + "/api/Employees/" + employeeNumber, this.employee, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    putPassword(employeeNumber: string, password: string, confirmPassword: string) {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let body = { "Password": password, "ConfirmPassword": confirmPassword };
        let options = new RequestOptions({ headers: headers });

        return this.http.put(AppComponent.url + "/api/ApplicationUser/" + employeeNumber, body, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert("Password change failed!");
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }
}