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
    supervisors: Employee[] = new Array();
    employees: Employee[] = new Array();
    employee: Employee = new Employee();

    constructor(private http: Http) { }

    /* Functions to be called when component is loaded */

    ngOnInit() {
        this.loadEmployees();
    }

    /* Utility methods */

    checkSupervisorNumber(employee: Employee) {
        if (localStorage.getItem("role") == "Supervisor") {
            if (localStorage.getItem("employeeNumber") == employee.supervisorNumber) {
                return true;
            } else {
                return false;
            }
        } else {
            return true;
        }
    }

    checkIfOwnEmployee(employee: Employee) {
        if (localStorage.getItem("employeeNumber") == employee.employeeNumber) {
            return false
        } else {
            return true;
        }
    }

    validateHRRole() {
        if (localStorage.getItem("role") == "Human Resources" || localStorage.getItem("role") == "Administrator") {
            return true;
        } else {
            return false;
        }
    }

    validateHRAndSupervisorRole() {
        if (localStorage.getItem("role") == "Human Resources" || localStorage.getItem("role") == "Administrator" || localStorage.getItem("role") == "Supervisor") {
            return true;
        } else {
            return false;
        }
    }

    setEmployees(employees: Employee[]) {
        this.employees = employees;
    }

    initializeAddComponent() {
        this.employee = new Employee();
        this.loadSupervisors();
    }

    initializeUpdateComponent(employee: Employee) {
        this.employee = employee;
        this.loadSupervisors();
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

    loadSupervisors() {
        this.getSupervisors()
            .subscribe(
            (supervisors: any) => this.supervisors = supervisors
            );
    }

    removeEmployee(employeeNumber: string) {
        this.deleteEmployee(employeeNumber)
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
