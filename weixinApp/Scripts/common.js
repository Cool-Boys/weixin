
$.fn.formSerialize = function (formdate) {
    var element = $(this);
    if (!!formdate) {
        for (var key in formdate) {
            var $id = element.find('#' + key);
            if ($id.length > 0) {
                var value = formdate[key];
                var type = $id.attr('type');
                if ($id.hasClass("select2-hidden-accessible")) {
                    type = "select";
                }
                switch (type) {
                    case "checkbox":
                        if (value == "true") {
                            $id.attr("checked", 'checked');
                        } else {
                            $id.removeAttr("checked");
                        }
                        break;
                    case "select":
                        $id.val(value).trigger("change");
                        break;
                    default:
                        $id.val(value);
                        break;
                }
            }
        };
        return false;
    }
    var postdata = {};
    element.find('input,select,select2,textarea,checkbox,hidden').each(function (r) {
        var $this = $(this);
        var id = $this.attr('id');
        var type = $this.attr('type');
        switch (type) {
            case "checkbox":
                postdata[id] = $this.is(":checked");
                break;
            case "select2":
                var value = $this.val();
                if (value == null && $("#" + id + " option").length > 0) {
                    value = $("#" + id + " option:first").val()
                }
                postdata[id] = value;
                break;
            default:
                var value = $this.val();
                postdata[id] = value;
                break;
        }
    });

    if ($('[name=__RequestVerificationToken]').length > 0) {
        postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    return postdata;
};











