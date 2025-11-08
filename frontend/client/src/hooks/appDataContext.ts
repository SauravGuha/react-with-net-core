import { createContext, useContext } from "react";
import type { Activity } from "../types";

const ActivityContext = createContext<Activity | undefined>(undefined);

const useActivityContext = function () {
    const activity = useContext(ActivityContext);
    if (activity) {
        return activity;
    }
    else {
        throw new Error("activityContext doesn't have data");
    }
}

const LoadingContext = createContext<{ isLoading: boolean, loading: (value: boolean) => void } | undefined>(undefined);

const useLoading = function () {
    const loadingContext = useContext(LoadingContext);
    if (loadingContext) {
        return loadingContext;
    }
    else {
        throw new Error("loadingContext doesn't have data");
    }
}

const FilterContext = createContext<{
    filterDate: string | undefined,
    changeFilterDate: (value: unknown) => void,
    filterBy: string | undefined,
    changeFilterBy: (value: string) => void
} | undefined>(undefined);

const useFilterContext = () => {
    const context = useContext(FilterContext);
    if (context) {
        return context;
    }
    else {
        throw new Error("filterContext doesn't have data");
    }
}

export { useActivityContext, ActivityContext, useLoading, LoadingContext, useFilterContext, FilterContext };