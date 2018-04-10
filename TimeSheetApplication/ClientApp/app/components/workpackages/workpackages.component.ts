import { Component } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Workpackage } from './workpackage';
import { DeleteWorkpackageComponent } from './deleteWorkPackage/deleteWorkpackage.component'; 
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

    loadWorkpackage(projectNumber: string, workPackageNumber: string) {
        console.log("you are searching for: " + projectNumber);
        console.log("you are searching for: " + workPackageNumber);

        this.workpackages = new Array();

        this.getWorkpackage(projectNumber, workPackageNumber)
            .subscribe(
            workpackage => this.workpackages[0] = workpackage,
            errors => {
                console.log(errors)
            }
        );


    }

    /* CRUD methods to make RESTful calls to the API */
    getWorkpackages(): Observable<Workpackage[]> {
        return this.http.get(AppComponent.url + "/api/workpackages/")
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }

    getWorkpackage(projectNumber: string, workPackageNumber: string): Observable<Workpackage> {
        return this.http.get(AppComponent.url + "/api/workpackages/" + projectNumber + "/" + workPackageNumber)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }


    getSelected = (item: Workpackage) => {

        console.log(item);
        this.workpackage = item;
    }
}