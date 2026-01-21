import React, { useEffect, useState } from "react";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";
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

export default function ModifyPermission() {
  const navigate = useNavigate();
  const [employeeName, setEmployeeName] = useState("");
  const [employeeLastName, setEmployeeLastName] = useState("");
  const [permissionType, setPermissionType] = useState("");
  const [id, setID] = useState(null);
  const [permissionResult, setPermissionResult] = useState("");
  const [open, setOpen] = React.useState(false);

  const handleClose = () => {
    setOpen(false);
  };

  const updateAPIData = (e) => {
    e?.preventDefault();
    try {
      if (employeeName !== "" && employeeLastName !== "" && permissionType !== "" && permissionType !== -1) {
        axios
          .put(import.meta.env.VITE_API_END_POINT + `/api/Permission`, {
            id: parseInt(id),
            permissionType: permissionType,
          })
          .then(function (response) {
            setPermissionResult("Permission updated");
            setOpen(true);
            navigate("/");
          })
          .catch(function (error) {
            setPermissionResult("Failed to update permission\n" + error);
            setOpen(true);
            navigate("/");
          });
      } else {
        setPermissionResult("Required values!!!");
        setOpen(true);
      }
    } catch (error) {
      setPermissionResult("Failed to update permission\n" + error);
      setOpen(true);
      navigate("/");
    }
  };

  const [items, setItems] = useState([]);
  useEffect(() => {
    setID(localStorage.getItem("id"));
    setEmployeeName(localStorage.getItem("employeeName"));
    setEmployeeLastName(localStorage.getItem("employeeLastName"));
    const storedPermissionType = localStorage.getItem("permissionType");
    
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
          const allItems = [{ key: "Select a value...", value: -1 }, ...results];
          setItems(allItems);
          
          if (storedPermissionType) {
            const permissionTypeNum = parseInt(storedPermissionType, 10);
            const isValidValue = allItems.some(item => item.value === permissionTypeNum);
            if (isValidValue) {
              setPermissionType(permissionTypeNum);
            } else {
              setPermissionType("");
            }
          } else {
            setPermissionType("");
          }
        })
        .catch(function (error) {
          throw error;
        });
    } catch (error) {
      throw error;
    }
  }, []);

  return (
    <div>
      <Container className="create-form">
        <Box sx={{ display: "flex", alignItems: "center", gap: 2, mb: 2 }}>
          <Link to="/" style={{ textDecoration: "none" }}>
            <Button startIcon={<HomeIcon />} />
          </Link>
        </Box>
        <Box sx={{ display: "flex", alignItems: "center", gap: 2, mb: 2 }}>
          <h3 className="main-header" style={{ margin: 0, flex: 1 }}>Modify</h3>
        </Box>
        <FormControl fullWidth>
          <InputLabel htmlFor="employeeName">Employee Name</InputLabel>
          <Input
            id="employeeName"
            value={employeeName}
            onChange={(e) => setEmployeeName(e.target.value)}
            readOnly
          />
        </FormControl>
        <FormControl fullWidth>
          <InputLabel htmlFor="employeeLastName">Employee Last Name</InputLabel>
          <Input
            id="employeeLastName"
            value={employeeLastName}
            onChange={(e) => setEmployeeLastName(e.target.value)}
            readOnly
          />
        </FormControl>
        <FormControl fullWidth>
          <InputLabel id="permissionTypeLabel">Permission Type</InputLabel>
          <Select
            labelId="permissionTypeLabel"
            id="permissionType"
            value={permissionType === "" || permissionType === null ? "" : permissionType}
            label="Permission Type"
            onChange={(e) => setPermissionType(e.target.value)}
            className="selection-data"
          >
            {items.map((option) => {
              return <MenuItem key={option.value} value={option.value}>{option.key}</MenuItem>;
            })}
          </Select>
        </FormControl>
        <Button variant="contained" onClick={updateAPIData}>
          Update
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
    </div>
  );
}
