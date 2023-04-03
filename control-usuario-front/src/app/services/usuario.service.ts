import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { UsuarioModel } from "../models/usuario.model";
import { ObtenerDatosToken, URI_API } from 'src/enviroments/environment';
@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  constructor(private http: HttpClient ) {
  }
  actualizarContrasenia(usuario: UsuarioModel ){  
    let datosToken = ObtenerDatosToken();
    return this.http.put(URI_API + "Usuario/CambiarContrasenia/" + datosToken.iIdUsuario, usuario, this.obtenerHeader());
  }
  private obtenerHeader(){
    const token = sessionStorage.getItem('token');  
    let headers = new HttpHeaders({
    'Authorization': `Bearer ${token}` });
    return { headers: headers };
  }

}
