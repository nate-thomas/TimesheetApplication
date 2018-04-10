import { Component } from '@angular/core';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    public static url: string = "http://timesheetapplication4.azurewebsites.net/";
=======
    public static url: string = "http://localhost:2216";
>>>>>>> d215bf0488c55ed10b0cbdfe52a03a4ec9ca18a7
}
