
// -----------------------------------------------------------------------------
//  GLOBAL
// -----------------------------------------------------------------------------

let mapObject;
let Adventurer;
let widthMax;
let heightMax;

var outputText = "";

// -----------------------------------------------------------------------------
//  HTTP QUERIES METHODS
// -----------------------------------------------------------------------------

async function SendText() {

    //clean log
    CleanLog();

    //get content of textArea
    var inputText = document.getElementById("textArea").value;


    //request option
    const requestOptions = {
        method: 'POST',
        body: JSON.stringify(inputText),
        headers: { 'Content-Type': 'application/json' }
    };
    try {

        //pass the options with the text
        const response = await fetch('https://localhost:44353/text', requestOptions);
        //handle response
        mapObject = await response.json();
        console.log(mapObject);

        //if map is generated
        if (mapObject.ClassName) {

            //show error
            DisplayError(mapObject.Message);

        } else {

            //create Map with object
            DisplayMap()
        }
    }
    catch (err) {
        DisplayError(err);
    }

}

async function SendFile() {
    //clean log
    CleanLog();

    //get from the file
    const formData = new FormData();
    const requestOptions = {
        method: 'POST',
        body: formData,
        headers: { 'Content-Type': 'multipart/form-data' }
    };

    //get input with the file
    let input = document.getElementById('file-selector');


    try {
        //get the file from the input
        let inputFile = input.files[0];

        //if file existe
        if (inputFile) {

            //get the name
            let name = inputFile.name.split(".");

            //check the name
            if (name.length > 1 && name[1] == "txt") {

                //add to the formData
                formData.append(name, inputFile)

                //pass the options with the formData
                const response = await fetch('https://localhost:44353/file', requestOptions);

                //handle response
                mapObject = await response.json();

                //check if return Exception
                if (mapObject.ClassName) {

                    //show errors
                    DisplayError(mapObject.Message);

                }

            }
            else {

                //if its not a txt file
                throw new Error('Required txt file');
            }

        }
        else {
            //show error from file
            DisplayError("Error file");
        }
    }
    catch (err) {
        DisplayError(err);
    }
}

async function SendPosition(P_step) {

    //clean
    CleanLog();

    //request option
    const requestOptions = {
        method: 'POST',
        body: JSON.stringify(P_step),
        headers: { 'Content-Type': 'application/json' }
    };



    try {
        //pass the options with the formData
        const response = await fetch('https://localhost:44353/move', requestOptions);

        //handle response
        const adventurerObject = await response.json();

        //check if return Exception
        if (adventurerObject.ClassName) {
            DisplayError(adventurerObject.Message);
        } else {

            //return adventurer
            return adventurerObject;
        }
    }
    catch (err) {
        DisplayError(err);
    }
}

async function SendTreasurePosition(P_x, P_y) {

    //clean log
    CleanLog();

    //request option
    const requestOptions = {
        method: 'POST',
        body: JSON.stringify(P_x + " - " + P_y),
        headers: { 'Content-Type': 'application/json' }
    };

    try {
        //pass the options with the text
        const response = await fetch('https://localhost:44353/treasure', requestOptions);

        //handle response
        mapObject = await response.json();

        console.log("Treasure position:");
        console.log(mapObject);

        //check if return Exception
        if (mapObject.ClassName) {
            DisplayError(mapObject.Message);
        } else {
            DisplayMap()
        }
    }
    catch (err) {
        DisplayError(err);
        DisplayError(err);
    }
}

// -----------------------------------------------------------------------------
//  LOG METHODS
// -----------------------------------------------------------------------------

function CleanLog() {

    //Clean log -> remove the text
    document.getElementById("log-text").innerHTML = "";
}

function DisplayError(message) {

    //display error
    document.getElementById("log-text").style.color = '#e53f48';
    document.getElementById("log-text").innerHTML = message;
}

// -----------------------------------------------------------------------------
//  MAP METHODS
// -----------------------------------------------------------------------------


function DisplayMap() {

    console.log("Map object:");
    console.log(mapObject);

    //set size
    widthMax = 100 / mapObject.width;
    heightMax = 100 / mapObject.height;

    //set Object
    Adventurer = mapObject.Adventurer;


    var map = document.getElementById('map');
    //clear map
    map.innerHTML = '';




    //Loop for each line
    for (let index_line = 0; index_line < mapObject.height; index_line++) {

        //create div line
        createLine(index_line);

        //Loop for each colum in the lines
        for (let index_column = 0; index_column < mapObject.width; index_column++) {

            //create div column
            createColumn(index_line, index_column);

            //create div surface
            surface = createSurface(index_line, index_column);

            //check if the adventurer is at this position
            if (Adventurer.xPosition == index_column && Adventurer.yPosition == index_line) {

                if (surface == "Mountain") {

                    throw 'Adventurer defined on the mountain.';

                }
                else {

                    setAdventurer(index_column, index_line)
                }

            }


        }
    }
}

function PlainImage() {
    return Math.floor(Math.random() * 5)+1;
}

function MountainImage() {
    return Math.floor(Math.random() * 5)+1;
}

function createLine(P_indexLine) {

    //create the line with the size
    var line = document.createElement('div');
    line.id = `grid-row${P_indexLine}`;
    line.className = "grid-row";
    line.style.cssText = `width:${100}%;height:${heightMax}%;`
    map.appendChild(line);
}

function createColumn(P_indexLine, P_columnLine) {

    //get line div
    line = document.getElementById(`grid-row${P_indexLine}`);

    //create the column with the size
    var box = document.createElement('div');
    box.id = `grid-column${P_indexLine + "-" + P_columnLine}`;
    box.className = "grid-column";
    box.style.cssText = `width:${widthMax}%;height:${100}%;`
    line.appendChild(box);
}

function createSurface(P_indexLine, P_columnLine) {

    var grid = document.createElement("img");

    let Surface = getSurfaceByPosition(P_columnLine, P_indexLine);

    if (Surface.surface == "Plain") {

        //set the Plain style
        grid.style.cssText = `width:${100}%;height:${80}%;`;

        //set the Plain sprite
        grid.src = `content/images/P${PlainImage()}.png`;

    } else if (Surface.surface == "Mountain") {

        //set the Mountain style
        grid.style.cssText = `width:${70}%;height:${100}%;`;

        //set the Mountain sprite
        grid.src = `content/images/M${MountainImage()}.png`;

    } else {


        //set the Treasure style
        grid.style.cssText = `width:${50}%;height:${50}%;`;

        //set the Treasure sprite
        grid.src = "content/images/treasure.png";


        //display the number of treasure
        var number = document.createElement("p");
        var node = document.createTextNode(Surface.numberChest);
        number.style.cssText = `width:${100}%;height:${100}%;`;
        number.id = `treasure-${P_indexLine + "-" + P_columnLine}`;
        number.className = "treasure-number";


        number.appendChild(node);

        document.getElementById(`grid-column${P_indexLine + "-" + P_columnLine}`).appendChild(number);


    }

    //create the surface with the size
    document.getElementById(`grid-column${P_indexLine + "-" + P_columnLine}`).appendChild(grid);

    return Surface.surface;
}

function getSurfaceByPosition(P_x, P_y) {

    //return the surface with a position
    let index_value;

    for (let index_position = 0; index_position < mapObject.mapGridsKeys.length; index_position++) {
        if (mapObject.mapGridsKeys[index_position].x == P_x && mapObject.mapGridsKeys[index_position].y == P_y) {
            index_value = index_position;
            break;
        }
    }
    return mapObject.mapGridsValues[index_value];
}


// -----------------------------------------------------------------------------
//  ADVENTURER METHODS
// -----------------------------------------------------------------------------


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
            adventurer.style.cssText = `width:${8}%;height:${28}%;right:20%;top:30%;`
            break;
        case "W":
            adventurer.style.cssText = `width:${8}%;height:${28}%;transform: scaleX(-1);right:70%;top:20%;`
            break;
    }

    //declare text log
    logText = "";

    //set log movement information
    switch (Adventurer.movement[Adventurer.movementStep]) {
        case "A":
            logText = "Avance vers ";
            logText += Adventurer.orientation == 'S' || Adventurer.orientation == 'N' ?
                Adventurer.orientation == 'N' ? "le nord" : "le sud" : Adventurer.orientation == 'O' ? "l'est" : "l'ouest";
            break;
        case "D":
            logText = "Tourne vers ";
            logText += Adventurer.orientation == 'S' || Adventurer.orientation == 'N' ?
                Adventurer.orientation == 'N' ? "l'est" : "l'ouest" : Adventurer.orientation == 'O' ? "le nord" : "le sud";
            break;
        case "G":
            logText = "Tourne vers ";
            logText += Adventurer.orientation == 'S' || Adventurer.orientation == 'N' ?
                Adventurer.orientation == 'N' ? "l'ouest" : "l'est" : Adventurer.orientation == 'O' ? "le sud" : "le nord";
            break;
    }


    //configure img adventurer element
    adventurer.src = `content/images/adventurer.png`;
    adventurer.id = "adventurer";
    document.getElementById(`grid-column${P_y + "-" + P_x}`).appendChild(adventurer);
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

        //getCoffre and 
        if (Surface.surface == "Treasure") {
            getCoffre(Adventurer.xPosition, Adventurer.yPosition);
        }
    }

    console.log(mapObject);
}

async function getCoffre(P_x, P_y) {
    SendTreasurePosition(P_x, P_y)
}



// -----------------------------------------------------------------------------
//  IO MANAGER
// -----------------------------------------------------------------------------


function getOutputeFile(P_text) {

    //TODO
    console.log("move");
}