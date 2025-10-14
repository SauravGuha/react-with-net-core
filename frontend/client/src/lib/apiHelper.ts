import axios from "axios";
import type { Activity, ActivityResponse } from "../types";
import { toast } from "react-toastify";
import { router } from "../app/router";

const env = import.meta.env.VITE_ENVIRONMENT;
const instance = axios.create({
    baseURL: import.meta.env.VITE_BASEURL,
    headers: {
        "Content-Type": "application/json"
    }
});

const delayer = function (timeSpan: number) {
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve("delayed");
        }, timeSpan);
    });
}

instance.interceptors.response.use(async (response) => {
    await delayer(1000);
    return response;
}, (error) => {
    const { status, data } = error.response;
    switch (status) {
        case 400:
            if (data.errors) {
                const modelStateErros = [];
                for (const key in data.errors) {
                    if (data.errors[key]) {
                        modelStateErros.push(data.errors[key]);
                    }
                }
                throw modelStateErros.flat();
            }
            else {
                toast.error(error.response.data.title);
            }

            break;
        case 401:
            toast.error("Unauthenticated");
            break;
        case 403:
            toast.error("Unauthorized");
            break;
        case 404:
            router.navigate("/notfound");
            //toast.error(data.errorMessage);
            break;
        default:
            if (env == "Development") {
                router.navigate('/servererror', { state: { error: data } })
            }
            else {
                toast.error(error.message);
            }
            break;
    }
    return Promise.reject(error);
});

const getallactivities = async function () {
    const result = await instance.get<ActivityResponse>("activity/getallactivities");
    return result.data.value;
}


const getActivityByid = async function (id: string) {
    const result = await instance.get<ActivityResponse>(`activity/GetActivityById?id=${id}`);
    return result.data.value.length > 0 ? result.data.value[0] : null;

}

const updateActivity = async function (activity: Activity) {
    const result = await instance.put<ActivityResponse>(`activity/UpdateActivity?id=${activity.id}`, activity);
    return result.data.value.length > 0 ? result.data.value[0] : null;
}

const createActivity = async function (activity: Activity) {
    const result = await instance.post<ActivityResponse>(`activity/CreateActivity`, activity);
    return result.data.value.length > 0 ? result.data.value[0] : null;
}

const deleteActivity = async function (id: string) {
    await instance.delete(`activity/DeleteActivity?id=${id}`);
}

export { getallactivities, updateActivity, createActivity, deleteActivity, getActivityByid };