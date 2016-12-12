$(function () {
    $(document).on("pageInit", "#router", function (e, id, page) {
        ////此种方法多次注册click事件
        //var $content = $(page).find(".card-header>a");

        //$content.on('click', function (e) {
        //   var clNo= $(this).text();
        //   GetDetail(clNo);
        //});

        $(document).on('refresh', '.pull-to-refresh-content', function (e) {

            var cardNumber = $(e.target).find('.card').length / 5 + 1;

            $.ajax({
                type: 'GET',
                url: '/Home/getCl',
                data: { count: cardNumber },
                beforeSend: function (xhr, settings) {
                    $.showPreloader("加载中....");
                },
                dataType: 'json',
                timeout: 3000,
                context: $('#data'),
                success: function (data) {
                    var datas = $.parseJSON(data.message);
                    var cardHTML = "";
                    setTimeout(function() {
                        $.each(datas, function(i, row) {

                            cardHTML += '<div class="card">' +
                                '<div class="card-header"><a href="#router3" onclick="GetDetail(\''+ row.CL_NO +'\') " class=" link">' + row.CL_NO + '</a></div>' +
                                '<div class="card-content">' +
                                '<div class="card-content-inner">' +
                                row.CL_NO + '|' + row.SNAME + '|' + row.SCALE +
                                '</div>' +
                                '</div>' +
                                '</div>';


                        });
                        $(e.target).find('.card-container').prepend(cardHTML);
                        // 加载完毕需要重置
                        $.pullToRefreshDone('.pull-to-refresh-content');
                        $.hidePreloader();
                    }, 1000);
                },
                complete: function (xhr, status) {



                },
                error: function (xhr, type) {
                    alert('Ajax error!' + type);
                }
            });


            // 模拟2s的加载过程
        
        });

    });
    $(document).on("pageInit", "#router3", function (e) {

    });

    $('#btnReturn').on('click', function () {
        //$.router.load("/Login/Index");  //加载ajax页面
        window.location.href = "/Login/Index";
    });
    $.init();
   // $('#clPage').load('Index');
    $('#btnIndex').on('click', function () {
        $(".bar-tab").find('.tab-item').removeClass('active');
        $('#btnIndex').addClass('active');
        $('#clPage').load('Index2').css('margin-top','60px');
    });
    $('#btnHome').on('click', function () {
        $(".bar-tab").find('.tab-item').removeClass('active');
        $('#btnHome').addClass('active');
        $('#clPage').load('IndexHome').css('margin-top', '0');
    });
    $('#btnIndexPic').on('click', function () {
        $(".bar-tab").find('.tab-item').removeClass('active');
        $('#btnIndexPic').addClass('active');
        $('#clPage').load('IndexPic').css('margin-top', '60px');
    });
});




function getRootPath(me) {
    var script = document.getElementById("common");
    me = !!document.querySelector ? script.src : script.getAttribute('src', 4);
    var srcBase = me.replace("/Scripts/common.js", "");;
    return srcBase;
};

function GetDetail(clNo) {
    var pathname = window.location.pathname.length == 1 ? "" : window.location.pathname;
    $.ajax({
        type: 'GET',
        url: '/Home/Detail',
        data: { clNo: clNo },
        beforeSend: function (xhr, settings) {
            $.showPreloader("加载中....");
        },
        dataType: 'json',
        timeout: 3000,
        context: $('#data'),
        success: function (data) {

            setTimeout(function () {
                $('#list').formSerialize($.parseJSON(data.message));
                $.hidePreloader();
            }, 500);

        },
        complete: function (xhr, status) {

          

        },
        error: function (xhr, type) {
            alert('Ajax error!' + type);
        }
    });
}
