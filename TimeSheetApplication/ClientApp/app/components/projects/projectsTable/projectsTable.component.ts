import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Component } from '@angular/core';
import { Project } from '../projects';
import { Observable } from 'rxjs/Observable';
import { AppComponent } from '../../app/app.component';

@Component({
    selector: 'projectsTable',
    styleUrls: ['./projectsTable.component.css'],
    templateUrl: './projectsTable.component.html'
})
export class ProjectsTableComponent {
    project: Project = new Project();
    projects: Project[] = new Array();

    constructor(private http: Http) { }


     /* Functions to be called when component is loaded */
    
    ngOnInit() {
        this.loadProjects();
    }

    /* Subscription methods to bind the response to a property (if applicable) */
    loadProjects() {
        this.getProjects()
            .subscribe(
                projects => this.projects = projects
        );

        console.log('It works here');
    }

    loadProject(employeeNumber: string) {
        this.getProject(employeeNumber)
            .subscribe(
                project => this.projects = [project]
        );
    }


    /* CRUD methods to make RESTful calls to the API */

    getProjects(): Observable<Project[]> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        console.log('It works here3');

        return this.http.get(AppComponent.url + "/api/Projects/", options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });   
    }

    getProject(employeeNumber: string): Observable<Project> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Projects/" + employeeNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }

}