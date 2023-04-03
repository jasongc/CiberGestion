import { Component } from '@angular/core';
import { ObtenerDatosToken } from "../../../enviroments/environment";

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {
  Year:number = new Date().getFullYear();
  FechaAcceso: any = ObtenerDatosToken().fecha_acceso;
  constructor(){
    console.log(ObtenerDatosToken());
  }
}
