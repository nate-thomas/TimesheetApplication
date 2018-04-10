import { Component } from '@angular/core';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    constructor() { }

    checkHRAndSupervisorRole() {
        if (typeof window !== 'undefined') {
            if (localStorage.getItem("role") == "Human Resources" || localStorage.getItem("role") == "Supervisor" || localStorage.getItem("role") == "Administrator") {
                return true;
            } else {
                return false;
            }
        }
    }

    checkPMAndSupervisorRole() {
        if (typeof window !== 'undefined') {
            if (localStorage.getItem("role") == "Project Manager" || localStorage.getItem("role") == "Supervisor" || localStorage.getItem("role") == "Administrator") {
                return true;
            } else {
                return false;
            }
        }
    }
}
