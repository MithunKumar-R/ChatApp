import { createTheme } from "@mui/material/styles";

const theme = createTheme({
    palette: {
        primary: {
            main: "#F54749",
            light: "#fffbf2",
        },
        secondary: {
            main: "#FFEECA",
            light: "#ffffff"
        },
        background: {
            default: "#733e35",
            paper: "#faf2de",
        },
        text: {
            primary: "#282F79",
            secondary: "#9BA8BB",
        },
    },
    typography: {
        fontFamily: "Poppins, sans-serif"
    },
    components: {},
});
export default theme;
