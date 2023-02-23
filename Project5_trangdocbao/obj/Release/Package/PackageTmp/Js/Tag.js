$(document).ready(function () {
    loadData();
    add();
});

function loadData(IdThe) {

    $.ajax({
        url: "/Admin/Tag/GetAllTag",
        type: "Post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (res) {
            $.each(res, function (k, v) {
                
                if (v.IdThe == IdThe)
                    $('#selectThe').append("<option value=" + v.IdThe + " selected>" + v.Name + "</option>");
                else
                    $('#selectThe').append("<option value=" + v.IdThe + " >" + v.Name + "</option>");

            });
        },
        error: function (res) {
            console.log("errr", res)
        }
    });
}

function add() {
    $("#add-tag-button").click(function () {
        var name = $('#ipTag').val();
        var obj = {
            name: name,
        }
        $.ajax({
            url: "/Admin/Tag/AddTag",
            data: JSON.stringify(obj),
            type: "Post",
            contentType: "application/json",
            dataType: "json",
            success: function (res) {
                alert("thêm thành công");
                $('#ipTag').val('');
                $("#selectThe").find('option').remove();
                loadData(res.IdThe);
                //$('#select').find('option[value="' + res.IdThe + '"]').attr("selected", true);
                //$('#selectThe').val(res.IdThe).change();
                
            },
            error: function (res) {
                console.log("errr"); 
                alert("thêm thất bại");
            }
        })
    });
}