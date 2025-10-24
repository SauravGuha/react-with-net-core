import { CloudUpload } from "@mui/icons-material";
import { Box, Typography } from "@mui/material";
import { useCallback } from "react";
import { useDropzone } from 'react-dropzone'

type FileDropZoneProps = {
    setFilePath: (path: string) => void;
}


export default function FileDropZone({ setFilePath }: FileDropZoneProps) {
    const onDrop = useCallback((acceptedFiles: File[]) => {
        const fileInfo = acceptedFiles[0];
        const previewUrl = URL.createObjectURL(fileInfo);
        setFilePath(previewUrl);
    }, [])
    const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop })

    return (
        <Box {...getRootProps()}
            sx={{
                border: 'dashed 3px #eee',
                borderColor: isDragActive ? 'green' : '#eee',
                pt: '30px',
                textAlign: 'center',
                height: '280px'
            }}>
            <input {...getInputProps()} />
            <CloudUpload sx={{ fontSize: 80 }} />
            <Typography variant="h5">Drop image here...</Typography>
        </Box>
    )
}