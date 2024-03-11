
interface StringReturn {
    data: string
}
interface CallbackData {
    jwt: string,
    code:  string,
    state: string 
}
interface streamerJsonData {
    name: string,
    path: string
  }
interface TransferPlaylistObject {
    identifier: string,
    data: any
}
interface PlaylistsArray {
    href?: string,
    limit?: number,
    next?: string,
    offset?: number,
    previous?: string,
    total?: number,
    items :itemsOfPlaylist[]
}
interface itemsOfPlaylist {
    collaborative?: boolean, //!-----------------------------
    description?: string, 
    external_urls?: {
        spotify?: string
    },
    href: string,
    id: string,
    images: {
        url: string,
        height: number,
        width: number
    }[],
    name: string,
    owner: {
        external_urls: {
            spotify: string
        },
        followers?: {        //!-----------------------------
            href: string,
            total: number
        },
        href: string,
        id: string,
        type: string,
        uri: string,
        display_name: string
    },
    public: boolean,
    snapshot_id: string,
    tracks?: {
        href: string,
        total: number
    },
    type: string,
    uri: string
}
interface NewTrackToPlaylist {
    track: NewTrack,
    playlistId: string
}
interface NewTrack {
    name: string,
    author: string,
    uri: string,
}
interface NewPlaylist {
    name: string,
    description: string,
    public: boolean
}

interface Playlist {
    next?: string,
    href?: string,
    id?: string,
    name?: string,
    total?: number,
    items :Track[]
}
interface Track {
    added_at?: Date,
    added_by?: UserInfo,
    is_local?: boolean,
    primary_color?: string,
    track: TrackInfo,
    video_thumbnail: {url?: string}
}
interface UserInfo {
    external_urls: {spotify: string},
    href:string,
    id:string,
    type:string,
    uri:string
}
interface TrackInfo {
    album: { 
        album_type: 'album', 
        artists: {name: string}[], 
        available_markets: string[], 
        external_urls: {spotify:string}[],
        images:{url:string}[], 
        href: string
    },
    artists: { name:string }[],
    available_markets: string[],
    disc_number: number,
    duration_ms: number,
    episode: boolean,
    explicit: boolean,
    external_ids: { isrc: string },
    external_urls:{ spotify: string },
    href: string,
    id: string, 
    is_local: boolean,
    name: string,
    popularity: number,
    preview_url: string,
    track: boolean,
    track_number: number,
    type: string,
    uri: string
}

export { StringReturn, CallbackData, PlaylistsArray, itemsOfPlaylist, 
    NewTrack, NewPlaylist, Playlist, Track, TrackInfo, UserInfo, 
    TransferPlaylistObject, NewTrackToPlaylist, streamerJsonData }