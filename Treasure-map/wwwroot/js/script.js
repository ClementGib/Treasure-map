
let mapObject;
var outputText = "";

async function SendText() {
    CleanLog();

    var inputText = document.getElementById("textArea").value;


    const requestOptions = {
        method: 'POST',
        body: JSON.stringify(inputText),
        headers: { 'Content-Type': 'application/json' }
    };
    try {


        const response = await fetch('https://localhost:44353/text', requestOptions);
        mapObject = await response.json();
        console.log(mapObject);
        //.includes("System")
        if (mapObject.ClassName) {
            ShowError(mapObject.Message);
        } else {
            ShowMap()
        }
    }
    catch (err) {
        ShowError(err);
    }

}

async function SendFile() {
    CleanLog();

    const formData = new FormData();
    const requestOptions = {
        method: 'POST',
        body: formData,
        headers: { 'Content-Type': 'multipart/form-data' }
    };

    let input = document.getElementById('file-selector');
    try {
        let inputFile = input.files[0];
        if (inputFile) {
            let name = inputFile.name.split(".");

            if (name.length > 1 && name[1] == "txt") {

                formData.append(name, inputFile)


                const response = await fetch('https://localhost:44353/file', requestOptions);
                mapObject = await response.json();

                if (mapObject.ClassName == "System.FormatException") {
                    ShowError(mapObject.Message);
                } else {
                }

            } else {
                throw new Error('Required txt file');
            }

        }
        else {
            ShowError("Error file");
        }
    }
    catch (err) {
        ShowError(err);
    }
}

async function SendPosition(P_step) {
    CleanLog();

    const requestOptions = {
        method: 'POST',
        body: JSON.stringify(P_step),
        headers: { 'Content-Type': 'application/json' }
    };
    try {


        const response = await fetch('https://localhost:44353/move', requestOptions);
        const adventurerObject = await response.json();
        
        if (adventurerObject.ClassName) {
            ShowError(adventurerObject.Message);
        } else {
            return adventurerObject;
        }
    }
    catch (err) {
        ShowError(err);
    }
}

async function SendTreasurePosition(P_x,P_y) {
    CleanLog();

    const requestOptions = {
        method: 'POST',
        body: JSON.stringify(P_x+" - "+ P_y),
        headers: { 'Content-Type': 'application/json' }
    };
    try {
        const response = await fetch('https://localhost:44353/treasure', requestOptions);
        mapObject = await response.json();
        console.log(mapObject);
        //.includes("System")
        if (mapObject.ClassName) {
            ShowError(mapObject.Message);
        } else {
            ShowMap()
        }
    }
    catch (err) {
        ShowError(err);
    }
}


function CleanLog() {
    document.getElementById("log-text").innerHTML = "";
}

function ShowError(message) {
    document.getElementById("log-text").style.color = '#e53f48';
    document.getElementById("log-text").innerHTML = message;
}

function ShowMap() {

    console.log(mapObject);

    var Adventurer = mapObject.Adventurer;
    var widthMax = 100 / mapObject.width;
    var heightMax = 100 / mapObject.height;

    var map = document.getElementById('map');
    //clear
    map.innerHTML = '';

    for (let index_line = 0; index_line < mapObject.height; index_line++) {

        var line = document.createElement('div');
        line.id = `grid-row${index_line}`;
        line.className = "grid-row";
        line.style.cssText = `width:${100}%;height:${heightMax}%;`
        map.appendChild(line);

        for (let index_column = 0; index_column < mapObject.width; index_column++) {
            line = document.getElementById(`grid-row${index_line}`);
            var box = document.createElement('div');
            box.id = `grid-column${index_line + "-" + index_column}`;
            box.className = "grid-column";
            box.style.cssText = `width:${widthMax}%;height:${100}%;`
            line.appendChild(box);


            var grid = document.createElement("img");
            let Surface = getSurfaceByPosition(index_column, index_line);


            if (Surface.surface == "Plain") {
                grid.style.cssText = `width:${100}%;height:${80}%;`;
                grid.src = `content/images/P${Surface.imageNumber}.png`;
            } else if (Surface.surface == "Mountain") {
                grid.style.cssText = `width:${70}%;height:${100}%;`;
                grid.src = `content/images/M${Surface.imageNumber}.png`;
            } else {
                grid.style.cssText = `width:${50}%;height:${50}%;`;
                grid.src = `content/images/treasure.png`;

                var number = document.createElement("p");   
                var node = document.createTextNode(Surface.numberChest);
                number.style.cssText = `width:${100}%;height:${100}%;`;
                number.id = `treasure-${index_line + "-" + index_column}`;
                number.className = "treasure-number";
                number.appendChild(node);

                document.getElementById(`grid-column${index_line + "-" + index_column}`).appendChild(number);
            }

            document.getElementById(`grid-column${index_line + "-" + index_column}`).appendChild(grid);


            if (Adventurer.xPosition == index_column && Adventurer.yPosition == index_line) {
                if (Surface.surface == "Mountain") {
                    throw 'Adventurer defined on the mountain.';
                } else {
                    setAdventurer(index_column, index_line)
                }

            }


        }
    }

   
}

function setAdventurer(P_x, P_y) {
    var Adventurer = mapObject.Adventurer;

    var adventurer = document.createElement("img");

    //set position adventurer with orientation
    switch (Adventurer.orientation) {
        case "S":
            adventurer.style.cssText = `width:8%;height:28%;transform: scaleX(-1);right:46%;top:25%;`
            break;
        case "N":
            adventurer.style.cssText = `width:8%;height:28%;right:46%;top:25%;`
            break;
        case "E":
            adventurer.style.cssText = `width:${8}%;height:${28}%;right:20%;top:20%;`
            break;
        case "W":
            adventurer.style.cssText = `width:${8}%;height:${28}%;transform: scaleX(-1);right:80%;top:20%;`
            break;
    } 

    
    adventurer.src = `content/images/adventurer.png`;
    adventurer.id = "adventurer";
    document.getElementById(`grid-column${P_y + "-" + P_x}`).appendChild(adventurer);

    logText = "";

    //set log movement information
    switch  (Adventurer.movement[Adventurer.movementStep]){
        case "A":
            logText = "Avance vers ";
            logText += Adventurer.orientation == 'S' || Adventurer.orientation == 'N' ?
                Adventurer.orientation == 'N' ? "le nord" : "le sud" : Adventurer.orientation == 'O' ? "l'ouest" : "l'est";
            break;
        case "D":
            logText = "Tourne vers ";
            logText += Adventurer.orientation == 'S' || Adventurer.orientation == 'N' ?
                Adventurer.orientation == 'N' ? "l'est" : "l'ouest" : Adventurer.orientation == 'O' ? "le nord" : "le sud";
            break;
        case "G":
            logText = "Tourne vers ";
            logText += Adventurer.orientation == 'S' || Adventurer.orientation == 'N' ?
                Adventurer.orientation == 'N' ? "l'ouest" :"l'est" : Adventurer.orientation == 'O' ? "le sud" : "le nord";
            break;
    }


    document.getElementById("log-text").style.color = '#60e091';
    document.getElementById("log-text").innerHTML = logText;
}

async function adventurerMove() {

    //send position
    let Adventurer = await SendPosition(mapObject.Adventurer.movementStep);
    let Surface = getSurfaceByPosition(Adventurer.xPosition, Adventurer.yPosition);

    //new adventurer
    mapObject.Adventurer = Adventurer;

    //remove adventurer from the DOM
    console.log(Surface);
    console.log(Adventurer.xPosition + " " + Adventurer.yPosition + " " + Adventurer.orientation);
    document.getElementById("adventurer").remove();

    if (Surface.surface == "Mountain") {
        throw 'Adventurer can not reach the mountain.';
    } else {
        //remove adventurerfrom the DOM
        setAdventurer(Adventurer.xPosition, Adventurer.yPosition)
        if (Surface.surface == "Treasure") {
            getCoffre(Adventurer.xPosition, Adventurer.yPosition);
        }
    }

    console.log(mapObject);
}

async function getCoffre(P_x, P_y) {
    SendTreasurePosition(P_x, P_y)
}

function getSurfaceByPosition(P_x, P_y) {
    let index_value;
    for (let index_position = 0; index_position < mapObject.mapGridsKeys.length; index_position++) {
        if (mapObject.mapGridsKeys[index_position].x == P_x && mapObject.mapGridsKeys[index_position].y == P_y) {
            index_value = index_position;
            break;
        }
    }
    return mapObject.mapGridsValues[index_value];
}




//function writeOutputFile(P_text) {
//    console.log("move");
//}