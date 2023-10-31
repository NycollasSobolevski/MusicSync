
interface StringReturn {
    data: string
}
interface CallbackData {
    jwt: string,
    code:  string,
    state: string 
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
    href?: string,
    limit?: number,
    next?: string,
    offset?: number,
    previous?: string,
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
//     album:{album_type: 'album', artists: Array(1), available_markets: Array(184), external_urls: {…}, href: 'https://api.spotify.com/v1/albums/4TOkZvtqNpg5UHyGxCn0mS', …}
// artists:(2) [{…}, {…}]
// available_markets:(184) ['AR', 'AU', 'AT', 'BE', 'BO', 'BR', 'BG', 'CA', 'CL', 'CO', 'CR', 'CY', 'CZ', 'DK', 'DO', 'DE', 'EC', 'EE', 'SV', 'FI', 'FR', 'GR', 'GT', 'HN', 'HK', 'HU', 'IS', 'IE', 'IT', 'LV', 'LT', 'LU', 'MY', 'MT', 'MX', 'NL', 'NZ', 'NI', 'NO', 'PA', 'PY', 'PE', 'PH', 'PL', 'PT', 'SG', 'SK', 'ES', 'SE', 'CH', 'TW', 'TR', 'UY', 'US', 'GB', 'AD', 'LI', 'MC', 'ID', 'JP', 'TH', 'VN', 'RO', 'IL', 'ZA', 'SA', 'AE', 'BH', 'QA', 'OM', 'KW', 'EG', 'MA', 'DZ', 'TN', 'LB', 'JO', 'PS', 'IN', 'BY', 'KZ', 'MD', 'UA', 'AL', 'BA', 'HR', 'ME', 'MK', 'RS', 'SI', 'KR', 'BD', 'PK', 'LK', 'GH', 'KE', 'NG', 'TZ', 'UG', 'AG', …]
// disc_number:1
// duration_ms:164205
// episode:false
// explicit:false
// external_ids:{isrc: 'USWB11701181'}
// external_urls:{spotify: 'https://open.spotify.com/track/4e4fqjx0Izh4svvTef1z7e'}
// href:"https://api.spotify.com/v1/tracks/4e4fqjx0Izh4svvTef1z7e"
// id:"4e4fqjx0Izh4svvTef1z7e"
// is_local:false
// name: "Meant to Be (feat. Florida Georgia Line)"
// popularity:76
// preview_url:"https://p.scdn.co/mp3-preview/0b972d24c49e50be428edfacbafae85cb80249e9?cid=14a2f8a4ac8a4dd48d9fa64dc997ffc0"
// track:true
// track_number:14
// type:"track"
// uri:"spotify:track:4e4fqjx0Izh4svvTef1z7e"
}

export { StringReturn, CallbackData, PlaylistsArray, itemsOfPlaylist, NewTrack, NewPlaylist, Playlist, Track, TrackInfo, UserInfo }