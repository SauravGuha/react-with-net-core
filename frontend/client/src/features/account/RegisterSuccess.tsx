import { Button, Paper, Typography } from "@mui/material";
import useAccountReactQuery from "../../hooks/useAccountReactQuery"
import { Check } from "@mui/icons-material";


export default function RegisterSuccess({ email }: { email?: string }) {
    const { isSendingReConfirmation, resendConfirmationEmail } = useAccountReactQuery();

    if (!email) return null;

    return (
        <Paper
            sx={{
                height: 400,
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'center',
                alignItems: 'center',
                padding: 5
            }}>
            <Check sx={{ fontSize: 100 }} color="primary" />
            <Typography gutterBottom variant="h3">
                You have successfully registered!
            </Typography>
            <Typography gutterBottom variant="h3">
                Please check your email to confirm your account
            </Typography>
            <Button loading={isSendingReConfirmation} onClick={async () => await resendConfirmationEmail(email)}>
                Re-send confirmation email to
            </Button>
            {email}
        </Paper>
    )
}
