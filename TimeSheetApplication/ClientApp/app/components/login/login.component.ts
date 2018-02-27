import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';

import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})
export class LoginComponent {
    username: string;
    password: string;

    constructor(private http: Http, private router: Router) {}

    login() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let body = { username: this.username, password: this.password };
        let options = new RequestOptions({ headers: headers });

        // Make a POST request to /api/login/ with username and password contained in an object
        return this.http.post("http://localhost:54255/api/login", body, options)
            .map(response => {
                // JSONify the repsonse object
                let user = response.json();

                // If response object contains a non-null object ...
                if (user.employeeId && user.token) {
                    // Store currentUser to localStorage
                    localStorage.setItem('currentUser', user.employeeId);

                    // Navigate to home page
                    this.router.navigateByUrl('/home');
                } else {
                    // Insert here an alert that login has failed in UI
                }
            });
    }

    logout() {
        this.username = "";
        this.password = "";

        localStorage.setItem('currentUser', "");
    }

}
