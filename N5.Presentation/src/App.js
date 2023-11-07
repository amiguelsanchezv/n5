import "./App.css";
import GetPermission from "./components/getPermissions";
import ModifyPermission from "./components/modifyPermissions";
import RequestPermission from "./components/requestPermission";
import { BrowserRouter, Routes, Route } from "react-router-dom";

function App() {
  console.log(process.env.REACT_APP_API_END_POINT);
  return (
    <div className="App">
      <header className="App-header">
        <div className="main">
          <h2 className="main-header">Permissions</h2>
          <div>
            <BrowserRouter>
              <div className="max-w-screen-md mx-auto pt-20">
                <Routes>
                  <Route exact path="/" element={<GetPermission />} />
                  <Route exact path="/request" element={<RequestPermission />} />
                  <Route exact path="/modify" element={<ModifyPermission />} />
                </Routes>
              </div>
            </BrowserRouter>
          </div>
        </div>
      </header>
    </div>
  );
}

export default App;
