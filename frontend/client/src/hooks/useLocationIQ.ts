import { useEffect, useState } from "react";
import type { LocationIQ } from "../types";
import { autoComplete, reverseGeoCoding } from "../lib/locationIQHelper";

// No longer being used
export default function useLocationIQ(lat: number, lon: number) {
    const [isLoading, setIsLoading] = useState(false);
    const [address, setAddress] = useState("");
    const [suggestions, setSuggestions] = useState<LocationIQ[]>([]);
    const [selectedAddress, setSelectedAddress] = useState('');

    useEffect(() => {
        if (address.length < 6) {
            return;
        }
        else {
            const operation = setTimeout(async () => {
                setIsLoading(true);
                try {
                    const suggestionsResponse = await autoComplete(address);
                    setSuggestions(suggestionsResponse);
                }
                finally {
                    setIsLoading(false);
                }
            }, 1000);
            return () => clearTimeout(operation);
        }
    }, [address]);

    useEffect(() => {
        const operation = setTimeout(async () => {
            try {
                setIsLoading(true);
                const locationInfo = await reverseGeoCoding(lat, lon);
                setSelectedAddress(locationInfo.display_name);
            }
            finally {
                setIsLoading(false);
            }
        }, 1000);
        return () => clearTimeout(operation);
    }, [lat, lon]);

    return { isLoading, address, setAddress, suggestions, selectedAddress };
}