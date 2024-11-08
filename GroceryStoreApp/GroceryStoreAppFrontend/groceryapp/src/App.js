import React from 'react';
import {
  BrowserRouter as Router,
  Routes,
  Route,
} from 'react-router-dom';
import { LoginForm } from './Components/LoginForm/LoginForm';
import { RegisterForm } from './Components/RegisterForm/RegisterForm';
import { Checkout } from './Components/Checkout/Checkout';
import { Homepage } from './Components/Homepage/Homepage';
import { FoodDetails } from './Components/FoodDetails/FoodDetails';
import { Cart } from './Components/Cart/Cart';
import { UserProvider } from './Components/UserContext/UserContext';

function App() {
  return (
    <div>
      <Router>
        <UserProvider>
          <Routes>
            <Route path="/" element={<LoginForm />} />
            <Route path="/RegisterForm" element={<RegisterForm />} />
            <Route path="/Checkout" element={<Checkout />} />
            <Route path="/homepage" element={<Homepage />} />
            <Route path="/food/:category/:id" element={<FoodDetails />} />
            <Route path="/cart" element={<Cart />} />
          </Routes>
        </UserProvider>
      </Router>
    </div>
  );
}

export default App;
