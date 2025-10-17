import type { Activity } from "../types";

export const categories = [
    {
        value: 'music',
        label: 'Music'
    },
    {
        value: 'culture',
        label: 'Culture'
    },
    {
        value: 'drinks',
        label: 'Drinks'
    },
    {
        value: 'film',
        label: 'Film'
    },
    {
        value: 'food',
        label: 'Food'
    },
    {
        value: 'travel',
        label: 'Travel'
    },
    {
        value: '',
        label: ''
    }
];


export function eventDateString(eventDate: string) {
    const value = new Date(eventDate);
    return value.toString();
}

export function eventDateInUtcFormat(eventDate: string) {
    const date = new Date(eventDate);
    const pad = (n: number) => n.toString().padStart(2, "0");

    const year = date.getFullYear();
    const month = pad(date.getUTCMonth() + 1);
    const day = pad(date.getUTCDate());
    const hour = pad(date.getUTCHours());
    const min = pad(date.getUTCMinutes());

    return `${year}-${month}-${day}T${hour}:${min}:00`;
}

export function getDefaultactivity(activity: Activity | undefined | null) {
    return activity ?? {
        id: "",
        category: "",
        city: "",
        description: "",
        eventDate: new Date().toISOString(),
        latitude: undefined,
        longitude: undefined,
        isCancelled: false,
        title: "",
        venue: ""
    };
}