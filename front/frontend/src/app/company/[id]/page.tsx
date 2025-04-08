import { CompanyPage } from "./CompanyPage";

export default async function Page(props: { params: Promise<{ id: number }> }) {
  const { id } = await props.params;

  return <CompanyPage id={id} />;
}
