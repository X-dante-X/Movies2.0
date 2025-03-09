import Link from "next/link";

export function Header() {
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
