import React, { useState } from "react";
import { Form, Button } from "react-bootstrap";
import ErrorMessage from "./ErrorMessage";
import axios from "axios";
import format from "date-fns/format";

export default function (props) {
  const [headline, setHeadline] = useState("");
  const [text, setText] = useState("");
  const [headlineInvalid, setHeadlineInv] = useState(true);
  const [textInvalid, setTextInv] = useState(true);

  function setHeadlineInvalid(param) {
    setHeadlineInv(param);
  }
  function setTextInvalid(param) {
    setTextInv(param);
  }

  const postContent = () => {
    axios
      .post("https://localhost:7022/api/Contents", {
        headline: headline,
        text: text,
        views: 0,
        posted:
          format(new Date(), "yyyy-MM-dd") +
          "T" +
          format(new Date(), "kk:mm:ss"),
        accountIdAccount: props.account.idAccount,
        accountUsername: props.account.username,
        subcategoryIdSubcategory: props.idSubcategory,
        comment: [],
      })
      .then((response) => {
        props.updateContent(response.data.subcategoryIdSubcategory);
        console.log(response);
      })
      .catch(function (error) {
        console.log(error);
      });
  };

  function triggerPost() {
    if (headlineInvalid === false && textInvalid === false) {
      props.triggerClose();
      postContent();
    }
  }
  if (props.open) {
    if (props.isLogged) {
      return (
        <div className="Filter-post-container">
          <Form>
            <Form.Group className="mb-3 mt-3" controlId="headline">
              <Form.Label className="text-danger">Headline</Form.Label>
              <Form.Control
                type="headline"
                placeholder="Enter headline"
                onChange={(e) => setHeadline(e.target.value)}
                value={headline}
              />
              <Form.Text className="text-muted">
                <ErrorMessage
                  text={headline}
                  setInvalid={setHeadlineInvalid}
                  error={props.error}
                  notError={props.notError}
                />
              </Form.Text>
            </Form.Group>

            <Form.Group className="mb-3" controlId="text">
              <Form.Label className="text-danger">Text</Form.Label>
              <Form.Control
                as="textarea"
                rows={3}
                placeholder="Enter text"
                onChange={(e) => setText(e.target.value)}
                value={text}
                style={{ height: 270 }}
              />
              <Form.Text className="text-muted">
                <ErrorMessage
                  text={text}
                  setInvalid={setTextInvalid}
                  error={props.error}
                  notError={props.notError}
                />
              </Form.Text>
            </Form.Group>

            <Button variant="primary" onClick={triggerPost}>
              Submit
            </Button>
          </Form>
        </div>
      );
    } else if (props.isLogged === false) {
      return (
        <div className="text-danger Filter-post-container">
          Please first you have to log in
        </div>
      );
    }
  }
}
