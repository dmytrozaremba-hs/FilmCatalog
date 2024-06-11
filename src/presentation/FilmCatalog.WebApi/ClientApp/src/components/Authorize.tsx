import { ReactNode, useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { Container } from "@mantine/core";
import { Role } from "../api/user";

import { isAuthorized, useUser } from "../services/userService";
import { ErrorMessage } from "./common/ErrorMessage";

type ProtectedRouteProps = {
  roles?: Role | Role[];
  children: ReactNode;
};

export function Authorize({ roles, children }: ProtectedRouteProps) {
  const navigate = useNavigate();
  const location = useLocation();
  const user = useUser();

  const [authorized, setAuthorized] = useState(true);

  useEffect(() => {
    if (!user) {
      navigate("/login", { replace: true, state: { from: location } });
    } else {
      setAuthorized(isAuthorized(user, roles));
    }
  }, [location, navigate, user, roles]);

  return authorized ? (
    <>{children}</>
  ) : (
    <Container my={80}>
      <ErrorMessage errors="Not authorized" />
    </Container>
  );
}
