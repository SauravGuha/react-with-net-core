import { useQuery } from "@tanstack/react-query"
import { autoComplete, reverseGeoCoding } from "../lib/locationIQHelper";

export default function useLocationIQReactQuery(query?: string, lat?: number, lon?: number) {

    const { isPending: isAutoCompleting, data: locationIqs } = useQuery({
        queryKey: ["autoComplete", query],
        queryFn: async () => {
            const result = await autoComplete(query ?? "");
            return result;
        },
        enabled: !!query,
        retry: false,
        staleTime: 24 * 60 * 60 * 1000
    });

    const { isPending: isReverseGeoCoding, data: locationIq } = useQuery({
        queryKey: ["reverseGeoCoding", lat, lon],
        queryFn: async () => {
            const result = await reverseGeoCoding(lat ?? 0, lon ?? 0);
            return result;
        },
        enabled: !!lat && !!lon,
        retry: false,
        staleTime: 24 * 60 * 60 * 1000
    });

    return { isAutoCompleting, locationIqs, isReverseGeoCoding, locationIq }
}