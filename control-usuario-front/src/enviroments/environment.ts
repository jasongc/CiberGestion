// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
import jwt_decode from 'jwt-decode';

export const environment = {
    production: true
  };
  
  let token = sessionStorage.getItem("token");
  let fecha_acceso = sessionStorage.getItem("fecha_acceso");
  let decodedToken: any = token != undefined && token != null? jwt_decode(token.toString()) : null;
  console.log(decodedToken);
  export const UserLogin = {
    _id: token,
    usuario: decodedToken == null ? "" : decodedToken.email,
    nombres: decodedToken == null ? "" : decodedToken.unique_name,
    perfil: decodedToken == null ? "" : decodedToken.role,
    fecha_acceso: fecha_acceso
  };
  export const ObtenerDatosToken:any = () =>{
    
    let tokentemp = sessionStorage.getItem("token");
    let decodedToken: any = tokentemp != undefined && tokentemp != null ? jwt_decode(tokentemp.toString()) : null;
    let retunrObj = {
      _id: token,
      email: decodedToken == null ? "" : decodedToken.email,
      unique_name: decodedToken == null ? "" : decodedToken.unique_name,
      role: decodedToken == null ? "" : decodedToken.role,
      iIdUsuario: decodedToken == null ? "" : decodedToken.iIdUsuario,
      fecha_acceso: sessionStorage.getItem("fecha_acceso")
    }
    return retunrObj;
  };
  export const URI_API = "https://localhost:7229/api/";
  /*
   * For easier debugging in development mode, you can import the following file
   * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
   *
   * This import should be commented out in production mode because it will have a negative impact
   * on performance if an error is thrown.
   */
  // import 'zone.js/dist/zone-error';  // Included with Angular CLI.
  