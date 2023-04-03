import { Component } from '@angular/core';
import { ObtenerDatosToken } from "../../../enviroments/environment";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {  
  userLogin = ObtenerDatosToken();  
}
