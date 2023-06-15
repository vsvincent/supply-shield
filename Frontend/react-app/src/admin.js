import React, { useEffect, useState } from 'react';
import {
    Accordion,
    AccordionItem,
    AccordionButton,
    AccordionPanel,
    AccordionIcon,
    Box,
    Button, ButtonGroup,
  } from '@chakra-ui/react';
  import { useNavigate } from 'react-router-dom';

const Admin = () => {
    const [items, setItems] = useState([]);
    const navigate = useNavigate();
    const gatewayBaseUrl = "https://gateway-cmlmuykhqq-lm.a.run.app/"
    const handleSignOut = () => {
        localStorage.removeItem('userToken');
        window.location.reload(false);
    };

  useEffect(() => {
    console.log("wow");
    const fetchData = async () => {
        try {
          const response = await fetch(gatewayBaseUrl + 'incident?organizationId=EVILSOAP', {
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
  
      fetchData();
    }, []);

  return (
    <div>
     <Accordion>
      {items.map((item) => (
        <AccordionItem key={item.id}>
          <h2>
            <AccordionButton>
              <Box as="span" flex="1" textAlign="left">
                {item.title}
              </Box>
              <AccordionIcon />
            </AccordionButton>
          </h2>
          <AccordionPanel pb={4}>{item.content}</AccordionPanel>
        </AccordionItem>
      ))}
    </Accordion>
<Button onClick={handleSignOut}>Sign Out</Button>
</div>
  );
};

export default Admin;