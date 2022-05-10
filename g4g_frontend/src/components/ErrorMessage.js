import React from "react";

export default function (props) {
  if (props.text === "") {
    props.setInvalid(true);
    return <div>{props.error}</div>;
  } else {
    props.setInvalid(false);
    return <div>{props.notError}</div>;
  }
}
