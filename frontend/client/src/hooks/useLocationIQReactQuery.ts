import { useQuery } from "@tanstack/react-query"
import { autoComplete, reverseGeoCoding } from "../lib/locationIQHelper";

export default function useLocationIQReactQuery(query?: string, lat?: number, lon?: number) {

    const { isPending, isError, data: locationIqs } = useQuery({
        queryKey: ["autoComplete", query],
        queryFn: async () => {
            const result = await autoComplete(query ?? "");
            return result;
        },
        enabled: !!query,
        retry: false
    });

    const { data: locationIq } = useQuery({
        queryKey: ["reverseGeoCoding", lat, lon],
        queryFn: async () => {
            const result = await reverseGeoCoding(lat ?? 0, lon ?? 0);
            return result;
        },
        enabled: !!lat && !!lon,
        retry: false
    });

    return { isPending, isError, locationIqs, locationIq }
}