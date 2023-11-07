import {
  Button,
  Container,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
} from "@mui/material";
import axios from "axios";
import { Link } from "react-router-dom";
import React, { useEffect, useState } from "react";

export default function GetPermission() {
  const [APIData, setAPIData] = useState([]);
  useEffect(() => {
    try {
      axios
        .get(process.env.REACT_APP_API_END_POINT + `/api/Permission`)
        .then((response) => {
          setAPIData(response.data);
        })
        .catch(function (error) {
          console.log(error);
        });
    } catch (error) {
      console.log(error);
    }
  }, []);

  const setData = (data) => {
    let { id, nombreEmpleado, apellidoEmpleado, tipoPermiso, fechaPermiso } =
      data;
    localStorage.setItem("id", id);
    localStorage.setItem("nombre", nombreEmpleado);
    localStorage.setItem("apellido", apellidoEmpleado);
    localStorage.setItem("tipoPermiso", tipoPermiso);
    localStorage.setItem("fechaPermiso", fechaPermiso);
  };

  return (
    <Container>
      <h3 className="main-header">Get</h3>
      <Link to="/request">
        <Button variant="contained">Crear</Button>
      </Link>
      <Table size="small">
        <TableHead>
          <TableRow>
            <TableCell>Nombre</TableCell>
            <TableCell>Apellido</TableCell>
            <TableCell>Tipo Permiso</TableCell>
            <TableCell>Fecha Permiso</TableCell>
            <TableCell>Editar</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {APIData.map((data) => {
            return (
              <TableRow key={data.id}>
                <TableCell>{data.nombreEmpleado}</TableCell>
                <TableCell>{data.apellidoEmpleado}</TableCell>
                <TableCell>{data.permiso}</TableCell>
                <TableCell>{data.fechaPermiso}</TableCell>
                <TableCell>
                  <Link to="/modify">
                    <Button onClick={() => setData(data)} variant="contained">
                      Actualizar
                    </Button>
                  </Link>
                </TableCell>
              </TableRow>
            );
          })}
        </TableBody>
      </Table>
    </Container>
  );
}
