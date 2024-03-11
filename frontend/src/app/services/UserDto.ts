interface jwt {
    value : string
}
interface userLoginData {
    identify : string,
    password : string,
    token?   : string 
}
interface userRegisterData {
    name: string,
    birth: Date,
    email: string,
    password: string,
}
interface JwtWithData<T> {
    jwt : jwt,
    data : T
}
interface JWTWithGetPlaylistData {
    jwt : jwt,
    offset : number,
    limit : number
}
interface jwtWithVerified {
    jwt : jwt,
    verified : boolean
}
interface userJwtData {
    Name : string,
    Email : string
}

export { jwt, userLoginData, userRegisterData, JWTWithGetPlaylistData, JwtWithData, jwtWithVerified,userJwtData }