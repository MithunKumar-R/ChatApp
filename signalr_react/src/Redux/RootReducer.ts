import { combineReducers } from "@reduxjs/toolkit";
import { loginReducer } from "./Slices/LoginSlice";

export default combineReducers({
    login: loginReducer
})