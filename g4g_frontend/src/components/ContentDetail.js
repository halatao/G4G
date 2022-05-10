import axios from "axios";
import React, { useEffect, useState } from "react";
import { Container, Button, Form, Col, Row } from "react-bootstrap";
import Comment from "./Comment";
import ErrorMessage from "./ErrorMessage";
import format from "date-fns/format";
import { useParams } from "react-router-dom";

export default function (props) {
  const [content, setContent] = useState([undefined]);
  const [contentId, setContentId] = useState(undefined);
  const [commentInvalid, setCommentInv] = useState(true);
  const [comment, setComment] = useState("");

  let params = useParams();
  console.log(params.contentId);
  function setCommentInvalid(param) {
    setCommentInv(param);
  }

  function postComment() {
    axios
      .post("https://localhost:7022/api/Comments", {
        text: comment,
        posted:
          format(new Date(), "yyyy-MM-dd") +
          "T" +
          format(new Date(), "kk:mm:ss"),
        accountIdAccount: props.account.idAccount,
        accountUsername: props.account.username,
        contentIdContent: contentId,
      })
      .then(function (response) {
        fetchContent(response.data.contentIdContent);
        console.log(response);
      })
      .catch(function (error) {
        console.log(error);
      });
  }

  const fetchContent = (contentId) => {
    axios
      .get("https://localhost:7022/api/Contents/" + contentId)
      .then((response) => {
        setContent(response.data);
      });
  };

  useEffect(() => {
    fetchContent(contentId);
  }, [contentId]);

  useEffect(() => {
    setContentId(params.contentId);
  }, [params.contentId]);

  function triggerComment() {
    if (commentInvalid === false) {
      postComment();
    }
  }
  return (
    <div className="mt-5">
      <div className="Content-responsive Round-corners-content mb-5">
        <Container className="Round-corners-content" fluid="lg">
          <Row className="text-center Content-height">
            <Col className="col-12 text-start Content-left-side pl-2 pt-2 col-md-3">
              <div>
                <h5>{content?.account?.username}</h5>
              </div>
              <div>posts: {content?.account?.contentsPosted}</div>
              <div className="mb-3">
                replies: {content?.account?.commentsPosted}
              </div>
            </Col>
            <Col className="col-12 col-md-9 text-start pl-2 pt-2 Content-right-side">
              <div>
                <h3> {content?.headline}</h3>
              </div>
              <div className="Thread">{content?.text}</div>
            </Col>
          </Row>
        </Container>
      </div>

      <Comment comments={content.comment} className="mt-3" />
      <Form className="mt-5 Cmnt-text-cnt">
        <Form.Group className="mb-3">
          <Form.Control
            style={{ height: 200 }}
            as="textarea"
            rows={3}
            placeholder="Enter comment"
            onChange={(e) => setComment(e.target.value)}
            value={comment}
          />
          <Form.Text className="text-muted">
            <ErrorMessage
              text={comment}
              setInvalid={setCommentInvalid}
              error={props.error}
              notError={props.notError}
            />
          </Form.Text>
        </Form.Group>

        <Button variant="primary" onClick={triggerComment} className="mb-5">
          Reply
        </Button>
      </Form>
    </div>
  );
}
