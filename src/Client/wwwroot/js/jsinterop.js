window.interopInstance = (dotnetHelper) => {
    dotnetHelper.invokeMethodAsync('LogText', 'Ciao sono il JS')
        .then(r => alert(r));
    dotnetHelper.dispose();
}

window.addEventListener('load', (event) => {
    setTimeout(function () {
        DotNet.invokeMethodAsync('Blazoring.PWA.Client', 'GetUserInfo', 'Dato dal javascript')
            .then(data => {
                alert(data);
            });
    }, 20000);
}, false);

window.addEventListener("message", (event) => {
    console.log(event);
    console.log(JSON.stringify(event));
}, false);