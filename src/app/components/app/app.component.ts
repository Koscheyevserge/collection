import { Component } from '@angular/core';
     
@Component({
    selector: 'app-root',
    template: `<label>������� ���:</label>
                 <input [(ngModel)]="name" placeholder="name">
                 <h1>����� ���������� {{name}}!</h1>`
})
export class AppComponent { 
    name= '';
}