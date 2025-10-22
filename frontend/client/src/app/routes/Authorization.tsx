import { Outlet, useLocation } from "react-router-dom";
import useAccountReactQuery from "../../hooks/useAccountReactQuery"
import LoginForm from "../../features/account/LoginForm";


export default function Authorization() {
    const { pathname } = useLocation();
    const { isUserDataLoading, userData } = useAccountReactQuery();
    if (isUserDataLoading) return <>Loading data...</>
    if (!isUserDataLoading && !userData) return <LoginForm urlPath={pathname} />
    return <Outlet />
}
