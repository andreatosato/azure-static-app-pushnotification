// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).

self.addEventListener('install', async event => {
    console.log('Installing service worker...');
    self.skipWaiting();
});

self.addEventListener('online', function () { console.log('online'); });
self.addEventListener('offline', function () { console.log('online'); });

self.addEventListener('fetch', () => { });

self.addEventListener('push', event => {
    const payload = event.data.json();
    var notification = {
        body: payload.body,
        vibrate: payload.vibrate,
        requireInteraction: true,
        tag: 'tag',
        actions: payload.actions,
        data: payload.actions
    };
    if (payload.image != null) {
        notification.icon = payload.icon;
        notification.badge = payload.badge;
        notification.image = payload.image;
    }
    event.waitUntil(self.registration.showNotification(payload.message, notification));    
});

self.addEventListener("notificationclick", event => {
    event.waitUntil(getClients(event));
});

async function getClients(event) {
    const clientList = await self.clients.matchAll();
    console.log(clientList);
    clients.openWindow('https://google.it/');
    const client = await clients.get(event.clientId);
    client.postMessage({
        msg: event
    });
}
