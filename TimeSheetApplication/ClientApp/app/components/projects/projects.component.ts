import { Component } from '@angular/core';

import { Employee } from '../employees/employees';

@Component({
    selector: 'projects',
    templateUrl: './projects.component.html',
    styleUrls: ['./projects.component.css']
})
export class ProjectsComponent {
    selectedProject: string;
    selectedWorkPackage: string;
    selectedMember: Employee;

    projectChange(event: any) {
        this.selectedProject = event;
    }

    workPackageChange(event: any) {
        this.selectedWorkPackage = event;
    }

    memberChange(event: any) {
        this.selectedMember = event;
    }
}