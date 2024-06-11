import { Link, useNavigate, useSearchParams } from "react-router-dom";
import { logout, useUser } from "../services/userService";
import { Logout } from "tabler-icons-react";
import { useForm } from "@mantine/form";

export default function NavBar() {
  const user = useUser();

  const [searchParams] = useSearchParams();

  const search = searchParams.get("search")?.trim() ?? "";

  const form = useForm({
    initialValues: {
      search: search,
    },
  });

  const navigate = useNavigate();

  const onSearch = ({ search }: typeof form.values) => {
    navigate(`/?search=${encodeURIComponent(search)}`);
  };

  return (
    <nav className="sticky top-0 z-50 w-full bg-blue-700 opacity-90">
      <div className="container mx-auto justify-between px-4 md:flex md:items-center md:px-8">
        <div className="flex items-center justify-between py-3 md:block md:py-5">
          <Link to="/">
            <h2 className="text-2xl font-bold text-white">F-Catalog</h2>
          </Link>
        </div>
        <div className="flex">
          <div className="flex flex-row justify-end self-center">
            <Link to="/admin/films">
              <h4 className="mr-4 font-bold text-white hover:text-gray-200">
                Films
              </h4>
            </Link>
            <Link to="/admin/persons">
              <h4 className="mr-4 font-bold text-white hover:text-gray-200">
                Persons
              </h4>
            </Link>
            <Link to="/admin/genres">
              <h4 className="mr-4 font-bold text-white hover:text-gray-200">
                Genres
              </h4>
            </Link>
            <Link to="/admin/tags">
              <h4 className="mr-4 font-bold text-white hover:text-gray-200">
                Tags
              </h4>
            </Link>
          </div>

          <button
            onClick={() => logout()}
            className="m-2 whitespace-nowrap rounded-md bg-white p-1 text-gray-800 shadow hover:bg-gray-100"
          >
            <Logout size={24} />
          </button>
        </div>
      </div>
    </nav>
  );
}
