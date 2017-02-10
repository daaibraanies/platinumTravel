$(document).ready(function () {
    Date.prototype.daysInMonth = function () {
        return 32 - new Date(this.getFullYear(), this.getMonth(), 32).getDate();
    };


    $("#quick-search-date-from").click(function (event) {
        event.stopPropagation();
        if ($("#quick-search-date-to").hasClass("date-here"))
        {
            $("#quick-search-date-to").removeClass("date-here");
            if ($("#calendar-wrapper").hasClass("cal-inactive"))
            {
                $("#calendar-wrapper").removeClass("cal-inactive");
                $("#calendar-wrapper").addClass("cal-active");
            }
            $(this).addClass("date-here");
            move_calendar(0);
        }
        else
        {
            move_calendar(0);
            $("#calendar-wrapper").toggleClass("cal-inactive");
            $("#calendar-wrapper").toggleClass("cal-active");
            $(this).addClass("date-here");
        }
    });

    $("#quick-search-date-to").click(function (event) {
        event.stopPropagation();
        if ($("#quick-search-date-from").hasClass("date-here"))
        {
            $("#quick-search-date-from").removeClass("date-here");
            if ($("#calendar-wrapper").hasClass("cal-inactive"))
            {
                $("#calendar-wrapper").removeClass("cal-inactive");
                $("#calendar-wrapper").addClass("cal-active");
            }
            $(this).addClass("date-here");
            move_calendar(0);
        }
        else
        {
            move_calendar(0);
            $("#calendar-wrapper").toggleClass("cal-active");
            $("#calendar-wrapper").toggleClass("cal-inactive");
            $(this).addClass("date-here");
        }

    });


    $(document).click(function (event) {
        if ($(event.target).closest(".cal-trigger").length)
            return;
        $("#calendar-wrapper").addClass("cal-inactive");
        $("#calendar-wrapper").removeClass("cal-active");
        event.stopPropagation();
    });


    $("li.cal-date").click(function () {
        var day = parseInt($(this).text());
        var month = parseInt($("#month").attr("cur-month")) + 1;
        if (day < 10) day = "0" + day;
        if (month < 10) month = "0" + month;
        day = day + "." + month + "." + $("#month").attr("cur-year");
        $(".date-here").val(day);
        $(".date-here").removeClass("date-here");
        $("#calendar-wrapper").toggleClass("cal-inactive");
        $("#calendar-wrapper").toggleClass("cal-active");
    });


    $('#forward-month').click(function (event) {
        event.stopPropagation();
        $("#calendar>ul li.block").removeClass('c-d-inactive');
        move_calendar(1);
    });

    $('#back-month').click(function (event) {
        event.stopPropagation();
        $("#calendar>ul li.block").removeClass('c-d-inactive');
        move_calendar(-1);
    });


    //ЗАПИЛИТЬ ОБРАЩЕНИЕ К ЛИ ЧЕРЕЗ АЙДИШНИКА!!!!Г!Р"

    function move_calendar(step)
    {
        var months = ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'];
        var curmonth = parseInt($("#month").attr("cur-month"));
        var curyear = parseInt($("#month").attr("cur-year"));
        var newmonth = curmonth + step;
        
        if (newmonth < 0) { newmonth = 11; }
        else if (newmonth > 11) { newmonth = 0; }

        d = new Date(curyear, newmonth);
        var day = 1;
        var days = d.daysInMonth();
        var firstday = d.getDay() - 1;
        var i = 0;
        delete d;
        $("#month").text(months[newmonth]);
        $("#month").attr("cur-month",newmonth);

        while(i != firstday)
        {
            var li = "li.c-d-" + i;
            $(li).text("");
            $(li).addClass('c-d-inactive');
            i++;
        }

        while (day <= days)
        {
            var li = "li.c-d-" + i;
            $(li).removeClass('c-d-inactive');
            $(li).text(day);
            day++;
            i++;
        }
        while (day > days && day < 35)
        {
            var li = "li.c-d-" +i;
            $(li).addClass('c-d-inactive');
            $(li).text("");
            day++;
            i++;
        }
     
    }

});
