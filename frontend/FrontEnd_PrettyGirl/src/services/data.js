
import { useEffect } from "react";
import { data } from "react-router-dom";

const URL = "https://localhost:7207/api/";

function GetAuthHeaders(){
    const token = sessionStorage.getItem("token");

    return{
       'Content-Type': 'application/json', 
        'Authorization': `Bearer ${token}`
    }
}

export function Login (user, password){
    let datos = {
        UserName : user,
        Password : password};
    
    return fetch(URL + 'login',{
        method: 'POST',
        body: JSON.stringify(datos),
        headers: {
            'Content-Type' : 'application/json'            
        }        
    })
    .then(async (res) => {
        const json = await res.json();
        if(!res.ok){
            throw new Error(json.message);
        }
        return json;
    })
}

export function Register(userName, userPassword, fullName, specialty, email, isActive = true){
    const datos = {userName, userPassword, fullName, specialty, email, isActive};

    return fetch(URL + 'registrar',{
        method: 'POST',
        body: JSON.stringify(datos),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(async (res)=> {
        const json = await res.json();
        if (!res.ok){
            throw new Error(json.message)
        }
        return json;
    })
}

export function UpdatePassword(UserName, OldPassword, NewPassword){
    const datos = {UserName, OldPassword, NewPassword};

    return fetch(URL + "actualizar-password",{
        method: 'PATCH',
        body: JSON.stringify(datos),
        headers:{
            'Content-Type': 'application/json'
        }
    })
    .then(async (res)=> {
        const json= await res.json();
        if (!res.ok){
            throw new Error(json.message);
        }
        return json
    })
}

export function LoadStylist(userName) { 
    return fetch(URL+"buscar-estilista?userName" + userName, {
        headers: GetAuthHeaders()
    })
    .then(async (res) => {
        if (!res.ok) {
            const errorMsg = await res.text();
            throw new Error(errorMsg);
        }
        return res.json();
    })
}

export function UpdateStylist(userName, userPassword, fullName, specialty, email, isActive){
    const datos = {userName, userPassword, fullName, specialty, email, isActive};

    return fetch(URL + "actualizar",{
        method: 'PATCH',
        body: JSON.stringify(datos),
        headers: {
            'Content-Type': 'applicaction/json'
        }
    })
    .then(async (res) => {
        const json= await res.json();
        if(!res.ok){
            throw new Error(json.message);
        }
        return json
    })
}

export function GetStylistAppointments(userName, date = null) {
    let url = URL + "citas?userName=" + userName;
    if (date) {
        url += "&date=" + date;
    }
    return fetch(url, {
        headers: GetAuthHeaders()
    })
    .then(async (res) => {
        if (!res.ok) {
            const errorMsg = await res.text();
            throw new Error(errorMsg);
        }
        return res.json();
    })
}

function StylistCalendar(){
    const [appointments, setAppointments] = useState([]);
    const [selectedDate, setSelectedDate] = useState(new Date());
    const {userName} = JSON.parse(sessionStorage.getItem('user'));

    useEffect(() => {
        GetStylistAppointments(userName, selectedDate.toISOString().split('T')[0])
            .then(data => setAppointments(data))
            .catch(error => console.error("Error al cargar las citas:", error));
    }, [userName, selectedDate]);

    const handlerStatusChange = async (appointmentId, newStatus) => {
        try {
            await API.UpdateAppointmentStatus(appointmentId, newStatus);
            const update = await GetStylistAppointments(userName, selectedDate.toISOString().split('T')[0]);
            setAppointments(update);
        } catch (error) {
            alert("Error al actualizar el estado de la cita: " + error.message);                        
        }
    };
}
