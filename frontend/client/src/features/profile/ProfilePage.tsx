import { Grid } from "@mui/material";
import ProfileHeader from "./ProfileHeader";
import ProfileContent from "./ProfileContent";
import { useParams } from "react-router-dom";
import type { ProfileSchema, UserSchema } from "../../types";
import useProfileReactQuery from "../../hooks/useProfileReactQuery";



export default function ProfilePage() {
    // const { userData } = useAccountReactQuery();
    const { id } = useParams();
    const { isProfileDataLoading, profileData } = useProfileReactQuery(id!);
    const pd = profileData as ProfileSchema;
    const { isUpdatingFollowing, followingUpdate, isGettingFollowers, isGettingFollowings,
        followers, followings } = useProfileReactQuery(pd?.id);

    if (isProfileDataLoading || isGettingFollowers || isGettingFollowings) return <></>

    return (
        <Grid container>
            <Grid size={12}>
                <ProfileHeader profileData={pd}
                    isUpdatingFollowing={isUpdatingFollowing}
                    followingUpdate={followingUpdate}
                    followers={followers as UserSchema[]} />
                <ProfileContent profileData={pd}
                    followers={followers as UserSchema[]}
                    followings={followings as UserSchema[]} />
            </Grid>
        </Grid>
    )
}
