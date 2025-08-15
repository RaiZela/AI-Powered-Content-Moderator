import {
  BrowserRouter as Router,
  Switch,
  Route,
  Routes,
} from "react-router-dom";
import Typewriter from "./components/background-typewriter-component";

export function RoutesComponent() {
  return (
    <Router>
      <Routes>
        <Route path="/typewriter" element={<Typewriter />} />
      </Routes>
    </Router>
  );
}
