import { Box, Button, Paper, TextField, Typography } from "@mui/material";
import type { FormEvent } from "react";
import { useNavigate, useSearchParams } from "react-router-dom"
import useAccountReactQuery from "../../hooks/useAccountReactQuery";

export default function PasswordReset() {
    const [searchParams] = useSearchParams();
    const code = searchParams.get("code");
    const emailId = searchParams.get("email");
    const { isResettingPassword, resetPassword } = useAccountReactQuery();
    const navigate = useNavigate();

    if (!code) return <></>

    async function onReset(ele: FormEvent<HTMLFormElement>) {
        ele.preventDefault();
        const passwordResetObject = new FormData(ele.currentTarget);
        const resetObject = Object.fromEntries(passwordResetObject.entries()) as Record<string, string>;
        await resetPassword(resetObject, {
            onSuccess: () => {
                navigate('/login');
            }
        });
    }

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
            <Typography gutterBottom variant="h3">
                Reset your password
            </Typography>
            <Box onSubmit={onReset} component='form' sx={{ display: 'flex', flexDirection: 'column', gap: '2', padding: 1 }}
                autoComplete="off">
                <input id='resetCode' name="resetCode" type='hidden' defaultValue={code} />
                <TextField sx={{ marginBottom: 1 }} id='emailId' name="emailId" label='Email'
                    defaultValue={emailId} aria-readonly />
                <TextField sx={{ marginBottom: 1 }} type="password" id='newPassword' name="newPassword" label='New Password'
                    defaultValue="" />
                <Box sx={{ display: "flex", justifyContent: 'right', gap: 3 }}>
                    <Button loading={isResettingPassword} fullWidth size="large"
                        type="submit" color="success"
                        variant="contained">Submit</Button>
                </Box>
            </Box>
        </Paper>
    )
}