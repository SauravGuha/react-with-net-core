import { Box, Paper, Tab, Tabs } from "@mui/material";
import { useState, type SyntheticEvent } from "react";
import type { ProfileSchema, UserSchema } from "../../types";
import ProfilePhotos from "./ProfilePhotos";
import ProfileFollowings from "./ProfileFollowings";
import ProfileEvent from "./ProfileEvent";

type ProfileContentProps = {
    profileData: ProfileSchema
    followers: UserSchema[],
    followings: UserSchema[]
}

export default function ProfileContent({ profileData, followers, followings }: ProfileContentProps) {
    const [value, setValue] = useState(0);
    const tabContent = [
        { label: 'About', content: <div>About</div> },
        { label: 'Photos', content: <ProfilePhotos profileData={profileData} /> },
        { label: 'Events', content: <ProfileEvent userId={profileData.id} /> },
        { label: 'Followers', content: <ProfileFollowings profileData={profileData} followerfollowings={followers} tabIndex={3} /> },
        { label: 'Following', content: <ProfileFollowings profileData={profileData} followerfollowings={followings} tabIndex={4} /> }
    ];

    const handleChange = (_: SyntheticEvent, newValue: number) => {
        setValue(newValue);
    };

    return (
        <Box component={Paper} sx={{ display: 'flex', alignItems: 'flex-start', mt: 2, p: 3, height: 500 }} elevation={3}>
            <Tabs
                orientation="vertical"
                variant="scrollable"
                value={value}
                onChange={handleChange}
                aria-label="Vertical tabs example"
                sx={{ borderRight: 1, height: 450, minWidth: 200 }}
            >
                {tabContent.map((item, index) =>
                    <Tab key={index} label={item.label} sx={{ mr: 3 }} />
                )}
            </Tabs>
            <Box sx={{ flexGrow: 1, p: 3 }}>
                {tabContent[value].content}
            </Box>
        </Box>
    )
}
