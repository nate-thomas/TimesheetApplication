import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
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
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
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
            { path: 'home', component: HomeComponent },
            { path: 'timesheets', component: TimesheetsComponent },
            { path: 'projects', component: ProjectsComponent },
            { path: 'employees', component: EmployeesComponent },
            { path: 'addEmployee', component: AddEmployeeComponent },
            { path: 'admintasks', component: AdmintasksComponent },
            { path: 'user', component: UserComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'login' }
        ])
    ]
})
export class AppModuleShared {
}
