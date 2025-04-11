export function getServerMinIoUrl(path?: string, defaultImage: string = "empty.jpg"): string {
  const host = process.env.NEXT_PUBLIC_DOCKER_ENV ? "nginx" : "localhost";
  return path ? `http://${host}/minio/uploads/${path}` : defaultImage;
}

export function getClientMinIoUrl(path?: string, defaultImage: string = "empty.jpg"): string {
  return path ? `http://localhost/minio/uploads/${path}` : defaultImage;
}

export function getBackgroundImage(srcSet = '') {
  const imageSet = srcSet
    .split(', ')
    .map(str => {
      const [url, dpi] = str.split(' ')
      return `url("${url}") ${dpi}`
    })
    .join(', ')
  return `image-set(${imageSet})`
}

