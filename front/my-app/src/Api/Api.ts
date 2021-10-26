import axios from "axios";

const axiosClient = axios.create({
    baseURL: "http://localhost:5000",
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

export interface Song {
    trackId : number,
    name : string,
    author : string,
    url : string
}

export interface Room {
    description: RoomDescription,
    players: Player[],
    songs: Song[]
}

class Api {
    getAll = async () => (await axiosClient.get<RoomDescription[]>("/rooms")).data
    get = async (id: string) => (await axiosClient.get<Room>(`/rooms/${id}`)).data
    loginOrRegister = async (tokenId: string) => (await axiosClient.post<string>(`/api/auth/google`, tokenId)).data
    codeAcceptor = async (code: string) => (await axiosClient.post<string>(`/api/auth/google_code`, code)).data
}

export default new Api();
