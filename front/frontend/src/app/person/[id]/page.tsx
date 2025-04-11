import { PersonPage } from "./PersonPage";

export default async function Page(props: { params: Promise<{ id: number }> }) {
  const { id } = await props.params;

  return <PersonPage id={id} />;
}