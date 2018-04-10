import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Project } from '../projects';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AppComponent } from '../../app/app.component';


@Component({
    selector: 'addProject',
    styleUrls: ['./addProject.component.css'],
    templateUrl: './addProject.component.html'
})
export class AddProjectComponent {
    @Input()
    project: Project;
    @Input()
    projects: Project[];
    @Output()
    projectsChange = new EventEmitter<Project[]>();

    constructor(private http: Http, private router: Router) { }

    ngOnInit() {

    }

    /* Utility methods */

    validateInput(input: string) {
        if (input == undefined || input == null || input == "") {
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

    addProject() {
        this.project.statusName = "Current";

        if (this.project.projectNumber &&
            this.project.description &&
            this.project.budget) {

            this.postProject(this.project)
                .subscribe(res => {
                    alert("Project added!");
                    this.ngOnInit();
                });

        } else {
            alert("All fields are required!");
        }
        
    }


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

    postProject(project: Project): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        console.log(project);

        return this.http.post(AppComponent.url + "/api/Projects/", this.project, options)
            .map((res: Response) => res.json())
            .catch((err: any) => {
                console.log(err._body);
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }
}