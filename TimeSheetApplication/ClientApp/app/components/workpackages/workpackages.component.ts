import { Component, EventEmitter, Output, Input, SimpleChanges } from '@angular/core';
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
    @Output()
    selectWorkpackage = new EventEmitter<Workpackage>();
    @Input()
    inputProjectNumber: string; 

    constructor(private http: Http) { }

    clearProperties() {
        this.workpackages = new Array();
        this.workpackage = new Workpackage();
    }

    /* Functions to be called when component is loaded */
    ngOnInit() {
    }

    ngOnChanges(changes: SimpleChanges) {

        if (changes['selectedProjectNumber'] !== undefined) {
            this.inputProjectNumber = changes['selectedProjectNumber'].currentValue;
        }

        if (this.inputProjectNumber !== undefined && this.inputProjectNumber != '') {
            this.loadWorkpackages();
        }
    }

    /* Subscription methods to bind the response to a property (if applicable) */

    onSelect(workpackageSearch: Workpackage) {
        //alert(projectNumber);

        console.log(workpackageSearch);

        this.getWorkpackage(workpackageSearch.projectNumber, workpackageSearch.workpackageNumber)
            .subscribe(workpackage => {
                this.workpackage = workpackage
                this.selectWorkpackage.emit(this.workpackage)
                console.log(this.workpackage)
            }); 
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
        console.log("in get");
        console.log(this.inputProjectNumber);

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
        this.selectWorkpackage.emit(this.workpackage);

    }
}