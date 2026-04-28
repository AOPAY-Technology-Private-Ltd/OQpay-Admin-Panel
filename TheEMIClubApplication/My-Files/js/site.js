// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const checkboxBars = document.querySelectorAll(".checkbox-bars tbody tr td label");
const newElement = "<span></span>";
checkboxBars.replaceWith(newElement);
