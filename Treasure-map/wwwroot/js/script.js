async function SendData() {

    var inputText = document.getElementById("textArea").value;

    const requestOptions = {
        method: 'POST',
        body: JSON.stringify(inputText),
        headers: { 'Content-Type': 'application/json' }
    };

    const response = await fetch('https://localhost:44353/input', requestOptions);
    const mapObject = await response.json();

    console.log(mapObject);
}
