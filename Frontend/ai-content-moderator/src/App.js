import logo from "./logo.svg";
import "./App.css";
import { RoutesComponent } from "./RoutesComponent";
import { FiChevronDown } from "react-icons/fi";
import Typewriter from "./components/background-typewriter-component";
import BasicTextCard from "./components/text-card-component";
import BasicImageCard from "./components/image-card-component";
function App() {
  return (
    <div className="App">
      <header className="App-header">
        <section className="hero">
          <div style={{ display: "flex" }}>
            <Typewriter
              className="type"
              text="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
          minim veniam, quis nostrud exercitation ullamco laboris nisi ut
          aliquip ex ea commodo consequat. Duis aute irure dolor in
          reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla
          pariatur. Excepteur sint occaecat cupidatat non proident, sunt in
          culpa qui officia deserunt mollit anim id est laborum"
              speed={100}
            />
          </div>
          <h1>“AI-Powered Content Moderation for Text & Images”</h1>
          <p>
            “Detect profanity, offensive language, threats, and inappropriate
            visuals — instantly. Keep your platform safe for everyone.”
          </p>
          <h2 className="tryItNow">Try It Now</h2>
          <a href="#next-section" className="scroll-btn">
            <FiChevronDown className="arrow" />
            <FiChevronDown className="arrow" />
          </a>
        </section>
        <section id="next-section">
            <div className="text-card">
              <BasicTextCard />
            </div>
            <div className="image-card">
              <BasicImageCard />
            </div>
        </section>
      </header>
      <RoutesComponent />
    </div>
  );
}

export default App;
