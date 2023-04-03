import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { UsuarioModel } from '../models/usuario.model';
import { URI_API, ObtenerDatosToken } from 'src/enviroments/environment';
@Injectable({
  providedIn: 'root'
})
export class LoginService {


  constructor(private http: HttpClient ) {
  }

  login(usuario: UsuarioModel ){  
    return this.http.post(URI_API + "Login/InicioSesion", usuario);
  }

  logout(){  
    console.log(this.obtenerHeader());
    return this.http.put(URI_API + "Login/RegistrarCierreLogin/" + ObtenerDatosToken().iIdUsuario, null, this.obtenerHeader());
  }
  private obtenerHeader(){
    const token = sessionStorage.getItem('token');  
    let headers = new HttpHeaders({
    'Authorization': `Bearer ${token}` });
    return { headers: headers };
  }
}
