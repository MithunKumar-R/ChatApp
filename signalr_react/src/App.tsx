import './App.css';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from './pages/login/Login';
import { Provider } from 'react-redux';
import store from './Redux/Store';
import { ThemeProvider } from "@mui/material";
import theme from './theme/theme';
import WaitingRoom from './pages/Chat/WaitingRoom';
import { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import ChatRoom from './pages/Chat/ChatRoom';

function App() {

  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("http://192.168.29.197:5102/notificationHub", {
        withCredentials: true
      })
      .configureLogging(signalR.LogLevel.Information)
      .build();
    setConnection(newConnection);
  }, []);

  return (
    <ThemeProvider theme={theme}>
      <Provider store={store}>
        <Router>
          <Routes>
            <Route path='/' element={<Login connection={connection} />} />
            <Route path='/chat' element={<WaitingRoom connection={connection} />} />
            <Route path='/chatroom' element={<ChatRoom connection={connection} />} />
          </Routes>
        </Router>
      </Provider>
    </ThemeProvider>
  );
}

export default App;
