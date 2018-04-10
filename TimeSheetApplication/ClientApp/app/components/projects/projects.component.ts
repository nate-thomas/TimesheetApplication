import { Component } from '@angular/core';

@Component({
    selector: 'projects',
    templateUrl: './projects.component.html',
    styleUrls: ['./projects.component.css']
})
export class ProjectsComponent {
    constructor() { }

    validatePMandSupervisorRole() {
        if (localStorage.getItem("role") == "Project Manager" || localStorage.getItem("role") == "Supervisor" || localStorage.getItem("role") == "Administrator") {
            return true;
        } else {
            return false;
        }
    }
}