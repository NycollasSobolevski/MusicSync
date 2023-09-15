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

interface JWTWithGetPlaylistData {
    jwt : jwt,
    offset : number,
    limit : number
}

export { jwt, userLoginData, userRegisterData, JWTWithGetPlaylistData }