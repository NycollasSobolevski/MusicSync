interface jwtReturn {
    value : string
}
interface userLoginData {
    identify : string,
    password : string
}
interface userRegisterData {
    id: string,
    name: string,
    birth: string,
    email: string,
    password: string,
    salt: string
}

export { jwtReturn, userLoginData, userRegisterData }