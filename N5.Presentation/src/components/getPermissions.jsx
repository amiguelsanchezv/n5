import {
  Button,
  Container,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import EditIcon from "@mui/icons-material/Edit";
import axios from "axios";
import { Link } from "react-router-dom";
import React, { useEffect, useState } from "react";

export default function GetPermission() {
  const [APIData, setAPIData] = useState([]);
  useEffect(() => {
    try {
      axios
        .get(import.meta.env.VITE_API_END_POINT + `/api/Permission`)
        .then((response) => {
          setAPIData(response.data);
        })
        .catch(function (error) {
          throw error;
        });
    } catch (error) {
      throw error;
    }
  }, []);

  const setData = (data) => {
    let { id, employeeName, employeeLastName, permissionType, permissionDate } =
      data;
    localStorage.setItem("id", id);
    localStorage.setItem("employeeName", employeeName);
    localStorage.setItem("employeeLastName", employeeLastName);
    localStorage.setItem("permissionType", permissionType);
    localStorage.setItem("permissionDate", permissionDate);
  };

  return (
    <Container>
      <h3 className="main-header">Get</h3>
      <Link to="/request" style={{ textDecoration: "none" }}>
        <Button variant="contained" startIcon={<AddIcon />}>
          Create
        </Button>
      </Link>
      <Table size="small">
        <TableHead>
          <TableRow>
            <TableCell>Employee Name</TableCell>
            <TableCell>Employee Last Name</TableCell>
            <TableCell>Permission Type</TableCell>
            <TableCell>Permission Date</TableCell>
            <TableCell>Update</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {APIData.map((data) => {
            return (
              <TableRow key={data.id}>
                <TableCell>{data.employeeName}</TableCell>
                <TableCell>{data.employeeLastName}</TableCell>
                <TableCell>{data.permissionType}</TableCell>
                <TableCell>{data.permissionDate}</TableCell>
                <TableCell>
                  <Link to="/modify" style={{ textDecoration: "none" }}>
                    <Button
                      onClick={() => setData(data)}
                      variant="contained"
                      startIcon={<EditIcon />}
                    >
                      Update
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
