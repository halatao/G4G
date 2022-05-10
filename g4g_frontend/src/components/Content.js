import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, Form, Container, Col, Row } from "react-bootstrap";
import ContentCard from "./ContentCard";
import { useParams } from "react-router-dom";
import CreateContent from "./CreateContent";

export default function (props) {
  const [data, setData] = useState([]);
  const [subCategory, setSub] = useState([]);
  const [subcatId, setSubcatId] = useState(undefined);
  const [open, setOpen] = useState(false);

  let params = useParams();

  function triggerClose() {
    setOpen(false);
  }

  function openToggle() {
    setOpen(!open);
  }

  function updateContent(param) {
    fetchData(param);
  }

  const fetchData = (subcatId) => {
    axios
      .get(
        "https://localhost:7022/api/Contents/?subcategoryIdSubcategory=" +
          subcatId
      )
      .then((response) => {
        setData(response.data);
      });
  };

  const fetchSub = () => {
    axios
      .get(
        "https://localhost:7022/api/SubCategories?categoryIdCategory=" +
          props.category.idCategory
      )
      .then((response) => {
        setSub(response.data);
      });
  };

  useEffect(() => {
    fetchData(subcatId);
  }, [subcatId]);

  useEffect(() => {
    setSubcatId(params.subcatId);
  }, [params.subcatId]);

  useEffect(() => {
    fetchSub();
  }, []);

  return (
    <div className="Content">
      <div className="Filter-post-container">
        <Row>
          <Col className="col-12 col-md-6 text-center text-md-start mb-4 mb-md-0">
            <Button className="rounded-pill" onClick={openToggle}>
              Create post
            </Button>
          </Col>
          <Col className="col-12 col-md-6">
            <Form.Select
              value={subcatId}
              onChange={(e) => setSubcatId(e.target.value)}
            >
              {subCategory.map((subcat) => (
                <option value={subcat.idSubcategory}>{subcat.name}</option>
              ))}
            </Form.Select>
          </Col>
        </Row>
      </div>

      <CreateContent
        open={open}
        error={props.error}
        notError={props.notError}
        triggerClose={triggerClose}
        idSubcategory={subcatId}
        account={props.account}
        isLogged={props.isLogged}
        updateContent={updateContent}
      />
      <div className="Category-container shadow">{props?.category?.name}</div>
      <div className="SubCategory-container shadow mb-3">
        <div className="ContentCard-stripe">
          <Container fluid="lg">
            <Row style={{ padding: "12px 0px 12px 0px" }}>
              <Col className="col-7 text-start">NAME</Col>
              <Col className="col-2 text-center">REPLIES</Col>
              <Col className="col-3 text-end">AUTHOR</Col>
            </Row>
          </Container>
        </div>

        {data.map((i, index) => (
          <ContentCard key={index} data={i} />
        ))}
      </div>
    </div>
  );
}
