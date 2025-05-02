import { AlertCircle, CheckCircle } from 'lucide-react';

interface NotificationMessageProps {
  type: 'success' | 'error';
  message: string;
}

export default function NotificationMessage({ type, message }: NotificationMessageProps) {
  const styles = {
    success: {
      container: "bg-green-500/90 backdrop-blur-xl shadow-green-900/50 border-green-400/30",
      icon: <CheckCircle className="h-5 w-5 text-white" />
    },
    error: {
      container: "bg-red-500/90 backdrop-blur-xl shadow-red-900/50 border-red-400/30",
      icon: <AlertCircle className="h-5 w-5 text-white" />
    }
  };
  
  return (
    <div className={`fixed bottom-4 right-4 z-50 flex items-center space-x-2 rounded-lg border p-4 text-white shadow-lg ${styles[type].container}`}>
      {styles[type].icon}
      <span>{message}</span>
    </div>
  );
}
