import { Grid, Typography } from "@mui/material";


export default function PhotoUploadWidget() {
    return (
        <Grid container spacing={2}>
            <Grid size={4}>
                <Typography variant="overline" color="secondary">Step 1 - Add photo</Typography>
            </Grid>
            <Grid size={4}>
                <Typography variant="overline" color="secondary">Step 2 - Resize Photo</Typography>
            </Grid>
            <Grid size={4}>
                <Typography variant="overline" color="secondary">Step 1 - Preview and Upload</Typography>
            </Grid>
        </Grid>
    )
}
