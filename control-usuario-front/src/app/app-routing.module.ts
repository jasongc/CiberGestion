import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from "./components/login/login.component";
import { UsuarioComponent } from './components/usuario/usuario.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [ 
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] } ,
  { path: 'cambiar-contrasenia', component: UsuarioComponent, canActivate: [AuthGuard] } ,
  { path: '**', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
