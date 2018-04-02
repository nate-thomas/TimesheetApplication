import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Employee } from '../employees';

@Component({
    selector: 'employeesTable',
    styleUrls: ['./employeesTable.component.css'],
    templateUrl: './employeesTable.component.html'
})
export class EmployeesTableComponent {
    url: string = "http://localhost:58911";

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
                employee => this.employee = employee
            );
    }

    removeEmployee(employeeNumber: string) {
        this.deleteEmployee(employeeNumber)
            .subscribe(res => console.log("Response: " + res));
    }

    addEmployee() {
        this.postEmployee(this.employee)
            .subscribe(res => console.log("Response: " + res));
    }

    updateEmployee() {
        this.putEmployee(this.employee.employeeNumber, this.employee)
            .subscribe(res => console.log("Response: " + res));
    }

    /* CRUD methods to make RESTful calls to the API */

    getEmployees(): Observable<Employee[]> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(this.url + "/api/Employees/", options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }

    getEmployee(employeeNumber: string): Observable<Employee> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(this.url + "/api/Employees/" + employeeNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }

    deleteEmployee(employeeNumber: string): Observable<Employee> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.delete(this.url + "/api/Employees/" + employeeNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }

    postEmployee(employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.url + "/api/EmployeesI/", this.employee, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }

    putEmployee(employeeNumber: string, employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.put(this.url + "/api/Employees/" + employeeNumber, this.employee, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert(err.json().error_description);
                return Observable.throw(new Error(err.json().error));
            });
    }
}
