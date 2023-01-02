const btnPass = document.getElementsByClassName("btnPass");
const eyeOff = document.getElementsByClassName("mdi-eye-off");
const eyeOn = document.getElementsByClassName("mdi-eye");

btnPass.addEventListener("click", changeOn);

function changeOn() {
    console.log("sbdvjnsvdk");
    if (eyeOn.style.display == "none") {
        eyeOff.style.display = "none";
        eyeOn.style.display = "block";
    }
    else {
        eyeOff.style.display = "block";
        eyeOn.style.display = "none";
    }
}

