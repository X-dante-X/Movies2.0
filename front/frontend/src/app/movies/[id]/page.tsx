import { MovieElement } from "./MovieElement";

export default async function Page(props: { params: Promise<{ id: number }> }) {
  const { id } = await props.params;

  return <MovieElement id={id} />;
}
