$(function () {
   
    var openId = GetQueryString("openid");
    //$('#username').val(decodeURI(nickName));
    $('#openId').val(openId);
    $('#btnBind').on('click', ghBind);
});

function ghBind() {
    var openId = GetQueryString("openid");
    var nickName = $('#username').val();
    var zgNo = $('#zgno').val();
    if (zgNo.trim() == "") {
        $.toast("请输入工号！");
        return false;
    }

    var param = {};
    param.openId = openId;
    param.nickname = nickName;
    param.zgno = zgNo;
    $.ajax({
        type: 'GET',
        url: '/GHBind/GhBind',
        data: param,
        dataType: 'json',
        beforeSend: function (xhr, settings) {
            $.showPreloader("加载中....");
        },
        timeout: 5000,
        //context: $('#data'),
        success: function (data) {
            //加载ajax页面
            $('#showMes').text(data.message);
            if (data.state == "success") {
                $('#showMes').css('color', 'green');
                $.hidePreloader();
            }
            else if (data.state == "error") {
                $('#showMes').css('color', 'red');
                $.hidePreloader();
            }
            else {
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


function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}