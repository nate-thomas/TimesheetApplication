import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from '../employees';
import { LaborGrade } from '../laborGrades'
import { AppComponent } from '../../app/app.component'

@Component({
    selector: 'AddEmployee',
    styleUrls: ['./addEmployee.component.css'],
    templateUrl: './addEmployee.component.html'
})
export class AddEmployeeComponent {
    employee: Employee = new Employee();
    laborGrades: LaborGrade[] = new Array();
    supervisors: Employee[] = new Array();

    constructor(private http: Http, private router: Router) { }

    /* Temporary method to clear the properties in the component */

    clearProperties() {
        this.employee = new Employee();

        this.router.navigateByUrl('/employees');
    }

    /* Functions to be called when component is loaded */

    ngOnInit() {
        this.employee = new Employee();
        this.loadLaborGrades();
        this.loadSupervisors();
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

    removeEmployee(employeeNumber: string) {
        this.deleteEmployee(employeeNumber)
            .subscribe(res => alert("Removal successful!"));
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

            this.postEmployee(this.employee)
                .subscribe(res => alert("Employee added!"));
        } else {
            alert("All fields are required!");
        }
        
    }

    updateEmployee() {
        this.putEmployee(this.employee.employeeNumber, this.employee)
            .subscribe(res => alert("Employee updated!"));
    }

    loadLaborGrades() {
        this.getLaborGrades()
            .subscribe(
                (laborGrades: any) => this.laborGrades = laborGrades
            );
    }

    loadSupervisors() {
        this.getSupervisors()
            .subscribe(
                (supervisors: any) => { this.supervisors = supervisors; console.log(JSON.stringify(supervisors)); }
            );
    }

    loadUserRoles() {

    }

    /* CRUD methods to make RESTful calls to the API */

    deleteEmployee(employeeNumber: string): Observable<Employee> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.delete(AppComponent.url + "/api/Employees/" + employeeNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(err.json().error));
            });
    }

    postEmployee(employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.post(AppComponent.url + "/api/Employees/", this.employee, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(err.json().error));
            });
    }

    putEmployee(employeeNumber: string, employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.put(AppComponent.url + "/api/Employees/" + employeeNumber, this.employee, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(err.json().error));
            });
    }

    getLaborGrades(): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/LaborGrades/", options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(err.json().error));
            });
    }

    getSupervisors(): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/UsersInRoles/" + "Supervisor", options)
            .map((res: Response) => { res.json(); console.log(res.json()) })
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(err.json().error));
            });
    }
}