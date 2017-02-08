$(document).ready(function () {

    $("#quick-search-date-from").click(function () {
        if ($("#quick-search-date-to").hasClass("date-here"))
        {
            $("#quick-search-date-to").removeClass("date-here");
            $(this).addClass("date-here");
        }
        else
        {
            $("#calendar-wrapper").toggleClass("cal-inactive");
            $("#calendar-wrapper").toggleClass("cal-active");
            $(this).addClass("date-here");
        }
    });

    $("#quick-search-date-to").click(function () {
        if ($("#quick-search-date-from").hasClass("date-here"))
        {
            $("#quick-search-date-from").removeClass("date-here");
            $(this).addClass("date-here");
        }
        else
        {
            $("#calendar-wrapper").toggleClass("cal-active");
            $("#calendar-wrapper").toggleClass("cal-inactive");
            $(this).addClass("date-here");
        }
    });

    $("li.cal-date").click(function () {
        $(".date-here").val($(this).text());
        $(".date-here").removeClass("date-here");
        $("#calendar-wrapper").toggleClass("cal-inactive");
        $("#calendar-wrapper").toggleClass("cal-active");
    });




    $('#forward-month').click(function () {
        var month = $("#calendar-header").find('h1').text();
        if (month == ('January')) { $('#month').text('February'); }
        else if (month == ('February')) { $('#month').html('March'); }
        else if (month == ('March')) { $('#month').html('April'); }
        else if (month == ('April')) { $('#month').html('May'); }
        else if (month == ('May')) { $('#month').html('June'); }
        else if (month == ('June')) { $('#month').html('July'); }
        else if (month == ('July')) { $('#month').html('August'); }
        else if (month == ('August')) { $('#month').html('September'); }
        else if (month == ('September')) { $('#month').html('October'); }
        else if (month == ('October')) { $('#month').html('November'); }
        else if (month == ('November')) { $('#month').html('December'); }
    });

    $("#back-month").click(function () {
        var month = $("#calendar-header").find('h1').text();
        if (month == ('December')) { $('#month').html('November'); }
        else if (month == ('November')) { $('#month').html('October'); }
        else if (month == ('October')) { $('#month').html('September'); }
        else if (month == ('September')) { $('#month').html('August'); }
        else if (month == ('August')) { $('#month').html('July'); }
        else if (month == ('July')) { $('#month').html('June'); }
        else if (month == ('June')) { $('#month').html('May'); }
        else if (month == ('May')) { $('#month').html('April'); }
        else if (month == ('April')) { $('#month').html('March'); }
        else if (month == ('March')) { $('#month').html('February'); }
        else if (month == ('February')) { $('#month').html('January'); }
    });
});
