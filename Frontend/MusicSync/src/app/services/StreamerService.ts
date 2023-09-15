import { CallbackData, StringReturn } from "./SpotifyDto";
import { jwt } from "./UserDto";

interface IStreamerService {
    GetAccesUrl ( data : jwt ) : Promise<StringReturn>;
    Callback ( data : CallbackData ) : Promise<void>;
    GetPlaylists ( data : jwt ) : Promise<void>;
    RefreshToken ( data : jwt ) : Promise<void>;
    LogOff ( data : jwt ) : Promise<void>;

}


export { IStreamerService }
