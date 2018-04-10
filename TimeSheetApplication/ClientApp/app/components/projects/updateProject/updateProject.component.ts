import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import { Input, Output, EventEmitter } from '@angular/core'

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from '../../app/app.component'
import { Project } from '../projects';

@Component({
    selector: 'UpdateProject',
    styleUrls: ['./updateProject.component.css'],
    templateUrl: './updateProject.component.html'
})
export class UpdateProjectComponent {
    @Input()
    project: Project;
    @Input()
    projects: Project[];
    @Output()
    projectsChange = new EventEmitter<Project[]>();

    //statuses: Object[] = new Array();

    constructor(private http: Http, private router: Router) { }

    /* Functions to be called when component is loaded */

    ngOnInit() {
        //this.loadStatuses();
    }

    /* Utility methods */

    validateInput(input: string) {
        if (input == undefined || input == null || input == "") {
            return 'invalid-input';
        } else {
            return '';
        }
    }

    validateInputNumber(input: number) {
        if (input == undefined || input == null) {
            return 'invalid-input';
        } else {
            return '';
        }
    }

    /* Subscription methods to bind the response to a property (if applicable) */

    loadProjects() {
        this.getProjects()
            .subscribe(
                projects => {
                    this.projects = projects;
                    this.projectsChange.emit(this.projects);
                }
            );
    }

    updateProject() {
        this.project.projectManager = "";
        this.project.statusName = "Current";

        if (this.project.projectNumber &&
            this.project.description &&
            this.project.budget) {

            this.putProject(this.project.projectNumber, this.project)
                .subscribe(res => {
                    alert("Project Updated!");
                });

        } else {
            alert("All fields are required!");
        }

    }

    //loadStatuses() {
    //    this.getStatuses()
    //        .subscribe(
    //        (grades: any) => this.statuses = statuses
    //        );
    //}


    /* CRUD methods to make RESTful calls to the API */

    getProjects(): Observable<Project[]> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Projects/", options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    putProject(projectNumber: string, project: Project): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });
        alert(projectNumber);

        return this.http.put(AppComponent.url + "/api/Projects/" + projectNumber, this.project, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

    //getStatuses(): Observable<Response> {
    //    let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
    //    let options = new RequestOptions({ headers: headers });

    //    return this.http.get(AppComponent.url + "/api/Statuses/", options)
    //        .map((res: Response) => res.json())
    //        .catch((err: Response) => {
    //            console.log(JSON.stringify(err));
    //            return Observable.throw(new Error(JSON.stringify(err)));
    //        });
    //}
    
}