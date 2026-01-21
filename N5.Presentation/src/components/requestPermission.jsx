import axios from "axios";
import {
  Button,
  Container,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  FormControl,
  Input,
  InputLabel,
  MenuItem,
  Select,
  Box,
} from "@mui/material";
import HomeIcon from "@mui/icons-material/Home";
import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";

export default function RequestPermission() {
  const navigate = useNavigate();
  const [employeeName, setEmployeeName] = useState("");
  const [employeeLastName, setEmployeeLastName] = useState("");
  const [permissionType, setPermissionType] = useState("");
  const [permissionResult, setPermissionResult] = useState("");
  const [open, setOpen] = React.useState(false);

  const handleClose = () => {
    setOpen(false);
  };

  const postData = (e) => {
    e?.preventDefault();
    try {
      if (employeeName !== "" && employeeLastName !== "" && permissionType !== "" && permissionType !== -1) {
        axios
          .post(import.meta.env.VITE_API_END_POINT + `/api/Permission`, {
            employeeName: employeeName,
            employeeLastName: employeeLastName,
            permissionType: permissionType,
            permissionDate: new Date(),
          })
          .then(function (response) {
            setPermissionResult("Permission registered");
            setOpen(true);
            navigate('/')
          })
          .catch(function (error) {
            setPermissionResult("Failed to register permission\n" + error);
            setOpen(true);
            navigate('/')
          });
      } else {
        setPermissionResult("Required values!!!");
        setOpen(true);
      }
    } catch (error) {
      setPermissionResult("Failed to register permission\n" + error);
      setOpen(true);
      navigate('/')
    }
  };

  const [items, setItems] = useState([]);
  useEffect(() => {
    try {
      const results = [];
      axios
        .get(import.meta.env.VITE_API_END_POINT + `/api/PermissionType`)
        .then((response) => {
          response.data.forEach((value) => {
            results.push({
              key: value.description,
              value: value.id,
            });
          });
          setItems([{ key: "Select a value...", value: -1 }, ...results]);
        })
        .catch(function (error) {
          throw error;
        });
    } catch (error) {
      throw error;
    }
  }, []);

  return (
    <Container className="create-form">
    <Box sx={{ display: "flex", alignItems: "center", gap: 2, mb: 2 }}>
      <Link to="/" style={{ textDecoration: "none" }}>
        <Button startIcon={<HomeIcon />} />
      </Link>
    </Box>
      <Box sx={{ display: "flex", alignItems: "center", gap: 2, mb: 2 }}>
        <h3 className="main-header" style={{ margin: 0, flex: 1 }}>Request</h3>
      </Box>
      <FormControl fullWidth>
        <InputLabel htmlFor="employeeName">Employee Name</InputLabel>
        <Input id="employeeName" onChange={(e) => setEmployeeName(e.target.value)} />
      </FormControl>
      <FormControl fullWidth>
        <InputLabel htmlFor="employeeLastName">Employee Last Name</InputLabel>
        <Input id="employeeLastName" onChange={(e) => setEmployeeLastName(e.target.value)} />
      </FormControl>
      <FormControl fullWidth>
        <InputLabel id="permissionTypeLabel">Permission Type</InputLabel>
        <Select
          labelId="permissionTypeLabel"
          id="permissionType"
          value={permissionType}
          label="Permission Type"
          onChange={(e) => setPermissionType(e.target.value)}
          className="selection-data"
        >
          {items.map((option) => {
            return <MenuItem key={option.value} value={option.value}>{option.key}</MenuItem>;
          })}
        </Select>
      </FormControl>
      <Button onClick={postData} variant="contained">
        Request
      </Button>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">{"Permissions"}</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            {permissionResult}
          </DialogContentText>
        </DialogContent>
        <DialogActions>
            <Button onClick={handleClose} autoFocus>
              Ok
            </Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
}
