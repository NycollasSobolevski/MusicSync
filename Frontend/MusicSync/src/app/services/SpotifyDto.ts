
interface StringReturn {
    data: string
}
interface CallbackData {
    jwt: string,
    code:  string,
    state: string 
}

export { StringReturn, CallbackData }