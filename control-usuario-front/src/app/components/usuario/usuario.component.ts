import { Component } from '@angular/core';
import { UsuarioModel } from 'src/app/models/usuario.model';
import { NgForm } from '@angular/forms';
import Swal from 'sweetalert2';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html'
})
export class UsuarioComponent {
  usuario = new UsuarioModel();
  showPassword: boolean = false;
  showNewPassword: boolean = false;
  showConfirmPassword: boolean = false;


  constructor(private usuarioService: UsuarioService,){
  }
  togglePassword() {
    this.showPassword = !this.showPassword;
  }
  toggleNewPassword() {
    this.showNewPassword = !this.showNewPassword;
  }
  toggleConfirmPassword() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
  guardarActualizacion(form: NgForm){      
    Swal.fire({
      allowOutsideClick: false,
      icon: 'info',
      text: 'Espere por favor...'
    });
    Swal.showLoading();

    if(form.invalid){ 
      Swal.fire({
        icon: 'warning',
        text: 'Ingrese correctamente los datos por favor.'
      });  
      return;
    }

    
    const passwordPattern = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{10,}$/;
    console.log(this.usuario);
    if(!passwordPattern.test(this.usuario.contrasenia) || !passwordPattern.test(this.usuario.nueva_contrasenia) || !passwordPattern.test(this.usuario.confirmar_contrasenia))
    {
      Swal.fire({
        icon: 'warning',
        text: 'Debe ingresar una constraseña correcta. Recuerde que debe tener un mínimo de 10 caracteres, al menos una mayúscula, minúscula, número y algún caracter especial(@#$%^&*()_)'
      });  
      return;
    }
    if(this.usuario.nueva_contrasenia != this.usuario.confirmar_contrasenia)
    {
      Swal.fire({
        icon: 'warning',
        text: 'Las contraseñas ingresadas en la nueva y confirmación no coinciden.'
      });  
      return;
    }

    this.usuarioService.actualizarContrasenia(this.usuario).subscribe({
      next: (resp: any) => {       
        if(resp.exito){
          Swal.fire({
            icon: 'info',
            text: resp.mensaje,
            timer: 5000
          }); 
          form.reset()  
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
