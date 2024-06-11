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
        <div>
          <div className="flex items-center justify-between py-3 md:block md:py-5">
            <Link to="/">
              <h2 className="text-2xl font-bold text-white">F-Catalog</h2>
            </Link>
          </div>
        </div>
        <form
          className="mx-auto ml-auto flex items-center md:mr-5"
          onSubmit={form.onSubmit(onSearch)}
        >
          <label htmlFor="simple-search" className="sr-only">
            Search
          </label>
          <div className="relative w-full md:w-64 lg:w-96">
            <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
              <svg
                aria-hidden="true"
                className="h-5 w-5 text-gray-500 dark:text-gray-400"
                fill="currentColor"
                viewBox="0 0 20 20"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  fillRule="evenodd"
                  d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z"
                  clipRule="evenodd"
                ></path>
              </svg>
            </div>
            <input
              type="text"
              className="block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 pl-10 text-sm text-gray-900 focus:outline-none dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-blue-400"
              placeholder="Search"
              {...form.getInputProps("search")}
            />
          </div>
          <button
            type="submit"
            className="ml-2 rounded-lg border border-blue-700 bg-blue-700 p-2.5 text-sm font-medium text-white hover:bg-blue-800 focus:outline-none focus:ring-4 focus:ring-blue-300 dark:bg-blue-500 dark:hover:bg-blue-600 dark:focus:ring-blue-800"
          >
            <svg
              className="h-5 w-5"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"
              ></path>
            </svg>
            <span className="sr-only">Search</span>
          </button>
        </form>

        {!user ? (
          <div>
            <div className="my-3 space-y-2 md:hidden">
              <Link
                to="/login"
                className="inline-block w-full rounded-md bg-gray-600 px-4 py-2 text-center text-white shadow hover:bg-gray-800"
              >
                Sign in
              </Link>
              <Link
                to="/register"
                className="inline-block w-full rounded-md bg-white px-4 py-2 text-center text-gray-800 shadow hover:bg-gray-100"
              >
                Sign up
              </Link>
            </div>
            <div className="hidden space-x-2 md:inline-block">
              <Link
                to="/login"
                className="whitespace-nowrap rounded-md bg-gray-600 px-4 py-2 text-white shadow hover:bg-gray-800"
              >
                Sign in
              </Link>
              <Link
                to="/register"
                className="whitespace-nowrap rounded-md bg-white px-4 py-2 text-gray-800 shadow hover:bg-gray-100"
              >
                Sign up
              </Link>
            </div>
          </div>
        ) : (
          <div className="flex flex-row items-center">
            {user.role === "Administrator" ? (
              <Link to="/admin/films">
                <h4 className="font-bold text-white hover:text-gray-200">
                  Admin
                </h4>
              </Link>
            ) : (
              <Link
                to={`/userprofile`}
                className="text-white hover:text-blue-200"
              >
                {user.username}
              </Link>
            )}
            <button
              onClick={() => logout()}
              className="m-2 whitespace-nowrap rounded-md bg-white p-1 text-gray-800 shadow hover:bg-gray-100"
            >
              <Logout size={24} />
            </button>
          </div>
        )}
      </div>
    </nav>
  );
}
