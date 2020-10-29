const loginFuntionality = () => {
  var userData = {
    grant_type: "password",
    username:  $("#txtUsername").val(),
    password:  $("#txtPassword").val(),
  };
  $.ajax({
    type: "POST",
    url: "http://localhost:65276/Login",
    data: userData,
    success: function (data) {
      let accessTokenFromSessionStorage = JSON.parse(
        sessionStorage.getItem("accessToken")
      );
      if (accessTokenFromSessionStorage != null) {
        accessTokenFromSessionStorage == data.access_token
          ? alert("You are logged in.")
          : alert("Another user is logged in.");
      } else {
        sessionStorage.setItem(
          "accessToken",
          JSON.stringify(data.access_token)
        );
        location.href="../html/student.html";
      }
    },
    fail: function () {
      alert("Ajax Call Failed.");
    },
  });
};
