// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).

self.addEventListener('install', async event => {
    console.log('Installing service worker...');
    self.skipWaiting();
});

self.addEventListener('online', () => { alert('online') });
self.addEventListener('offline', () => { alert('offline') });

self.addEventListener('fetch', () => { });

self.addEventListener('push', event => {
    const payload = event.data.json();
    event.waitUntil(self.registration.showNotification('Codegen 2021 news!',
        {
            body: payload.message,
            icon: 'icon-512.png',
            badge: 'icon-512.png',
            image: 'cat.png',
            vibrate: [100, 50, 100],
            data: { url: payload.url },
            requireInteraction: true,
            actions: [
                {
                    action: "agenda", title: "Open Agenda", icon: 'icon-512.png'
                },
                {
                    action: "close", title: "Ignore",
                },
            ]
        })
    );    
});

self.addEventListener('notificationclick', function (event) {
    console.log('On notification click: ', event.notification.tag);
    event.notification.close();
}, false);

//self.addEventListener('notificationclick', function (event) {
//    console.log('On notification click: ', event.notification.tag);
//    event.notification.close();
//    //if (!event.action) {
//    //    // Was a normal notification click
//    //    console.log('Notification Click.');
//    //    return;
//    //}

//    //event.waitUntil(async function () {
//    //    if (!event.clientId) return;

//    //    const client = await clients.get(event.clientId);
//    //    if (!client) return;

//    //    client.postMessage(event.data.json());
//    //}());



//    //switch (event.action) {
//    //    case 'agenda':           
//    //        console.log('Go to agenda');
//    //        break;
//    //    default:
//    //        console.log(`Unknown action clicked: '${event.action}'`);
//    //        break;
//    //}
//});