import { Box, Button, MenuItem, Paper, TextField, Typography } from "@mui/material";
import { categories } from "../../../lib/common";
import { type Activity } from "../../../types/activity";
import type { FormEvent } from "react";

type props = {
    activity: Activity | undefined,
    showActivityForm: (value: boolean) => void
}

export default function ActivityForm({ activity, showActivityForm }: props) {

    const formActivity = activity ?? {
        id: "",
        category: "",
        city: "",
        description: "",
        eventDate: new Date().toISOString(),
        latitude: 0.0,
        longitude: 0.0,
        isCancelled: false,
        title: "",
        venue: ""
    }

    const eventDate = formActivity.eventDate.slice(0, formActivity.eventDate.indexOf('T'));

    function onSubmit(ele: FormEvent<HTMLFormElement>) {
        ele.preventDefault();
        const formData = new FormData(ele.currentTarget);
        const postActivity = Object.fromEntries(formData.entries());
        console.log(postActivity);
    }


    return (
        <Paper key={activity ? 'Update' : 'Create'}>
            <Typography variant="h5" gutterBottom color="Primary">
                {activity ? 'Update' : 'Create'} Activity
            </Typography>
            <Box onSubmit={onSubmit} component='form' sx={{ display: 'flex', flexDirection: 'column', gap: '2', padding: 1 }}
                autoComplete="off">
                <input type="hidden" id="id" name='id' defaultValue={formActivity.id}/>
                <TextField sx={{ marginBottom: 1 }} required id='title' name='title' label="Title"
                    variant="outlined" defaultValue={formActivity.title} />
                <TextField sx={{ marginBottom: 1 }} required id='description' name='description' label="Description"
                    multiline maxRows={4} defaultValue={formActivity.description} />
                <TextField sx={{ marginBottom: 1 }} required type="date" id='eventDate' name="eventDate" label='Event Date'
                    defaultValue={eventDate} />
                <TextField sx={{ marginBottom: 1 }} select required
                    id='category' name='category' label="Category" variant="outlined"
                    defaultValue={formActivity.category.toLowerCase()}>
                    {
                        categories.map(item => <MenuItem key={item.value} value={item.value}>
                            {item.label}
                        </MenuItem>)
                    }
                </TextField>
                <TextField sx={{ marginBottom: 1 }} required id='city' name='city' label="City" variant="outlined"
                    defaultValue={formActivity.city} />
                <TextField sx={{ marginBottom: 1 }} required id='venue' name='venue' label="Venue" variant="outlined"
                    defaultValue={formActivity.venue} />
                <TextField sx={{ marginBottom: 1 }} required id='latitude' name='latitude' label="Latitude" variant="outlined"
                    defaultValue={formActivity.latitude} />
                <TextField sx={{ marginBottom: 1 }} required id='longitude' name='longitude' label="Logitude" variant="outlined"
                    defaultValue={formActivity.longitude} />
                <Box sx={{ display: "flex", justifyContent: 'end', gap: 3 }}>
                    <Button color="warning" variant="contained" onClick={() => showActivityForm(false)}>Cancel</Button>
                    <Button type="submit" color="success" variant="contained">Submit</Button>
                </Box>
            </Box>
        </Paper>
    )
}