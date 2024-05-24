var express = require('express');
var app = express();
var http = require('http').Server(app);
var io = require('socket.io')(http);
var bodyParser = require('body-parser');

var count = 0;

app.use(express.static(__dirname + '/LaunchSystem'));

app.get('/jquery', function(req, res){
    count++;
    res.sendFile(__dirname + '/jquery-3.6.4.min.js');
    //
});

app.post('/gameState', function(req, res) {
    const { GamePlay, Setup, GameQuiz } = req.body;

    // Log the received states
    console.log('Received game state:', { GamePlay, Setup, GameQuiz });

    // Emit the game state to all connected clients
    io.emit('gameState', { GamePlay, Setup, GameQuiz });

    // Send a response back to Unity
    res.status(200).send('Game state received');
});

var userId = 0;

io.on('connection', function(socket){
    socket.userId = userId ++;
    console.log('a user connected, user id: ' + socket.userId);

    socket.on('spawn', function(msg){
        //msg = JSON.parse(msg);
        console.log('message from user ' + msg.pseudo);
        io.emit("spawn",msg.pseudo);
    });

    socket.on('swipe', function(msg){
    // msg = JSON.parse(msg);
    console.log('send : '+msg);
    io.emit("swipe",msg);
    });
  
});


http.listen(3000, function(){
    console.log('listening on *:3000');
});