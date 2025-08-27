import { useState } from "react";
import * as API from '../../services/data';
import { Navigate, useNavigate } from "react-router-dom";

export function UpdateStylist(){
    const [form, setForm] = useState({userName: "", userPassword:"", fullName:"", specialty:"", email:"", isActive: Boolean});

    const navigate = useNavigate();

    async function handlerSubmit(e){
        
    }
}