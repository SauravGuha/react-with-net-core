import { Link } from "react-router-dom";
import type { UserSchema } from "../../types";
import { Box, Card, CardContent, CardMedia, Chip, Divider, Typography } from "@mui/material";
import { Person } from "@mui/icons-material";


export default function ProfileCard({ userData, followersCount = 0 }: { userData: UserSchema, followersCount: number | undefined }) {
    const following = false;

    return (
        <Link to={`/profile/${userData.id}`} style={{ textDecoration: 'none' }}>
            <Card sx={{ p: 3, maxWidth: 300, textDecoration: 'none' }} elevation={4}>
                <CardMedia component='img'
                    src={userData.imageUrl || '/images/user.png'}
                    sx={{ width: 200, zIndex: 50 }}
                    alt={userData.displayName + 'image'} />

                <CardContent>
                    <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                        <Typography variant="h5">{userData.displayName}</Typography>
                        {following && <Chip size='small' label='Following' color="secondary" variant="outlined" />}
                    </Box>
                </CardContent>
                <Divider sx={{ mb: 2 }} />
                <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'start' }}>
                    <Person />
                    <Typography sx={{ ml: 1 }}>{followersCount} Followers</Typography>
                </Box>
            </Card>
        </Link>
    )
}
