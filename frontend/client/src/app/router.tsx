import { createBrowserRouter, NavLink } from "react-router-dom"
import App from "./layout/App";
import Home from "../features/home/Home";
import Dashboard from "../features/activities/dashboard/Dashboard";
import ActivityDetailsPage from "../features/activities/details/ActivityDetailsPage";
import ActivityForm from "../features/activities/form/ActivityForm";
import NotFound from "./component/NotFound";
import ServerError from "./component/ServerError";


export const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [{
            path: "",
            element: <Home />
        },
        {
            path: "/activities",
            element: <Dashboard />
        },
        {
            path: "/activity/:id",
            element: <ActivityDetailsPage />
        },
        {
            path: "/createactivity",
            element: <ActivityForm key="create" />
        },
        {
            path: "/updateactivity/:id",
            element: <ActivityForm key="update" />
        },
        {
            path: "/notfound",
            element: <NotFound />
        }, {
            path: "/servererror",
            element: <ServerError />
        }, {
            path: "*",
            element: <NavLink replace to='/notfound' />
        }
        ]
    }
]); 