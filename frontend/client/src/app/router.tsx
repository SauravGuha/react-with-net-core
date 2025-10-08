import { createBrowserRouter } from "react-router-dom"
import App from "./layout/App";
import Home from "../features/home/Home";
import Dashboard from "../features/activities/dashboard/Dashboard";
import ActivityDetail from "../features/activities/details/ActivityDetail";
import ActivityForm from "../features/activities/form/ActivityForm";


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
            element: <ActivityDetail />
        },
        {
            path: "/createactivity",
            element: <ActivityForm key="create" />
        },
        {
            path: "/updateactivity/:id",
            element: <ActivityForm key="update" />
        }
        ]
    }
]); 