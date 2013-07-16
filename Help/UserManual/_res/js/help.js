
$(function() {
    $('<div id="PageHeader"></div>)').append($('<p></p>').text(document.title)).prependTo('body');

    $('<p><br /><br /><br /></p>').appendTo('body');

    //	$('<div id="PageBottom"></div>')
    //		.html('明源数据访问层-用户手册')
    //		.appendTo('body');

    var height1 = $(window).height() - $("#PageHeader").height() - $("#PageBottom").height();
    var height2 = $("#bodyContent").height();
    if (height2 < height1)
        $("#bodyContent").css("height", (height1 - 15) + "px");
    else
        $("<p style='height: 53px'></p>").insertBefore('#PageBottom');

});
