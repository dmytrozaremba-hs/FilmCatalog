import {
  unstable_HistoryRouter as Router,
  Route,
  Routes,
} from "react-router-dom";
import { MantineProvider, createEmotionCache } from "@mantine/core";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { initUserService } from "./services/userService";
import { jwtInterceptor } from "./helpers/jwtInterceptor";
import { errorInterceptor } from "./helpers/errorInterceptor";
import { customHistory } from "./helpers/history";

import FilmCatalog from "./pages/FilmCatalog";
import FilmDetails from "./pages/FilmDetails";
import Login from "./pages/Login";
import Register from "./pages/Register";
import UserProfile from "./pages/UserProfile";
import PublicLayout from "./PublicLayout";
import AdminLayout from "./AdminLayout";
import Genres from "./pages/admin/Genres";
import Tags from "./pages/admin/Tags";
import Persons from "./pages/admin/Persons";
import Films from "./pages/admin/Films";
// import Films from "./pages/admin/Films";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: (failureCount, error) => {
        // do not retry on Not Found and authorization errors
        try {
          if ([404, 401, 403].includes((error as any).response.status)) {
            return false;
          }
        } finally {
          /* empty */
        }
        return failureCount < 3;
      },
    },
  },
});

function App() {
  initUserService(queryClient);

  jwtInterceptor();
  errorInterceptor();

  const myCache = createEmotionCache({
    key: "mantine",
    prepend: false,
  });
  return (
    <Router history={customHistory}>
      <MantineProvider emotionCache={myCache} withGlobalStyles withNormalizeCSS>
        <QueryClientProvider client={queryClient}>
          <Routes>
            <Route element={<PublicLayout />}>
              <Route path="/" element={<FilmCatalog />} />
              <Route path="/film/:id" element={<FilmDetails />} />
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
              <Route path="/userprofile" element={<UserProfile />} />
            </Route>
            <Route element={<AdminLayout />}>
              <Route path="/admin/genres" element={<Genres />} />
              <Route path="/admin/tags" element={<Tags />} />
              <Route path="/admin/persons" element={<Persons />} />
              <Route path="/admin/films" element={<Films />} />
            </Route>
          </Routes>
        </QueryClientProvider>
      </MantineProvider>
    </Router>
  );
}

export default App;
