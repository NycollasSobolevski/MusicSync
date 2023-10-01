interface jwt {
    value : string
}
interface userLoginData {
    identify : string,
    password : string
}
interface userRegisterData {
    name: string,
    birth: Date,
    email: string,
    password: string,
}
interface JwtWithData {
    jwt : jwt,
    data : any
}
interface JWTWithGetPlaylistData {
    jwt : jwt,
    offset : number,
    limit : number
}

export { jwt, userLoginData, userRegisterData, JWTWithGetPlaylistData, JwtWithData }