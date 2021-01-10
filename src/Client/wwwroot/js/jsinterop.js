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

//navigator.serviceWorker.addEventListener('message', event => {
//    console.log(JSON.stringify(event));
//    console.log(event.data.msg, event.data.url);
//});