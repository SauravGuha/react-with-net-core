import { useQuery } from "@tanstack/react-query"
import { locationInfo, reverseLocationInfo } from "../lib/apiHelper";

export default function useLocationIQReactQuery(query?: string, lat?: number, lon?: number) {

    const { isPending: isAutoCompleting, data: locationIqs } = useQuery({
        queryKey: ["autoComplete", query],
        queryFn: async () => {
            const result = await locationInfo(query ?? "");
            console.log(result);
            return result;
        },
        enabled: !!query && !lat && !lon,
        retry: false,
        staleTime: 24 * 60 * 60 * 1000
    });

    const { isPending: isReverseGeoCoding, data: locationIq } = useQuery({
        queryKey: ["reverseGeoCoding", lat, lon],
        queryFn: async () => {
            const result = await reverseLocationInfo(lat ?? 0, lon ?? 0);
            return result;
        },
        enabled: !!lat && !!lon && !query,
        retry: false,
        staleTime: 24 * 60 * 60 * 1000
    });

    return { isAutoCompleting, locationIqs, isReverseGeoCoding, locationIq }
}