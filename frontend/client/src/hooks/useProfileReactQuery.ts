import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useLoading } from "./appDataContext";
import { currentUserFollowers, currentUserFollowing, deletePhoto, profileDetails, updateFollowing, uploadPhoto, userPhotos } from "../lib/apiHelper";
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

    const { isPending: isUploading, mutateAsync: profilePhotoUpload } = useMutation({
        mutationFn: async (data: Blob) => {
            await uploadPhoto(data);
        },
        onError: (err) => {
            console.error(err);
        },
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["profile", id] });
            queryClient.invalidateQueries({ queryKey: ["user"] });
        }
    });

    const { isPending: isDeleting, mutateAsync: profilePhotoDelete } = useMutation({
        mutationFn: async (id: string) => {
            await deletePhoto(id);
        },
        onError: (err) => {
            console.error(err);
        },
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["profile", id] });
            queryClient.invalidateQueries({ queryKey: ["user"] });
        }
    });

    const { isPending: isUpdatingFollowing, mutateAsync: followingUpdate } = useMutation({
        mutationFn: async (data: { targetId: string, isFollowing: boolean }) => {
            await updateFollowing(data.targetId, data.isFollowing);
        }, onError: (err) => {
            console.error(err);
        },
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["profile", id] });
            queryClient.invalidateQueries({ queryKey: ["user"] });
            queryClient.invalidateQueries({ queryKey: ["followers", id] });
        }
    });

    const { isPending: isGettingFollowers, data: followers } = useQuery({
        queryKey: ["followers", id],
        queryFn: async () => {
            loading(true);
            try {
                return await currentUserFollowers(id!);
            }
            finally {
                loading(false);
            }
        },
        enabled: !(typeof (id) == "undefined"),
        staleTime: 60 * 1000 * 1,
        retry: false
    });

    const { isPending: isGettingFollowings, data: followings } = useQuery({
        queryKey: ["followings", id],
        queryFn: async () => {
            loading(true);
            try {
                return await currentUserFollowing(id!);
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
        isFetchingPhotos, photos, isLoggedInUser,
        isUploading, profilePhotoUpload,
        isDeleting, profilePhotoDelete,
        isUpdatingFollowing, followingUpdate,
        isGettingFollowers, followers,
        isGettingFollowings, followings
    }
}