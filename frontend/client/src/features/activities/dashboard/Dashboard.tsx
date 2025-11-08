
import { Grid } from "@mui/material"
import ActivityList from "./ActivityList"
import ActivityFilters from "./ActivityFilters"
import { useState } from "react"
import { eventDateInUtcFormat } from "../../../lib/common";
import { FilterContext } from "../../../hooks/appDataContext";


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

    function onFilterDateChanged(value: unknown) {
        const selectedFilterDate = eventDateInUtcFormat(value as string);
        setFilterDate(selectedFilterDate);
    }

    return (
        <>
            <FilterContext.Provider
                value={{
                    filterBy, changeFilterBy: onFilterChanged,
                    filterDate, changeFilterDate: onFilterDateChanged
                }}>
                <Grid container spacing={2}>
                    <Grid size={8}>
                        <ActivityList />
                    </Grid>
                    <Grid
                        size={4}
                        sx={{
                            position: 'sticky',
                            alignSelf: 'flex-start',
                            top: 75
                        }}>
                        <ActivityFilters />
                    </Grid>
                </Grid>
            </FilterContext.Provider>
        </>
    )
}
