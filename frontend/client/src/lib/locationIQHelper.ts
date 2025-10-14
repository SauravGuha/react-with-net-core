import axios from "axios";

const apiKey = import.meta.env.VITE_LOCATIONIQAPIKEY;

const instance = axios.create({
    baseURL: 'https://api.locationiq.com/v1/'
});

export const autoComplete = async function(address:string){
    const result = await instance.get("autocomplete", {params:{
                            key: apiKey,
                            q: address,
                            limit: 1,
                            format: 'json'
    }});
    return result.data;
}

