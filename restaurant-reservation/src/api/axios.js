import axios from "axios";

const instance = axios.create({
  baseURL: "http://localhost:5111/api", 
});

export default instance;
