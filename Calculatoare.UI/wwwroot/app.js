let listProductHTML = document.querySelector('.row line');
let listCartHTML = document.querySelector('.listCart');
let iconCartSpan = document.querySelector('.login-item span');


let listProducts = [];
let carts = [];

const addDataToHTML = () => {
    listProductHTML.innerHTML = '';
    if(listProducts.length > 0){
        listProducts.forEach(product => {
            let newProduct = document.createElement('div');
            newProduct.classList.add('border');
            newProduct.dataset.id = product.id;
            newProduct.innerHTML = `
            <div class="cutie">
                    <img src="${product.image}"onmouseover="hover10(this);" onmouseout="unhover10(this);">
                </div>
            <h4>${product.name}</h4>
            <h3>${product.price}</h3>
            <a href="" class="hero-btn hero-btnn red-btn">Adaugă în coș.</a>
            `;
            listProductHTML.appendChild(newProduct);
        })
    }
}
listProductHTML.addEventListener('click', (event) => {
    let positionClick = event.target;
    if (positionClick.classList.contains('hero-btn hero-btnn red-btn')){
        let product_id = positionClick.parentElement.dataset.id;
        addToCart(product_id);
    }
})

const addToCart = (product_id) => {
    let positionThisProductInCart = carts.findIndex((value) => value.product_id == product_id);
    if (carts.length <= 0) {
        carts = [{
            product_id: product_id,
            quantity: 1
        }]
    } else if (positionThisProductInCart < 0) {
        carts.push({
            product_id: product_id,
            quantity: 1
        });
    } else {
        carts[positionThisProductInCart].quantity = carts[positionThisProductInCart].quantity + 1;
    }
    addCartToHTML();
    addCartToMemory();
}
const addCartToMemory = () => {
    localStorage.setItem('shop', JSON.stringify(carts));
}
const addCartToHTML = () => {
    listCartHTML.innerHTML = '';
    let totalQuantity = 0;
    if (carts.length > 0) {
        carts.forEach(cart => {
            totalQuantity = totalQuantity + cart.quantity;
            let newCart = document.createElement('div');
            newcart.classList.add('border-col');
            let positionProduct = listProduct.findIndex((value) => value.id == cart.product_id);
            let info = listProducts[positionProduct];
            newCart.innerHTML = `
            <div class="image">
                <img src="${info.image}" alt="">
            </div>
            <div class="name">
                ${info.name}
            </div>
            <div class="price">
                $${info.price * cart.quantity}
            </div>
            <div class="quantity">
                <span class="minus"> - </span>
                <span>${cart.quantity}</span >
                <span class="plus"> + </span>
            </div>
            `;
            listCartHTML.appendChild(newCart);
        })
    }
    iconCartSpan.innerText = totalQuantity;
}
const initApp = () => {
    //get data from json
    fetch('products.json')
    .the(response => response.json())
    .then(data => {
        listProducts = data;
        addDataToHTML();

        if (localStorage.getItem('shop')) {
            carts = JSON.parse(localStorage.getItem('shop'));
            addCartToHTML();
        }
    })
}