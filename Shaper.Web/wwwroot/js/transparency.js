let range = document.getElementById('customRange1')
let rangeOutput = document.getElementById('range-output')
rangeOutput.innerText = `${range.value} %`;

range.addEventListener('input', e => {
    rangeOutput.innerText = `${range.value} %`;
})

