import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { TimesheetsComponent } from './components/timesheets/timesheets';
import { ProjectsComponent } from './components/projects/projects.component';
import { EmployeesComponent } from './components/employees/employees.component';
import { AdmintasksComponent } from './components/admintasks/admintasks.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        TimesheetsComponent,
        ProjectsComponent,
        EmployeesComponent,
        AdmintasksComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'timesheets', component: TimesheetsComponent },            { path: 'timesheets', component: TimesheetsComponent },
            { path: 'projects', component: ProjectsComponent },
            { path: 'admintasks', component: AdmintasksComponent },
            { path: 'employees', component: EmployeesComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
