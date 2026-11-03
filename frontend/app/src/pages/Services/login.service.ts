import type{ User } from "../Login/model/User.model";
import { API_URL } from "../../config";

export class LoginService {
    async fetchUser(user:User):Promise<Response | null> {
        const response = await fetch(`${API_URL}/api/login`,{
            method:'POST',
            headers:{
                'Content-Type':'application/json'
            },
            body:JSON.stringify(user)
        });

        if(response.ok) {
            const data = await response.json();
            return data.json();
        } else {
            return null;
        }
    }
}