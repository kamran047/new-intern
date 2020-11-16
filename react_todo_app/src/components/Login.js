import React, { Component } from "react";
import axios from "axios";
import $ from "jquery";
class Login extends Component {
  state = {
    username: "",
    password: "",
  };

  handleChange = (e) => {
    this.setState({
      [e.target.name]: e.target.value,
    });
  };

  onSubmit = (event) => {
    event.preventDefault();
    const user = {
      grant_type: "password",
      username: this.state.username,
      password: this.state.password,
    };
    axios.post(`http://localhost:65276/Login`, $.param(user)).then((res) => {});
  };

  render() {
    return (
      <div>
        <label> Username: </label>
        <input type="text" name="username" onChange={this.handleChange} />
        <label> Password: </label>
        <input type="text" name="password" onChange={this.handleChange} />
        <button type="submit" onClick={this.onSubmit}>
          Login
        </button>
      </div>
    );
  }
}
export default Login;
