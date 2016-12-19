$(function () {

    var code = GetQueryString("code");
    $('#showMes').text(code);
    //var openId = GetQueryString("openid");
    //  var url = 'https://api.weixin.qq.com/sns/oauth2/access_token?appid=wx44f8483b5dd952d0&secret=48dd07d24ca75aff634e7e50e4b5ceb1&code=' + code + '&grant_type=authorization_code';
    //$.getJSON(url,
    //    function (data) {
    //        $('#openId').val(data);
    //        $.toast(data);
    //    });


    $.ajax({
        type: 'GET',
        url: '/GHBind/GetOpenId',
        data: { code: code },
        dataType: 'json',
        beforeSend: function (xhr, settings) {
            $.showPreloader("加载中....");
        },
        //timeout: 5000,
        //context: $('#data'),
        success: function (data) {

            if (data.state == "success") {

                if (data.data.isExist == "1") {
                    $.alert('用户已经绑定！', function () {
                        wx.closeWindow();
                    });

                } else {
                    //加载ajax页面
                    $('#openId').val(data.data.openid);
                    $('#username').val(data.data.nickname);
                }
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


    //$('#username').val(decodeURI(nickName));
    //$('#openId').val(openId);
    $('#btnBind').on('click', ghBind);
});




function ghBind() {
    var openId = $('#openId').val();
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
                $.alert('用户绑定成功！', function () {
                    wx.closeWindow();
                });
            }
            else if (data.state == "error") {
                $('#showMes').css('color', 'red');
                $.hidePreloader();
                $.alert('用户绑定失败！');
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