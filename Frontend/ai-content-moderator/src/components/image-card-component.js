import * as React from "react";
import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";

function BasicImageCard() {
  const [value, setValue] = React.useState(0);
  const handleChange = (event, newValue) => {
    setValue(newValue);
  };
  return (
    <Card sx={{ flex: "1 0 auto", maxWidth: 300 }}>
      <CardContent>
        <Box>
          <Tabs
            variant="scrollable"
            scrollButtons={true}
            allowScrollButtonsMobile
            onChange={handleChange}
            value={value}
          >
            <Tab label="Image" value={0}></Tab>
            <Tab label="Caption" value={1}></Tab>
            <Tab label="Dense Caption" value={2}></Tab>
            <Tab label="Tags" value={3}></Tab>
            <Tab label="Objects" value={4}></Tab>
            <Tab label="Smart Crops" value={5}></Tab>
            <Tab label="People" value={6}></Tab>
            <Tab label="Read" value={7}></Tab>
            <Tab label="All Features" value={8}></Tab>
          </Tabs>
        </Box>
      </CardContent>
    </Card>
  );
}

export default BasicImageCard;
