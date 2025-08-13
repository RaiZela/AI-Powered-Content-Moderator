import { BrowserRouter as Router, Switch, Route, Routes } from "react-router-dom";

export function RoutesComponent(){
    return (
    <Router>
      <Routes>
        <Route path="/" element={<textModeration />} />
      </Routes>
    </Router>
  );
}
