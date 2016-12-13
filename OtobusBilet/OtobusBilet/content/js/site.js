$(function () {
    //-------------------------------------------------------------
    function alert(title, text) {
        var html = '';
        html += '<section class="ae-modal-scope default">';
        html += '<section class="ae-modal-box">';
        html += '<div class="ae-modal-body">';
        html += '<h2 class="heading">' + title + '</h2>';
        html += '<p class="caption">' + text + '</p>';
        html += '<div class="ae-modal-footer">';
        html += '<a href="" class="button blue">Sayfayı Yenile</a>';
        html += '<a class="button gray close">iptal</a>';
        html += '</div>';
        html += '</div>';
        html += '</section>';
        html += '</section>';

        $("body").append(html);

        $("body").on("click", ".ae-modal-scope .close", function () {
            $(this).parents(".ae-modal-scope").remove();
        });
    }
    function alertMesaj(title, text) {
        var html = '';
        html += '<section class="ae-modal-scope default">';
        html += '<section class="ae-modal-box">';
        html += '<div class="ae-modal-body">';
        html += '<h2 class="heading">' + title + '</h2>';
        html += '<p class="caption">' + text + '</p>';
        html += '<div class="ae-modal-footer">';
        html += '<a class="button blue close">Kapat</a>';
        html += '</div>';
        html += '</div>';
        html += '</section>';
        html += '</section>';

        $("body").append(html);

        $("body").on("click", ".ae-modal-scope .close", function () {
            $(this).parents(".ae-modal-scope").remove();
        });
    }
    //-------------------------------------------------------------
    var anasayfaLeftMenu = function () {
        $(".AnaSayfaIcerik:first").show();
        $(".contentAnaSayfaLeftMenu li").click(function () {
            var index = $(this).index();
            $(".AnaSayfaIcerik").hide();
            $(".AnaSayfaIcerik:eq(" + index + ")").fadeTo('slow', '0.9');
        });
    };
    //------------------------------------------------------------
    var anasayfaLeftMenuBack = function () {
        $(".contentAnaSayfaLeftMenu li:first").addClass("current");
        $("body").on("click", ".contentAnaSayfaLeftMenu li", function () {
            $(".contentAnaSayfaLeftMenu li").removeClass("current");
            $(this).addClass("current");
            var posizyon = $(this).position();
            $(".anasayfaLeftMenuBack").animate({
                marginTop: posizyon.top
            });
        })
    };

    //------------------------------------------------------------
    var takvimLarge = function () {
        $(".takvimLarge").hide();
        $("body").on("click", ".takvimSmall", function () {
            $(".takvimLarge").toggle();
        });
    };
    //------------------------------------------------------------
    var biletYon = function () {
        $("#tekYonRadio").prop("checked", true);
        $("body").on("click", "#rbTekYon", function () {
            if ($("#txtCiftYon").val() === "1")
                $(".donus").slideUp(500);
        });
        $("body").on("click", "#rbCiftYon", function () {
            if ($("#txtCiftYon").val() === "1")
                $(".donus").slideDown(500);
        });
    };
    //------------------------------------------------------------
    var koltukSec = function () {
        $("body").on("click", ".koltuk", function () {
            if ($(this).hasClass("koltukDolu")) {
                alertMesaj("Koltuk dolu", "lütfen boş olan bir koltuk seçiniz..");
            }
            else if ($(this).hasClass("koltukBos")) {
                var number = $(this).attr("number"),
                      kullaniciCinsiyet = $("#txtCinsiyet").val(),
                          koltuSayisi = 0,
                          yon;
                if ($(this).parents(".yon").hasClass("gidis")) {
                    yon = "gidis";
                }
                else if ($(this).parents(".yon").hasClass("donus")) {
                    yon = "donus";
                }
                if (yon === "gidis")
                    koltuSayisi = $("#txtGidisKoltukSayisi").val();
                else
                    koltuSayisi = $("#txtDonusKoltukSayisi").val();

                if (koltuSayisi > 28 && kullaniciCinsiyet != "") {
                    var yanKoltukSira = number,
                       yanKoltukCinsiyet = "",
                       koltukSira = number % 2;

                    if (koltukSira === 0) {
                        yanKoltukSira--;
                    }
                    else if (koltukSira === 1) {
                        yanKoltukSira++;
                    }
                    if ($(".koltuk[number=" + yanKoltukSira + "]").hasClass("koltukBos")) {
                        if (yon === "gidis") {
                            $("#txtGidisNo").val(number);
                            $("#txtGidisNo").attr("value", number);
                        }
                        else if (yon === "donus") {
                            $("#txtDonusNo").val(number);
                            $("#txtDonusNo").attr("value", number);
                        }
                    }
                    else {
                        if ($("." + yon + " .koltuk[number=" + yanKoltukSira + "]").hasClass("koltukArkaBayan"))
                            yanKoltukCinsiyet = "Bayan";
                        else if ($("." + yon + " .koltuk[number=" + yanKoltukSira + "]").hasClass("koltukArkaBay"))
                            yanKoltukCinsiyet = "Bay";

                        if (yanKoltukCinsiyet === kullaniciCinsiyet) {
                            if (yon === "gidis") {
                                $("#txtGidisNo").val(number);
                                $("#txtGidisNo").attr("value", number);
                            }
                            else if (yon === "donus") {
                                $("#txtDonusNo").val(number);
                                $("#txtDonusNo").attr("value", number);
                            }
                        }
                        else {
                            alertMesaj("Hatalı Koltuk Seçimi ", "Hemcinsiniz yanına oturunuz...");
                        }
                    }
                }
                else {
                    if (yon === "gidis") {
                        $("#txtGidisNo").val(number);
                        $("#txtGidisNo").attr("value", number);
                    }
                    else if (yon === "donus") {
                        $("#txtDonusNo").val(number);
                        $("#txtDonusNo").attr("value", number);
                    }
                }
            }
        });
    };

    anasayfaLeftMenu();
    biletYon();
    takvimLarge();
    anasayfaLeftMenuBack();
    koltukSec();

});