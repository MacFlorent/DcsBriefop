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

function ApexChartFormatAxisX(value, label) {
	let labelOut = [value];
	if (label)
		labelOut[1] = label;

	return labelOut;
}

function ApexChartCreate(title, dataPoints, waypointNames, waypointTracks, waypointSpeeds, lineColor, width, height, selector) {
	const htmlElement = document.querySelector("#" + selector)
	const style = getComputedStyle(htmlElement)

	if(lineColor == null) {
		lineColor = style.accentColor;
	}

	const options = {
		chart: {
			type: "line",
			toolbar: {
				show: false
			},
			animations: {
				enabled: false
			},
			fontFamily: style.fontFamily,
			foreColor: style.color,
			width: width,
			height: height
		},
		title: {
			text: title,
			align: "right",
			margin: 0,
			offsetX: 0,
			offsetY: 0,
			floating: false,
			style: {
				fontSize: style.fontSize,
				fontWeight: 400,
			},
		},
		colors: [lineColor],
		tooltip: {
			enabled: false,
		},
		dataLabels: {
			enabled: true,
			textAnchor: "middle",
			style: {
				//fontSize: style.fontSize,
				fontFamily: style.fontFamily,
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
			borderColor: style.borderColor,
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
					return ApexChartFormatAxisX(value, waypointNames[value])
				},
				style: {
					//fontSize: style.fontSize,
					fontFamily: style.fontFamily,
					//fontWeight: 400
				},
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