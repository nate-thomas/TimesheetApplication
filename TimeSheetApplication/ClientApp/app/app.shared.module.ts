import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { LoginComponent } from './components/login/login.component';
import { TimesheetsComponent } from './components/timesheets/timesheets.component';
import { ProjectsComponent } from './components/projects/projects.component';
import { EmployeesComponent } from './components/employees/employees.component';
import { AdmintasksComponent } from './components/admintasks/admintasks.component';
import { UserComponent } from './components/user/user.component';
import { TimesheetsTableComponent } from './components/timesheets/timesheetsTable/timesheetsTable.component';
import { AddEmployeeComponent } from './components/employees/addEmployee/addEmployee.component';
import { ProjectsTableComponent } from './components/projects/projectsTable/projectsTable.component';
import { AddProjectComponent } from './components/projects/addProject/addProject.component';
import { EmployeesTableComponent } from './components/employees/employeesTable/employeesTable.component';
import { WorkPackagesComponent } from './components/workpackages/workpackages.component';
import { ManageProjectsComponent } from './components/projects/manageProjects/manageProjects.component';
import { ViewTimesheetsComponent } from './components/timesheets/viewTimesheets/viewTimesheets.component'

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        TimesheetsComponent,
        ProjectsComponent,
        EmployeesComponent,
        EmployeesTableComponent,
        AddEmployeeComponent,
        AdmintasksComponent,
        UserComponent,
		LoginComponent,
        TimesheetsTableComponent,
        ProjectsTableComponent,
        AddProjectComponent,
        ManageProjectsComponent,
        WorkPackagesComponent,
        ViewTimesheetsComponent

    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'login', pathMatch: 'full' },
            { path: 'login', component: LoginComponent },
            { path: 'timesheets', component: TimesheetsComponent },
            { path: 'projects', component: ProjectsComponent },
            { path: 'manage-projects', component: ManageProjectsComponent },
            { path: 'employees', component: EmployeesComponent },
            { path: 'admintasks', component: AdmintasksComponent },
            //{ path: 'addTimesheet', component: TimesheetsTableComponent },
            //{ path: 'addEmployee', component: AddEmployeeComponent },
            //{ path: 'addProject', component: AddProjectComponent },
            { path: 'user', component: UserComponent },
            { path: '**', redirectTo: 'login' }
        ])
    ]
})
export class AppModuleShared {
}
