$(document).ready(function () {
    $("#button-check2").click(function () {
        var content = CKEDITOR.instances['txtnoidung'].getData();
        var test = $('#oldText').val();

        var obj = {
            content: content
        }

        $.ajax({
            url: "CheckCk",
            data: JSON.stringify(obj),
            type: "Post",
            contentType: "application/json",
            dataType: "json",
            success: function (res) {
                console.log("ok", res);
                if ($('#oldText').val() == '') {
                    $("#oldText").val(content)
                }
                CKEDITOR.instances['txtnoidung'].setData(res.Message);
                if (res.Count == 1)
                    $('#content').html("Có tồn tại từ bị cấm");
                else
                    $('#content').html("Không tồn tại từ bị cấm");
            },
            error: function (res) {
                console.log("errr");
            }
        })
    })

    $("#button-thaythe2").click(function () {
        var content = CKEDITOR.instances['txtnoidung'].getData();
        var bandau = $('#oldText').val()
        if (bandau!="")
            content = bandau;
   


        var obj = {
            content: content
        }

        $.ajax({
            url: "ThayThe",
            data: JSON.stringify(obj),
            type: "Post",
            contentType: "application/json",
            dataType: "json",
            success: function (res) {
                console.log("ok", res);
                //if ($('#oldText').val() == '') {
                //    $("#oldText").val(content)
                //}
                CKEDITOR.instances['txtnoidung'].setData(res.Message);
                $('#content').html("Đã thay thế");
            },
            error: function (res) {
                console.log("errr");
            }
        })
    })
});