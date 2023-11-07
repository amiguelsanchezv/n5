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
} from "@mui/material";
import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";

export default function RequestPermission() {
  const navigate = useNavigate();
  const [nombre, setNombre] = useState("");
  const [apellido, setApellido] = useState("");
  const [tipoPermiso, setTipoPermiso] = useState("");
  const [resultadoPermiso, setResultadoPermiso] = useState("");
  const [open, setOpen] = React.useState(false);

  const handleClose = () => {
    setOpen(false);
  };

  const postData = () => {
    try {
      if (nombre !== "" && apellido !== "" && tipoPermiso !== -1) {
        axios
          .post(process.env.REACT_APP_API_END_POINT + `/api/Permission`, {
            NombreEmpleado: nombre,
            ApellidoEmpleado: apellido,
            TipoPermiso: tipoPermiso,
            FechaPermiso: new Date(),
          })
          .then(function (response) {
            console.log(response);
            setResultadoPermiso("Permiso registrado");
            setOpen(true);
            navigate('/')
          })
          .catch(function (error) {
            setResultadoPermiso("Falla al registrar permiso\n" + error);
            setOpen(true);
            navigate('/')
          });
      } else {
        setResultadoPermiso("Valores obligatorios!!!");
        setOpen(true);
      }
    } catch (error) {
      setResultadoPermiso("Falla al registrar permiso\n" + error);
      setOpen(true);
      console.log(error);
      navigate('/')
    }
  };

  const [items, setItems] = useState([]);
  useEffect(() => {
    try {
      const results = [];
      axios
        .get(process.env.REACT_APP_API_END_POINT + `/api/PermissionType`)
        .then((response) => {
          response.data.forEach((value) => {
            results.push({
              key: value.descripcion,
              value: value.id,
            });
          });
          setItems([{ key: "Seleciona un valor...", value: -1 }, ...results]);
        })
        .catch(function (error) {
          console.log(error);
        });
    } catch (error) {
      console.log(error);
    }
  }, []);

  return (
    <Container className="create-form">
      <Link to="/">
        <h3 className="main-header">Request</h3>
      </Link>
      <FormControl fullWidth>
        <InputLabel htmlFor="nombre">Nombre Empleado</InputLabel>
        <Input id="nombre" onChange={(e) => setNombre(e.target.value)} />
      </FormControl>
      <FormControl fullWidth>
        <InputLabel htmlFor="apellido">Apellido Empleado</InputLabel>
        <Input id="apellido" onChange={(e) => setApellido(e.target.value)} />
      </FormControl>
      <FormControl fullWidth>
        <InputLabel id="tipoPermisoLabel">Tipo de Permiso</InputLabel>
        <Select
          labelId="tipoPermisoLabel"
          id="tipoPermiso"
          value={tipoPermiso}
          label="Tipo de Permiso"
          onChange={(e) => setTipoPermiso(e.target.value)}
          className="selection-data"
        >
          {items.map((option) => {
            return <MenuItem value={option.value}>{option.key}</MenuItem>;
          })}
        </Select>
      </FormControl>
      <Button onClick={postData} type="submit" variant="contained">
        Solicitar
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
            {resultadoPermiso}
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
