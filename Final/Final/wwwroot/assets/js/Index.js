const slides = document.querySelectorAll(".slide");
const next = document.getElementById("next");
const prev = document.getElementById("prev");
let index = 0;

function showSlide(n) {
    slides.forEach((slide) => slide.classList.remove("active"));
    slides[n].classList.add("active");

    const lines = slides[n].querySelectorAll(".line");
    lines.forEach((line, i) => {
        line.style.animation = "none";
        line.offsetHeight;
        line.style.animation = `fadeUp 0.8s ease forwards ${0.3 + i * 0.3}s`;
    });
}

next.addEventListener("click", () => {
    index = (index + 1) % slides.length;
    showSlide(index);
});

prev.addEventListener("click", () => {
    index = (index - 1 + slides.length) % slides.length;
    showSlide(index);
});

setInterval(() => {
    index = (index + 1) % slides.length;
    showSlide(index);
}, 8000);



document.addEventListener("DOMContentLoaded", () => {

    const dropdown = document.getElementById("sidebar-dropdown");
    const toggleBtn = document.querySelector(".sidebar-icon");

    toggleBtn.addEventListener("click", () => {
        dropdown.classList.toggle("active");
    });

    document.addEventListener("click", (e) => {
        if (!dropdown.contains(e.target) && !toggleBtn.contains(e.target)) {
            dropdown.classList.remove("active");
        }
    });

});


document.addEventListener("DOMContentLoaded", () => {
    const btn = document.getElementById("userDropdownBtn");
    const menu = document.getElementById("userDropdownMenu");

    btn.addEventListener("click", (e) => {
        e.stopPropagation();
        menu.classList.toggle("show");
    });

    document.addEventListener("click", (e) => {
        if (!menu.contains(e.target) && !btn.contains(e.target)) {
            menu.classList.remove("show");
        }
    });
});




const years = document.querySelectorAll(".year");

years.forEach(yearEl => {
    const target = parseInt(yearEl.getAttribute("data-year"));
    let count = 0;
    const speed = 20;
    const step = Math.ceil(target / 100);

    const counter = setInterval(() => {
        count += step;
        if (count >= target) {
            count = target;
            clearInterval(counter);
        }
        yearEl.textContent = count;
    }, speed);
});





document.addEventListener("DOMContentLoaded", () => {
    const wrapper = document.querySelector(".reviews-wrapper");
    const prev = document.getElementById("review-prev");
    const next = document.getElementById("review-next");
    const reviews = document.querySelectorAll(".review");

    if (!wrapper || !prev || !next || reviews.length === 0) return;

    const gap = 40;
    const cardWidth = reviews[0].offsetWidth + gap;
    let index = 0;
    let startX = 0;
    let isDown = false;

    wrapper.innerHTML += wrapper.innerHTML;
    const allReviews = document.querySelectorAll(".review");

    wrapper.style.width = `${allReviews.length * cardWidth}px`;

    function updateSlider(animate = true) {
        wrapper.style.transition = animate ? "transform 0.5s ease" : "none";
        wrapper.style.transform = `translateX(-${index * cardWidth}px)`;
    }

    function checkLoop() {
        if (index >= allReviews.length / 2) {
            index = 0;
            updateSlider(false);
        } else if (index < 0) {
            index = allReviews.length / 2 - 1;
            updateSlider(false);
        }
    }

    next.addEventListener("click", () => {
        index++;
        updateSlider();
        setTimeout(checkLoop, 500);
    });

    prev.addEventListener("click", () => {
        index--;
        updateSlider();
        setTimeout(checkLoop, 500);
    });

    wrapper.addEventListener("mousedown", (e) => {
        isDown = true;
        startX = e.pageX;
        wrapper.style.cursor = "grabbing";
    });

    wrapper.addEventListener("mouseup", (e) => {
        if (!isDown) return;
        isDown = false;
        wrapper.style.cursor = "grab";
        const diff = e.pageX - startX;
        if (diff > 50) index--;
        if (diff < -50) index++;
        updateSlider();
        setTimeout(checkLoop, 500);
    });

    wrapper.addEventListener("mouseleave", () => {
        isDown = false;
        wrapper.style.cursor = "grab";
    });

    wrapper.addEventListener("touchstart", (e) => {
        startX = e.touches[0].clientX;
    });

    wrapper.addEventListener("touchend", (e) => {
        const diff = e.changedTouches[0].clientX - startX;
        if (diff > 50) index--;
        if (diff < -50) index++;
        updateSlider();
        setTimeout(checkLoop, 500);
    });
});


const cartIcon = document.getElementById("cartIcon");
const cartSidebar = document.getElementById("cartSidebar");
const closeCart = document.getElementById("closeCart");
const cartOverlay = document.getElementById("cartOverlay");

cartIcon.addEventListener("click", () => {
    cartSidebar.classList.add("active");
    cartOverlay.classList.add("active");
});

closeCart.addEventListener("click", () => {
    cartSidebar.classList.remove("active");
    cartOverlay.classList.remove("active");
});

cartOverlay.addEventListener("click", () => {
    cartSidebar.classList.remove("active");
    cartOverlay.classList.remove("active");
});



