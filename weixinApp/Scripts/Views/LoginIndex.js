$(function () {
    $(document).on("pageInit", "#pageIndex", function (e, id, page) {



    });


    $.init();
    setTimeout(function () {
        $('#loadImg').removeClass('page-current');
        $('#pageIndex').addClass('page-current');
    }, 2000);
    $('#btnLog').on('click', btnLogin);
    $('#btnReg').on('click', btnReg);
    $('#btnSub').on('click', function () {
        $.confirm('Are you sure?', function () {
            $.showPreloader("模拟提交中....");
            window.setTimeout(function () {
                $.hidePreloader();
                $.alert('注册成功！', '提示', function () {
                    showIndex();
                });
            }, 1000);



        });


    });
    $('#btnCan').on('click', function () {
        showIndex();
    });

});


function showIndex() {

    $('#pageReg').removeClass('page-current');
    $('#pageIndex').addClass('page-current');
}

function showReg() {
    $('#pageIndex').removeClass('page-current');
    $('#pageReg').addClass('page-current');
}

function btnReg() {
    showReg();

}

function btnLogin() {

    var param = $('#listBlock').formSerialize();
    if (param.username.trim() == "") {
        $('#showMes').text("用户不能为空！");
        return false;

    }
    if (param.password.trim() == "") {
        $('#showMes').text("密码不能为空！");
        return false;

    }
    $.ajax({
        type: 'GET',
        url: '/Login/CheckLogin',
        data: param,
        dataType: 'json',
        beforeSend: function (xhr, settings) {
            $.showPreloader("加载中....");
        },
        timeout: 5000,
        context: $('#data'),
        success: function (data) {
            //加载ajax页面
            $('#showMes').text(data.message).css('color', 'green');
            if (data.state == "success") {
                window.setTimeout(function() {
                    $.hidePreloader();
                    window.location.href = "/Home/Index";
                    //   $.router.load("/Home/Index", true);
                }, 2000);
            } else {
                $.hidePreloader();
            }

        },
        complete: function (xhr, status) {

        },
        error: function (xhr, type) {
            alert('Ajax error!');
        }
    });
}