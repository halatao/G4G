import React from "react";
import { Container, Row, Col } from "react-bootstrap";

export default function (props) {
  console.log(props);
  return (
    <div class>
      {props?.comments?.map((comment, index) => (
        <div>
          <div style={{ marginTop: "30px" }}>
            <div className="Content-responsive Round-corners-comment">
              <Container className="Round-corners-comment" fluid="lg">
                <Row className="text-center Comment-height">
                  <Col className="col-12 text-start Content-left-side pl-2 pt-2 col-md-3">
                    <div>
                      <h5>{comment.accountUsername}</h5>
                    </div>
                    <div>posts: {comment.account.contentsPosted}</div>
                    <div>replies: {comment.account.commentsPosted}</div>
                  </Col>
                  <Col className="col-12 col-md-9 text-start pl-2 pt-2 Content-right-side">
                    <div className="text-end">
                      <h5>#{index + 1}</h5>
                    </div>
                    <div className="Thread">{comment.text}</div>
                  </Col>
                </Row>
              </Container>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
}
