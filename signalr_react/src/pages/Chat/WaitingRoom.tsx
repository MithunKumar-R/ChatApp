import signalR from '@microsoft/signalr';
import { Button, Grid, TextField } from '@mui/material';
import { useFormik } from 'formik';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

interface props {
    connection: signalR.HubConnection | null
}

const WaitingRoom = ({ connection }: props) => {

    const navigate = useNavigate();

    const initialValues = {
        UserName: '',
        ChatRoom: ''
    }

    const formik = useFormik({
        initialValues: initialValues,
        onSubmit: (values, { resetForm }) => {
            if (connection) {
                connection.on("ConnectRoom", (admin: string, message: string) => {
                    console.log('Message::', message);
                })
                connection.invoke("JoinSpecificChatRoom", values);
            }
            navigate('/chatroom', { state: formik.values.UserName });
        }
    });

    // useEffect(() => {
    //     if (connection) {
    //         connection.start()
    //             .then(() => {
    //                 console.log('signalR Connected');
    //                 connection.invoke("SendNotification", "Hello from client");
    //             })
    //             .catch(err => console.error('SignalR Connection Error: ', err));
    //         connection.on("JoinSpecificChatRoom", (message: string) => {
    //             console.log('Message::', message);
    //         })
    //     }
    // }, [connection])


    return <Grid container sx={{ height: '100vh' }} display={'flex'} justifyContent={'center'} alignItems={'center'} >
        <Grid md={6} display='flex' flexDirection={'column'}>
            <TextField
                label="UserName"
                {...formik.getFieldProps("UserName")}
                sx={{ margin: '10px' }}
            />
            <TextField
                label="ChatRoom"
                {...formik.getFieldProps("ChatRoom")}
                sx={{ margin: '10px' }}
            />
            <Button onClick={() => formik.handleSubmit()} variant='contained' sx={{ width: '3rem', margin: '10px', alignSelf: 'end' }}>
                Submit
            </Button>
        </Grid>
    </Grid>
}

export default WaitingRoom;