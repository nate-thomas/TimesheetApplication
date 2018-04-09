import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from '../employees';
import { AppComponent } from '../../app/app.component'

@Component({
    selector: 'UpdateEmployee',
    styleUrls: ['./updateEmployee.component.css'],
    templateUrl: './updateEmployee.component.html'
})
export class UpdateEmployeeComponent {
    
}