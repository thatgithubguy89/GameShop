import { BrowserRouter, Routes, Route } from "react-router-dom";
import "./App.css";
import { HomePage } from "./pages/HomePage";
import { NavBar } from "./components/navigation/NavBar";
import { GameDetailsPage } from "./pages/GameDetailsPage";
import { CartPage } from "./pages/CartPage";
import { ConfirmationPage } from "./pages/ConfirmationPage";
import { CreateReviewPage } from "./pages/CreateReviewPage";

function App() {
  return (
    <BrowserRouter>
      <NavBar />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/gamedetails/:id" element={<GameDetailsPage />} />
        <Route path="/shoppingcart" element={<CartPage />} />
        <Route path="/confirmation" element={<ConfirmationPage />} />
        <Route
          path="/createreview/:gameid/:gameorderid/:username"
          element={<CreateReviewPage />}
        />
        <Route path="*" element={<HomePage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
