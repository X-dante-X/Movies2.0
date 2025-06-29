export interface User {
    username: string
    userStatus: number
}

export enum WatchStatus {
    PlanToWatch,
    Watching,
    Completed,
    Dropped,
    Favorite
}