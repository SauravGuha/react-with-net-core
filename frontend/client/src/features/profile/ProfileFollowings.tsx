import { Box, Divider, Typography } from "@mui/material";
import type { ProfileSchema, UserSchema } from "../../types";
import ProfileCard from "./ProfileCard";

type ProfileFollowingsProps = {
    profileData: ProfileSchema,
    tabIndex: number,
    followerfollowings: UserSchema[],
}

export default function ProfileFollowings({ profileData, tabIndex, followerfollowings }: ProfileFollowingsProps) {
    const followersCount = followerfollowings.length;

    return (
        <Box>
            <Box sx={{ display: 'flex' }}>
                <Typography variant="h5">
                    {tabIndex == 3
                        ? `${profileData.displayName} is followed by:`
                        : `${profileData.displayName} is following:`}
                </Typography>
            </Box>
            <Divider sx={{ my: 2 }} />
            <Box display='flex' marginTop={3} gap={2}>
                {
                    followerfollowings.map(item =>
                        <ProfileCard key={item.id} userData={item} followersCount={followersCount} />
                    )
                }
            </Box>
        </Box>
    )
}