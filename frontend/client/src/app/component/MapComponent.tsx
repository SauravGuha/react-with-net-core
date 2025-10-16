
import { MapContainer, TileLayer, Marker, useMap, Popup } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';

type MapComponentProp = {
    latitude: number,
    longitude: number,
    venue: string
}

export default function MapComponent({ latitude, longitude, venue }: MapComponentProp) {
    return (
        <MapContainer center={[latitude, longitude]}
            zoom={18}
            scrollWheelZoom={false}
            style={{ height: '100%' }} >
            <TileLayer
                attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            <Marker position={[latitude, longitude]}>
                <Popup>
                    {venue}
                </Popup>
            </Marker>
        </MapContainer>
    )
}