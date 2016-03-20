var isQuit = false;

function OnMouseEnter() {
    //change text color
    GetComponent.<Renderer>().material.color = Color.red;
}

function OnMouseExit() {
    //change text color
    GetComponent.<Renderer>().material.color = Color.white;
}

function OnMouseUp() {
    //is this quit
    if (isQuit == true) {
        //quit the game
        Application.Quit();
    }
    else {
        //load level
        Application.LoadLevel(1); //NEEDS REPLACING/updating 
    }
}

function Update() {
    //quit game if escape key is pressed
    if (Input.GetKey(KeyCode.Escape)) {
        Application.Quit();
    }
}