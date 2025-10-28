import { Avatar, Box, Button, Chip, Divider, Grid, Paper, Stack, Typography } from "@mui/material";
import type { ProfileSchema, UserSchema } from "../../types";
import useAccountReactQuery from "../../hooks/useAccountReactQuery";

type ProfileHeaderProps = {
    isUpdatingFollowing: boolean,
    profileData: ProfileSchema,
    followingUpdate: (data: { targetId: string, isFollowing: boolean }) => void,
    followers: UserSchema[]
}


export default function ProfileHeader({ profileData,
    isUpdatingFollowing, followingUpdate, followers }: ProfileHeaderProps) {

    const { userData } = useAccountReactQuery();
    const isFollowing = followers?.find(e => e.id == userData?.id) ?? false;
    return (
        <Paper elevation={3} sx={{ p: 3 }}>
            <Grid container>
                <Grid size={8}>
                    <Stack direction='row' spacing={3} alignItems='center'>
                        <Avatar sx={{ width: 150, height: 150 }} src={profileData?.imageUrl} />
                        <Box sx={{ display: 'flex', gap: 2, flexDirection: 'column' }}>
                            <Typography variant="h4">{profileData.displayName}</Typography>
                            {
                                isFollowing && <Chip variant="outlined" color="secondary" label="Following" />
                            }
                        </Box>
                    </Stack>
                </Grid>
                <Grid size={4}>
                    <Stack spacing={3} alignItems='center'>
                        <Box sx={{ display: 'flex', gap: 2, justifyContent: 'space-around', width: '100%' }}>
                            <Box textAlign='center'>
                                <Typography variant="h6">Followers</Typography>
                                <Typography variant="h3">{profileData.followersCount}</Typography>
                            </Box>
                            <Box textAlign='center'>
                                <Typography variant="h6">Following</Typography>
                                <Typography variant="h3">{profileData.followingsCount}</Typography>
                            </Box>
                        </Box>
                        <Divider sx={{ width: '100%' }} />
                        {userData?.id != profileData?.id ? <Button loading={isUpdatingFollowing}
                            onClick={() => followingUpdate({ targetId: profileData.id, isFollowing: !isFollowing })}
                            fullWidth variant="outlined" color={isFollowing ? 'error' : 'success'}>
                            {isFollowing ? 'Unfollow' : 'Follow'}
                        </Button> : <></>}
                    </Stack>
                </Grid>
            </Grid>
        </Paper>
    )
}
