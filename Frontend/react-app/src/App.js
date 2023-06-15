import logo from './logo.svg';
import './App.css';
import SignIn from './signIn.js';
import Admin from './admin.js';
import Incident from './incident.js';
import { BrowserRouter as Router, Route, Routes, Navigate} from "react-router-dom";
import { ChakraProvider } from '@chakra-ui/react'

function App() {
  return (
    <ChakraProvider>
    <Router>
      <Routes>
        <Route path="/" element={localStorage.getItem("userToken") ? <Admin /> : <Navigate to="/signin" />}/>
        <Route path='/signin' element={localStorage.getItem("userToken") ? <Navigate to='/admin'/> : <SignIn/>} />
        <Route path="/admin" element={localStorage.getItem("userToken") ? <Admin /> : <Navigate to="/signin" />}/>
        <Route path="/admin/incident" element={localStorage.getItem("userToken") ? <Incident /> : <Navigate to="/signin" />}/>
        <Route path="*" element={<Navigate to="/signin"/>}/>
      </Routes>
    </Router>
    </ChakraProvider>
  );
}

export default App;
