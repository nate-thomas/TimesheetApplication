import { Component } from '@angular/core';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    constructor() { }

    checkRole(role: string) {
        if (typeof window !== 'undefined') {
            if (localStorage.getItem("role") == role) {
                return true;
            } else {
                return false;
            }
        }
    }
}
