export interface ApiResponse<T> {
    status: boolean;
    data: T;
    resultCode: number;
    message: string;
}
