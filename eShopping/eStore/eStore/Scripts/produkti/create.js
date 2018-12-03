var produktiform = $('#__AjaxAntiForgeryForm');
var token = $('input[name="__RequestVerificationToken"]', produktiform).val();

var updateprodukti = function () {
    var jqXHRs = $.ajax({
        url: '/desk/PRODUKTI/Create',
        type: 'POST',
        async: false,
        data: {
            __RequestVerificationToken: token,
            ID: $("#ID").val(),
            Emertimi: $("#Emertimi").val(),
            CmimiBaze: $("#CmimiBaze").val(),
            TVSH: $("#TVSH").val(),
            Zbritja: $("#Zbritja").val(),
            ProdhuesiID: $("#ProdhuesiID").val(),
            Pershkrimi: $("#Pershkrimi").val(),
            Sasia: $("#Sasia").val(),
            Kodi: $("#Kodi").val()
         
        },
        success: function (result) {
            $("#ID").val(result.ID);
            loadkategorite();
            loadkategoritelista();
        },
        error: function (jqXHR, error, errorThrown) {
        }
    });

    //return jqXHRs.responseText;
    if (jqXHRs.status && jqXHRs.status == 400) {
        for (var i = 0; i < jqXHRs.responseJSON.length; i++) {
            var obj = jqXHRs.responseJSON[i];
            if (obj.key == "GjuhaID") {
                if (obj.key == "GjuhaID") {
                    $('select[name="' + obj.key + '"]').parent().addClass("has-error");
                    $('select[name="' + obj.key + '"]').addClass("error");
                    //$('select[name="' + obj.key + '"]').parent().find("span").text(obj.errors[0]);
                    $('#prodhuesimsg').text(obj.errors[0]);
                }
            }
            else {
                $('input[name="' + obj.key + '"]').parent().addClass("has-error");
                $('input[name="' + obj.key + '"]').addClass("error");
                $('input[name="' + obj.key + '"]').parent().find("span").text(obj.errors[0]);
            }
        }

        return false;
    }
    return true;
}

var loadkategorite = function () {
    var obj = { __RequestVerificationToken: token, id:$("#ID").val() };

    $.ajax({
        url: "/desk/PRODUKTI/LoadKategoria",
        cache: false,
        type: "POST",
        dataType: "html",
        data: obj,
        success: function (rs) {

            $("#listakategorive").html(rs);
        },
        error: function (jqXHR, error, errorThrown) {
            if (jqXHR.status && jqXHR.status == 400) {
                console.log(jqXHR.responseText);
            } else {
                console.log("Something went wrong");
            }
        }
    });
};

var shtokategorine = function (btn) {
    var kategoriaid = $(btn).data("kategoriaid");
    var obj = { __RequestVerificationToken: token, KategoriaID: kategoriaid, ProduktiID: $("#ID").val()};

    $.ajax({
        url: "/desk/PRODUKTI/AddKategori",
        cache: false,
        type: "POST",
        dataType: "html",
        data: obj,
        success: function (rs) {
            var retobj = JSON.parse(rs);
            loadkategorite();
            loadkategoritelista();
            $(btn).hide();
         

        },
        error: function (jqXHR, error, errorThrown) {
            if (jqXHR.status && jqXHR.status == 400) {
                console.log(jqXHR.responseText);
            } else {
                console.log("Something went wrong");
            }
        }
    });
};

var loadkategoritelista = function () {
    var obj = { __RequestVerificationToken: token, ProduktiID: $("#ID").val() };

    $.ajax({
        url: "/desk/PRODUKTI/LoadAddedKategorite",
        cache: false,
        type: "POST",
        dataType: "html",
        data: obj,
        success: function (rs) {

            $("#artikullinekategori").html(rs);


        },
        error: function (jqXHR, error, errorThrown) {
            if (jqXHR.status && jqXHR.status == 400) {
                console.log(jqXHR.responseText);
            } else {
                console.log("Something went wrong");
            }
        }
    });
};

var deletechosenkategoria = function (btn) {
    var ProduktiID = $('#ID').val();
    var KategoriaID = $(btn).data("id");

    var obj = { __RequestVerificationToken: token, ProduktiID: ProduktiID, KategoriaID: KategoriaID };

    $("#DeleteChosenKategoriaModal").modal();

    $.ajax({
        url: "/desk/PRODUKTI/DeleteChosenKategoria",
        cache: false,
        type: "POST",
        dataType: "html",
        data: obj,
        success: function (rs) {
            $("#DeleteChosenKategoriaContent").empty();
            $("#DeleteChosenKategoriaContent").html(rs);


        },
        error: function (jqXHR, error, errorThrown) {
            if (jqXHR.status && jqXHR.status == 400) {
                console.log(jqXHR.responseText);
            } else {
                console.log("Something went wrong");
            }
        }
    });


};

var loadmedialista = function () {
    var obj = { __RequestVerificationToken: token,id: $("#ID").val()};

    $.ajax({
        url: "/desk/PRODUKTI/LoadMediat",
        cache: false,
        type: "POST",
        dataType: "html",
        data: obj,
        success: function (rs) {

            $("#listamediave").html(rs);


        },
        error: function (jqXHR, error, errorThrown) {
            if (jqXHR.status && jqXHR.status == 400) {
                console.log(jqXHR.responseText);
            } else {
                console.log("Something went wrong");
            }
        }
    });
};

var shtomedia = function (btn) {
    var MediaID = $(btn).data("mediaid");
    var obj = { __RequestVerificationToken: token, MediaID: MediaID, ProduktiID: $("#ID").val() };

    $.ajax({
        url: "/desk/PRODUKTI/AddMedia",
        cache: false,
        type: "POST",
        dataType: "html",
        data: obj,
        success: function (rs) {
            var retobj = JSON.parse(rs);
            loadmedialista();
            loadmedianeProdukte();
            $(btn).hide();


        },
        error: function (jqXHR, error, errorThrown) {
            if (jqXHR.status && jqXHR.status == 400) {
                console.log(jqXHR.responseText);
            } else {
                console.log("Something went wrong");
            }
        }
    });
};

var loadmedianeProdukte = function () {
    var obj = { __RequestVerificationToken: token, ProduktiID: $("#ID").val() };

    $.ajax({
        url: "/desk/PRODUKTI/LoadAddedMedia",
        cache: false,
        type: "POST",
        dataType: "html",
        data: obj,
        success: function (rs) {

            $("#mediatneartikujt").html(rs);


        },
        error: function (jqXHR, error, errorThrown) {
            if (jqXHR.status && jqXHR.status == 400) {
                console.log(jqXHR.responseText);
            } else {
                console.log("Something went wrong");
            }
        }
    });
};

var deletechosenmedia = function (btn) {
    var ProduktiID = $('#ID').val();
    var MediaID = $(btn).data("id");

    var obj = { __RequestVerificationToken: token, ProduktiID: ProduktiID, MediaID: MediaID };

    $("#DeleteChosenMediaModal").modal();

    $.ajax({
        url: "/desk/PRODUKTI/DeleteChosenMedia",
        cache: false,
        type: "POST",
        dataType: "html",
        data: obj,
        success: function (rs) {
            $("#DeleteChosenMediaContent").empty();
            $("#DeleteChosenMediaContent").html(rs);


        },
        error: function (jqXHR, error, errorThrown) {
            if (jqXHR.status && jqXHR.status == 400) {
                console.log(jqXHR.responseText);
            } else {
                console.log("Something went wrong");
            }
        }
    });


};



$(document).ready(function () {

    $("#btnPerfundo").click(function () {

        var data = new FormData();
     
        data.append("__RequestVerificationToken", token);
        data.append("ID", $("#ID").val());
        data.append("Emertimi", $("#Emertimi").val());
        data.append("Pershkrimi", $("#Pershkrimi").val());
        data.append("Kodi", $("#Kodi").val());
        data.append("Sasia", $("#Sasia").val());
        data.append("CmimiBaze", $("#CmimiBaze").val());
        data.append("GjuhaID", $("#GjuhaID").val());
        data.append("TVSH", $("#TVSH").val());
        data.append("Zbritja", $("#Zbritja").val());
        data.append("Prodhuesi", $("#Prodhuesi").val());

        // Make Ajax request with the contentType = false, and procesDate = false
        var ajaxRequest = $.ajax({
            type: "POST",
            url: '/desk/Produkti/Perfundo',
            contentType: false,
            processData: false,
            data: data
        });

        ajaxRequest.done(function (xhr, textStatus) {
            // Do other operation
            location.href = "/Produkti/Index";
          

        });
    });

});




var form = $("#produktiform").show();

form.steps({
    headerTag: "h3",
    bodyTag: "fieldset",
    transitionEffect: "slideLeft",
    onStepChanging: function (event, currentIndex, newIndex) {
        if (currentIndex > newIndex) {
            return true;
        }

        console.log(currentIndex);
        if (currentIndex == 0) {

            if (window.location.href.indexOf("Edit") > -1) {
                return updateprodukti();
            }
            else {
                return updateprodukti();
            }
        }

        if (Number($("#ID").val()) == 0) {
            return false;
        }

        if (currentIndex == 1) {
            loadmedialista();
            loadmedianeProdukte();
        }
        else if (currentIndex == 2) {
        
        }


        // Allways allow previous action even if the current form is not valid!

        // Forbid next action on "Warning" step if the user is to young

        // Needed in some cases if the user went back (clean up)
        if (currentIndex < newIndex) {
            // To remove error styles
            //form.find(".body:eq(" + newIndex + ") label.error").remove();
            //form.find(".body:eq(" + newIndex + ") .error").removeClass("error");

            $("has-error").find("span").text("");
            $("has-error").removeClass("has-error");
            $("error").removeClass("error");
        }


        form.validate().settings.ignore = ":disabled,:hidden";
        return form.valid();
    },
    onStepChanged: function (event, currentIndex, priorIndex) {
        // Used to skip the "Warning" step if the user is old enough.
        if (currentIndex === 2 && Number($("#age-2").val()) >= 18) {
            form.steps("next");
        }
        // Used to skip the "Warning" step if the user is old enough and wants to the previous step.
        if (currentIndex === 2 && priorIndex === 3) {
            form.steps("previous");
        }
    },
    onFinishing: function (event, currentIndex) {

        $("#btnPerfundo").trigger("click");

        form.validate().settings.ignore = ":disabled";
        return form.valid();
    },
    onFinished: function (event, currentIndex) {
        //alert("Submitted!");
    }
}).validate({
    errorPlacement: function errorPlacement(error, element) { element.before(error); },
    rules: {
        confirm: {
            equalTo: "#password-2"
        }
    }
});
