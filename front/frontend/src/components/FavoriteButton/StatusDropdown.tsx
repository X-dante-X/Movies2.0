import { CategoryType } from '../userpage/types';
import { STATUS_OPTIONS } from './constants';
import { LucideIcon } from 'lucide-react';

interface StatusDropdownProps {
  onSelectStatus: (status: CategoryType) => void;
}

export default function StatusDropdown({ onSelectStatus }: StatusDropdownProps) {
  return (
    <div className="absolute z-20 bottom-full mb-3 w-64 origin-bottom-right right-0 rounded-2xl shadow-2xl bg-gray-900/90 backdrop-blur-xl border border-purple-400/20 overflow-hidden transform transition-all duration-300 animate-fadeInUp">
      <div className="py-3 divide-y divide-gray-600/20">
        <div className="px-5 py-3 text-purple-300 text-sm font-medium uppercase tracking-wider">
          Add movie to:
        </div>
        {STATUS_OPTIONS.map((option) => {
          const IconComponent = option.icon as LucideIcon;
          
          return (
            <button
              key={option.value}
              onClick={() => onSelectStatus(option.value)}
              className={`w-full px-5 py-4 text-white hover:bg-purple-900/40 transition-all duration-200 flex items-center space-x-4 border-l-4 border-transparent hover:border-l-4 hover:border-${option.color.split(' ')[0].replace('bg-', '')}`}
            >
              <div className={`w-10 h-10 rounded-full ${option.color.split(' ')[0]} flex items-center justify-center shadow-lg shadow-black/30`}>
                <IconComponent size={20} strokeWidth={2} />
              </div>
              <span className="font-medium text-left">{option.label}</span>
            </button>
          );
        })}
      </div>
      <div className="absolute bottom-0 right-8 transform translate-y-1/2 rotate-45 w-4 h-4 bg-gray-900/90 border-r border-b border-purple-400/20"></div>
    </div>
  );
}