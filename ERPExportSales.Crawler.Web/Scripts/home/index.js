
function initIndex(usertype) {
    $('#tbHoliday').DataTable({
        "bSort": false,
        "searching": false,
        "pagingType": "simple",
        "info": false,
        "pageLength": 15,
        "lengthChange": false
    });
    // $('#tbOrders').DataTable();
    $('#tbFreight').DataTable({
        "bSort": false,
        "searching": false,
        "pagingType": "simple",
        "info": false,
        "pageLength": 15,
        "lengthChange": false
    });
    $('#infoUl a:last').click(function (e) {
        e.preventDefault();
        $(this).tab('show');
        $("#usdcnyDiv").css("width", $("#usdcnyDiv").width());
        $("#usdcnyDiv").css("height", "300px");
        $("#q195Div").css("width", $("#q195Div").width());
        $("#q195Div").css("height", "300px");

        var q195div = echarts.getInstanceByDom(document.getElementById("q195Div"));
        var usdcnyDiv = echarts.getInstanceByDom(document.getElementById("usdcnyDiv"));
        if (q195div != null && q195div != "" && q195div != undefined) {
            q195div.dispose();
        }
        if (usdcnyDiv != null && usdcnyDiv != "" && usdcnyDiv != undefined) {
            usdcnyDiv.dispose();
        }
        var myChart1 = echarts.init(document.getElementById('usdcnyDiv'));
        var myChart2 = echarts.init(document.getElementById('q195Div'));
        myChart1.showLoading();
        myChart2.showLoading();
        $.ajax({
            type: "POST",
            url: "/Chart/GetUSDCNY",
            dataType: "json",
            success: function (data) {
                drawUSDCNYLines(myChart1, data);
                myChart1.hideLoading();
                //  myChart1.resize();
            }
        });
        $.ajax({
            type: "POST",
            url: "/Chart/GetQ195",
            dataType: "json",
            success: function (data) {
                drawQ195Lines(myChart2, data);
                myChart2.hideLoading();
                //  myChart2.resize();
            }
        });

        $.ajax({
            type: "POST",
            url: "/Chart/GetNewUSDCNY",
            dataType: "json",
            success: function (data) {
                $("#newUSDCNY").html("Update time:  <b>" + data.PublishDate + "</b> USDCNY: <h4 class='box-title'><b>" + data.Price + "</b></h4>");
            }
        });
        $.ajax({
            type: "POST",
            url: "/Chart/GetNewQ195",
            dataType: "json",
            success: function (data) {
                $("#newQ195").html("Update time:  <b>" + data.PublishDate + "</b> Price: <h4 class='box-title'><b>" + data.Price + "</b></h4> RMB/T");
            }
        });
         q195div = echarts.getInstanceByDom(document.getElementById("q195Div"));
         usdcnyDiv = echarts.getInstanceByDom(document.getElementById("usdcnyDiv"));
        window.onresize = function () {
            $("#usdcnyDiv").css("width", "100%");
            $("#q195Div").css("width", "100%");
            q195div.resize();
            usdcnyDiv.resize();
        }
     
    })

    //$.getJSON("../../Files/data/data.json", function (data, status) {
    //    if (status == 'success') {
    //        result = data;
    //        drawLines(data);
    //    } else {
    //        return false;
    //    }
    //})
    // var dataSource = getData();


    // 根据配置项和数据显示图表
    hrefChangePasswordPage(usertype);
}

function tabEventListener(q195div,usdcnyDiv) {
        if (q195div != null && q195div != undefined) {
            $("#q195Div").css("width", $("#q195Div").width());
            q195div.resize();
        }
        if (usdcnyDiv != null && usdcnyDiv != undefined) {
            $("#usdcnyDiv").css("width", $("#usdcnyDiv").width());
            usdcnyDiv.resize();
        }
}

function hrefChangePasswordPage(usertype) {
    $("#hrefChangePassowrd").click(function () {
        if (usertype == 2) {
            $("#OldPassword").val("");
            $("#NewPassword").val("");
            $("#ConfirmPassword").val("");
            $("#form-changepassword").validate().resetForm();
            $("#modal-changepassword").modal("show");
        } else {
            $("#modal-changepassword-noshow").modal("show");
        }
    })
}


function drawUSDCNYLines(myChart, data) {
    myChart.setOption({
        title: {
            text: 'USDCNY',
            x: 'center'
        },
        tooltip: {
            trigger: 'axis'
        },
        grid: {
            left: 50,
            right: 40
        },
        xAxis: {
            data: data.map(function (item) {
                return item.PublishDate;
            }),
            inverse: true
        },
        yAxis: {
            name: 'RMB',
            min: 6
        },
        series: [
            {
                name: 'USDCNY',
                type: 'line',
                //markPoint: {
                //    data: [{ type: 'min', name: '最小值', symbolSize: 70 }]
                //},
                data: data.map(function (item) {
                    return item.Price;
                })
            }
            //, {
            //    name: '现汇买入',
            //    type: 'line',
            //    markPoint: {
            //        data: [{ type: 'max', name: '最大值', symbolSize: 70 }]
            //    },
            //    data: data.map(function (item) {
            //        return item[0];
            //    })
            //}, {
            //    name: '现钞买入',
            //    type: 'line',
            //    markPoint: {
            //        data: [{ type: 'max', name: '最大值', symbolSize: 70 }]
            //    },
            //    data: data.map(function (item) {
            //        return item[1];
            //    })
            //}
        ]
    });
}
function drawQ195Lines(myChart, data) {
    myChart.setOption({
        title: {
            text: 'Q195',
            x: 'center'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            top: 25,
            left: 'right',
            data: []
        },
        grid: {
            left: 50,
            right: 40
        },
        xAxis: {
            data: data.map(function (item) {
                return item.PublishDate;
            }),
            inverse: true
        },
        yAxis: {
            name: 'Price',
            min: 2500
        },
        series: [
            {
                name: 'Q195 Price',
                type: 'line',
                //markPoint: {
                //    data: [{ type: 'min', name: '最小值', symbolSize: 70 }]
                //},
                data: data.map(function (item) {
                    return item.Price;
                })
            }
        ]
    });
}
function onBegin() {
    $("#imgPLoading").show();
    $("#btnPSubmit").removeClass("btn-primary");
    $("#btnPSubmit").addClass("btn-gray");
    $("#btnPSubmit").attr("disabled", true);
}
function onSuccess(data) {
    if (data.Result) {
        $("#modal-changepassword-success").modal("show");
    } else {
        alert(data.Message);
    }
}
function onFailure(xhr, status, error) {
    alert(error);
}
function onComplete() {
    $("#imgPLoading").hide();
    $("#btnPSubmit").removeClass("btn-gray");
    $("#btnPSubmit").addClass("btn-primary");
    $("#btnPSubmit").removeAttr("disabled");
}
function confimRedirectLogin() {
    location.href = "/Account/Login";
}
function closeChangePassword() {
    $("#OldPassword").val("");
    $("#NewPassword").val("");
    $("#ConfirmPassword").val("");
    $("#modal-changepassword").modal("hide");
    $("#form-changepassword").validate().resetForm();
}