import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Employee } from './employees';

@Component({
    selector: 'employees',
    templateUrl: './employees.component.html'
})
export class EmployeesComponent {
    employees: Employee[] = new Array();
    employee: Employee = new Employee();

    constructor(private http: Http) { }

    /* Temporary method to clear the properties in the component */

    clearProperties() {
        this.employees = new Array();
        this.employee = new Employee();
    }

    /* Subscription methods to bind the response to a property (if applicable) */

    loadEmployees() {
        this.getEmployees()
            .subscribe(
                employees => this.employees = employees,
                errors => {
                    console.log(errors)
                }
            );
    }

    loadEmployee(employeeNumber: string) {
        this.getEmployee(employeeNumber)
            .subscribe(
                employee => this.employee = employee,
                errors => {
                    console.log(errors)
                }
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
        return this.http.get("http://localhost:61150/api/EmployeesAPI/")
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    getEmployee(employeeNumber: string): Observable<Employee> {
        return this.http.get("http://localhost:61150/api/EmployeesAPI/" + employeeNumber)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    deleteEmployee(employeeNumber: string): Observable<Employee> {
        return this.http.delete("http://localhost:61150/api/EmployeesAPI/" + employeeNumber)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    postEmployee(employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post("http://localhost:61150/api/EmployeesAPI/", this.employee, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    putEmployee(employeeNumber: string, employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.put("http://localhost:61150/api/EmployeesAPI/" + employeeNumber, this.employee, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }
}