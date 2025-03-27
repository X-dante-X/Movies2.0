import Link from "next/link";
import { routes } from '../../src/app/routes'
import { useLogout } from '../../src/stores/userStore';
import { useRouter } from "next/navigation";

export function Header() {
  const router = useRouter();
  const logoutMutation = useLogout();

  const handleLogout = async () => {
    await logoutMutation.mutateAsync();
    router.push(routes.login.pattern); 
  };

  return (
    <header className="bg-gradient-to-r from-indigo-600 to-purple-600 text-white shadow-lg w-screen flex items-center justify-between p-5">
      <nav className="flex space-x-6">
        <Link
          href="/"
          className="text-xl font-semibold hover:underline">
          Main
        </Link>
        <Link
          href="/movies"
          className="text-xl font-semibold hover:underline">
          Movies
        </Link>
        <Link
          href="/admin"
          className="text-xl font-semibold hover:underline">
          Admin
        </Link>
        <Link
          href={routes.login.pattern}
          className="text-xl font-semibold hover:underline">
          Login
        </Link>
        <Link
          href={routes.register.pattern}
          className="text-xl font-semibold hover:underline">
          Register
        </Link>
        <button onClick={handleLogout} className="text-xl font-semibold hover:underline">
          Logout
        </button>
      </nav>
      <div className="flex items-center space-x-2">
        <input
          type="text"
          placeholder="Search..."
          className="px-4 py-2 rounded-md bg-white text-black placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
        />
        <button className="px-4 py-2 bg-indigo-500 text-white rounded-md hover:bg-indigo-700 focus:outline-none">
          <span className="material-icons">search</span> {/* Замените иконку на вашу */}
        </button>
      </div>
    </header>
  );
}
