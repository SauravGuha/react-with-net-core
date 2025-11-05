import { useInfiniteQuery, useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { activityAttendence, createActivity, deleteActivity, getActivityByid, getAllActivitiesByParam, updateActivity } from "../lib/apiHelper";
import { useLoading } from "./appDataContext";
import useAccountReactQuery from "./useAccountReactQuery";
import { useLocation } from "react-router-dom";
import type { Activity } from "../types";



export default function useActivityReactQuery(id?: string) {
    const { loading } = useLoading();
    const queryClient = useQueryClient();
    const { userData } = useAccountReactQuery();
    const location = useLocation();

    const { isPending, isError, data: activitiesGroup, error, isFetchingNextPage, fetchNextPage, hasNextPage } = useInfiniteQuery({
        queryKey: ["activities"],
        queryFn: async ({ pageParam }: { pageParam?: string }) => {
            loading(true);
            try {
                const result = await getAllActivitiesByParam(pageParam, "3");
                return result;
            }
            finally {
                loading(false);
            }
        },
        initialPageParam: undefined,
        getNextPageParam: (result) => result.cursor,
        enabled: () => (typeof (id) == "undefined") && location.pathname == "/activities" && !!userData,
        staleTime: 1 * 1000 * 60,
        retry: false
    });

    // const { isPending, isError, data: activities, error } = useQuery({
    //     queryKey: ["activities"],
    //     queryFn: async () => {
    //         loading(true);
    //         try {
    //             const result = await getallactivities<{ result: Activity[] }>();
    //             return result;
    //         }
    //         finally {
    //             loading(false);
    //         }
    //     },
    //     enabled: () => (typeof (id) == "undefined") && location.pathname == "/activities" && !!userData,
    //     staleTime: 1 * 1000 * 60,
    //     retry: false
    // });

    const { isPending: isGettingActivity, data: activity } = useQuery({
        queryKey: ["activity", id],
        queryFn: async () => {
            loading(true);
            try {
                const result = await getActivityByid<Activity>(id!);
                return result;
            }
            finally {
                loading(false);
            }
        },
        enabled: () => !(typeof (id) == "undefined") && !!userData,
        staleTime: 1 * 1000 * 60,
        retry: false
    });

    const { isPending: isUpdating, mutateAsync: activityUpdate } = useMutation({
        mutationFn: async (data: Activity) => {
            loading(true)
            try {
                return updateActivity(data)
            }
            finally {
                loading(false);
            };
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ["activities"] });
            await queryClient.invalidateQueries({ queryKey: ["activity", id] });
        },
        onError: (error) => {
            console.error(error);
        }
    });

    const { isPending: isCreating, mutateAsync: activityCreate } = useMutation({
        mutationFn: createActivity,
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ["activities"] });
        },
        onError: (error) => {
            console.error(error);
        }
    });

    const { isPending: isDeleting, mutateAsync: activityDelete } = useMutation({
        mutationFn: deleteActivity,
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ["activities"] });
            await queryClient.invalidateQueries({ queryKey: ["activity", id] });
        },
        onError: (error) => {
            console.error(error);
        }
    });

    const { isPending: isUpdatingAttendee, mutateAsync: addUpdateAttendee } = useMutation({
        mutationFn: activityAttendence,
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ["activity", id] });
        },
        onError: (error) => {
            console.error(error);
        }
    });

    return {
        isPending, isError, activitiesGroup, error, isFetchingNextPage, fetchNextPage, hasNextPage,
        isUpdating, activityUpdate,
        isCreating, activityCreate,
        isDeleting, activityDelete,
        isGettingActivity, activity,
        isUpdatingAttendee, addUpdateAttendee
    };
}