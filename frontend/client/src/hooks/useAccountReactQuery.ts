import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { forgotPassword, getCsrfToken, passwordReset, resendConfirmation, userDetails, userLogin, userLogout, userRegistration } from "../lib/apiHelper";
import { useLoading } from "./appDataContext";
import type { LoginSchema } from "../types";


export default function useAccountReactQuery() {
    const queryClient = useQueryClient();
    const { loading } = useLoading();

    const { isPending: isLogingIn, mutateAsync: loginUser } = useMutation({
        mutationFn: async (data: LoginSchema) => {
            return await userLogin(data);
        },
        onSuccess: async () => {
            queryClient.invalidateQueries({ queryKey: ["user"] });
            const tokenData = await getCsrfToken();
            localStorage.setItem("tokenData", JSON.stringify(tokenData));
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
            localStorage.removeItem("tokenData");
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

    const { isPending: isSendingReConfirmation, mutateAsync: resendConfirmationEmail } = useMutation({
        mutationFn: async (email: string) => await resendConfirmation(email),
        onError: (err) => {
            console.log(err);
        }
    });

    const { isPending: isSendingPasswordReset, mutateAsync: forgotPasswordRequest } = useMutation({
        mutationFn: async (email: string) => await forgotPassword(email),
        onError: (err) => {
            console.log(err);
        }
    });

    const { isPending: isResettingPassword, mutateAsync: resetPassword } = useMutation({
        mutationFn: async (resetBody: Record<string, string>) => await passwordReset(resetBody),
        onError: (err) => {
            console.log(err);
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
        staleTime: 60 * 1000 * 1,
        retry: false
    });

    return {
        isLogingIn, loginUser,
        isUserDataLoading, userData,
        isLogingOut, logoutUser,
        isRegistering, registerUser,
        isSendingReConfirmation, resendConfirmationEmail,
        isSendingPasswordReset, forgotPasswordRequest,
        isResettingPassword, resetPassword
    };
}
