import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import type { User } from './../model/User.model';
import { LoginService } from '../../Services/login.service';
import './Login.scss';

export function Login() {
   const navigate = useNavigate();
   
   const [username, setUsername] = useState('');
   const [password, setPassword] = useState('');
   const [error, setError] = useState('');

   const routeToRegister = () => {
    navigate('/sign-up', { replace: true });
   };

   
   const handleLoginSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const user: User = {
            username: username,
            password: password
        };

        const loginService = new LoginService();
        const response: Response | null = await loginService.fetchUser(user);
    
        if (response) {
          navigate('/dashboard', { replace: true });
        } else {
            setError('Invalid username or password');
        }
   };

    return (
        <>
        <form onSubmit={handleLoginSubmit} className='loginForm'>
            <div className='loginFormHeader'>
                <label>Username:</label>
                <input 
                    type='text' 
                    value={username} 
                    onChange={(uname) => setUsername(uname.target.value)}
                />
            </div>
            <div className='loginFormHeader'>
                <label>Password:</label>
                <input 
                    type='password' 
                    value={password} 
                    onChange={(upassword) => setPassword(upassword.target.value)}
                />
            </div>
            <div className='loginFormFooter'>
                <button type='reset' className='btn btn-secondary'>Reset</button>
                <button type="submit" className='btn btn-primary'>Login</button>
                <button type="button" className='btn btn-link' onClick={routeToRegister}>Register</button>
            </div>
            <div className={error ? 'errorBox' : 'hidden'}>
                {error}
            </div>
        </form>
        </>
    );
}