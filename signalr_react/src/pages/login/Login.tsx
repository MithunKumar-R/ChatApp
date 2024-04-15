import React, { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import axios from 'axios';
import { useAppDispatch, useAppSelector } from '../../Redux/Store';
import { addCount } from '../../Redux/Slices/LoginSlice';
import { useNavigate } from 'react-router-dom';

interface props {
    connection: signalR.HubConnection | null
}

const Login = ({ connection }: props) => {

    const [notification, setNotification] = useState('');
    // const [count, setCount] = useState(0);

    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const { count } = useAppSelector(state => state.login);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(() => {
                    console.log('signalR Connected');
                    connection.invoke("SendNotification", "Hello from client");
                })
                .catch(err => console.error('SignalR Connection Error: ', err));
            connection.on("ReceiveNotification", (message: string) => {
                console.log('Message::', message);
                setNotification(message);
            })
            connection.on("EntryAdded", (res: any) => {
                console.log("added Id::", res);
                dispatch(addCount(res));
                // setCount(res);
            })
        }
    }, [connection])

    const sendNotification = () => {
        if (connection) {
            connection.invoke("SendNotification", "second notification");
        }
    }

    const addNotification = async () => {
        // await axios.post("https://localhost:44363/AddNotification");
        navigate('/chat');
    }

    return (
        <div>
            <h1>Notification: {notification}</h1>
            <button onClick={sendNotification}>send Message</button>
            <button onClick={addNotification}>Add</button>
            <div>count - </div>
        </div>
    )
}

export default Login;