import React, { useState } from 'react';
import {initializeApp} from 'firebase/app';
import {signInWithEmailAndPassword, getAuth} from 'firebase/auth';
import { Navigate, useNavigate } from 'react-router-dom';
import { Input, Heading, Box, Center  } from '@chakra-ui/react'

// Initialize Firebase with your configuration
const firebaseConfig = {
    apiKey: "AIzaSyA0ZTEFlqXW_8GgO11P8fR-AFq4d5bJkk4",
    authDomain: "supply-shield-381721.firebaseapp.com",
    projectId: "supply-shield-381721",
    storageBucket: "supply-shield-381721.appspot.com",
    messagingSenderId: "506661741186",
    appId: "1:506661741186:web:ac20701607980421fb4a9b",
    measurementId: "G-4Q36BD44S7"
    };

initializeApp(firebaseConfig);
const auth = getAuth();

const SignIn = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [userToken, setUserToken] = useState(null);
  const navigate = useNavigate();

  const handleSignIn = async (e) => {
    e.preventDefault();

    try {
      var userCredential = await signInWithEmailAndPassword(auth, email, password);
      console.log(userCredential);
      var token = await userCredential.user.accessToken;
      setUserToken(token);
      localStorage.setItem("userToken", token);
      console.log( localStorage.getItem("userToken"));
    } catch (error) {
      console.log(error);
      // Handle sign-in error
    }
    window.location.reload(false);
  };

  return (
    <Center w="90%">
      <Heading as="h2">Sign In</Heading>
      <form onSubmit={handleSignIn}>
        <label>
          Email:
          <Input
            type="email"
            placeholder='Enter email here...'
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </label>
        <br />
        <label>
          Password:
          <Input
            type="password"
            placeholder='Enter password here...'
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </label>
        <br />
        <button type="submit">Sign In</button>
      </form>
    </Center >
  );
};

export default SignIn;