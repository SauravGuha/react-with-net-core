
import Popover from '@mui/material/Popover';
import { useState } from 'react';
import type { UserSchema } from '../../types';
import { Avatar } from '@mui/material';
import { Link } from 'react-router-dom';
import ProfileCard from '../../features/profile/ProfileCard';

export default function AvatarPopover({ userData }: { userData: UserSchema }) {
    const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);

    const handlePopoverOpen = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handlePopoverClose = () => {
        setAnchorEl(null);
    };

    const open = Boolean(anchorEl);

    return (
        <div>
            <Avatar
                key={userData.id}
                alt={userData.displayName}
                src={userData.imageUrl}
                component={Link}
                to={`/profile/${userData.id}`}
                onMouseEnter={handlePopoverOpen}
                onMouseLeave={handlePopoverClose} />
            <Popover
                id="mouse-over-popover"
                sx={{ pointerEvents: 'none' }}
                open={open}
                anchorEl={anchorEl}
                anchorOrigin={{
                    vertical: 'bottom',
                    horizontal: 'left',
                }}
                transformOrigin={{
                    vertical: 'top',
                    horizontal: 'left',
                }}
                onClose={handlePopoverClose}
                disableRestoreFocus
            >
                <ProfileCard userData={userData} />
            </Popover>
        </div>
    );
}
