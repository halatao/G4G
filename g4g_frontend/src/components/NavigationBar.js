import React from "react";
import { Col, Container, Navbar, Row } from "react-bootstrap";
import Account from "./Account";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faGamepad } from "@fortawesome/free-solid-svg-icons";
import { library } from "@fortawesome/fontawesome-svg-core";
library.add(faGamepad);

export default function (props) {
  return (
    <Navbar bg="dark" variant="dark">
      <Container>
        <Navbar.Brand href="/">
          <FontAwesomeIcon icon={faGamepad} size="lg" />
          <span className="ms-1">G4G</span>
        </Navbar.Brand>
        <Col md={4}>
          <Account
            isLogged={props.isLogged}
            setLogged={props.setLogged}
            error={props.error}
            notError={props.notError}
            account={props.account}
            setAccount={props.setAccount}
          />
        </Col>
      </Container>
    </Navbar>
  );
}
