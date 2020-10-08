const isAlpha = (name) => {
  return /^[0-9]+$/.test(name);
};

const isValidEmail=(email)=>{
return /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(email);
}

const isValidPassword = (password,confirm_password) => {
    return (password==confirm_password);
};
