import { Component } from '@angular/core';
import { Workpackage } from './workpackage';

@Component({
    selector: 'workpackage',
    styleUrls: ['./workpackage.component.css'],
    templateUrl: './workpackage.component.html'
})
export class WorkpackageComponent {

    workpackages: Workpackage[] = new Array();

}