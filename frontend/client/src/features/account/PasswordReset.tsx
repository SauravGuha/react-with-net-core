import { useParams } from "react-router-dom"

export default function PasswordReset() {
    const resetCode = useParams();
    console.log(resetCode);
    return (
        <div>PasswordReset</div>
    )
}