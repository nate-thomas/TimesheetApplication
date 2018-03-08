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
import { TimesheetsTableComponent } from './components/timesheetsTable/timesheetsTable.component';
import { AddEmployeeComponent } from './components/employees/addEmployee/AddEmployee.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        TimesheetsComponent,
        ProjectsComponent,
        EmployeesComponent,
        AddEmployeeComponent,
        AdmintasksComponent,
        UserComponent,
		LoginComponent,
        TimesheetsTableComponent,

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
            { path: 'employees', component: EmployeesComponent },
            { path: 'addEmployee', component: AddEmployeeComponent },
            { path: 'admintasks', component: AdmintasksComponent },
            { path: 'user', component: UserComponent },
            { path: '**', redirectTo: 'login' }
        ])
    ]
})
export class AppModuleShared {
}
