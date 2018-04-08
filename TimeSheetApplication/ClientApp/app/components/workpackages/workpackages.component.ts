﻿import { Component } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Workpackage } from './workpackage';
import { AddWorkpackageComponent } from './addWorkPackage/addWorkpackage.component'; 
import { Observable } from 'rxjs';
import { AppComponent } from '../app/app.component'



import 'rxjs/add/operator/map';

@Component({
    selector: 'workpackages',
    templateUrl: './workpackages.component.html',
    styleUrls: ['./workpackages.component.css']
})
export class WorkpackageComponent {
    workpackages: Workpackage[] = new Array();
    workpackage: Workpackage = new Workpackage();

    constructor(private http: Http) { }

    clearProperties() {
        this.workpackages = new Array();
        this.workpackage = new Workpackage();
    }

    /* Functions to be called when component is loaded */
    ngOnInit() {
        this.loadWorkpackages()
        this.workpackage.projectNumber = "Work Package";
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

    loadWorkpackage(workPackageNumber: string) {
        console.log("you are searching for: " + workPackageNumber);

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
        return this.http.get(AppComponent.url + "/api/workpackages/")
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    getWorkpackage(workPackageNumber: string): Observable<Workpackage> {
        return this.http.get(AppComponent.url + "/api/workpackages/" + workPackageNumber)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }


    deleteWorkpackage(workPackageNumber: string): Observable<Workpackage> {
        return this.http.delete(AppComponent.url + "api/workpackages" + workPackageNumber)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    postWorkpackage(workpackage: Workpackage): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(AppComponent.url + "api/workpackages", this.workpackage, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    putWorkpackage(workPackageNumber: string, workpackage: Workpackage): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.put(AppComponent.url + "/api/workpackages/" + workPackageNumber, this.workpackage, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    getSelected = (item: Workpackage) => {
        this.workpackage = item;
    }
}