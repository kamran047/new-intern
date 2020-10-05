let link = "http://localhost:65276/api/student";
const ajaxCall = (httpMethod, link, data, callBackMethod) => {
  $.ajax({
    type: httpMethod,
    url: link,
    data: data,
    success: callBackMethod,
  });
};

let table = $("#table_data");
let save_index = null;
let getData;
ajaxCall("GET", link, "json", function (data) {
  getData = data;
  if (data != null) {
    for (i = 0; i < data.length; i++) {
      let table_row = `<tr id=row${i}>
              <td>${data[i].name}</td>
              <td >${data[i].email}</td>
              <td >${data[i].password}</td>
              <td >${data[i].confirmPassword}</td>
              <td >${data[i].phoneNo}</td>
              <td> <button onclick="editStudent(${data[i].studentId},${i})" id="edit${i}">Edit</button></td>
              <td><button onclick="deleteStudent(${data[i].studentId})" id="delete${i}">Delete</button></td>
              </tr>`;
      table.append(table_row);
    }
  }
});

const editStudent = (data, index) => {
  let editData = getData[index];
  save_index = data;
  $(`#row${index}`).hide();
  renderInputFields();
  $("#fname").val(editData.name);
  $("#email").val(editData.email);
  $("#password").val(editData.password);
  $("#confirm_password").val(editData.confirmPassword);
  $("#number").val(editData.phoneNo);
  $("#input_feilds").attr("disabled", true);
  for (let i = 0; i < getData.length; i++) {
    $(`#edit${i}`).attr("disabled", true);
    $(`#delete${i}`).attr("disabled", true);
  }
};

const renderInputFields = (flag) => {
  let table = $("#table_data");
  let table_row = `<tr>
    <td>
    <input id="fname" type="text" required />
    </td>
    <td>
    <input id="email" type="text" required />
    </td>
    <td>
    <input id="password" type="text" required />
    </td>
    <td>
    <input id="confirm_password" type="text" required />
    </td>
    <td>
    <input id="number" type="number" required />
    </td>
    <td> <button onclick="saveData()" id="save">Save</button></td>
      <td><button onclick="cancelData()" id="cancel">Cancel</button></td>
      </tr>`;
  table.append(table_row);
  console.log("This method is working.");
};

const saveData = () => {
  let student = {
    name: $("#fname").val(),
    email: $("#email").val(),
    password: $("#password").val(),
    confirmPassword: $("#confirm_password").val(),
    phoneNo: $("#number").val(),
  };
  let isValid = true;
  isValid = formValidation(student);

  if (isValid) {
    let studentData;
    studentData = getData;

    if (save_index != null) {
      ajaxCall(
        "PUT",
        link,
        (data = {
          StudentId: save_index,
          Name: student.name,
          Email: student.email,
          Password: student.password,
          ConfirmPassword: student.confirmPassword,
          PhoneNo: student.phoneNo,
        }),
        function () {
          save_index = null;
          location.reload();
        }
      );
    }
    if (studentData == null) {
      studentData = [];
    }
    if (save_index == null) {
      ajaxCall(
        "POST",
        link,
        (data = {
          Name: student.name,
          Email: student.email,
          Password: student.password,
          ConfirmPassword: student.confirmPassword,
          PhoneNo: student.phoneNo,
        }),
        function () {
          save_index = null;
          location.reload();
        }
      );
    }
  }
};

const cancelData = () => {
  location.reload();
};

const deleteStudent = (index) => {
  if (index == null) {
    alert("Table has no record to show..");
  } else {
    ajaxCall("DELETE", link + "/" + index, "json", function () {
      location.reload();
    });
  }
};

const formValidation = (student) => {
  if (isAlpha(student.name)) {
    alert("Numbers are not allowed..!!!");
    return false;
  }
  if (!isValidEmail(student.email)) {
    alert("Not a valid Email Address..!!!");
    return false;
  }
  if (!isValidPassword(student.password, student.confirmPassword)) {
    alert("Both Passwords are not same");
    return false;
  } else return true;
};
