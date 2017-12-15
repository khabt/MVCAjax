var getValueInputToChart = {
    value0: '[Value0]',
    value1: '[Value1]',
    value2: '[Value2]',
    value3: '[Value3]',
    value4: '[Value4]',
    value5: '[Value5]',
    formatValue: function (value) {
        value = value.replace(/,/g, "")
        value = value > 0 ? value : "5000";
        return parseFloat(value);
    }
}

var densityCanvas = document.getElementById("densityChart");

Chart.defaults.global.defaultFontFamily = "Lato";
Chart.defaults.global.defaultFontSize = 18;

var densityData = {
    label: 'USD',
    data: [getValueInputToChart.formatValue(getValueInputToChart.value0),
        getValueInputToChart.formatValue(getValueInputToChart.value1),
        getValueInputToChart.formatValue(getValueInputToChart.value2),
        getValueInputToChart.formatValue(getValueInputToChart.value3),
        getValueInputToChart.formatValue(getValueInputToChart.value4),
        getValueInputToChart.formatValue(getValueInputToChart.value5)],
    backgroundColor: [
    'rgba(0, 99, 132, 0.6)',
    'rgba(0, 99, 132, 0.6)',
    'rgba(0, 99, 132, 0.6)',
    'rgba(0, 99, 132, 0.6)',
    'rgba(0, 99, 132, 0.6)',
    'rgba(0, 99, 132, 0.6)',
    ],
    bezierCurve: false,
    animation: false
};

var barChart = new Chart(densityCanvas, {
    type: 'bar',
    data: {
        labels: ["0", "5", "10", "15", "20", "25"],
        datasets: [densityData],
        backgroundColor: [
       'rgba(0, 111111, 132, 0.6)',
       'rgba(0, 111111, 132, 0.6)',
       'rgba(0, 111111, 132, 0.6)',
       'rgba(0, 111111, 132, 0.6)',
       'rgba(0, 111111, 132, 0.6)',
       'rgba(0, 111111, 132, 0.6)',
        ]
    },
    options: {
        tooltips: {
            callbacks: {
                label: function (tooltipItem, data) {
                    var value = data.datasets[0].data[tooltipItem.index];
                    value = value.toString();
                    value = value.split(/(?=(?:...)*$)/);
                    value = value.join(',');
                    return value;
                }
            } // end callbacks:
        }, //end tooltips
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    userCallback: function (value, index, values) {
                        // Convert the number to a string and splite the string every 3 charaters from the end
                        value = value.toString();
                        value = value.split(/(?=(?:...)*$)/);
                        value = value.join(',');
                        return value;
                    }
                }
            }],
            xAxes: [{
                ticks: {
                }
            }]
        }
    },
    plugins: [{
        afterRender: function () {            
            renderIntoImage()
        },
    }],
});

const renderIntoImage = () => {
    const canvas = document.getElementById('densityChart')
    const imgWrap = document.getElementById('url')
    var img = new Image();
    imgWrap.src = canvas.toDataURL();
    canvas.style.display = 'none';
}