
document.addEventListener("click", function (e) {

    const btn = e.target.closest("#userDropdownBtn");
    const menu = document.getElementById("userDropdownMenu");

    if (!btn || !menu) return;

    menu.classList.toggle("show");

}, true); 


document.addEventListener("click", function (e) {

    const menu = document.getElementById("userDropdownMenu");
    const btn = document.getElementById("userDropdownBtn");

    if (!menu || !btn) return;

    if (!menu.contains(e.target) && !btn.contains(e.target)) {
        menu.classList.remove("show");
    }
});
