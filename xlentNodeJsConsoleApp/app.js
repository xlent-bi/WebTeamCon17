if (process.argv.length != 5) {
	console.log("Give arguments <displayId> <width> <height>");
	return;
}

var displayId = process.argv[2];
var width = process.argv[3];
var height = process.argv[4];

console.log('Starting Up...');

var config = require("app-settings")("config.json");

var PORT = config.udpserver.port;
var HOST = config.udpserver.url;

var dgram = require('dgram');
var client = dgram.createSocket('udp4');

var http = require('http');

var connect = function() {
	// Connections url will never end, and if it does, just connect again
	http.request({
		host: 'localhost',
		path: '/LedDisplayApi/Connections/Create?displayId=' + displayId + '&width=' + width + '&height=' + height,
		method: 'post'
	},
	function(response) {

	response.on('data', function (chunk) {
			if (chunk) {
                // Send to UDP server
                var message = new Buffer(chunk);
                client.send(message, 0, message.length, PORT, HOST, function (err, bytes) {
                    if (err) Console.log(err);
                });
			}
		});

		response.on('end', function () {
			// Reconnect if connection was broken
			connect();
		});
	}).end();
}
connect();
