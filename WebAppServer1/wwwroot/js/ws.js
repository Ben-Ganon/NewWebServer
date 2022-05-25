
    var connection = new signalR.HubConnection().withUrl("/myHub").build();
    console.log("in ws.js");
    connection.start();
    var chatIn = document.getElementById('chatIn');
    document.getElementById('chatIn').onsubmit = () => {
        console.log('sending:' + chatIn.value);
        connection.invoke("UpdateChat", chatIn.from, chatIn.to, chatIn.value);

    };

    connection.on("MessageSent", function (from, value) {
        console.log("received: " + value);
    });


//$(function () {
//    var connection = new signalR.HubConnection().withUrl("/myHub").build();

//    connection.start();

//    $('chatIn').submit(() => {
//        console.log('sending:' + $('chatIn').val());
//        connection.invoke("UpdateChat", $('chatIn').from, $('chatIn').to, $('chatIn').val());
        
//    });

//    connection.on("MessageSent", function (from, value) {
//        console.log("received: " + value);
        
//    });
//});