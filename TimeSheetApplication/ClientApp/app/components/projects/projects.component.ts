import { Component } from '@angular/core';

import { Employee } from '../employees/employees';

@Component({
    selector: 'projects',
    templateUrl: './projects.component.html',
    styleUrls: ['./projects.component.css']
})
export class ProjectsComponent {
    selectedProject: string = 'WebPrj128';
    selectedWorkPackage: string = 'A2';
    selectedMember: Employee;

    constructor() { }

    projectChange(event: any) {
        this.selectedProject = event.projectNumber;
    }

    workPackageChange(event: any) {
        this.selectedWorkPackage = event;
    }

    memberChange(event: any) {
        this.selectedMember = event;
    }

    validatePMandSupervisorRole() {
        if (localStorage.getItem("role") == "Project Manager" || localStorage.getItem("role") == "Supervisor" || localStorage.getItem("role") == "Administrator") {
            return true;
        } else {
            return false;
        }
    }
}