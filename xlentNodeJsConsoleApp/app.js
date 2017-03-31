console.log('Starting Up...');

var config = require("app-settings")("config.json");


//client.connect('http://xlent-test-pw.azurewebsites.net:80/', 'echo-protocol');

//http://160.23.1.5:3100


console.log("udp server url: " + config.udpserver.url);
console.log("azure url: " + config.azure.url); 
var socket = require('socket.io-client')(config.azure.url);
var readline = require('readline'),
    rl = readline.createInterface(process.stdin, process.stdout);



var udpSocket = require('socket.io-client')(config.udpserver.url);


socket.on('connect', function () {
    console.log('Connected to azure!');

    
});
socket.on('error', function () {
    console.log('Erorr connecting to server');
});

socket.on('DATA', function (data) {
    console.log(data);

    socket.emit('RANDOM', { value: 'consoleApp: ' + data.Message });


    /// **** Send data to udp-server


});
rl.setPrompt('Server says> : ');
rl.prompt();

rl.on('line', function (line) {
    socket.emit('RANDOM', { value: line });
    
    rl.prompt();
}).on('close', function () {
    console.log('Have a great day!');
    process.exit(0);
});

/** Create socket connection  to udp server**/
udpSocket.on('connect', function () {
    console.log('Connected to UDP Server!');
});

udpSocket.on('error', function () {
    console.log('Erorr connecting to UDP Server');
});

udpSocket.on('DATA', function (data) {
    console.log(data);

    // Message from UDP Server

});
