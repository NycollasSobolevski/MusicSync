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

export { jwt, userLoginData, userRegisterData }