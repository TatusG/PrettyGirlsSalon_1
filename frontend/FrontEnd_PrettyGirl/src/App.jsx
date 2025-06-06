import { useState } from 'react';
import Menu from './components/Menu';
import Resena from './cliente/reseñas/Resena';


import './assets/styles/menu.css';
import './assets/styles/style.css';


function App() {
  
  const [showResena, setShowResena] = useState(false);

  return (
    <>    
      {!showResena ? (
        <Menu onNext={() => setShowResena(true)} />
      ) : (
        <Resena onBack={() => setShowResena(false)} />
      )}
    </>
  );
}

export default App;