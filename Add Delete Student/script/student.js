let link = "http://localhost:65276/api/";
let courseApi;
const ajaxCall = (httpMethod, link, data, handleSuccess, handleFail) => {
  $.ajax({
    type: httpMethod,
    url: link,
    data: data,
    success: handleSuccess,
    fail: handleFail,
  });
};

ajaxCall(
  "GET",
  link + "course",
  "json",
  function (response) {
    courseApi = response;
  },
  function () {
    alert("Failed to Load Courses.");
  }
);
let table = $("#table_data");
let save_index = null;
let getData;
ajaxCall(
  "GET",
  link + "student",
  "json",
  function (data) {
    getData = data;
    if (data != null) {
      for (i = 0; i < data.length; i++) {
        let table_row = `<tr id=row${i}>
              <td>${data[i].student.name}</td>
              <td >${data[i].student.email}</td>
              <td >${data[i].student.password}</td>
              <td >${data[i].student.confirmPassword}</td>
              <td >${data[i].student.phoneNo}</td>
              <td> <button onclick="editStudent(${data[i].student.studentId},${i})" id="edit${i}">Edit</button></td>
              <td><button onclick="deleteStudent(${data[i].student.studentId})" id="delete${i}">Delete</button></td>
              </tr>`;
        table.append(table_row);
      }
    }
  },
  function () {
    alert("Failed to Load Students data.");
  }
);

const editStudent = (data, index) => {
  let editData = getData[index];
  save_index = data;
  $(`#row${index}`).hide();
  renderInputFields();
  $("#fname").val(editData.student.name);
  $("#email").val(editData.student.email);
  $("#password").val(editData.student.password);
  $("#confirm_password").val(editData.student.confirmPassword);
  $("#number").val(editData.student.phoneNo);
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
    <td><select id="course_data" multiple="multiple">${courseApi.map(
      (element) =>
        `<option value="${element.courseId}">${element.courseName}</option>`
    )}</select></td>
    <td> <button onclick="saveData()" id="save">Save</button></td>
    <td><button onclick="cancelData()" id="cancel">Cancel</button></td>
      </tr>`;
  table.append(table_row);
};

const saveData = () => {
  let student = {
    name: $("#fname").val(),
    email: $("#email").val(),
    password: $("#password").val(),
    confirmPassword: $("#confirm_password").val(),
    phoneNo: $("#number").val(),
  };
  var course = $("#course_data").val();
  let isValid = true;
  isValid = formValidation(student);

  if (isValid) {
    let studentData;
    studentData = getData;

    if (save_index != null) {
      ajaxCall(
        "PUT",
        link + "student",
        (data = {
          Student: {
            StudentId: save_index,
            Name: student.name,
            Email: student.email,
            Password: student.password,
            ConfirmPassword: student.confirmPassword,
            PhoneNo: student.phoneNo,
          },
          Courses: course,
        }),
        function () {
          save_index = null;
          location.reload();
        },
        function () {
          alert("Failed to Update Student data.");
        }
      );
    }
    if (studentData == null) {
      studentData = [];
    }
    if (save_index == null) {
      ajaxCall(
        "POST",
        link + "student",
        (data = {
          Student: {
            Name: student.name,
            Email: student.email,
            Password: student.password,
            ConfirmPassword: student.confirmPassword,
            PhoneNo: student.phoneNo,
          },
          Courses: course,
        }),
        function () {
          save_index = null;
          location.reload();
        },
        function () {
          alert("Failed to Add Student data.");
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
    ajaxCall(
      "DELETE",
      link + "student/" + index,
      "json",
      function () {
        location.reload();
      },
      function () {
        alert("Failed to Delete Student data.");
      }
    );
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
