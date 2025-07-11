export const getInitials = (userName: string): string => {
  return userName
    .split(" ")
    .map(name => name[0])
    .join("")
    .toUpperCase()
    .slice(0, 2);
};

export const getRandomColor = (userName: string): string => {
  const colors = [
    "bg-blue-500",
    "bg-green-500",
    "bg-purple-500",
    "bg-pink-500",
    "bg-indigo-500",
    "bg-yellow-500",
    "bg-red-500",
    "bg-teal-500",
  ];
  let hash = 0;
  for (let i = 0; i < userName.length; i++) {
    hash = userName.charCodeAt(i) + ((hash << 5) - hash);
  }
  return colors[Math.abs(hash) % colors.length];
};