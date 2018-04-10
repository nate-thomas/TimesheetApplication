import { Component } from '@angular/core';

import { Employee } from '../employees/employees';
import { Workpackage } from '../workpackages/workpackage';

@Component({
    selector: 'projects',
    templateUrl: './projects.component.html',
    styleUrls: ['./projects.component.css']
})
export class ProjectsComponent {
    selectedProject: string = 'WebPrj128';
    selectedWorkPackage: string = 'A2';
    selectedMember: Employee;
    selectedWorkpackage: Workpackage;

    constructor() { }

    projectChange(event: any) {
        this.selectedProject = event;
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

    workpackagesChange(workpackage: Workpackage) {
        this.selectedWorkpackage = workpackage;
    }
}