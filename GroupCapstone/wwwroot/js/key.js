// JavaScript source code
var someKey = "";

function setKeys(keyPassedIn) {
    someKey = keyPassedIn;
    sessionStorage.setItem('pusher', someKey)
}

