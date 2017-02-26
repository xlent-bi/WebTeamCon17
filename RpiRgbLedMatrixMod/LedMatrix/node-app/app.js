var PORT = 8888;
var HOST = '127.0.0.1';

var dgram = require('dgram');
var client = dgram.createSocket('udp4');
var sleep = require('system-sleep');

for (var i = -1; i < 1024; i++) {
    var message = new Buffer(i + '\n');

    client.send(message, 0, message.length, PORT, HOST, function (err, bytes) {
        if (err) throw err;
    });

    sleep(100); // sleep for 100ms
}

client.close();


