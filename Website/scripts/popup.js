document.querySelector("#openPopup").addEventListener("click", function(){
    document.body.classList.add("active-popup");
});

document.querySelector(".popup .closeButton").addEventListener("click", function(){
    document.body.classList.remove("active-popup");
});