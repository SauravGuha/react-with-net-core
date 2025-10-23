import { useQuery, useQueryClient } from "@tanstack/react-query";
import { useLoading } from "./appDataContext";
import { profileDetails, userPhotos } from "../lib/apiHelper";
import type { UserSchema } from "../types";
import { useMemo } from "react";

export default function useProfileReactQuery(id?: string) {
    const queryClient = useQueryClient();
    const { loading } = useLoading();

    const { isLoading: isProfileDataLoading, data: profileData } = useQuery({
        queryKey: ["profile", id],
        queryFn: async () => {
            loading(true);
            try {
                return await profileDetails(id!);
            }
            finally {
                loading(false);
            }
        },
        enabled: !(typeof (id) == "undefined"),
        staleTime: 60 * 1000 * 1,
        retry: false
    });

    const { isLoading: isFetchingPhotos, data: photos } = useQuery({
        queryKey: ["photo", id],
        queryFn: async () => {
            loading(true);
            try {
                return await userPhotos(id!);
            }
            finally {
                loading(false);
            }
        },
        enabled: !(typeof (id) == "undefined"),
        staleTime: 60 * 1000 * 1,
        retry: false
    });

    const isLoggedInUser = useMemo(() => {
        return id === queryClient.getQueryData<UserSchema>(["user"])?.id;
    }, [id, queryClient]);

    return {
        isProfileDataLoading, profileData,
        isFetchingPhotos, photos, isLoggedInUser
    }
}