import "./App.css";
import Category from "./components/Category";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import NavigationBar from "./components/NavigationBar";
import Content from "./components/Content";
import { useState } from "react";
import ContentDetail from "./components/ContentDetail";

function App() {
  const [account, setAcc] = useState([]);
  const [isLogged, setLog] = useState(false);
  const [categories, setCategories] = useState([]);
  const [category, setCat] = useState([]);
  const [error] = useState("This field is required");
  const [notError] = useState("");
  function setCategory(param) {
    setCat(param);
  }
  function setLogged(param) {
    setLog(param);
  }
  function setAccount(param) {
    setAcc(param);
  }

  return (
    <body>
      <NavigationBar
        isLogged={isLogged}
        setLogged={setLogged}
        error={error}
        nError={notError}
        setAccount={setAccount}
        account={account}
      />
      <BrowserRouter>
        <Routes>
          <Route
            path="/"
            element={
              <Category
                categories={categories}
                setCategories={setCategories}
                setCategory={setCategory}
              />
            }
          />
          <Route
            path="SubCategory/:subcatId"
            element={
              <Content
                category={category}
                error={error}
                notError={notError}
                account={account}
                isLogged={isLogged}
              />
            }
          />
          <Route
            path="Content/:contentId"
            element={
              <ContentDetail
                account={account}
                error={error}
                notError={notError}
              />
            }
          />
        </Routes>
      </BrowserRouter>
    </body>
  );
}

export default App;
