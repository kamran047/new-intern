import React, { Component } from "react";
import Select from "react-select";
import { Redirect } from "react-router-dom";
import axios from "axios";
import $ from "jquery";
class RegisterationForm extends Component {
  state = {
    student: {
      name: "",
      email: "",
      password: "",
      confirmPassword: "",
      phoneNo: "",
    },
    courses: [],
    studentCourses: [],
    base64Image: [],
  };

  componentDidMount() {
    axios({
      method: "GET",
      url: "http://localhost:65276/api/course",
      crossdomain: true,
    }).then((res) => {
      const courses = res.data;
      let options = courses.map((course) => ({
        value: course.courseId,
        label: course.courseName,
      }));
      this.setState({ courses: options });
      if (this.props.student != null) {
        this.setState({ student: this.props.student });
      }
    });
  }

  handleChange = (selectedCourses) => {
    for (let index = 0; index < selectedCourses.length; index++) {
      let coursesId = selectedCourses[index].value.toString();
      this.setState({
        studentCourses: [...this.state.studentCourses, coursesId],
      });
    }
  };

  saveData = () => {
    if (this.props.student != null) {
      let studentId = this.props.student.studentId;
      let studentData = {
        studentId,
        name: this.state.student.name,
        email: this.state.student.email,
        password: this.state.student.password,
        confirmPassword: this.state.student.confirmPassword,
        phoneNo: this.state.student.phoneNo,
      };
      axios({
        method: "PUT",
        url: "http://localhost:65276/api/student",
        data: {
          Student: studentData,
          Courses: this.state.studentCourses,
          ImagePath: this.state.base64Image,
        },
        crossdomain: true,
      }).then(() => {
        this.setState({ redirect: "/" });
      });
    } else {
      axios({
        method: "POST",
        url: "http://localhost:65276/api/student",
        data: {
          Student: this.state.student,
          Courses: this.state.studentCourses,
          ImagePath: this.state.base64Image,
        },
        crossdomain: true,
      }).then(() => {
        this.setState({ redirect: "/" });
      });
    }
  };

  convertToBase64 = () => {
    var selectFile = $("#imageUpload")[0].files;
    if (selectFile.length > 0) {
      var selectSingleFile = selectFile[0];
      var fileReader = new FileReader();
      fileReader.onload = (FileLoadEvent) => {
        var srcData = FileLoadEvent.target.result;
        let baseArray = srcData.split(",");
        this.setState({ base64Image: baseArray });
      };
      fileReader.readAsDataURL(selectSingleFile);
    }
  };

  onChange = (e) => {
    let student = { ...this.state.student };
    student[e.target.name] = e.target.value;
    this.setState({ student: student });
  };

  render() {
    if (this.state.redirect) return <Redirect to={this.state.redirect} />;
    const { selectedOptions } = this.state;
    return (
      <div>
        <label> Name: </label>
        <input
          name="name"
          type="text"
          value={this.state.student.name}
          onChange={this.onChange}
          required
        />
        <br></br>
        <label> Email: </label>
        <input
          name="email"
          type="text"
          value={this.state.student.email}
          onChange={this.onChange}
          required
        />
        <br></br>

        <label> Password: </label>
        <input
          name="password"
          type="text"
          value={this.state.student.password}
          onChange={this.onChange}
          required
        />
        <br></br>

        <label> Confirm Password: </label>
        <input
          name="confirmPassword"
          type="text"
          value={this.state.student.confirmPassword}
          onChange={this.onChange}
          required
        />
        <br></br>

        <label> Phone Number: </label>
        <input
          name="phoneNo"
          type="number"
          value={this.state.student.phoneNo}
          onChange={this.onChange}
          required
        />
        <br></br>

        <Select
          value={selectedOptions}
          onChange={this.handleChange}
          options={this.state.courses}
          isMulti={true}
        />
        <br></br>

        <label> Upload Image: </label>
        <input type="file" id="imageUpload" onChange={this.convertToBase64} />
        <br></br>

        <button onClick={() => this.saveData()} className="btn btn-primary">
          Save
        </button>
      </div>
    );
  }
}
export default RegisterationForm;
