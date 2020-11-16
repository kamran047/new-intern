import React, { Component } from "react";
import "./css/Calculator.css";

class Calculator extends Component {
  state = {
    number: "",
    result: 0,
    operator: "",
  };

  getButtonValue = (input) => {
    let number = this.state.number + input.toString();
    this.setState({
      number: number,
    });
  };

  saveOperatorToState = (operator) => {
    this.setState({ operator: operator });
    this.setState({ result: this.state.number });
    this.setState({ number: "" });
  };

  getResultAfterOperation = () => {
    const { operator } = this.state;
    const { result } = this.state;
    const { number } = this.state;
    let resultingNumber;
    if (operator === "+") {
      resultingNumber = parseInt(result) + parseInt(number);
    } else if (operator === "-") {
      resultingNumber = parseInt(result) - parseInt(number);
    } else if (operator === "*") {
      resultingNumber = parseInt(result) * parseInt(number);
    } else if (operator === "/") {
      resultingNumber = parseInt(result) / parseInt(number);
    }
    this.setState({ number: resultingNumber });
  };

  clearInputField = () => {
    this.setState({ result: 0 });
    this.setState({ number: "" });
    this.setState({ operator: "" });
  };

  render() {
    return (
      <React.Fragment>
        <input readOnly={true} className="input" value={this.state.number} />
        <div>
          <button
            value="1"
            className="btn btn-dark"
            onClick={() => this.getButtonValue(1)}
          >
            1
          </button>
          <button
            value="2"
            className="btn btn-dark"
            onClick={() => this.getButtonValue(2)}
          >
            2
          </button>
          <button
            value="3"
            className="btn btn-dark"
            onClick={() => this.getButtonValue(3)}
          >
            3
          </button>
          <button
            className="btn btn-primary btnOperations"
            onClick={() => this.saveOperatorToState("+")}
          >
            +
          </button>
          <button
            className="btn btn-primary btnOperations"
            onClick={() => this.saveOperatorToState("-")}
          >
            -
          </button>
        </div>
        <div>
          <button
            className="btn btn-dark"
            onClick={() => this.getButtonValue(4)}
          >
            4
          </button>
          <button
            className="btn btn-dark"
            onClick={() => this.getButtonValue(5)}
          >
            5
          </button>
          <button
            className="btn btn-dark"
            onClick={() => this.getButtonValue(6)}
          >
            6
          </button>
          <button
            className="btn btn-primary btnOperations"
            onClick={() => this.saveOperatorToState("*")}
          >
            *
          </button>
          <button
            className="btn btn-primary btnOperations"
            onClick={() => this.saveOperatorToState("/")}
          >
            /
          </button>
        </div>
        <div>
          <button
            className="btn btn-dark"
            onClick={() => this.getButtonValue(7)}
          >
            7
          </button>
          <button
            className="btn btn-dark"
            onClick={() => this.getButtonValue(8)}
          >
            8
          </button>
          <button
            className="btn btn-dark"
            onClick={() => this.getButtonValue(9)}
          >
            9
          </button>
          <button
            className="btn btn-dark btnOperations"
            onClick={() => this.getButtonValue(0)}
          >
            0
          </button>
          <button
            className="btn btn-success btnOperations"
            onClick={() => this.getResultAfterOperation()}
          >
            =
          </button>
          <div>
            <button
              className="btn btn-warning btnClear"
              onClick={this.clearInputField}
            >
              Clear
            </button>
          </div>
        </div>
      </React.Fragment>
    );
  }
}

export default Calculator;
