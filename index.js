const WebSocket = require('ws')
const wss = new WebSocket.Server({port: 8080}, ()=>{
    console.log('server started')
})

wss.on('connection', function connection(ws){
    ws.on('message',(data)=>{
        console.log('data recieved \n %o', data.toString())
        ws.send(data);
    })
})

wss.on('listening', ()=> {
    console.log('server is listening on port 8080')
})

///

// const http = require('http');

// const requestListener = function (req, res) {
//     res.writeHead(200);
//     res.end('Test');
// }

// const server = http.createServer(requestListener);
// server.listen(8080);