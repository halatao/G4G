import Login from "./Login";
import { Button } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
export default function (props) {
  function userLogout() {
    props.setLogged(false);
  }
  if (props.isLogged) {
    return (
      <div>
        <Button className="float-end rounded-pill" onClick={userLogout}>
          Logout
        </Button>
        <div className="float-end mt-2 me-2" style={{ color: "white" }}>
          Welcome {props.account.username}
        </div>
      </div>
    );
  }
  return (
    <Login
      setLogged={props.setLogged}
      error={props.error}
      notError={props.notError}
      setAccount={props.setAccount}
    />
  );
}
