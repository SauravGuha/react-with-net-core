import { Grid } from "@mui/material";
import ProfileHeader from "./ProfileHeader";
import ProfileContent from "./ProfileContent";
import { useParams } from "react-router-dom";
import type { ProfileSchema } from "../../types";
import useProfileReactQuery from "../../hooks/useProfileReactQuery";



export default function ProfilePage() {
    // const { userData } = useAccountReactQuery();
    const { id } = useParams();
    const { isProfileDataLoading, profileData } = useProfileReactQuery(id!);
    const pd = profileData as ProfileSchema;

    if (isProfileDataLoading) return <></>

    return (
        <Grid container>
            <Grid size={12}>
                <ProfileHeader profileData={pd} />
                <ProfileContent profileData={pd} />
            </Grid>
        </Grid>
    )
}
