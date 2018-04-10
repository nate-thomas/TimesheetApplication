import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from '../../employees/employees';
import { AppComponent } from '../../app/app.component'
import { Workpackage } from '../workpackage';
import { Project } from '../../projects/projects';


@Component({
    selector: 'AddWorkpackage',
    templateUrl: './addWorkpackage.component.html',
    styleUrls: ['./addWorkpackage.component.css']
})
export class AddWorkpackageComponent {

    workpackages: Workpackage[] = new Array();
    workpackage: Workpackage = new Workpackage();
    workpackageSubmission: Workpackage = new Workpackage();
    projects: Project[] = new Array();


    constructor(private http: Http, private router: Router) { }

    /* Temporary method to clear the properties in the component */

    clearProperties() {
        this.workpackages = new Array();
        this.workpackage = new Workpackage();
        this.projects = new Array();
    }

    ngOnInit() {
        this.loadProjects();
    }

    /* Subscription methods to bind the response to a property (if applicable) */
    loadProjects() {
        this.getProjects()
            .subscribe(
            projects => this.projects = projects,
            errors => {
                console.log(errors)
            }
            );
    }

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
        this.getWorkpackage(workPackageNumber)
            .subscribe(
            workpackage => this.workpackage = workpackage,
            errors => {
                console.log(errors)
            }
            );
    }

    addWorkpackage() {
        this.postWorkpackage(this.workpackageSubmission)
            .subscribe(res => console.log("Response: " + res));
    }

    updateWorkpackage() {
        this.putWorkpackage(this.workpackage.workpackageNumber, this.workpackage)
            .subscribe(res => console.log("Response: " + res));
    }

    /* CRUD methods to make RESTful calls to the API */

    getProjects(): Observable<Project[]> {
        //let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        //let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Projects/")
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });

    }


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

    postWorkpackage(workpackage: Workpackage): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(AppComponent.url + "/api/workpackages", this.workpackageSubmission, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error || "Server Error"));
    }

    putWorkpackage(workPackageNumber: string, workpackage: Workpackage): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.put(AppComponent.url + "/api/workpackages/" + workPackageNumber, this.workpackage, options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || "Server Error"));
    }
    /* Subscription methods to bind the response to a property (if applicable) */
}