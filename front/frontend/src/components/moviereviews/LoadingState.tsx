export function LoadingState() {
  return (
    <div className="w-full max-w-4xl mx-auto p-6">
      <div className="animate-pulse">
        <div className="h-8 bg-slate-700/50 rounded mb-6 w-1/3"></div>
        <div className="space-y-4">
          {[1, 2, 3].map(i => (
            <div key={i} className="flex gap-4">
              <div className="w-12 h-12 bg-slate-700/50 rounded-full"></div>
              <div className="flex-1">
                <div className="h-4 bg-slate-700/50 rounded w-1/4 mb-2"></div>
                <div className="h-4 bg-slate-700/50 rounded w-3/4"></div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
