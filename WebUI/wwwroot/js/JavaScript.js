const loadMore = document.getElementById("loadMore");
const productList = document.getElementById("productList");
const productCount = document.getElementById("productCount");
let count = 8;
loadMore.addEventListener("click", function () {
    fetch( `/Product/LoadMore?skip=${count}`) .then(data => data.text())
        .then(response => {
            productList.innerHTML += response;
            count += 8;

            if (count >= productCount.value) {
                loadMore.remove();
            }
        })
})
