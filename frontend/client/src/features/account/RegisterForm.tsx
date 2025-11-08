import { Box, Button, Paper, TextField } from "@mui/material";
import { useState, type FormEvent } from "react";
import { registrationObject } from "../../types";
import { ZodError } from "zod";
import { camelCase } from 'lodash';
import { Link, useNavigate } from "react-router-dom";
import useAccountReactQuery from "../../hooks/useAccountReactQuery";


export default function RegisterForm() {
    const [formErrors, setFormErrors] = useState<Record<string, string>>({});
    const { isRegistering, registerUser } = useAccountReactQuery();
    const navigate = useNavigate();

    async function onSubmit(ele: FormEvent<HTMLFormElement>) {
        ele.preventDefault();
        const formData = new FormData(ele.currentTarget);
        const errors: Record<string, string> = {};
        try {
            const userRegistrationData = registrationObject.parse(Object.fromEntries(formData.entries()));
            await registerUser(userRegistrationData);
            navigate("/activities");
        }
        catch (err) {
            if (err instanceof ZodError) {
                err.issues.forEach(item => {
                    const { message, path } = item;
                    const fieldName = path[0].toString();
                    errors[fieldName] = message;
                });
                setFormErrors(errors);
            }
            else {
                debugger;
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
        <Paper>
            <Box onSubmit={onSubmit} component='form'
                sx={{ display: 'flex', flexDirection: 'column', gap: '2', padding: 1 }}
                autoComplete="off">
                <TextField sx={{ marginBottom: 1 }} id='email' name="email" label='Email'
                    defaultValue="" error={!!formErrors.email} helperText={formErrors.email} />

                <TextField sx={{ marginBottom: 1 }} type="password" id='password' name="password" label='Password'
                    defaultValue="" error={!!formErrors.password} helperText={formErrors.password} />

                <TextField sx={{ marginBottom: 1 }} id='displayName' name="displayName" label='Display Name'
                    defaultValue="" error={!!formErrors.password} helperText={formErrors.password} />

                <TextField sx={{ marginBottom: 1 }} id='bio' name="bio" label='Biography'
                    defaultValue="" error={!!formErrors.password} helperText={formErrors.password} />

                <Box sx={{ display: "flex", justifyContent: 'space-evenly', gap: 3 }}>
                    <Button fullWidth size="large" component={Link} to='/' color="warning" variant="contained">Cancel</Button>
                    <Button fullWidth size="large" type="submit" loading={isRegistering} color="success" variant="contained">Submit</Button>
                </Box>
            </Box>
        </Paper>
    )
}
