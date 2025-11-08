import { useQuery } from "@tanstack/react-query";
import { useLoading } from "./appDataContext";
import { getUserEvents } from "../lib/apiHelper";


export function useProfileEventReactQuery(userId: string, filterBy: string) {

    const { loading } = useLoading();

    const { isPending: isFetching, data: events } = useQuery({
        queryKey: ['events', userId, filterBy],
        queryFn: async () => {
            loading(true);
            try {
                const result = await getUserEvents(userId, filterBy);
                return result;
            }
            finally {
                loading(false);
            }
        },
        enabled: !(typeof (userId) == "undefined" && typeof (filterBy) == "undefined"),
        staleTime: 60 * 1000 * 1,
        retry: false
    });

    return { isFetching, events };

}