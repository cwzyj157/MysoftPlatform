$.fn.extend({
    bindRow: function () {
        var mode = $(this).find("option:selected").text();
        var index = 0;
        if (mode == "建筑面积") {
            index = 4;
        }
        if (mode == "套内面积") {
            index = 5;
        }
        if (mode == "套") {
            index = 6;
        }
        for (var i = 4; i < 7; i++) {
            val = $(this).find("td:eq(" + i.toString() + ")").text();
            if ($.trim(val) == "") {
                val = $(this).find("td:eq(" + i.toString() + ") input").val();
            }
            if (i == index) {
                var html = "<input type=\"text\" style=\"border:solid 1px black;width:60px;\" value=\"" + val + "\" />";
                $(this).find("td:eq(" + i.toString() + ")").html(html);
                $(this).find("td:eq(" + i.toString() + ") input").on("change", function () {
                    $(this).parent().parent().caclPrice();
                });
            }
            else {
                $(this).find("td:eq(" + i.toString() + ")").html(val.toString());
            }
        }
    },
    caclPrice: function () {
        var mode = $(this).find("option:selected").text();
        var val = 0;
        var area = 0;
        var tnarea = 0;
        area = parseFloat($(this).find("td:eq(1)").text());
        tnarea = parseFloat($(this).find("td:eq(2)").text());
        if (mode == "建筑面积") {
            val = parseFloat($(this).find("td:eq(4) input").val());
            var total = val * area;
            var tnprice = total / tnarea;
            $(this).find("td:eq(5)").text(tnprice.toFixed(2).toString());
            $(this).find("td:eq(6)").text(total.toFixed(2).toString());
        }
        if (mode == "套内面积") {
            val = parseFloat($(this).find("td:eq(5) input").val());
            var total = val * tnarea;
            var price = total / area;
            $(this).find("td:eq(4)").text(price.toFixed(2).toString());
            $(this).find("td:eq(6)").text(total.toFixed(2).toString());
        }
        if (mode == "套") {
            val = parseFloat($(this).find("td:eq(6) input").val());
            var tnprice = val / tnarea;
            var price = val / area;
            $(this).find("td:eq(4)").text(price.toFixed(2).toString());
            $(this).find("td:eq(5)").text(tnprice.toFixed(2).toString());
        }
    },
    jumpToUrl: function () {
        var url = location.href;
        var idx = url.indexOf("?");
        if (idx != -1) {
            url = url.substring(0, idx);
        }
        location.href = url + $(this).children('option:selected').val();
    },
    getData: function () {
        var data = "<data>";
        $(this).find("tr").each(function (i) {
            if (i == 0) {
                return true;
            }

            var val1 = $(this).find("td:eq(4)").text();
            if ($.trim(val1) == "") {
                val1 = $(this).find("td:eq(4) input").val();
            }

            var val2 = $(this).find("td:eq(5)").text();
            if ($.trim(val2) == "") {
                val2 = $(this).find("td:eq(5) input").val();
            }

            var val3 = $(this).find("td:eq(6)").text();
            if ($.trim(val3) == "") {
                val3 = $(this).find("td:eq(6) input").val();
            }

            var val4 = $(this).find("td:eq(10)").text();
            if ($.trim(val4) == "") {
                val4 = $(this).find("td:eq(10) input").val();
            }

            var val5 = $(this).find("option:selected").text();

            data += "<row oid=\"" + val4 + "\" price=\"" + val1 + "\" tnprice=\"" + val2 + "\" total=\"" + val3 + "\" djarea=\"" + val5 + "\"></row>";
        });
        data += "</data>";
        return data;
    }
});