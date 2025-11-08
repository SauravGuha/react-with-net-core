import { Avatar, Box, Button, ListItemIcon, ListItemText, Menu, MenuItem } from "@mui/material";
import { useState } from "react";
import type { UserSchema } from "../../types";
import { NavLink, useNavigate } from "react-router-dom";
import useAccountReactQuery from "../../hooks/useAccountReactQuery";
import { Add, Logout, Person } from "@mui/icons-material";


export default function UserMenu({ data }: { data: UserSchema }) {

    const navigate = useNavigate();
    const { logoutUser, userData } = useAccountReactQuery();
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    const onLogout = async () => {
        await logoutUser();
        navigate("/");
    }

    return (
        <>
            <Button
                sx={{ color: 'inherit' }}
                id="basic-button"
                onClick={handleClick}
                size="large"
            >
                <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
                    <Avatar
                        key={userData!.id}
                        alt={userData!.displayName}
                        src={userData!.imageUrl} />
                    {data.displayName}
                </Box>

            </Button>
            <Menu
                id="basic-menu"
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                slotProps={{
                    list: {
                        'aria-labelledby': 'basic-button',
                    },
                }}
            >
                <MenuItem component={NavLink} to='/createactivity'>
                    <ListItemIcon>
                        <Add />
                    </ListItemIcon>
                    <ListItemText>Create Activity</ListItemText>
                </MenuItem>
                <MenuItem component={NavLink} to={`/profile/${userData!.id}`}>
                    <ListItemIcon>
                        <Person />
                    </ListItemIcon>
                    <ListItemText>User Profile</ListItemText>
                </MenuItem>
                <MenuItem onClick={onLogout}>
                    <ListItemIcon>
                        <Logout />
                    </ListItemIcon>
                    <ListItemText>Logout</ListItemText>
                </MenuItem>
            </Menu>
        </>
    )
}
