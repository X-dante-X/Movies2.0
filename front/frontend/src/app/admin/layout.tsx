import Link from "next/link";

export default function layout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <>
      <nav className="w-screen flex items-center justify-between p-5">
        <Link
          href="/admin/movie"
          className="text-xl font-semibold hover:underline">
          Add Movie
        </Link>
        <Link
          href="/admin/genre"
          className="text-xl font-semibold hover:underline">
          Add Genre
        </Link>
        <Link
          href="/admin/tag"
          className="text-xl font-semibold hover:underline">
          Add Tag
        </Link>
        <Link
          href="/admin/productionCompany"
          className="text-xl font-semibold hover:underline">
          Add Production Company
        </Link>
        <Link
          href="/admin/person"
          className="text-xl font-semibold hover:underline">
          Add Person
        </Link>
      </nav>
      {children}
    </>
  );
}
