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
  return (
    <Card sx={{ minWidth: 275 }}>
      <CardContent>
        <Box>
          <Tabs
            variant="scrollable"
            scrollButtons={true}
            allowScrollButtonsMobile
            centered
          >
            <Tab label="Image"></Tab>
            <Tab label="Caption"></Tab>
            <Tab label="Dense Caption"></Tab>
            <Tab label="Tags"></Tab>
            <Tab label="Objects"></Tab>
            <Tab label="Smart Crops"></Tab>
            <Tab label="People"></Tab>
            <Tab label="Read"></Tab>
            <Tab label="All Features"></Tab>
          </Tabs>
        </Box>
      </CardContent>
    </Card>
  );
}

export default BasicImageCard;
