import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { getallactivities, updateActivity } from "../lib/apiHelper";



export default function useActivities() {
    const queryClient = useQueryClient();

    const {isPending, isError, data : activities, error} = useQuery({
        queryKey:["activities"],
        queryFn: getallactivities
    });

    const {isPending:isUpdating, mutate: activityUpdate } = useMutation({
        mutationFn: updateActivity,
        onSuccess: ()=>{
            queryClient.invalidateQueries({queryKey:["activities"]});
        },
        onError:(error)=>{
            console.error(error);
        }
    });

    return {isPending, isError, activities, error, isUpdating, activityUpdate};
}