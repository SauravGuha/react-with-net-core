import { Grid } from "@mui/material";
import ProfileHeader from "./ProfileHeader";
import ProfileContent from "./ProfileContent";
// import { useParams } from "react-router-dom";
// import useAccountReactQuery from "../../hooks/useAccountReactQuery";



export default function ProfilePage() {
    // const { userData } = useAccountReactQuery();
    // const { id } = useParams();
    // if (userData!.id != id) return <NotFound />
    return (
        <Grid container>
            <Grid size={12}>
                <ProfileHeader />
                <ProfileContent />
            </Grid>
        </Grid>
    )
}
