interface Alert {
    isAlert : boolean,
    message : string,
    title   : string,
    functionToCall? : Function, 
}


export { Alert }