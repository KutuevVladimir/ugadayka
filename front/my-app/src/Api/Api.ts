import axios, { AxiosResponse } from "axios";

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
    requiresPassword: boolean,
    playlist: number | undefined
}

export interface TrackDescription {
    id: number,
    name: string,
    author: string,
    url: string
}

export interface PlayListDescription {
    playlistId : number,
    name: string,
    playerId: string,
    trackIds : number[]
}

export interface Player {
    userId: string,
    nickName: string,
}

export interface PlayerProfile {
    playerId: string,
    displayName: string,
    email: string,
    rating: number,
    image: string
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
    getAllPlayLists = async () => (await axiosClient.get<PlayListDescription []>("/playlists")).data
    createRoom = async (data : RoomDescription) => (await axiosClient.post<RoomDescription, AxiosResponse<string>>("/rooms/addroom", data)).data;
    // TODO Fix problem with response.data in this request. Now fixed by duck tape response.request.response
    loginOrRegister = async (tokenId: string) => (await axiosClient.post<string>(`/api/auth/google`, tokenId)).request.response
    getPlayerProfile = async (tokenId: string) => (await axiosClient.get<PlayerProfile>(`/players/${tokenId}`)).data
}

export default new Api();
