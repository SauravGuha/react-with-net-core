import { HubConnectionBuilder, type HubConnection } from '@microsoft/signalr';
import type { ActivityResponse } from '../types';


const baseUrl = import.meta.env.VITE_BASEURL;

const connection: HubConnection = new HubConnectionBuilder()
    .withUrl(`${baseUrl}/comments`) // Replace with your actual hub URL
    .build();

connection.on("AllActivityComments", (response: ActivityResponse) => { });

export const startSignalRConnection = async function () {
    try {
        await connection.start();
        console.log("SignalR Connected!");
    } catch (err) {
        console.error("Error connecting to SignalR:", err);
        // Implement retry logic or error handling as needed
    }
}