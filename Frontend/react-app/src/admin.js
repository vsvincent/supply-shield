import React, { useEffect, useState } from 'react';
import {
    Accordion,
    AccordionItem,
    AccordionButton,
    AccordionPanel,
    AccordionIcon,
    Box,
    Button, ButtonGroup,
    Stack,
    Heading,
  } from '@chakra-ui/react';
  import { useNavigate } from 'react-router-dom';

const Admin = () => {
    const [items, setItems] = useState([]);
    const navigate = useNavigate();
    const gatewayBaseUrl = "https://gateway-cmlmuykhqq-lm.a.run.app/";

    const handleSignOut = () => {
        localStorage.removeItem('userToken');
        window.location.reload(false);
    };

    const handleNewIncident = () => {
        navigate("./incident")
    };
    const handleNewOrganization = () => {
        navigate("./organization")
    };
    const handleNewUser = () => {
        navigate("./user")
    };

    const fetchOrganizationId = async () => {
        try {
            const response = await fetch(gatewayBaseUrl + 'user?email=' + (localStorage.getItem("email")), {
              method: 'GET',
              headers: {
                'Cache-Control': 'no-cache',
                Authorization: 'Bearer ' + localStorage.getItem('userToken'),
                'Content-Type': 'application/json',
              },
            });
    
            if (response.ok) {
              const data = await response.json();
              localStorage.setItem("organizationId", data.organizationId)
              console.log(data);
            } else {
              throw new Error('Request failed with status ' + response.status);
            }
          } catch (error) {
            console.error(error);
          }
    };

  useEffect(() => {
    const fetchData = async () => {
        try {
          const response = await fetch(gatewayBaseUrl + 'incident?organizationId='+localStorage.getItem('organizationId'), {
            method: 'GET',
            headers: {
              'Cache-Control': 'no-cache',
              Authorization: 'Bearer ' + localStorage.getItem('userToken'),
              'Content-Type': 'application/json',
            },
          });
  
          if (response.ok) {
            const data = await response.json();
            setItems(data);
            console.log(data);
          } else {
            throw new Error('Request failed with status ' + response.status);
          }
        } catch (error) {
          console.error(error);
        }
      };
        fetchOrganizationId();
        fetchData();
    }, []);

  return (
    <Box margin='10px'>
        <Stack direction='row' mb="10px">
        <Button onClick={handleSignOut}>Sign Out</Button>
        <Button onClick={handleNewIncident}>Declare Incident</Button>
        <Button onClick={handleNewOrganization}>Create Organization</Button>
        <Button onClick={handleNewUser}>Create User</Button>
        </Stack>
     <Accordion id="IncidentList">
        {items.map((item) => (
            <AccordionItem key={item.id}>
            <h2>
                <AccordionButton>
                <Box as="span" flex="1" textAlign="left">
                    Incident Type: {item.type}
                </Box>
                <AccordionIcon />
                </AccordionButton>
            </h2>
            <AccordionPanel pb={4}>Description: {item.description}</AccordionPanel>
            </AccordionItem>
        ))}
    </Accordion>
</Box>
  );
};

export default Admin;