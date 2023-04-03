import { Component } from '@angular/core';
import { UsuarioModel } from "./models/usuario.model";
import { UserLogin } from '../enviroments/environment';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  nombreComponenteActivo: string = "";;

  title = 'control-usuario-front';
  UsuarioLogin:any;
  UsuarioLoginData:any;
  bUsuarioLogueado: boolean = true;
  constructor(private router: Router){
    this.UsuarioLoginData = UserLogin;
    this.router.events.subscribe((event:any) => {
      if (event instanceof NavigationEnd) {
        // Verificar si la nueva ruta es la página de inicio de sesión
        if (event.urlAfterRedirects === '/login' || event.urlAfterRedirects === '/') {
          // Borrar el token de acceso del almacenamiento local
          localStorage.removeItem('token');
          localStorage.removeItem('perfil');
          localStorage.removeItem('usuario');
          this.UsuarioLoginData.usuario = null;
          this.UsuarioLoginData.perfil = null;
          this.UsuarioLoginData.token = null;
          this.bUsuarioLogueado = false;
        }else{
          this.bUsuarioLogueado = true;
        }
        
      }
    });    
  }
}
