import { Autocomplete, Box, Button, MenuItem, Paper, TextField, Typography } from "@mui/material";
import { categories, eventDateInUtcFormat, getDefaultactivity } from "../../../lib/common";
import { activityObject, type LocationIQ } from "../../../types";
import { useEffect, useRef, useState, type FormEvent } from "react";
import useActivityReactQuery from "../../../hooks/useActivityReactQuery";
import { Link, useNavigate, useParams } from "react-router-dom";
import { ZodError } from "zod";
import { camelCase } from 'lodash';
import { autoComplete, reverseGeoCoding } from "../../../lib/locationIQHelper";


export default function ActivityForm() {

    let activity = undefined;
    const { id } = useParams();
    const navigate = useNavigate();
    const { isUpdating, activityUpdate, isCreating, activityCreate, activities } = useActivityReactQuery();
    if (id) {
        activity = activities?.find(e => e.id == id);
    }
    const formActivity = getDefaultactivity(activity);
    const [formErrors, setFormErrors] = useState<Record<string, string>>({});

    const eventDate = eventDateInUtcFormat(formActivity.eventDate);
    //"2025-11-05T00:00:00";
    async function onSubmit(ele: FormEvent<HTMLFormElement>) {
        ele.preventDefault();
        const formData = new FormData(ele.currentTarget);
        try {
            const postActivity = activityObject.parse(Object.fromEntries(formData.entries()));
            postActivity.eventDate = postActivity.eventDate + ":00.000Z";
            if (postActivity.id) {
                await activityUpdate(postActivity);
            }
            else {
                const createdActivity = await activityCreate(postActivity);
                postActivity.id = createdActivity!.id;
            }
            navigate(`/activity/${postActivity.id}`);
        }
        catch (err) {
            const errors: Record<string, string> = {};
            if (err instanceof ZodError) {
                err.issues.forEach(item => {
                    const { message, path } = item;
                    const fieldName = path[0].toString();
                    errors[fieldName] = message;
                })
            }
            else {
                if (err instanceof Array) {
                    err.forEach((item: string) => {
                        const errorArray = item.split("'");
                        errors[camelCase(errorArray[1])] = errorArray.join("");
                    })
                }
            }
            setFormErrors(errors);
        }
    }

    const [selectedAddress, setSelectedAddress] = useState('');
    useEffect(() => {
        if (activity) {
            const operation = setTimeout(async () => {
                const locationInfo = await reverseGeoCoding(activity.latitude, activity.longitude);
                setSelectedAddress(locationInfo.display_name);
            }, 1000);
            return () => clearTimeout(operation);
        }
    }, [activity]);

    const [locationDisbaled, setLocationDisabled] = useState(false);
    const [address, setAddress] = useState("");
    const [suggestions, setSuggestions] = useState<LocationIQ[]>([]);
    const cityRef = useRef<HTMLInputElement>(null);
    const venueRef = useRef<HTMLInputElement>(null);
    const latitudeRef = useRef<HTMLInputElement>(null);
    const longitudeRef = useRef<HTMLInputElement>(null);

    useEffect(() => {
        if (address.length < 6) {
            return;
        }
        else {
            const operation = setTimeout(async () => {
                setLocationDisabled(true);
                try {
                    const suggestionsResponse = await autoComplete(address);
                    setSuggestions(suggestionsResponse);
                }
                finally {
                    setLocationDisabled(false);
                }
            }, 1000);
            return () => clearTimeout(operation);
        }
    }, [address]);


    return (
        <Paper key={activity ? 'Update' : 'Create'}>
            <Typography variant="h5" gutterBottom color="Primary">
                {activity ? 'Update' : 'Create'} Activity
            </Typography>
            <Box onSubmit={onSubmit} component='form' sx={{ display: 'flex', flexDirection: 'column', gap: '2', padding: 1 }}
                autoComplete="off">
                <input type="hidden" id="id" name='id' defaultValue={formActivity.id} />
                <TextField autoFocus sx={{ marginBottom: 1 }} id='title' name='title' label="Title"
                    variant="outlined" defaultValue={formActivity.title}
                    error={!!formErrors.title} helperText={formErrors.title} />
                <TextField sx={{ marginBottom: 1 }} id='description' name='description' label="Description"
                    multiline maxRows={4} defaultValue={formActivity.description}
                    error={!!formErrors.description} helperText={formErrors.description} />
                <TextField sx={{ marginBottom: 1 }} type="datetime-local" id='eventDate' name="eventDate" label='Event Date'
                    defaultValue={eventDate} error={!!formErrors.eventDate} helperText={formErrors.eventDate} />
                <TextField sx={{ marginBottom: 1 }} select
                    id='category' name='category' label="Category" variant="outlined"
                    defaultValue={formActivity.category.toLowerCase()}
                    error={!!formErrors.category} helperText={formErrors.category}>
                    {
                        categories.map(item => <MenuItem key={item.value} value={item.value}>
                            {item.label}
                        </MenuItem>)
                    }
                </TextField>

                <input hidden type="checkbox" name="isCancelled" id="isCancelled" defaultChecked={activity?.isCancelled} />
                {/* <FormControlLabel sx={{ mb: 1 }} control={<Checkbox defaultChecked={activity?.isCancelled} />}
                    label="Cancelled" name="isCancelled" id="isCancelled" /> */}

                <Autocomplete options={suggestions.map(item => ({
                    label: item.display_name,
                    id: item.osm_id,
                    lat: item.lat,
                    lon: item.lon,
                    city: item.address?.city || "",
                    venue: item.display_place || ""
                }))}
                    freeSolo sx={{ mb: 1 }}
                    renderInput={(params) => <TextField {...params} label="Location" />}
                    onInputChange={(e, v) => {
                        setAddress(v);
                    }}
                    value={selectedAddress}
                    onChange={(event, value) => {
                        if (value) {
                            if (cityRef.current) { cityRef.current.value = value.city; }
                            if (venueRef.current) { venueRef.current.value = value.venue; }
                            if (latitudeRef.current) { latitudeRef.current.value = value.lat; }
                            if (longitudeRef.current) { longitudeRef.current.value = value.lon; }
                        }
                    }}
                    disabled={locationDisbaled} />
                <TextField sx={{ marginBottom: 1 }} id='city' name='city' label="City" variant="outlined"
                    defaultValue={formActivity.city}
                    error={!!formErrors.city} helperText={formErrors.city}
                    inputRef={cityRef} />
                <TextField sx={{ marginBottom: 1 }} id='venue' name='venue' label="Venue" variant="outlined"
                    defaultValue={formActivity.venue}
                    error={!!formErrors.venue} helperText={formErrors.venue}
                    inputRef={venueRef} />
                <TextField sx={{ marginBottom: 1 }} id='latitude' name='latitude' label="Latitude" variant="outlined"
                    defaultValue={formActivity.latitude}
                    error={!!formErrors.latitude} helperText={formErrors.latitude}
                    inputRef={latitudeRef} />
                <TextField sx={{ marginBottom: 1 }} id='longitude' name='longitude' label="Longitude" variant="outlined"
                    defaultValue={formActivity.longitude}
                    error={!!formErrors.longitude} helperText={formErrors.longitude}
                    inputRef={longitudeRef} />

                <Box sx={{ display: "flex", justifyContent: 'end', gap: 3 }}>
                    <Button component={Link} to='/activities' color="warning" variant="contained">Cancel</Button>
                    <Button type="submit" loading={isUpdating || isCreating} color="success" variant="contained">Submit</Button>
                </Box>
            </Box>
        </Paper>
    )
}