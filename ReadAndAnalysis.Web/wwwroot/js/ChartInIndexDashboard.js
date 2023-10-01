 <script>
            Chart.defaults.global.defaultFontFamily = "IranSans";

            var config1 = {
                type: "bar",
                data: {
                    labels: ["@(sps[0].ShownDate)", "@(sps[1].ShownDate)", "@(sps[2].ShownDate)", "@(sps[3].ShownDate)", "@(sps[4].ShownDate)", "@(sps[5].ShownDate)"],
                    datasets: [{
                        backgroundColor: "rgba(151,187,205,0.5)",
                        borderColor: "rgba(151,187,205,0.7)",
                        borderWidth: 2,
                        label: "تعداد کل خبر ها",
                        data: [@(sps[0].NotOilNewsCount), @(sps[1].NotOilNewsCount), @(sps[2].NotOilNewsCount), @(sps[3].NotOilNewsCount)
                                                            , @(sps[4].NotOilNewsCount), @(sps[5].NotOilNewsCount)]
                    }]
                },
                options: {
                    maintainAspectRatio: false,
                    responsive: true,
                    title: {
                        display: true
                    },
                    hover: {
                        mode: "nearest",
                        intersect: true
                    },
                    scales: {
                        xAxes: [{
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: "زمان"
                            }
                        }],
                        yAxes: [{
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: "مقدار"
                            },
                            ticks: {
                                suggestedMin: -10,
                                suggestedMax: 250
                            },
                        }]
                    }
                }
            };
            window.onload = function () {
                var ctx = document.getElementById("bar1").getContext("2d");
                window.bar1 = new Chart(ctx, config1);
            };
      
            Chart.defaults.global.defaultFontFamily = "IranSans";

            var config2 = {
                type: "bar",
                data: {
                    labels: ["@(sps[0].ShownDate)", "@(sps[1].ShownDate)", "@(sps[2].ShownDate)", "@(sps[3].ShownDate)", "@(sps[4].ShownDate)", "@(sps[5].ShownDate)"],
                    datasets: [{
                        backgroundColor: "rgba(155,180,202,0.5)",
                        borderColor: "rgba(156,175,200,0.7)",
                        borderWidth: 2,
                        label: "تعداد خبر های نفتی",
                        data: [@(sps[0].OilNewsCount), @(sps[1].OilNewsCount), @(sps[2].OilNewsCount), @(sps[3].OilNewsCount)
                                                                                                , @(sps[4].OilNewsCount), @(sps[5].OilNewsCount)]
                    }]
                },
                options: {
                    maintainAspectRatio: false,
                    responsive: true,
                    title: {
                        display: true
                    },
                    hover: {
                        mode: "nearest",
                        intersect: true
                    },
                    scales: {
                        xAxes: [{
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: "زمان"
                            }
                        }],
                        yAxes: [{
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: "مقدار"
                            },
                            ticks: {
                                suggestedMin: 0,
                                suggestedMax: @maxSp
                                        },
                        }]
                    }
                }
            };
            window.onload = function () {
                var ctx = document.getElementById("bar2").getContext("2d");
                window.bar2 = new Chart(ctx, config2);
            };
        </script>