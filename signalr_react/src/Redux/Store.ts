import { configureStore } from "@reduxjs/toolkit";
import CombineReducer from './RootReducer';
import { TypedUseSelectorHook, useDispatch } from "react-redux";
import { useSelector } from "react-redux";

export const RESET_STATE = "RESET_STATE";

export const resetState = () => ({
    type: RESET_STATE,
});

const rootReducer = (state: any, action: any) => {
    if (action.type === RESET_STATE) {
        state = undefined;
    }

    return CombineReducer(state, action);
};


const store = configureStore({
    reducer: rootReducer
})

export default store;

export const useAppDispatch = () => useDispatch<typeof store.dispatch>();
export const useAppSelector: TypedUseSelectorHook<ReturnType<typeof store.getState>> = useSelector;