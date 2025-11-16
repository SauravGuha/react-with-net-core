import axios from "axios";
import type {
    Activity, ActivityResponse, attendeeSchema, AttendenceSchema,
    EventType,
    LocationIQ,
    LoginSchema, NewActivityResponse, pagedList, PhotoSchema, ProfileSchema, RegistrationSchema, UserSchema
} from "../types";
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
    if (env != "Production") {
        await delayer(1000);
    }
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
            //toast.error("Data already exists");
            toast.error(data.errorMessage ?? data);
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

const getAllActivitiesByParam = async function (cursor?: string, limit?: string, filterBy?: string, filterDate?: string) {
    const result = await instance.get<NewActivityResponse>("activity/getallactivities", {
        params: {
            "cursor": cursor,
            "limit": limit,
            "filterBy": filterBy,
            "filterDate": filterDate
        }
    })
    return result.data.value as pagedList<Activity[], string | null>;
}

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
    const result = await instance.get<ActivityResponse>(`profile/Details/${id}`);
    return Array.isArray(result.data)
        ? result.data.value[0] as ProfileSchema
        : result.data.value;
}

const userPhotos = async function (userId: string) {
    const result = await instance.get<ActivityResponse>(`profile/UserImages/${userId}`);
    return result.data.value as PhotoSchema[];
}

const uploadPhoto = async function (data: Blob) {
    const formdata = new FormData();
    formdata.append("userPhoto", data);
    const result = await instance.post<ActivityResponse>(`profile/UploadPhoto`, formdata, {
        headers: {
            "Content-Type": "multipart/form-data"
        }
    });
    return result.data.value as PhotoSchema[];
}

const deletePhoto = async function (id: string) {
    await instance.delete(`profile/DeleteImage?imageId=${id}`);
}

const updateFollowing = async function (targetId: string, isFollowing: boolean) {
    await instance.post(`profile/FollowersUpdate?targetId=${targetId}&isFollowing=${isFollowing}`);
}

const currentUserFollowers = async function (userid: string) {
    const result = await instance.get<ActivityResponse>(`profile/GetFollowers?userId=${userid}`);
    return result.data.value as UserSchema[];
}

const currentUserFollowing = async function (userid: string) {
    const result = await instance.get<ActivityResponse>(`profile/GetFollowing?userId=${userid}`);
    return result.data.value as UserSchema[];
}

const getUserEvents = async function (userId: string, filter: string) {
    const result = await instance.get<ActivityResponse>(`activity/GetEvents?userid=${userId}&filter=${filter}`);
    return result.data.value as EventType[];
}

const locationInfo = async function (address: string) {
    const result = await instance.get<{ value: LocationIQ[] }>(`LocationIq/GetAutoComplete?address=${address}`);
    return result.data.value;
}

const reverseLocationInfo = async function (lat: number, lon: number) {
    const result = await instance.get<{ value: LocationIQ }>(`LocationIq/GetReverse?latitude=${lat}&longitude=${lon}`);
    return result.data.value;
}

const resendConfirmation = async function (email: string) {
    const result = await instance.get<string>(`activityaccount/ResendConfirmationEmail?email=${email}`);
    return result;
}

export {
    getallactivities, updateActivity, createActivity,
    deleteActivity, getActivityByid, userLogin,
    userLogout, userDetails, userRegistration,
    activityAttendence, profileDetails,
    userPhotos, uploadPhoto,
    deletePhoto, updateFollowing,
    currentUserFollowers, currentUserFollowing,
    getAllActivitiesByParam, getUserEvents,
    locationInfo, reverseLocationInfo,
    resendConfirmation
};