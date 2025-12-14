document.addEventListener("click", function (e) {
    const btn = e.target.closest(".add-to-cart");
    if (!btn) return;

    e.preventDefault();

    const productId = parseInt(btn.dataset.id);

    const cartDiv = btn.closest(".cart");
    const qtyInput = cartDiv.querySelector(".add-qty");

    let quantity = parseInt(qtyInput.value);
    if (isNaN(quantity) || quantity < 1) quantity = 1;

    const formData = new URLSearchParams();
    formData.append("ProductId", productId);
    formData.append("Quantity", quantity);

    fetch("/basket/addtobasket", {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: formData.toString()
    })
        .then(res => {
            if (!res.ok) {
                return res.text().then(msg => { throw msg; });
            }
            return res.text();
        })
        .then(html => {
            document.getElementById("basketContent").innerHTML = html;

            const meta = document.querySelector("#basketMeta");
            document.getElementById("basketCount").textContent =
                meta ? meta.dataset.count : 0;
        })
        .catch(msg => {
            alert(msg);
        });
});



(function () {

    window.productFilter = {
        categoryId: null,
        brandId: null,
        minPrice: null,
        maxPrice: 1000,
        page: 1
    };

    function loadProducts() {
        const params = new URLSearchParams(window.productFilter).toString();

        fetch(`/Shop/Filter?${params}`)
            .then(res => res.text())
            .then(html => {
                const wrapper = document.querySelector(".products-area-wrapper");
                if (wrapper) {
                    wrapper.innerHTML = html;
                }
            });
    }

    document.addEventListener("input", function (e) {

        if (e.target && e.target.id === "priceRange") {

            const value = e.target.value;

            const priceSpan = document.getElementById("priceValue");
            if (priceSpan) {
                priceSpan.textContent = value;
            }

            window.productFilter.maxPrice = value;
            window.productFilter.page = 1;

            loadProducts();
        }
    });

    document.addEventListener("click", function (e) {

        const categoryItem = e.target.closest(".filter-category");
        if (!categoryItem) return;

        window.productFilter.categoryId = categoryItem.dataset.category;
        window.productFilter.page = 1;

        loadProducts();
    });

    document.addEventListener("click", function (e) {

        const brandItem = e.target.closest(".filter-brand");
        if (!brandItem) return;

        window.productFilter.brandId = brandItem.dataset.brand;
        window.productFilter.page = 1;

        loadProducts();
    });

    document.addEventListener("click", function (e) {

        const pageLink = e.target.closest(".pagination .page");
        if (!pageLink) return;

        e.preventDefault();

        const page = pageLink.dataset.page;
        if (!page) return;

        window.productFilter.page = page;
        loadProducts();
    });

    document.addEventListener("DOMContentLoaded", function () {
        loadProducts();
    });
    document.addEventListener("click", function (e) {

        const brandItem = e.target.closest(".filter-brand");
        if (!brandItem) return;

        document.querySelectorAll(".filter-brand")
            .forEach(x => x.classList.remove("active"));

        brandItem.classList.add("active");

        window.productFilter.brandId = brandItem.dataset.brand;
        window.productFilter.page = 1;

        loadProducts();
    });
    document.addEventListener("click", function (e) {

        const categoryItem = e.target.closest(".filter-category");
        if (!categoryItem) return;

        document.querySelectorAll(".filter-category")
            .forEach(x => x.classList.remove("active"));

        categoryItem.classList.add("active");

        window.productFilter.categoryId = categoryItem.dataset.category;
        window.productFilter.page = 1;

        loadProducts();
    });
})();

document.addEventListener("click", function (e) {
    const btn = e.target.closest(".remove-item");
    if (!btn) return;

    e.preventDefault();

    const id = btn.dataset.id;

    fetch(`/basket/remove/${id}`, {
        method: "POST"
    })
        .then(res => res.text())
        .then(html => {
            const basket = document.getElementById("basketContent");
            basket.innerHTML = html;

            const meta = basket.querySelector("#basketMeta");
            const totalCount = meta ? meta.dataset.count : 0;

            const badge = document.getElementById("basketCount");
            if (badge) badge.textContent = totalCount;
        });
});



