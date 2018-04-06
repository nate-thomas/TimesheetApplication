import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Employee } from '../employees';
import { AppComponent } from '../../app/app.component'

@Component({
    selector: 'employeesTable',
    styleUrls: ['./employeesTable.component.css'],
    templateUrl: './employeesTable.component.html'
})
export class EmployeesTableComponent {
    employees: Employee[] = new Array();
    employee: Employee = new Employee();

    constructor(private http: Http) { }

    /* Temporary method to clear the properties in the component */

    clearProperties() {
        this.employees = new Array();
        this.employee = new Employee();
    }

    /* Functions to be called when component is loaded */

    ngOnInit() {
        this.loadEmployees();
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
                employee => this.employees = [employee]
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
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    getEmployee(employeeNumber: string): Observable<Employee> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Employees/" + employeeNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    deleteEmployee(employeeNumber: string): Observable<Employee> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.delete(AppComponent.url + "/api/Employees/" + employeeNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    postEmployee(employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.post(AppComponent.url + "/api/EmployeesI/", this.employee, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

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
}
