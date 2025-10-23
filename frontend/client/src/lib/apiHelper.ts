import axios from "axios";
import type { Activity, ActivityResponse, attendeeSchema, AttendenceSchema, LoginSchema, PhotoSchema, ProfileSchema, RegistrationSchema, UserSchema } from "../types";
import { toast } from "react-toastify";
import { router } from "../app/routes/router";


const env = import.meta.env.VITE_ENVIRONMENT;
const instance = axios.create({
    baseURL: import.meta.env.VITE_BASEURL,
    headers: {
        "Content-Type": "application/json"
    },
    withCredentials: true
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
        case 409:
            toast.error("Data already exists");
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

const getallactivities = async function <T>() {
    const result = await instance.get<ActivityResponse>("activity/getallactivities");
    return result.data.value as T;
}


const getActivityByid = async function <T>(id: string) {
    const result = await instance.get<ActivityResponse>(`activity/GetActivityById?id=${id}`);
    return result.data.value.length > 0 ? result.data.value[0] as T : null;

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

const userLogin = async function (value: LoginSchema) {
    await instance.post('activityaccount/login', value);
}

const userLogout = async function () {
    await instance.post('activityaccount/logout');
}

const userDetails = async function () {
    const result = await instance.get<UserSchema>('activityaccount/UserDetail');
    return result.data;
}

const userRegistration = async function (value: RegistrationSchema) {
    await instance.post('activityaccount/registeruser', value);
}

const activityAttendence = async function (data: AttendenceSchema) {
    const result = await instance.put<ActivityResponse>("attendee/CreateUpdateAttendee", data);
    return Array.isArray(result.data)
        ? result.data.value[0] as attendeeSchema
        : result.data.value
}

const profileDetails = async function (id: string) {
    const result = await instance.get<ActivityResponse>(`activityaccount/ProfileDetails/${id}`);
    return Array.isArray(result.data)
        ? result.data.value[0] as ProfileSchema
        : result.data.value
}

const userPhotos = async function (userId: string) {
    const result = await instance.get<ActivityResponse>(`activityaccount/UserImages/${userId}`);
    return result.data.value as PhotoSchema[];
}

export {
    getallactivities, updateActivity, createActivity,
    deleteActivity, getActivityByid, userLogin,
    userLogout, userDetails, userRegistration,
    activityAttendence, profileDetails,
    userPhotos
};