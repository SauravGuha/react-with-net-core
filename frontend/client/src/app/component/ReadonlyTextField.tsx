import { TextField } from "@mui/material";
function toPascalCase(value: string) {
    if (!value) return;
    else {
        return value.charAt(0).toLocaleUpperCase() + value.slice(1);
    }
}

type ReadonlyTextFieldProps = {
    name: string,
    defaultValue: string | number | undefined,
    error: boolean,
    helperText: string,
    inputRef?: React.Ref<HTMLInputElement>
}


export default function ReadonlyTextField({ name, defaultValue, error, helperText, inputRef }: ReadonlyTextFieldProps) {
    return (
        <TextField sx={{ marginBottom: 1 }} id={name} name={name} label={toPascalCase(name)} variant="outlined"
            defaultValue={defaultValue}
            error={error} helperText={helperText}
            inputRef={inputRef} slotProps={{ input: { readOnly: true, style: { backgroundColor: '#f5f5f5' } } }} />
    )
}
