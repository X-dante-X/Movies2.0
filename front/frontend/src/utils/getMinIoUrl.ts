export function getServerMinIoUrl(path?: string, defaultImage: string = "empty.jpg"): string {
  const host = process.env.NEXT_PUBLIC_DOCKER_ENV ? "nginx" : "localhost";
  return path ? `http://${host}/minio/uploads/${path}` : defaultImage;
}

export function getClientMinIoUrl(path?: string, defaultImage: string = "empty.jpg"): string {
  return path ? `http://localhost/minio/uploads/${path}` : defaultImage;
}