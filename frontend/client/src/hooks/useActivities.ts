import { useQuery, useQueryClient } from "@tanstack/react-query";



export default function useActivities() {
    //const queryClient = useQueryClient();

    const {isPending, isError, data : activities, error} = useQuery({
        queryKey:["activities"],
        queryFn: ()=>{}
    });

    return {isPending, isError, activities, error};
}