import axios from "axios";
import React, { Component } from "react";
import { Redirect } from "react-router-dom";

class StudentListing extends Component {
  state = {
    students: [],
    visible: false,
  };

  componentDidMount() {
    axios({
      method: "GET",
      url: "http://localhost:65276/api/student",
      crossdomain: true,
    }).then((res) => {
      const students = res.data;
      this.setState({ students: students });
    });
  }

  deleteRecord = (id) => {
    axios.delete(`http://localhost:65276/api/student/` + id).then(() => {
      let updatedStudentList = this.state.students.filter(
        (student) => student.student.studentId !== id
      );
      this.setState({ students: updatedStudentList });
    });
  };

  editRecord = (student, courses) => {
    this.props.getStudentData(student, courses);
    this.setState({ redirect: "/RegistrationForm" });
  };

  addStudent = () => {
    this.setState({ redirect: "/RegistrationForm" });
  };

  render() {
    if (this.state.redirect) return <Redirect to={this.state.redirect} />;
    return (
      <div>
        <button className="btn btn-primary" onClick={() => this.addStudent()}>
          Add Student
        </button>
        <table className="table table-hover table-bordered">
          <thead>
            <tr>
              <th> Name </th>
              <th> Email</th>
              <th> Password</th>
              <th> Confirm Password</th>
              <th> Phone Number</th>
              <th> Course Name</th>
            </tr>
          </thead>
          <tbody>
            {this.state.students.map((student) => (
              <tr>
                <td>{student.student.name} </td>
                <td>{student.student.email} </td>
                <td>{student.student.password} </td>
                <td>{student.student.confirmPassword} </td>
                <td>{student.student.phoneNo} </td>
                <td>
                  {student.courses.map((course) => (
                    <ul>
                      <li>{course}</li>
                    </ul>
                  ))}
                </td>
                <td>
                  <button
                    className="btn btn-info"
                    onClick={() =>
                      this.editRecord(student.student, student.courses)
                    }
                  >
                    Edit
                  </button>
                </td>
                <td>
                  <button
                    className="btn btn-danger"
                    onClick={() => this.deleteRecord(student.student.studentId)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  }
}
export default StudentListing;
