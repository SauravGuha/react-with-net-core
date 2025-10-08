import axios from "axios";
import type { Activity } from "../types/activity";


const instance = axios.create({
    baseURL : import.meta.env.VITE_BASEURL,
    headers:{
        "Content-Type":"application/json"
    }
});

const delayer = function(timeSpan: number){
    return new Promise((resolve)=>{
        setTimeout(() => {
            resolve("delayed");
        }, timeSpan);
    });
}

instance.interceptors.response.use(async (response)=>{
    await delayer(1000);
    return response;
},(error)=>{
    console.log(error);
    return Promise.reject(error);
});

const getallactivities = async function(){
    const result = await instance.get<Activity[]>("activity/getallactivities");
    return result.data;
}

const updateActivity = async function(activity: Activity){
    const result = await instance.put<Activity[]>(`activity/UpdateActivity?id=${activity.id}`,activity);
    return result.data.length > 0 ? result.data[0] : null;
}

export {getallactivities, updateActivity};