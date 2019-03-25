var url = "api/bookstore/";

function booksList(){
    $.get(url + "allBooks", function(data){
        var books = "";
        for(var i = 0; i < data.length; i++){
            console.log(data[i]);
            books += "<p><b>Book: </b>" + data[i].name + "</br><b>Author:</b> " + data[i].author.name + "</p>";
        }
        document.getElementById("booksList_d").innerHTML = books;
        document.getElementById("message_d").innerHTML = "";
    });
}

function authorsList(){
    $.get(url + "allAuthors", function(data){
        var books = "";
        for(var i = 0; i < data.length; i++){
            var tab = "";
            console.log(data[i]);
            books += "<p><b>Author:</b> " + data[i].name + "</br><b>Books: </b>";
            if(data[i].books.length !== 0)
                books += data[i].books[0].name + "</br>";
            
            for(var j = 0; j < 13; j++){
                tab += "&nbsp;";
            }

            for(var j = 1; j < data[i].books.length; j++){
                books += tab + data[i].books[j].name + "</br>";
            }
            books += "</p>";
        }
        document.getElementById("booksList_d").innerHTML = books;
        document.getElementById("message_d").innerHTML = "";
    });
}

function insertMessage(){
    var returnMessage = 'This button is not working yet, please insert data using Postman.<br>Data model for insert a Author = { "name": "NameAuthor", "books": [] }</br>';
    returnMessage += 'Data model to insert a Book = { "name": "NameBook", "authorId": 1 }</br><b>Obs.</b> In "authorId" remember to use id from a created Author';
    document.getElementById("booksList_d").innerHTML = "";
    document.getElementById("message_d").innerHTML = returnMessage;
}