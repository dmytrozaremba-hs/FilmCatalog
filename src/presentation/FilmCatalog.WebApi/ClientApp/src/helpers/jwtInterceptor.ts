import axios from "axios";
import { isApiUrl } from "../api/router";

import { currentUser } from "../services/userService";

export function jwtInterceptor() {
  axios.interceptors.request.use((request) => {
    // add auth header with jwt if account is logged in and request is to the api url
    const user = currentUser;
    const isLoggedIn = !!user;

    if (isLoggedIn && isApiUrl(request.url)) {
      if (request.headers) {
        request.headers.Authorization = `Bearer ${user.token}`;
      }
    }

    return request;
  });
}
