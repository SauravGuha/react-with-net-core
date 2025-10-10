
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