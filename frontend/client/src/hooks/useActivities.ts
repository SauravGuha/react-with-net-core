import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { createActivity, deleteActivity, getActivityByid, getallactivities, updateActivity } from "../lib/apiHelper";



export default function useActivities(id?: string) {
    const queryClient = useQueryClient();

    const { isPending, isError, data: activities, error } = useQuery({
        queryKey: ["activities"],
        queryFn: getallactivities,
        enabled: () => (typeof (id) == "undefined")
    });

    const { isPending: isGettingActivity, data: activity } = useQuery({
        queryKey: ["activity"],
        queryFn: async () => await getActivityByid(id!),
        enabled: () => !(typeof (id) == "undefined")
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