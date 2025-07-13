
import { useAuth } from "./AuthProvider";
import { useMemo } from "react";

function RequireRole({ allowedRoles, children }) {
  const { user } = useAuth();
  if (!user) return null; // hoặc loading spinner

  const isInRole = useMemo(() => {
    if (!user) return false;
      return user.roles.some(role => allowedRoles.includes(role));
    }, [user, allowedRoles]);
    if(!isInRole) return null 

  return children; // ✅ Có quyền
}

export default RequireRole;
