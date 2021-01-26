function test() {


    var inputText = document.getElementById("textArea").value;
    const options = {
        method: 'POST',
        body: JSON.stringify(inputText),
        headers: {
            'Content-Type': 'application/json'
        }
    }


    //fetch(https://jsonplaceholder.typicode.com/todos/1)
    //    .then(response => response.json())
    //        .then(json => console.log(json))



}
