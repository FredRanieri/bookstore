var url = "api/bookstore/";

function startServer(){
    $.get(url + "start", function(data){
        console.log(data);
    });
}

function booksList(){
    $.get(url + "allBooks", function(data){
        books = ""

        data.forEach(function(d) {
            books += "<p><b>Book: </b>" + d.bookName + "</br><b>Author:</b> " + d.authorName + "</p>";
        });

        document.getElementById("booksList_d").innerHTML = books;
        document.getElementById("message_d").innerHTML = "";
    });
}

function authorsList(){
    $.get(url + "allAuthors", function(data){
        var books = "";
        data.forEach(function(d) {
            books += "<p><b>Author: </b>" + d.authorName + "</br><b>Book:</b> ";
            d.booksName.forEach(function(b){
                books += b + "<br>";
            })
            books += "</p>";
        });
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