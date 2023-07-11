var button = document.getElementById('logAsAdmin');
function setAdmin(){
  var loginInput = document.getElementById('login');
  var passwordInput = document.getElementById('password');
  console.log(loginInput);
  loginInput.value = "admin";
  passwordInput.value = "@Aa123456";
}
button.addEventListener("click", setAdmin);