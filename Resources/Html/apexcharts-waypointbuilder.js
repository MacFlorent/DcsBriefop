function ApexChartFormatDataLabel(arrayLabels) {
	let labelOut = [];
	let iOut = 0;
	let iIn = 0;

	while (iIn < arrayLabels.length) {

		if (arrayLabels[iIn] != 0) {
			labelOut[iOut] = arrayLabels[iIn];
			iOut++;
		}

		iIn++;
	}

	return labelOut;
}

function ApexChartCreate(title, dataPoints, waypointNames, waypointTracks, waypointSpeeds, fontSize, color, selector) {
	var options = {
		title: {
			text: title,
			align: "right",
			margin: 0,
			offsetX: 0,
			offsetY: 50,
			floating: false,
			style: {
				fontSize: fontSize,
				fontWeight: "bold",
				fontFamily: undefined,
				color: color
			},
		},
		chart: {
			type: "line",
			toolbar: {
				show: false
			}
		},
		colors: [color],
		tooltip: {
			enabled: false,
		},
		dataLabels: {
			enabled: true,
			textAnchor: "middle",
			style: {
				fontSize: fontSize,
				fontWeight: 400
			},
			formatter: function (val, opt) {
				return ApexChartFormatDataLabel([val, waypointTracks[opt.dataPointIndex], waypointSpeeds[opt.dataPointIndex]]);
			},
			offsetY: -10
		},
		stroke: {
			curve: "smooth",
		},
		grid: {
			borderColor: '#e7e7e7',
		},
		markers: {
			enabled: true,
			size: 5
		},
		series: [{
			name: "altitude",
			data: dataPoints,
		}],

		xaxis: {
			labels: {
				formatter: function (value) {
					return [value, waypointNames[value]]
				}
			}
		},
		yaxis: {
			title: {
				text: 'Altitude', forceNiceScale: true
			}
		}
	}

	var chart = new ApexCharts(document.querySelector("#" + selector), options);
	chart.render();
}