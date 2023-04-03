import { Component, OnInit } from '@angular/core';
import { UserLogin, ObtenerDatosToken } from "../../../enviroments/environment";
import { NgbCollapse } from '@ng-bootstrap/ng-bootstrap';
import { LoginService } from 'src/app/services/login.service';
import Swal from 'sweetalert2';
import { RespuestaUsuarioModel } from 'src/app/models/respuesta.model';
import { Router } from '@angular/router';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  providers: [NgbCollapse]
})
export class NavbarComponent{
  usuarioLogin = UserLogin;
  bEsWebmaster = ObtenerDatosToken().role == "WEBMASTER";
  sNombres = ObtenerDatosToken().unique_name;
  constructor(private loginService: LoginService, private router: Router) {
  }

  logout(){
    
    this.loginService.logout().subscribe({
      next: (resp: RespuestaUsuarioModel) => {       
        if(!resp.exito){
            Swal.fire({
              icon: 'warning',
              title: 'ALERTA',
              text: resp.mensaje
            });  
        }else{
          Swal.close();
          this.router.navigate(['/login'])            
        }        
      },
      error: (err: any) => {
        console.log(err)
        Swal.fire({
          icon: 'error',
          title: 'ERROR',
          text: err.error != null ? err.error.mensaje : err.message
        });   
      }
    })
  }
}
