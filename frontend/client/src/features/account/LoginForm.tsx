import { Box, Button, Paper, TextField } from "@mui/material";
import { useRef, useState, type FormEvent } from "react";
import { loginObject } from "../../types";
import { ZodError } from "zod";
import { camelCase } from 'lodash';

import useAccountReactQuery from "../../hooks/useAccountReactQuery";
import { Link, useNavigate } from "react-router-dom";
import { AxiosError } from "axios";
import RegisterSuccess from "./RegisterSuccess";

export default function LoginForm({ urlPath }: { urlPath?: string }) {
    const [registrationSuccess, setRegistrationSuccess] = useState(false);
    const { isLogingIn, loginUser } = useAccountReactQuery();
    const [formErrors, setFormErrors] = useState<Record<string, string>>({});
    const userEmail = useRef<HTMLInputElement>(null);
    const navigate = useNavigate();

    async function onSubmit(e: FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const data = new FormData(e.currentTarget);
        const errors: Record<string, string> = {};
        try {
            const loginData = loginObject.parse(Object.fromEntries(data.entries()));
            await loginUser(loginData);
            navigate(urlPath ? urlPath : "/activities");
        }
        catch (err) {
            if (err instanceof AxiosError) {
                if (err.status == 401) {
                    setRegistrationSuccess(true);
                }
            }
            else if (err instanceof ZodError) {
                err.issues.forEach(item => {
                    const { message, path } = item;
                    const fieldName = path[0].toString();
                    errors[fieldName] = message;
                });
                setFormErrors(errors);
            }
            else {
                if (err instanceof Array) {
                    err.forEach((item: string) => {
                        const errorArray = item.split("'");
                        errors[camelCase(errorArray[1])] = errorArray.join("");
                    });
                }
                setFormErrors(errors);
            }
        }
    }

    return (
        <>
            {registrationSuccess
                ? <RegisterSuccess email={userEmail.current?.value} />
                : <Paper>
                    <Box onSubmit={onSubmit} component='form'
                        sx={{ display: 'flex', flexDirection: 'column', gap: '2', padding: 1 }}
                        autoComplete="off">
                        <TextField sx={{ marginBottom: 1 }} id='email' name="email" label='Email'
                            defaultValue="" error={!!formErrors.email} helperText={formErrors.email} inputRef={userEmail} />
                        <TextField sx={{ marginBottom: 1 }} type="password" id='password' name="password" label='Password'
                            defaultValue="" error={!!formErrors.password} helperText={formErrors.password} />
                        <Box sx={{ display: "flex", justifyContent: 'space-evenly', gap: 3 }}>
                            <Button fullWidth size="large" component={Link} to='/' color="warning" variant="contained">Cancel</Button>
                            <Button fullWidth size="large" type="submit" loading={isLogingIn} color="success" variant="contained">Submit</Button>
                        </Box>
                    </Box>
                </Paper>
            }
        </>

    )
}
