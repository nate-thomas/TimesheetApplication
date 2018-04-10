import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Component, Output, EventEmitter } from '@angular/core';
import { Project } from '../projects';
import { Observable } from 'rxjs/Observable';
import { AppComponent } from '../../app/app.component';
import { Router } from '@angular/router';


@Component({
    selector: 'projectsTable',
    styleUrls: ['./projectsTable.component.css'],
    templateUrl: './projectsTable.component.html'
})
export class ProjectsTableComponent {
    project: Project = new Project();
    projects: Project[] = new Array();
    employeeNumber: string = localStorage.getItem("employeeNumber") || "";
    @Output()
    selectProject = new EventEmitter<Project>();

    constructor(private http: Http, private router: Router) { }

    /* Check Roles */

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


    /* Functions to be called when component is loaded */

    ngOnInit() {
        if (this.checkSupervisorRole()) {
            this.loadSupervisorProjects();
            //alert("Hi Supervisor");
        } else if (this.checkPMRole()) {
           // alert("SHELY: this is emp num: " + this.employeeNumber)
            this.loadPMProjects(this.employeeNumber);
            //alert("Hi Project Manager");
        } else {
            alert("This must have been a mistake for you to have landed on this page");
        }
    }


    setProjects(projects: Project[]) {
        this.projects = projects;
    }


    initializeAddComponent() {
        this.project = new Project();
    }

    initializeUpdateComponent(project: Project) {
        this.project = project;
    }


    /* Subscription methods to bind the response to a property (if applicable) */
    //loadProjects() {
    //    this.getProjects()
    //        .subscribe(projects => this.projects = projects);
        
    //}

    loadSupervisorProjects() {
    this.getProjects()
        .subscribe(projects => this.projects = projects);

    }

    loadPMProjects(employeeNumber: string) {
        console.log("hi this is" + this.project);
        alert("empNum is: " + employeeNumber)
        this.getProjectsByPM(employeeNumber)
            .subscribe(projects => {
                //This part is not working
                alert("loadPM: " + employeeNumber)
                this.projects = projects
                //alert(this.employeeNumber);
            });

    }

    /* Output selected Project Object in table */

    onSelect(projectNumber: string) {
        //alert(projectNumber);
        this.getProjectPN(projectNumber)
            .subscribe(project => {
                this.project = project
                this.selectProject.emit(this.project);
                console.log(this.project)
            });
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

    /* Get project by project number */
    getProjectPN(projectNumber: string): Observable<Project> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.get(AppComponent.url + "/api/Projects/" + projectNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }


    /* get projects by project manager */
    getProjectsByPM(employeeNumber: string): Observable<Project[]> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });
        console.log(employeeNumber);

        return this.http.get(AppComponent.url + "/api/Projects/" + employeeNumber, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }


    /* Archiving */

    archiveProject(index: string) {
        this.project.statusName = "Archived";
        this.project.projectNumber = index;

        this.putProject(index, this.project)
            .subscribe(res => {
                //alert("Project updated!")
                this.ngOnInit();
            });
      
        console.log('archived project');
    }
    
    putProject(projectNumber: string, project: Project): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('access_token') })
        let options = new RequestOptions({ headers: headers });

        return this.http.put(AppComponent.url + "/api/Projects/" + projectNumber, this.project, options)
            .map((res: Response) => res.json())
            .catch((err: Response) => {
                console.log(JSON.stringify(err));
                return Observable.throw(new Error(JSON.stringify(err)));
            });
    }
}