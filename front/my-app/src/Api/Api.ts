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

export interface TrackDescription {
    id: number,
    name: string,
    author: string,
    url: string
}

export interface PlayListDescription {
    PlaylistId : number,
    Name: string,
    PlayerId: string,
    trackIds : number[]
}

export interface Player {
    userId: string,
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
export interface DeezerTrackDescription{
    id: number,
    title: string,
    link: string,
    preview: string,
    artist: {
        id: number,
        name : string,
    },
    album: string    
}
class Api {
    getAll = async () => (await axiosClient.get<RoomDescription[]>("/rooms")).data
    getAllTracks = async () => (await axiosClient.get<TrackDescription[]>("/tracks")).data
    getNewTracks = async (search:string) => 
        (await axiosClient.get<DeezerTrackDescription[]>(`/api/addtracks/${search}`)).data
    addTracks = async (data : TrackDescription[]) => (await axiosClient.post<TrackDescription[]>("/tracks", data)).data
    createPlayList = async (data : PlayListDescription) => (await axiosClient.post<PlayListDescription>("/playlists", data))
    get = async (id: string) => (await axiosClient.get<Room>(`/rooms/${id}`)).data
    loginOrRegister = async (tokenId: string) => (await axiosClient.post<string>(`/api/auth/google`, tokenId)).data
    codeAcceptor = async (code: string) => (await axiosClient.post<string>(`/api/auth/google_code`, code)).data
}

export default new Api();
