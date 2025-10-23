import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { profileDetails, userDetails, userLogin, userLogout, userRegistration } from "../lib/apiHelper";
import { useLoading } from "./appDataContext";


export default function useAccountReactQuery(id?: string) {
    const queryClient = useQueryClient();
    const { loading } = useLoading();

    const { isPending: isLogingIn, mutateAsync: loginUser } = useMutation({
        mutationFn: userLogin,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["user"] });
        },
        onError: (err) => {
            console.error(err);
        }
    });

    const { isPending: isLogingOut, mutateAsync: logoutUser } = useMutation({
        mutationFn: async () => await userLogout(),
        onSuccess: () => {
            queryClient.removeQueries({ queryKey: ["user"] });
            queryClient.removeQueries({ queryKey: ["activities"] });
        },
        onError: (err) => {
            console.error(err);
        }
    });

    const { isPending: isRegistering, mutateAsync: registerUser } = useMutation({
        mutationFn: userRegistration,
        onError: (err) => {
            console.error(err);
        }
    });

    const { isLoading: isUserDataLoading, data: userData } = useQuery({
        queryKey: ["user"],
        queryFn: async () => {
            loading(true);
            try {
                return await userDetails();
            }
            finally {
                loading(false);
            }
        },
        enabled: typeof (id) == "undefined",
        staleTime: 60 * 1000 * 1,
        retry: false
    });

    const { isLoading: isProfileDataLoading, data: profileData } = useQuery({
        queryKey: ["user", id],
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

    return {
        isLogingIn, loginUser,
        isUserDataLoading, userData,
        isLogingOut, logoutUser,
        isRegistering, registerUser,
        isProfileDataLoading, profileData
    };
}
