import { Box, Button, ImageList, ImageListItem, Typography } from "@mui/material";
import useProfileReactQuery from "../../hooks/useProfileReactQuery";
import { useState } from "react";
import PhotoUploadWidget from "../../app/component/PhotoUploadWidget";


export default function ProfilePhotos({ id }: { id: string }) {
    const { isFetchingPhotos, photos, isLoggedInUser } = useProfileReactQuery(id);
    const [editMode, setEditMode] = useState(false);

    if (isFetchingPhotos) return <></>;

    if (!photos || photos.length <= 0) return <Typography>No photos</Typography>

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
                    ? <PhotoUploadWidget />
                    : <ImageList sx={{ width: 500, height: 450 }} cols={3} rowHeight={164}>
                        {photos!.map((item) => (
                            <ImageListItem key={item.publicId}>
                                <img
                                    srcSet={`${item.url}`}
                                    src={`${item.url}`}
                                    alt={item.publicId}
                                    loading="lazy"
                                />
                            </ImageListItem>
                        ))}
                    </ImageList>
            }
        </Box>
    )
}