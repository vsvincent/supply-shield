import React, { useEffect, useState } from 'react';
import {
    Accordion,AccordionItem,AccordionButton,AccordionPanel,AccordionIcon,
    Box,
    Button, ButtonGroup,
    Heading,
    Input,
    Center,
    Radio,RadioGroup,
    Stack,
  } from '@chakra-ui/react';
  import { useNavigate } from 'react-router-dom';

const Incident = () => {
    const [incidentType, setincidentType] = useState('Other');
    const [incidentDescription, setincidentDescription] = useState('');
    const navigate = useNavigate();
    const gatewayBaseUrl = "https://gateway-cmlmuykhqq-lm.a.run.app/";
    console.log(localStorage.getItem("userUid"));
    console.log(localStorage.getItem("userToken"));

    const cancelNewIncident = () => {
        navigate("./admin")
    };

    const postIncident = async () => {
      try {
        const response = await fetch(gatewayBaseUrl + 'incident', {
          method: 'POST',
          headers: {
            'Cache-Control': 'no-cache',
            Authorization: 'Bearer ' + localStorage.getItem('userToken'),
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            Type:incidentType,
            UserId:localStorage.getItem("userUid"),
            OrganizationID:localStorage.getItem("organizationId"),
            Description:incidentDescription
          }),
        });
    
        if (response.ok) {
          const data = await response.json();
          console.log(data);
        } else {
          throw new Error('Request failed with status ' + response.status);
        }
      } catch (error) {
        console.error(error);
      }
    };

  return (
    <Box margin='10px'>
      <Heading as="h2">Declare Supply Chain Incident</Heading>
    <Center w="90%">
      <form onSubmit={postIncident}>
        <label>
          Incident Type:
          <RadioGroup onChange={setincidentType} value={incidentType} defaultValue='Other'>
            <Stack direction='row'>
              <Radio value='Maintenance'>Maintenance</Radio>
              <Radio value='Logistics'>Logistics</Radio>
              <Radio value='Conflict'>Conflict</Radio>
              <Radio value='Sabotage'>Sabotage</Radio>
              <Radio value='NaturalDisaster'>Natural Disaster</Radio>
              <Radio value='Other'>Other</Radio>
            </Stack>
          </RadioGroup>
        </label>
        <br />
        <label>
          Description:
          <Input
            placeholder='Enter description here...'
            value={incidentDescription}
            onChange={(e) => setincidentDescription(e.target.value)}
            required
          />
        </label>
        <br />
        <Stack direction='row' mt='10px'>
          <Button onClick={cancelNewIncident}>Cancel</Button>
          <Button type="submit">Declare Incident</Button>
        </Stack>
      </form>
    </Center >
    </Box>
  );
};

export default Incident;