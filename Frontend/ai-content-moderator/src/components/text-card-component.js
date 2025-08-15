import * as React from "react";
import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";

function BasicTextCard() {
  return (
    <Card sx={{ minWidth: 275 }}>
      <CardContent>
        <Box>
          <Tab label="Text"></Tab>
          <Tab label="Blocklist"></Tab>
        </Box>
      </CardContent>
    </Card>
  );
}

export default BasicTextCard;
