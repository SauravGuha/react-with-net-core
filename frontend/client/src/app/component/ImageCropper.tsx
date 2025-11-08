import { useRef } from "react";
import Cropper, { type ReactCropperElement } from "react-cropper";
import "cropperjs/dist/cropper.css";

type ImageCropperProps = {
    filePath: string,
    setBlobdata: (value: Blob) => void
}

export default function ImageCropper({ filePath, setBlobdata }: ImageCropperProps) {
    const cropperRef = useRef<ReactCropperElement>(null);
    const onCrop = () => {
        const cropper = cropperRef.current?.cropper;
        cropper?.getCroppedCanvas().toBlob((blob => {
            if (blob)
                setBlobdata(blob);
        }));
    };

    return (
        <Cropper
            src={filePath}//"https://raw.githubusercontent.com/roadmanfong/react-cropper/master/example/img/child.jpg"
            style={{ height: '280px', width: "100%" }}
            aspectRatio={1}
            // Cropper.js options
            initialAspectRatio={16 / 9}
            guides={false}
            crop={onCrop}
            ref={cropperRef}
            preview='.img-preview'
            background={false}
        />
    );
}