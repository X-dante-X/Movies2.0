import { AlertCircle, CheckCircle } from 'lucide-react';

interface NotificationProps {
  type: "success" | "error";
  message: string;
}

export function Notification({ type, message }: NotificationProps) {
  const styles = {
    success: {
      bg: "bg-green-500/90",
      icon: <CheckCircle className="h-5 w-5 text-white" />,
    },
    error: {
      bg: "bg-red-500/90",
      icon: <AlertCircle className="h-5 w-5 text-white" />,
    },
  };

  return (
    <div className={`fixed bottom-4 right-4 z-50 flex items-center gap-2 px-4 py-3 rounded-lg text-white shadow-xl border ${styles[type].bg}`}>
      {styles[type].icon}
      <span>{message}</span>
    </div>
  );
}
