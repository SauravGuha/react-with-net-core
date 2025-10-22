import { Grid } from "@mui/material";
import ProfileHeader from "./ProfileHeader";
import ProfileContent from "./ProfileContent";
import { useParams } from "react-router-dom";
import useAccountReactQuery from "../../hooks/useAccountReactQuery";
import type { ProfileSchema } from "../../types";



export default function ProfilePage() {
    // const { userData } = useAccountReactQuery();
    const { id } = useParams();
    const { isProfileDataLoading, profileData } = useAccountReactQuery(id!);
    const pd = profileData as ProfileSchema;

    if (isProfileDataLoading) return <></>

    return (
        <Grid container>
            <Grid size={12}>
                <ProfileHeader profileData={pd} />
                <ProfileContent />
            </Grid>
        </Grid>
    )
}
