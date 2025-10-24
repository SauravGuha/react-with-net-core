import { Avatar, Box, Button, Chip, Divider, Grid, Paper, Stack, Typography } from "@mui/material";
import type { ProfileSchema } from "../../types";
import useAccountReactQuery from "../../hooks/useAccountReactQuery";


export default function ProfileHeader({ profileData }: { profileData: ProfileSchema }) {
    const isFollowing = true;
    const { userData } = useAccountReactQuery();
    return (
        <Paper elevation={3} sx={{ p: 3 }}>
            <Grid container>
                <Grid size={8}>
                    <Stack direction='row' spacing={3} alignItems='center'>
                        <Avatar sx={{ width: 150, height: 150 }} src={userData?.imageUrl} />
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
                                <Typography variant="h3">5</Typography>
                            </Box>
                            <Box textAlign='center'>
                                <Typography variant="h6">Following</Typography>
                                <Typography variant="h3">42</Typography>
                            </Box>
                        </Box>
                        <Divider sx={{ width: '100%' }} />
                        <Button fullWidth variant="outlined" color={isFollowing ? 'error' : 'success'}>
                            {isFollowing ? 'Unfollow' : 'Follow'}
                        </Button>
                    </Stack>
                </Grid>
            </Grid>
        </Paper>
    )
}
