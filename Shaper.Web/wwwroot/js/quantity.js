
const decrease = document.getElementById('decreasequantity');
const increase = document.getElementById('increasequantity');
let quantityDisplay = document.getElementById('productquantity');
let quantity = quantityDisplay.value;

decrease.addEventListener('click', () => {
    if (quantity>1) {
        quantity--;
        quantityDisplay.value = quantity;
    }
})

increase.addEventListener('click', () => {
    if (quantity < 101) {
        quantity++;
        quantityDisplay.value = quantity;
    }
})