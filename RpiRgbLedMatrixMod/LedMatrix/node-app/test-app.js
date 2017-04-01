var PORT = 8888;
var HOST = 'rpi-3';

var dgram = require('dgram');
var client = dgram.createSocket('udp4');
var sleep = require('system-sleep');
var http = require('http');

http.get({
        host: 'localhost',
        path: '/TestWebApp/'
    }, function(response) {
        // Continuously update stream with data
        response.on('data', function(d) {
            console.log(d + "\r\n");
            var message = new Buffer(d + '\n');

            client.send(message, 0, message.length, PORT, HOST, function (err, bytes) {
                if (err) throw err;
            });
        });

        response.on('end', function() {
            // Data reception is done, do whatever with it!
        });
});

//client.close();
