$(document).ready(function () {
    var placeholderElement = $('#modal-placeholder');
    var isChangeTieuDe = false;
    var isChangePhuDe = false;
    var isChangeNoiDung = false;
    $("#button-check").click(function () {
        var content = CKEDITOR.instances['txtnoidung'].getData();
        var tieuDe = CKEDITOR.instances['tieuDe'].getData();
        var phuDe = CKEDITOR.instances['phuDe'].getData();

       
       
        var cbTieuDe = $("#cbTieuDe")[0].checked;
        var cbNoiDung = $("#cbNoiDung")[0].checked;
        var test = $('#oldText').val();

        var obj = {
            NoiDung: content,
            TieuDe: tieuDe,
            PhuDe: phuDe,
            IsTieuDe: cbTieuDe,
            IsNoiDung: cbNoiDung
        }
        var url = $(this).data('url');
        isChangeNoiDung = true;
        isChangePhuDe = true;
        isChangeTieuDe = true;
        $.ajax({
            url: "/Admin/PostManager/CheckCk",
            data: JSON.stringify(obj),
            type: "Post",
            contentType: "application/json",
            dataType: "json",
            success: function (res) {
                console.log("ok", res);
                //if ($('#oldText').val() == '') {
                //    $("#oldText").val(content);
                //}
                //if ($('#oldTieuDe').val() == '') {
                //    $("#oldTieuDe").val(tieuDe);
                //}
                //if ($('#oldPhuDe').val() == '') {
                //    $("#oldPhuDe").val(phuDe);
                //}
                isChangeNoiDung = true;
                isChangePhuDe = true;
                isChangeTieuDe = true;
                CKEDITOR.instances['txtnoidung'].setData(res.NoiDung);
                CKEDITOR.instances['tieuDe'].setData(res.TieuDe);
                CKEDITOR.instances['phuDe'].setData(res.PhuDe);
                isChangeNoiDung = true;
                isChangePhuDe = true;
                isChangeTieuDe = true;
                var stringcam = "";
                for (var i = 0; i < res.TuCams.length; i++) {
                    stringcam += res.TuCams[i] + " ";
                }
                if (res.TuCams.length> 0)
                    $('#content').html("Có tồn tại từ bị cấm");
                else
                    $('#content').html("Không tồn tại từ bị cấm");
                $('#tucams')[0].innerText = stringcam;
            },
            error: function (res) {
                console.log("errr");
            }
        })
    })


    $("#button-thaythe").click(function () {
        var content = CKEDITOR.instances['txtnoidung'].getData();
        var tieuDe = CKEDITOR.instances['tieuDe'].getData();

        var phuDe = CKEDITOR.instances['phuDe'].getData();
        var bandau = $('#oldText').val();
        var oldTieuDe = $('#oldTieuDe').val();
        var oldPhuDe = $('#oldPhuDe').val();
        if (bandau!="")
            content = bandau;
        if (oldTieuDe != "")
            tieuDe = oldTieuDe;
        if (oldPhuDe != "")
            phuDe = oldPhuDe;
      
        var cbTieuDe = $("#cbTieuDe")[0].checked;
        var cbNoiDung = $("#cbNoiDung")[0].checked;

        var obj = {
            NoiDung: content,
            TieuDe: tieuDe,
            PhuDe: phuDe,
            IsTieuDe: cbTieuDe,
            IsNoiDung: cbNoiDung
        }

        $.ajax({
            url: "/Admin/PostManager/ThayThe",
            data: JSON.stringify(obj),
            type: "Post",
            contentType: "application/json",
            dataType: "json",
            success: function (res) {
                console.log("ok", res);              
                CKEDITOR.instances['txtnoidung'].setData(res.NoiDung);
                CKEDITOR.instances['tieuDe'].setData(res.TieuDe);
                CKEDITOR.instances['phuDe'].setData(res.PhuDe);
                $('#content').html("Đã thay thế");
            },
            error: function (res) {
                console.log("errr");
            }
        })
    })
    //Nhận diện IA
    $("#kiemtraIA").click(function () {
        var content = CKEDITOR.instances['txtnoidung'].getData();
        var tieuDe = CKEDITOR.instances['tieuDe'].getData();
        var phuDe = CKEDITOR.instances['phuDe'].getData();       
        var obj = {
            NoiDung: content,
            TieuDe: tieuDe,
            PhuDe: phuDe,
            IsTieuDe: cbTieuDe,
            IsNoiDung: cbNoiDung
        }
        $.ajax({
            url: "/Admin/PostManager/DinhDangIA",
            data: JSON.stringify(obj),
            type: "Post",
            contentType: "application/json",
            dataType: "json",
            success: function (res) {
                console.log("ok", res);
                CKEDITOR.instances['txtnoidung'].setData(res.NoiDung);
                CKEDITOR.instances['tieuDe'].setData(res.TieuDe);
                CKEDITOR.instances['phuDe'].setData(res.PhuDe);
                $('#content').html("Đã kiểm tra IA");
            },
            error: function (res) {
                console.log("errr");
            }
        })
    })

   
    CKEDITOR.instances['tieuDe'].on('change', function () {
        if (!isChangeTieuDe)
        {
            $("#oldTieuDe").val(CKEDITOR.instances['tieuDe'].getData());
            $("#TieuDeResult").val(CKEDITOR.instances["tieuDe"].document.getBody().getText())
        }
        isChangeTieuDe = false;
    });
    CKEDITOR.instances['phuDe'].on('change', function () {
        if (!isChangePhuDe) {
            $("#oldPhuDe").val(CKEDITOR.instances['phuDe'].getData());
            $("#PhuDeResult").val(CKEDITOR.instances["phuDe"].document.getBody().getText())
        }
        isChangePhuDe = false;
    });
    CKEDITOR.instances['txtnoidung'].on('change', function () {
        if (!isChangeNoiDung) {
            $("#oldText").val(CKEDITOR.instances['txtnoidung'].getData());
        }
        isChangeNoiDung = false;
    });
   
    //list
    $("#dangbai").click(function () {
        var content = CKEDITOR.instances['txtnoidung'].getData();
        var tieuDe = CKEDITOR.instances["tieuDe"].document.getBody().getText();
        var phuDe = CKEDITOR.instances["phuDe"].document.getBody().getText();
        var linkAnh = $('#txt_avt_post').val();
        var idTheLoai = $('#IDTheLoai').val();
        var idThe = $('#selectThe').val();
        var obj = {
            NoiDung: content,
            TieuDe: phuDe,
            TenBaiDang: tieuDe,
            AnhDaiDien: linkAnh,
            IsDangBai: true,
            IDTheLoai: idTheLoai,
            IDThe: idThe,
        }
        var url = $(this).data('url');
        $.ajax({
            url: "/Admin/PostManager/Create2",
            data: JSON.stringify(obj),
            type: "Post",
            contentType: "application/json",
            dataType: "json",
            success: function (res) {
                window.location = res;
                console.log("ok", res);
            },
            error: function (res) {
                console.log("errr", res);
            }
        })
    })
    $("#dangnhap").click(function () {
        var content = CKEDITOR.instances['txtnoidung'].getData();
        var tieuDe = CKEDITOR.instances["tieuDe"].document.getBody().getText();
        var phuDe = CKEDITOR.instances["phuDe"].document.getBody().getText();
        var linkAnh = $('#txt_avt_post').val();
        var idTheLoai = $('#IDTheLoai').val();
        var idThe = $('#selectThe').val();
        var obj = {
            NoiDung: content,
            TieuDe: phuDe,
            TenBaiDang: tieuDe,
            AnhDaiDien: linkAnh,
            IsDangBai: false,
            IDTheLoai: idTheLoai,
            IDThe: idThe,
        }
        var url = $(this).data('url');
        $.ajax({
            url: "/Admin/PostManager/Create2",
            data: JSON.stringify(obj),
            type: "Post",
            contentType: "application/json",
            dataType: "json",
            success: function (res) {
                window.location = res;
                console.log("ok", res);
            },
            error: function (res) {
                console.log("errr");
            }
        })
    })

    $("#capnhatbai").click(function () {
        var content = CKEDITOR.instances['txtnoidung'].getData();
        var tieuDe = CKEDITOR.instances["tieuDe"].document.getBody().getText();
        var phuDe = CKEDITOR.instances["phuDe"].document.getBody().getText();
        var linkAnh = $('#txt_avt_post').val();
        var idTheLoai = $('#IDTheLoai').val();
        var idThe = $('#selectThe').val();
        var IdBaiDang = $('#IDBaiDang').val();
        var obj = {
            NoiDung: content,
            TieuDe: phuDe,
            TenBaiDang: tieuDe,
            AnhDaiDien: linkAnh,
            IsDangBai: true,
            IDTheLoai: idTheLoai,
            IDThe: idThe,
            IDBaiDang: IdBaiDang,
        }
        var url = $(this).data('url');
        $.ajax({
            url: "/Admin/PostManager/UpdatePost",
            data: JSON.stringify(obj),
            type: "Post",
            contentType: "application/json",
            dataType: "json",
            success: function (res) {
                window.location = res;
                console.log("ok", res);
            },
            error: function (res) {
                console.log("errr");
            }
        })
    })
    $("#capnhatnhap").click(function () {
        var content = CKEDITOR.instances['txtnoidung'].getData();
        var tieuDe = CKEDITOR.instances["tieuDe"].document.getBody().getText();
        var phuDe = CKEDITOR.instances["phuDe"].document.getBody().getText();
        var linkAnh = $('#txt_avt_post').val();
        var idTheLoai = $('#IDTheLoai').val();
        var idThe = $('#selectThe').val();
        var IdBaiDang = $('#IDBaiDang').val();
        var obj = {
            NoiDung: content,
            TieuDe: phuDe,
            TenBaiDang: tieuDe,
            AnhDaiDien: linkAnh,
            IsDangBai: false,
            IDTheLoai: idTheLoai,
            IDThe: idThe,
            IDBaiDang: IdBaiDang
        }
        var url = $(this).data('url');
        $.ajax({
            url: "/Admin/PostManager/UpdatePost",
            data: JSON.stringify(obj),
            type: "Post",
            contentType: "application/json",
            dataType: "json",
            success: function (res) {
                window.location = res;
                console.log("ok", res);
            },
            error: function (res) {
                console.log("errr");
            }
        })
    })

    $("#xemtruoc").click(function () {
        var content = CKEDITOR.instances['txtnoidung'].getData();
        var tieuDe = CKEDITOR.instances["tieuDe"].document.getBody().getText();
        var phuDe = CKEDITOR.instances["phuDe"].document.getBody().getText();
        var linkAnh = $('#txt_avt_post').val();
        var idTheLoai = $('#IDTheLoai').val();
        var idThe = $('#selectThe').val();
        $('#title').html(tieuDe);
        $('#sub-title').html(phuDe);
        $('#contentx').html(content);
        //var obj = {
        //    NoiDung: content,
        //    TieuDe: phuDe,
        //    TenBaiDang: tieuDe,
        //    AnhDaiDien: linkAnh,
        //    IsDangBai: true,
        //    IDTheLoai: idTheLoai,
        //    IDThe: idThe,
        //}
        //var url = $(this).data('url');
        //$.ajax({
        //    url: "/Admin/PostManager/XemTruoc",
        //    data: JSON.stringify(obj),
        //    type: "Post",
        //    contentType: "application/json",
        //    dataType: "json",
        //    success: function (res) {
        //        placeholderElement.html(data);
        //        placeholderElement.find('.modal').modal('show');
        //        $(".hidden-search-box").select2({
        //            minimumResultsForSearch: Infinity
        //        });
        //    },
        //    error: function (res) {
        //        console.log("errr",res);
        //    }
        //})
    })



    //Sự kiện cho các nút bên trong bảng
    $('#div_table').on('click', '.btnTable', function () {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
            $(".hidden-search-box").select2({
                minimumResultsForSearch: Infinity
            });
        });
    });

});