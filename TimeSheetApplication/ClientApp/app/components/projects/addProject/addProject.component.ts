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
    employeeNumber: string = localStorage.getItem("employeeNumber") || "";

    constructor(private http: Http, private router: Router) { }

    checkSupervisorRole() {
        if (typeof window !== 'undefined') {
            if (localStorage.getItem("role") == "Supervisor" || localStorage.getItem("role") == "Administrator") {
                return true;
            } else {
                return false;
            }
        }
    }

    checkPMRole() {
        if (typeof window !== 'undefined') {
            if (localStorage.getItem("role") == "Project Manager" || localStorage.getItem("role") == "Administrator") {
                return true;
            } else {
                return false;
            }
        }
    }

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

    validateInputNumber(input: number) {
        if (input == undefined || input == null) {
            return 'invalid-input';
        } else {
            return '';
        }
    }

    /* Subscription methods to bind the response to a property (if applicable) */

    //loadProjects() {
    //    this.getProjects()
    //        .subscribe(
    //        projects => {
    //            this.projects = projects;
    //            this.projectsChange.emit(this.projects);
    //        }
    //     );
    //}

    loadProjects() {
        alert("loadingProjectsFn");
        if (this.checkSupervisorRole()) {
   //         this.loadSupervisorProjects();
        } else if (this.checkPMRole()) {
            //this.getProjects()
            //    .subscribe(
            //    projects => {
            //        this.projects = projects;
            //        this.projectsChange.emit(this.projects);
            //    }
            //);
            
            this.loadPMProjects(this.employeeNumber);
        } else {
            alert("This must have been a mistake for you to have landed on this page");
        }
    }

    //loadSupervisorProjects() {
    //    this.getProjects()
    //        .subscribe(projects => {
    //            this.projects = projects;
    //            this.projectsChange.emit(this.projects);
    //        });
    //}

    loadPMProjects(employeeNumber: string) {
        this.getProjectsByPM(employeeNumber)
            .subscribe(projects => {
                this.projects = projects;
                this.projectsChange.emit(this.projects);
                console.log(projects);
            });
    }

    addProject() {
        this.project.statusName = "Current";
        this.project.projectManager = this.employeeNumber;

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

        //console.log(project);

        return this.http.post(AppComponent.url + "/api/Projects/", this.project, options)
            .map((res: Response) => res.json())
            .catch((err: any) => {
                console.log(err._body);
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }


    /* get projects by project manager */
    getProjectsByPM(employeeNumber: string): Observable<Project[]> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });
        console.log(employeeNumber);

        return this.http.get(AppComponent.url + "/api/Projects/pm/" + employeeNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                alert("The project name already exists!");
                //console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }
}