import { StreamerService } from "../services/Streamer.Service";
import { jwt } from "../services/UserDto";

export default abstract class StreamerCard {
    protected abstract Streamer : string ;
    protected abstract url : string;
    protected abstract jwt : jwt;

    constructor ( protected service : StreamerService ) { }

    public abstract Connect () : void ;

}