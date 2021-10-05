import axios from "axios";

const axiosClient = axios.create({
    baseURL: "http://localhost:5000/rooms",
    headers: {
        "Content-type": "application/json"
    }
});

export interface RoomDescription {
    id: number,
    name: string,
    playersCount: number,
    maxPlayersCount: number,
    requiresPassword: boolean
}

export interface Player {
    userId: number,
    nickName: string,
}

export interface Room {
    description: RoomDescription,
    players: Player[],
    songs: string[]
}

class Api {
    getAll = async () => (await axiosClient.get<RoomDescription[]>("/")).data
    get = async (id: string) => (await axiosClient.get<Room>(`/${id}`)).data
}

export default new Api();
