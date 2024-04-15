import { createAsyncThunk } from "@reduxjs/toolkit";
import { ApiResponse } from "../Utilities/ApiResponse";
import axios from "axios";

//add second type argument for specifying type of passing parameter
export const UserLogin = createAsyncThunk<ApiResponse<any>>(
    "Login/Login", async () => {
        try {

        }
        catch (error: any) {
            return error.response.data;
        }
    }
)