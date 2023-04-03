export const environment = {
    production: true
  };
  
  let id = sessionStorage.getItem("token");
  
  export const UserLogin = {
    email: undefined,
    role: undefined,
    _id: id
  };
  
  