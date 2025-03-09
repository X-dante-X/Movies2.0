import Image from "next/image";

interface VideoCardProps {
  title: string;
  description: string;
  ReleaseDate: string;
  avatarUrl?: string;
}

export function Card({ title, description, avatarUrl, ReleaseDate }: VideoCardProps) {
  return (
    <div className="max-w-xs rounded-lg overflow-hidden shadow-lg bg-white hover:shadow-xl transition-shadow duration-300">
      <div className="p-4">
        <h3 className="text-xl font-semibold text-gray-800 truncate">{title}</h3>
        <p className="text-gray-600 text-sm mt-2">{description}</p>
        <div className="flex items-center mt-3">
          <div className="flex-shrink-0 w-8 h-8 rounded-full bg-gray-300">
            <Image
              src={avatarUrl ? avatarUrl : "/empty.jpg"}
              width={50}
              height={50}
              alt="Uploader Avatar"
              className="w-full h-full rounded-full object-cover"
            />
          </div>
          <div className="ml-3">
            <p className="text-xs text-gray-500">{ReleaseDate}</p>
          </div>
        </div>
      </div>
    </div>
  );
}
