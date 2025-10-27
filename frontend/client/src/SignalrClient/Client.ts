import { HubConnectionBuilder, type HubConnection } from '@microsoft/signalr';

const baseUrl = import.meta.env.VITE_SIGNALR_BASEURL;

export const startSignalrActivityConnection = async function (activityId: string) {
    const connection: HubConnection = new HubConnectionBuilder()
        .withUrl(`${baseUrl}comments?activityid=${activityId}`) // Replace with your actual hub URL
        .withAutomaticReconnect()
        .build();
    return connection;
}