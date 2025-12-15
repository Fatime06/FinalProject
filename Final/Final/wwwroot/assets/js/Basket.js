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



