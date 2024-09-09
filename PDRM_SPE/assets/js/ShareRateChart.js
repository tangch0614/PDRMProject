
function generateChartData(data) {
    var chartDataList = data;
    var chartDataArray = chartDataList.split(";");
    for (var i = 0; i < chartDataArray.length; i++) {
        var data = chartDataArray[i].split(",");
        var dateSplit = data[0].split(/-| |:/);
        var dataDate = new Date(dateSplit[0], dateSplit[1] - 1, dateSplit[2], dateSplit[3], dateSplit[4], dateSplit[5], 0);
        chartData.push({ date: dataDate, rate: parseFloat(data[1]), unit: parseFloat(data[2]) });
    }
}

function generateChart(rateText, unitText, dateText) {
    chart = new AmCharts.AmStockChart();

    // DATASETS //////////////////////////////////////////
    var dataSet = new AmCharts.DataSet();
    dataSet.color = "#b0de09";
    dataSet.fieldMappings = [{
        fromField: "rate",
        toField: "rate"
    }, {
        fromField: "unit",
        toField: "unit"
    }];
    dataSet.dataProvider = chartData;
    dataSet.categoryField = "date";

    // set data sets to the chart
    chart.dataSets = [dataSet];

    // AXES ///////////////////////////////////////////
    var categoryAxesSettings = new AmCharts.CategoryAxesSettings();
    categoryAxesSettings.maxSeries = 0;
    categoryAxesSettings.minPeriod = "mm";
    chart.categoryAxesSettings = categoryAxesSettings;

    // PANELS ///////////////////////////////////////////
    // first stock panel
    var stockPanel1 = new AmCharts.StockPanel();
    stockPanel1.showCategoryAxis = false;
    stockPanel1.title = rateText;
    stockPanel1.percentHeight = 70;

    // graph of first stock panel
    var graph1 = new AmCharts.StockGraph();
    graph1.valueField = "rate";
    graph1.bullet = "round";
    graph1.bulletColor = "#FFFFFF";
    graph1.bulletBorderColor = "#00BBCC";
    graph1.bulletBorderAlpha = 1;
    graph1.bulletBorderThickness = 2;
    graph1.bulletSize = 7;
    graph1.lineThickness = 2;
    graph1.lineColor = "#00BBCC";
    graph1.fillAlphas = 0.4,
    graph1.useDataSetColors = false;
    stockPanel1.addStockGraph(graph1);

    // create stock legend
    var stockLegend1 = new AmCharts.StockLegend();
    stockLegend1.valueTextRegular = " ";
    stockLegend1.markerType = "none";
    stockPanel1.stockLegend = stockLegend1;

    // second stock panel
    var stockPanel2 = new AmCharts.StockPanel();
    stockPanel2.title = unitText;
    stockPanel2.percentHeight = 30;
    var graph2 = new AmCharts.StockGraph();
    graph2.valueField = "unit";
    graph2.type = "column";
    graph2.lineColor = "#00BBCC";
    graph2.fillAlphas = 1;
    //graph2.useDataSetColors = false;
    stockPanel2.addStockGraph(graph2);

    // create stock legend
    var stockLegend2 = new AmCharts.StockLegend();
    stockLegend2.valueTextRegular = " ";
    stockLegend2.markerType = "none";
    stockPanel2.stockLegend = stockLegend2;

    // set panels to the chart
    chart.panels = [stockPanel1, stockPanel2];

    // OTHER SETTINGS ////////////////////////////////////
    var scrollbarSettings = new AmCharts.ChartScrollbarSettings();
    scrollbarSettings.graph = graph1;
    scrollbarSettings.updateOnReleaseOnly = false;
    chart.chartScrollbarSettings = scrollbarSettings;

    var cursorSettings = new AmCharts.ChartCursorSettings();
    cursorSettings.valueBalloonsEnabled = true;
    cursorSettings.graphBulletSize = 1;
    chart.chartCursorSettings = cursorSettings;

    var panelsSettings = new AmCharts.PanelsSettings();
    panelsSettings.creditsPosition = "bottom-right";
    panelsSettings.marginRight = 16;
    panelsSettings.marginLeft = 16;
    chart.panelsSettings = panelsSettings;


    // PERIOD SELECTOR ///////////////////////////////////
    var periodSelector = new AmCharts.PeriodSelector();
    periodSelector.periods = [{
        period: "DD",
        count: 10,
        label: "10 days"
    }, {
        period: "MM",
        count: 1,
        selected: true,
        label: "1 month"
    }, {
        period: "YYYY",
        count: 1,
        label: "1 year"
    }, {
        period: "YTD",
        label: "YTD"
    }, {
        period: "MAX",
        label: "MAX"
    }];
    chart.periodSelector = periodSelector;

    chart.write('chartdiv');
}