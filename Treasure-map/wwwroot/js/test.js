async function test() {

    var inputText = document.getElementById("textArea").value;

    const requestOptions = {
        method: 'POST',
        body: JSON.stringify(inputText),
        headers: { 'Content-Type': 'application/json' }
    };

    const response = await fetch('https://localhost:44353/input', requestOptions);
    const responseJSON = await response.json();
    console.log(responseJSON);

}
