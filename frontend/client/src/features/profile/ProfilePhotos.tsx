import { ImageList, ImageListItem, Typography } from "@mui/material";
import useProfileReactQuery from "../../hooks/useProfileReactQuery";
import useAccountReactQuery from "../../hooks/useAccountReactQuery";

export default function ProfilePhotos({ id }: { id: string }) {
    const { isFetchingPhotos, photos } = useProfileReactQuery(id);
    const {userData} = useAccountReactQuery();

    if (isFetchingPhotos) return <></>;
    debugger;
    if (!photos || photos.length <= 0) return <Typography>No photos</Typography>

    return (
        <ImageList sx={{ width: 500, height: 450 }} cols={3} rowHeight={164}>
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
    )
}