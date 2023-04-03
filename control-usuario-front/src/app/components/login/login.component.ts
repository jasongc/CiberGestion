import { Component, OnInit, ɵConsole } from '@angular/core';
import {FormGroup,FormControl, Validators} from '@angular/forms';
import { UsuarioModel } from "../../models/usuario.model";
import { UserLogin } from "../../../enviroments/environment";
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { RespuestaUsuarioModel } from '../../models/respuesta.model';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  usuario = new UsuarioModel();
  forma:FormGroup;
  userLogin = UserLogin;  
  constructor(private loginService: LoginService, private router: Router) { 
    sessionStorage.removeItem("token");
    this.userLogin._id = null;
    this.userLogin.usuario = null;
    this.userLogin.perfil = null;
    const passwordPattern = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{10,}$/;
    this.forma = new FormGroup({
      'usuario': new FormControl('', [Validators.required, Validators.pattern("[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$")]),
      'contrasenia': new FormControl('', [Validators.required, Validators.pattern(passwordPattern)]),
    })
  }

  ngOnInit() {
  }
  login(){
    if(this.forma.invalid){ 
      console.log("Formulario invalido");  
      return;
    }
    Swal.fire({
      icon: 'info',
      allowOutsideClick: false,
      title: 'Cargando',
      text: 'Espere por favor...'
    });
    
    Swal.showLoading();
    this.usuario = this.forma.value;
    this.loginService.login(this.usuario).subscribe({
      next: (resp: RespuestaUsuarioModel) => {
       
        if(!resp.exito){
            Swal.fire({
              icon: 'warning',
              title: 'ALERTA',
              text: resp.mensaje
            });  
        }else{
          Swal.close();
          console.log(resp.resultado)
          sessionStorage.setItem("token", resp != null && resp.resultado != null ? resp.resultado.json_web_token : "");
          sessionStorage.setItem("fecha_acceso", resp != null && resp.resultado != null ? resp.resultado.fecha_ultimo_acceso : "");
          this.userLogin._id = resp != null && resp.resultado != null ? resp.resultado.json_web_token : "";
          //this.userLogin.fecha_acceso = resp != null && resp.resultado != null ? resp.resultado.fecha_ultimo_acceso : "";
          this.forma.reset();
          this.router.navigate(['/home'])
            
        }
        
      },
      error: (err: any) => {
        console.log(err)
        Swal.fire({
          icon: 'error',
          title: 'ERROR',
          text: err.error.mensaje
        });   
      }
    })
  }
}
