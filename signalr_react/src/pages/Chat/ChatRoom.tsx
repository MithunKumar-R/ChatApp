import { Avatar, Button, Grid, IconButton, TextField } from '@mui/material';
import { useFormik } from 'formik';
import React, { useEffect, useRef, useState } from 'react';
import { useLocation } from 'react-router-dom';
import SendIcon from '@mui/icons-material/Send';
import { blue } from '@mui/material/colors';
interface props {
    connection: signalR.HubConnection | null
}

interface chat {
    message: string;
    userName: string;
}
const ChatRoom = ({ connection }: props) => {

    const [chatData, setChatData] = useState<chat[]>([{ message: '', userName: '' }]);
    const messagesEndRef = useRef<HTMLDivElement>(null);

    const location = useLocation();

    const userName = location.state;

    useEffect(() => {
        scrollToBottom();
    }, [chatData.length]);

    const scrollToBottom = () => {
        if (messagesEndRef.current) {
            messagesEndRef.current.scrollIntoView({ behavior: "smooth" });
        }
    };

    useEffect(() => {
        if (connection) {
            connection.on("RecieveSpecificMessage", (admin: string, message: string) => {
                setChatData((prev: any) => [
                    ...prev,
                    {
                        message: message,
                        userName: admin
                    }
                ]);
            });
        }

        return () => {
            if (connection) {
                connection.off("RecieveSpecificMessage");
            }
        };
    }, []);

    const formik = useFormik({
        initialValues: { message: '' },
        onSubmit: (values, { resetForm }) => {
            console.log('first')
            if (connection) {
                // connection.on("RecieveSpecificMessage", (admin: string, message: string) => {
                //     setMessages(prev => [...prev, message])
                // })
                connection.invoke("SendMessage", values.message);
            }
            resetForm();
        }
    });

    const chatBubbleStyle = { backgroundColor: '#fff', borderRadius: '10px', padding: '10px', margin: '10px', display: 'flex', alignItems: 'baseline' };

    return <Grid container display={'flex'} justifyContent={'center'}>
        <Grid display={'flex'} alignItems={'flex-end'} md={10} sx={{ backgroundColor: '#77e78e', borderRadius: '10px' }}>
            <Grid md={12}>
                <Grid display={'flex'} flexDirection={'column'} alignItems={'flex-end'} sx={{ overflowY: 'auto', height: '88vh' }}>
                    {chatData.length > 0 && chatData.map((data: chat, index: number) => {
                        return <Grid container display={'flex'} justifyContent={data.userName === userName ? 'end' : 'start'}>
                            {data.message !== '' && <span style={chatBubbleStyle}>
                                {userName !== data.userName && <Avatar sx={{ backgroundColor: '#77e78e', color: '#FFF', width: 24, height: 24, fontSize: '14px' }}>{data.userName.slice(0, 1)}</Avatar>}
                                &nbsp;
                                {data.message}
                            </span>}
                        </Grid>
                    })}
                    <div ref={messagesEndRef} />
                </Grid>
                <Grid display={'flex'} justifyContent={'center'}>
                    <form onSubmit={(e) => {
                        e.preventDefault()
                        formik.handleSubmit()
                    }}
                        style={{ display: 'flex', alignItems: 'center', width: '100%' }}
                    >
                        <TextField
                            {...formik.getFieldProps("message")}
                            sx={{ margin: '10px' }}
                            fullWidth
                        />
                        {/* <Button type='submit' variant='contained' sx={{ width: '3rem', margin: '10px', alignSelf: 'end' }}>
                            Submit
                        </Button> */}
                        <IconButton type='submit'>
                            <SendIcon sx={{ color: '#000' }} />
                        </IconButton>
                    </form>
                </Grid>
            </Grid>
        </Grid>
    </Grid >
}

export default ChatRoom;