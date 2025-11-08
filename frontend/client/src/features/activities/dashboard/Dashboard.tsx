
import { Grid } from "@mui/material"
import ActivityList from "./ActivityList"
import ActivityFilters from "./ActivityFilters"
import { useState } from "react"
import { eventDateInUtcFormat } from "../../../lib/common";


export default function Dashboard() {

    const [filterBy, setFilterBy] = useState<string | undefined>(undefined);
    const [filterDate, setFilterDate] = useState<string | undefined>(undefined);

    function onFilterChanged(value: string) {
        if (value == "All events") {
            setFilterBy("");
        }
        else {
            setFilterBy(value);
        }
    }

    function onFilterDateChanged(value: any) {
        const selectedFilterDate = eventDateInUtcFormat(value);
        setFilterDate(selectedFilterDate);
    }

    return (
        <>
            <Grid container spacing={2}>
                <Grid size={8}>
                    <ActivityList filterBy={filterBy} filterDate={filterDate} />
                </Grid>
                <Grid
                    size={4}
                    sx={{
                        position: 'sticky',
                        alignSelf: 'flex-start',
                        top: 75
                    }}>
                    <ActivityFilters filterChanged={onFilterChanged} filterDateChanged={onFilterDateChanged} />
                </Grid>
            </Grid>
        </>
    )
}
