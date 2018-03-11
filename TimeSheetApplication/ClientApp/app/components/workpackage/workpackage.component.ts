import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Workpackage } from './workpackage';

@Component({
    selector: 'workpackage',
    styleUrls: ['./workpackage.component.css'],
    templateUrl: './workpackage.component.html'
})
export class WorkpackageComponent {
    url: string = "http://localhost:51050";

    workpackages: Workpackage[] = new Array();
    workpackage: Workpackage = new Workpackage();

    constructor(private http: Http) { }

    clearProperties() {
        this.workpackages = new Array();
        this.workpackage = new Workpackage();
    }

    /* Functions to be called when component is loaded */
    ngOnInit() {
        this.loadWorkpackages();
    }


    /* Subscription methods to bind the response to a property (if applicable) */
    loadWorkpackages() {
        this.getWorkpackages()
            .subscribe(
                workpackages => this.workpackages = workpackages,
                errors => {
                    console.log(errors)
                }
            );
    }

    loadWorkpackge(workPackageNumber: string) {
        this.getWorkpackage(workPackageNumber)
            .subscribe(
                workpackage => this.workpackage = workpackage,
                    errors => {
                        console.log(errors)
                    }
            );
    }

    removeEmployee(workPackageNumber: string) {
        this.deleteWorkpackage(workPackageNumber)
            .subscribe(res => console.log("Response: " + res));
    }

    addEmployee() {
        this.postWorkpackage(this.workpackage)
            .subscribe(res => console.log("Response: " + res));
    }

    updateEmployee() {
        this.putWorkpackage(this.workpackage.workpackageNumber, this.workpackage)
            .subscribe(res => console.log("Response: " + res));
    }

    /* CRUD methods to make RESTful calls to the API */
    getWorkpackages(): Observable<Workpackage[]> {
        return this.http.get(this.url + "/api/workpackages/")
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    getWorkpackage(workPackageNumber: string): Observable<Workpackage> {
        return this.http.get(this.url + "/api/workpackages/" + workPackageNumber)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    deleteWorkpackage(workPackageNumber: string): Observable<Workpackage> {
        return this.http.delete(this.url + "api/workpackages" + workPackageNumber)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    postWorkpackage(workpackage: Workpackage): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.url + "api/workpackages", this.workpackage, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    putWorkpackage(workPackageNumber: string, workpackage: Workpackage): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.put(this.url + "/api/workpackages/" + workPackageNumber, this.workpackage, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }
}