import { useState } from 'react';
import {useNavigate} from 'react-router-dom';

export function Login () {
   const navigate = useNavigate();
   // User + Password + Error
   const [username,setUsername] = useState('');
   const [password,setPassword] = useState('');

   const routeToRegister = () => {
    navigate('/sign-up',{replace:true});
   };

   const loginUser = async() => {

   };

    return (
        <>
        <form onSubmit={loginUser}>
            <div>
                <label></label>
                <input type='text' value={username} onChange={(uname) => setUsername(uname.target.value)}/>
            </div>
            <div>
                <label></label>
                <input type='password' value={password} onChange={(upassword) => setPassword(upassword.target.value)}/>
            </div>
            <div>
                <button type='reset'></button>
                <button type="submit" onClick={loginUser}></button>
                <button type="button" onClick={routeToRegister}></button>
            </div>
        </form>
        </>
    );
}