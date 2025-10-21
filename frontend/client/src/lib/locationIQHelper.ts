import axios from "axios";
import type { LocationIQ } from "../types";

const apiKey = import.meta.env.VITE_LOCATIONIQAPIKEY;

const instance = axios.create({
    baseURL: 'https://api.locationiq.com/v1/'
});

export const autoComplete = async function (address: string) {
    const result = await instance.get<LocationIQ[]>("autocomplete", {
        params: {
            key: apiKey,
            q: address,
            limit: 3,
            format: 'json'
        }
    });
    return result.data;
}

export const reverseGeoCoding = async function (lat: number, lon: number) {
    const result = await instance.get<LocationIQ>("reverse", {
        params: {
            key: apiKey,
            lat: lat,
            lon: lon,
            limit: 1,
            format: 'json'
        }
    });
    return result.data;
}

