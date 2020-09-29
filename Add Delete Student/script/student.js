let table = $("#table_data");
let save_index = null;
let getData = JSON.parse(localStorage.getItem("STUDENT"));
if (getData != null) {
  for (i = 0; i < getData.length; i++) {
    let table_row = `<tr id=row${i}>
          <td>${getData[i].name}</td>
          <td >${getData[i].email}</td>
          <td >${getData[i].password}</td>
          <td >${getData[i].confirm_password}</td>
          <td >${getData[i].phone_no}</td>
          <td> <button onclick="editStudent(${i})" id="edit${i}">Edit</button></td>
          <td><button onclick="deleteStudent(${i})" id="delete${i}">Delete</button></td>
          </tr>`;
    table.append(table_row);
  }
}

const editStudent = (index) => {
  let editData = getData[index];
  save_index = index;
  $(`#row${index}`).hide();
  renderInputFields();
  $("#fname").val(editData.name);
  $("#email").val(editData.email);
  $("#password").val(editData.password);
  $("#confirm_password").val(editData.confirm_password);
  $("#number").val(editData.phone_no);
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
    confirm_password: $("#confirm_password").val(),
    phone_no: $("#number").val(),
  };
  let isValid = true;
  isValid = formValidation(student);

  if (isValid) {
    let studentData = JSON.parse(localStorage.getItem("STUDENT"));
    if (save_index != null) {
      studentData[save_index] = student;
    }
    if (studentData == null) {
      studentData = [];
    }
    if (save_index == null) {
      studentData.push(student);
    }
    localStorage.setItem("STUDENT", JSON.stringify(studentData));
    save_index = null;
    location.reload();
  }
};

const cancelData = () => {
  location.reload();
};

const deleteStudent = (index) => {
  if (getData == null) {
    alert("Table has no record to show..");
  } else {
    getData.splice(index, 1);
    localStorage.setItem("STUDENT", JSON.stringify(getData));
    location.reload();
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
  if (!isValidPassword(student.password, student.confirm_password)) {
    alert("Both Passwords are not same");
    return false;
  } else return true;
};
