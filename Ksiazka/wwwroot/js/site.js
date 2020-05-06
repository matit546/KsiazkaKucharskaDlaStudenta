// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function ShowPreview(input) {
    if (input.files && input.files[0]) {
        var ImageDir = new FileReader();
        ImageDir.onload = function (e) {
            $('#impPrev').attr('src', e.target.result);
        }
        ImageDir.readAsDataURL(input.files[0]);
    }
}

function schowaj_pokaz(i) {
    var b = i;
    var x = document.getElementById(b);
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}

function zmiana_koloru(id) {
    var count = id;
    var p = document.getElementById(count);
    p.style.opacity = 0.5;

}

function zmiana_koloru_1(id) {
    var count = id;
    var p = document.getElementById(count);
    p.style.opacity = 1;

}