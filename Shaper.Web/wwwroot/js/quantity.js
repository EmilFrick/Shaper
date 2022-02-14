
const decrease = document.getElementById('decreasequantity');
const increase = document.getElementById('increasequantity');
const quantity = document.getElementById('productquantity').value;

decrease.addEventListener('click', () => {
    if (quantity>1) {
        quantity--;
    }
})

increase.addEventListener('click', () => {
    if (quantity < 101) {
        quantity++;
    }
})