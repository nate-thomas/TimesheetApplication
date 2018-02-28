import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Employee } from './employee';

@Component({
    selector: 'employees',
    templateUrl: './employees.component.html'
})
export class EmployeesComponent {
    employees: Employee[];
    employee: Employee;

    constructor(private http: Http) { }

    ngOnInit() {
        this.loadEmployees();
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

    loadEmployee(employeeNumber: number) {
        this.getEmployee(employeeNumber)
            .subscribe(
            employee => this.employee = employee,
            errors => {
                console.log(errors)
            }
            );
    }

    removeEmployee(employeeNumber: number) {
        this.deleteEmployee(employeeNumber)
            .subscribe(res => console.log("Response: " + res));
    }

    addEmployee(employee: Employee) {
        console.log(employee);

        this.postEmployee(employee)
            .subscribe(res => console.log("Response: " + res));
    }

    updateEmployee(employee: Employee) {
        this.putEmployee(employee.employeeNumber, employee)
            .subscribe(res => console.log("Response: " + res));
    }

    /* CRUD methods to make RESTful calls to the API */

    getEmployees(): Observable<Employee[]> {
        return this.http.get("http://localhost:61150/api/EmployeesAPI/")
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    getEmployee(employeeNumber: number): Observable<Employee> {
        return this.http.get("http://localhost:61150/api/EmployeesAPI/" + employeeNumber)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    deleteEmployee(employeeNumber: number): Observable<Employee> {
        return this.http.delete("http://localhost:61150/api/EmployeesAPI/" + employeeNumber)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    postEmployee(employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let body = { employee: this.employee };
        let options = new RequestOptions({ headers: headers });

        return this.http.post("http://localhost:61150/api/EmployeesAPI/", body, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    putEmployee(employeeNumber: number, employee: Employee): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let body = { employee: this.employee };
        let options = new RequestOptions({ headers: headers });

        return this.http.put("http://localhost:61150/api/EmployeesAPI/" + employeeNumber, body, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }
}