import * as React from "react";
import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";
import TextField from "@mui/material/TextField";
import TabPanel from "@mui/lab/TabPanel";
import TabContext from "@mui/lab/TabContext";
import TabList from "@mui/lab/TabList";

function BasicTextCard() {
  const [value, setValue] = React.useState("0");
  const handleChange = (event, newValue) => {
    setValue(newValue);
  };

  const [textFieldValue, setTextFieldValue] = React.useState("");

  const handleTextChange = (event) => {
    setTextFieldValue(event.target.value);
  };
  const [showComponent, setShowComponent] = React.useState(false);

  function MyComponent() {
    const [data, setData] = React.useState(null);
    const [loading, setLoading] = React.useState(true);
    const [error, setError] = React.useState(null);
    React.useEffect(() => {
      const fetchData = async () => {
        try {
          const response = await fetch(
            `https://localhost:7044/textmoderator/get-severity?input=${textFieldValue}`
          );
          if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
          }
          const result = await response.json();
          setData(result);
        } catch (error) {
          setError(error);
        } finally {
          setLoading(false);
        }
      };

      fetchData();
    }, []);

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error.message}</div>;

    return (
      <div>
        {data
          // ?.filter((item) => item.severity > 1)
          .map((item, index) => (
            <div key={index}>
              <h6>Severity: {item.severity}</h6>
              <h6>Category: {item.category}</h6>
              <hr />
            </div>
          ))}
      </div>
    );
  }

  return (
    <Card
      sx={{
        minWidth: 500,
        marginBottom: 7,
      }}
    >
      <CardContent>
        <TabContext value={value}>
          <Box component="form" noValidate autoComplete="off">
            <TabList onChange={handleChange} value={value}>
              <Tab label="Text" value="0"></Tab>
              <Tab label="Blocklist" value="1"></Tab>
            </TabList>
          </Box>
          <TabPanel
            value="0"
            sx={{
              display: "flex",
              flexDirection: "column",
              alignItems: "flex-start",
              justifyContent: "flex-start",
            }}
          >
            <TextField
              label="Type something here..."
              id="text-area"
              variant="outlined"
              fullWidth
              value={textFieldValue}
              className="text-textfield"
              onChange={handleTextChange}
            />
            <Button variant="outlined" onClick={() => setShowComponent(true)}>
              Check
            </Button>

            {showComponent && <MyComponent />}
          </TabPanel>
        </TabContext>
      </CardContent>
    </Card>
  );
}

export default BasicTextCard;
