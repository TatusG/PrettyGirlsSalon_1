import { useState, useEffect, use } from "react";
import { useNavigate, Link } from "react-router-dom";
import * as API from '../../services/data';
import { data } from "react-router-dom";

export function Citas() {
    const cita = JSON.parse(sessionStorage.getItem('user'));
    const [appointments, setAppointments] = useState([]);

    useEffect(() => {
        if(cita.user){
            API.GetStylistAppointments(cita.user)
                .then(data => {
                    console.log("Citas Pendientes",data);
                    setAppointments(data);
        })
        .catch(error => console.error("Error al cargar las citas:", error));
    }
}, [user]);
}