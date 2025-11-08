import { Box, Button, Grid, Typography } from "@mui/material";
import FileDropZone from "./FileDropZone";
import ImageCropper from "./ImageCropper";
import { useState } from "react";
import useProfileReactQuery from "../../hooks/useProfileReactQuery";


export default function PhotoUploadWidget({ userId }: { userId: string }) {

    const [imagePreviewUrl, setImagePreviewUrl] = useState('');
    const [imageBlob, setImageBlobdata] = useState<Blob>();
    const { isUploading, profilePhotoUpload } = useProfileReactQuery(userId);

    return (
        <Grid container spacing={2}>
            <Grid size={4}>
                <Box>
                    <Typography variant="overline" color="secondary">Step 1 - Add photo</Typography>
                    <FileDropZone setFilePath={setImagePreviewUrl} />
                </Box>

            </Grid>
            <Grid size={4}>
                <Box>
                    <Typography variant="overline" color="secondary">Step 2 - Resize Photo</Typography>
                    <ImageCropper filePath={imagePreviewUrl} setBlobdata={setImageBlobdata} />
                </Box>
            </Grid>
            <Grid size={4}>
                {
                    imagePreviewUrl && (<>
                        <Typography variant="overline" color="secondary">Step 3 - Preview and Upload</Typography>
                        <div className="img-preview"
                            style={{ width: '100%', height: '280px', overflow: 'hidden' }} />
                        <Button loading={isUploading}
                            onClick={async () => {
                                await profilePhotoUpload(imageBlob!);
                            }}>Upload</Button>
                    </>)
                }
            </Grid>
        </Grid>
    )
}
