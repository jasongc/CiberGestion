import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import Swal from 'sweetalert2';
import { UserLogin, ObtenerDatosToken } from '../../enviroments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard  {
  constructor(private router: Router){  }
   canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) : boolean  {
    let datosUsuario = ObtenerDatosToken();
    console.log(datosUsuario);
    if(datosUsuario.email != null){
      if ((datosUsuario.role == "WEBMASTER" && (state.url == "/home" || state.url == "/cambiar-contrasenia"))){
        return true;
      }
      else if((datosUsuario.role == "INVITADO" && state.url == "/home")){
        return true;
      }else{
        Swal.fire({
          icon: 'warning',
          title: 'UPS!',
          text: 'Detectamos que su perfil no pertenece a la vista que intenta ingresar.'
        }).finally(
          () => this.router.navigate(['/'])
        );
        return false;
      }
    }else{
      
      Swal.fire({
        icon: 'warning',
        title: 'UPS!',
        text: 'Detectamos que no tiene una sesión activa, por favor, incie sesión para continuar.'
      }).finally(
        () => this.router.navigate(['/'])
      );
      
      return false

    }
  }
  
}
