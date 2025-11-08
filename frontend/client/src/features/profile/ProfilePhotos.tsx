import { Box, Button, IconButton, ImageList, ImageListItem, ImageListItemBar } from "@mui/material";
import useProfileReactQuery from "../../hooks/useProfileReactQuery";
import { useState } from "react";
import PhotoUploadWidget from "../../app/component/PhotoUploadWidget";
import { DeleteForeverOutlined, StarBorderOutlined } from "@mui/icons-material";
import type { ProfileSchema } from "../../types";


export default function ProfilePhotos({ profileData }: { profileData: ProfileSchema }) {
    const { isFetchingPhotos, photos, isLoggedInUser, profilePhotoDelete } = useProfileReactQuery(profileData.id);
    const [editMode, setEditMode] = useState(false);

    if (isFetchingPhotos) return <></>;

    return (
        <Box>
            {
                isLoggedInUser && (<Box>
                    <Button onClick={() => setEditMode(!editMode)}>
                        {
                            editMode ? 'Cancel' : 'Add Photo'
                        }
                    </Button>
                </Box>)
            }
            {
                editMode
                    ? <PhotoUploadWidget userId={profileData.id} />
                    : <ImageList sx={{ width: '100%', height: 400 }} cols={3} rowHeight={164}>
                        {photos!.map((item) => (
                            <ImageListItem key={item.publicId}>
                                <img
                                    srcSet={`${item.url}`}
                                    src={item.url}
                                    alt={item.publicId}
                                    loading="lazy"
                                    style={{ width: '100', height: '164', marginBottom: 4 }}
                                />
                                {isLoggedInUser && <ImageListItemBar
                                    sx={{
                                        background:
                                            'linear-gradient(to bottom, rgba(0,0,0,0.7) 0%, ' +
                                            'rgba(0,0,0,0.3) 70%, rgba(0,0,0,0) 100%)',
                                    }}
                                    position="top"
                                    actionIcon={
                                        <>
                                            <IconButton
                                                sx={{ color: 'white' }}
                                                aria-label={`star ${item.publicId}`}
                                                onClick={async () => { await profilePhotoDelete(item.publicId); }}
                                            >
                                                <DeleteForeverOutlined />
                                            </IconButton>
                                            <IconButton
                                                sx={{ color: 'white' }}
                                                aria-label={`star ${item.publicId}`}
                                            >
                                                <StarBorderOutlined />
                                            </IconButton>
                                        </>
                                    }
                                    actionPosition="right"
                                />}
                            </ImageListItem>
                        ))}
                    </ImageList>
            }
        </Box>
    )
}