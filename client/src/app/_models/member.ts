import { Photo } from "./photo";

export interface Member {
    id: number;
    username: string;
    photoUrl: string;
    age: number;
    created: Date;
    lastActive: Date;
    gender: string;
    description: string;
    interests: string;
    city: string;
    country: string;
    photos: Photo[];
}

