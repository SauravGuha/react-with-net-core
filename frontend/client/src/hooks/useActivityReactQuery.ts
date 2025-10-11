import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { createActivity, deleteActivity, getActivityByid, getallactivities, updateActivity } from "../lib/apiHelper";
import { useLoading } from "./appDataContext";



export default function useActivityReactQuery(id?: string) {
    const { loading } = useLoading();
    const queryClient = useQueryClient();

    const { isPending, isError, data: activities, error } = useQuery({
        queryKey: ["activities"],
        queryFn: async () => {
            loading(true);
            try {
                const result = await getallactivities();
                return result;
            }
            finally {
                loading(false);
            }
        },
        enabled: () => (typeof (id) == "undefined"),
        staleTime: 1 * 1000 * 60
    });

    const { isPending: isGettingActivity, data: activity } = useQuery({
        queryKey: ["activity", id],
        queryFn: async () => {
            loading(true);
            try {
                const result = await getActivityByid(id!);
                return result;
            }
            finally {
                loading(false);
            }
        },
        enabled: () => !(typeof (id) == "undefined"),
        staleTime: 1 * 1000 * 60,
        retry: false
    });

    const { isPending: isUpdating, mutateAsync: activityUpdate } = useMutation({
        mutationFn: updateActivity,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["activities"] });
        },
        onError: (error) => {
            console.error(error);
        }
    });

    const { isPending: isCreating, mutateAsync: activityCreate } = useMutation({
        mutationFn: createActivity,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["activities"] });
        },
        onError: (error) => {
            console.error(error);
        }
    });

    const { isPending: isDeleting, mutateAsync: activityDelete } = useMutation({
        mutationFn: deleteActivity,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["activities"] });
        },
        onError: (error) => {
            console.error(error);
        }
    });

    return {
        isPending, isError, activities, error,
        isUpdating, activityUpdate,
        isCreating, activityCreate,
        isDeleting, activityDelete,
        isGettingActivity, activity
    };
}