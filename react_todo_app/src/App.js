import React, { Component } from "react";
import { BrowserRouter as Router, Route } from "react-router-dom";
import StudentListing from "./components/StudentListing";
import RegistrationForm from "./components/RegisterationForm";
import "./App.css";
import Login from "./components/Login";

class App extends Component {
  state = {
    student: undefined,
  };

  setStudentData = (student, courses) => {
    this.setState({ student: student });
    this.setState({ courses: courses });
  };

  setStudentDataForRegisterationForm = () => {
    if (this.state.student !== undefined && this.state.student !== null)
      return this.state.student;
    else return undefined;
  };
  render() {
    return (
      <Router>
        <Route
          exact
          path="/"
          render={(props) => (
            <StudentListing getStudentData={this.setStudentData} />
          )}
        />
        <Route path="/Login" component={Login} />
        <Route
          path="/RegistrationForm"
          render={(props) => (
            <RegistrationForm
              student={this.setStudentDataForRegisterationForm()}
              courses={this.state.courses}
            />
          )}
        />
      </Router>
    );
  }
}
export default App;
