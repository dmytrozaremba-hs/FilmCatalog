import axios from "axios";

import { logout } from "../services/userService";

export function errorInterceptor() {
  axios.interceptors.response.use(undefined, (error) => {
    const { response } = error;
    if (!response) {
      // network error
      // console.error(error);
      return Promise.reject(error);
    }

    if ([401].includes(response.status)) {
      // logout if 401
      logout();
    }

    //const errorMessage = response.data?.message || response.statusText;
    //console.error("ERROR:", errorMessage);
    return Promise.reject(error);
  });
}
