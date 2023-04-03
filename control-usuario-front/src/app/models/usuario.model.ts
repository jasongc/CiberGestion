export class UsuarioModel {
    public usuario: string;
    public contrasenia: string;
    public confirmar_contrasenia: string;
    public nueva_contrasenia: string;
    public nombre: string;
    public apellido_paterno: string;
    public apellido_materno: string;
    public rol: string;
    constructor(){ 
        this.usuario = "", 
        this.contrasenia = "" , 
        this.nueva_contrasenia = "" , 
        this.confirmar_contrasenia = "" , 
        this.nombre = "" , 
        this.apellido_paterno = "" , 
        this.apellido_materno = "",
        this.rol = "INVITADO"
    }
}