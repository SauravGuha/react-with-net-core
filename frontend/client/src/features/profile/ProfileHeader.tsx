import { Avatar, Box, Button, Chip, Divider, Grid, Paper, Stack, Typography } from "@mui/material";
import type { ProfileSchema } from "../../types";
import useProfileReactQuery from "../../hooks/useProfileReactQuery";
import useAccountReactQuery from "../../hooks/useAccountReactQuery";


export default function ProfileHeader({ profileData }: { profileData: ProfileSchema }) {

    const { userData } = useAccountReactQuery();
    const { isUpdatingFollowing, followingUpdate, followers } = useProfileReactQuery(profileData.id);
    const isFollowing = followers?.find(e => e.id == userData?.id);

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
                        <Button loading={isUpdatingFollowing} onClick={() => followingUpdate({ targetId: profileData.id, isFollowing: !isFollowing })} fullWidth variant="outlined" color={isFollowing ? 'error' : 'success'}>
                            {isFollowing ? 'Unfollow' : 'Follow'}
                        </Button>
                    </Stack>
                </Grid>
            </Grid>
        </Paper>
    )
}
