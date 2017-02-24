$(document).ready(function () {
    Date.prototype.daysInMonth = function () {
        return 32 - new Date(this.getFullYear(), this.getMonth(), 32).getDate();
    };

    $("ul#menu-items li a").each(function () {
        var location = window.location.href;
        var link = this.href;
        if (location == link)
        {
            $(this).addClass("active-link");
        }
        console.log(link+" "+location);
    });
    

    $("form #quick-search-input").focus(function () {
        $("form .disp-trig").removeClass("disp-false");
        $("form .disp-trig").addClass("disp-true");
        return;
    });

    $("form#quick-search-form input").blur(function (event) {
        if ($(event.relatedTarget).hasClass('disp-trig')) return;
        else
        {
            $("form .disp-trig").removeClass("disp-true");
            $("form .disp-trig").addClass("disp-false");
        }
    });

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
        $("form .disp-trig").removeClass("disp-false");
        $("form .disp-trig").addClass("disp-true");
        $("#calendar-wrapper").toggleClass("cal-inactive");
        $("#calendar-wrapper").toggleClass("cal-active");
    });


    $('div#forward-month').click(function (event) {
        event.stopPropagation();
        $("form .disp-trig").removeClass("disp-false");
        $("form .disp-trig").addClass("disp-true");
        $("div#calendar>ul li.block").removeClass('c-d-inactive');
        move_calendar(1);
    });

    $('div#back-month').click(function (event) {
        event.stopPropagation();
        $("form .disp-trig").removeClass("disp-false");
        $("form .disp-trig").addClass("disp-true");
        $("div#calendar>ul li.block").removeClass('c-d-inactive');
        move_calendar(-1);
    });


    function move_calendar(step)
    {
        var months = ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'];
        var curmonth = parseInt($("h1#month").attr("cur-month"));
        var curyear = parseInt($("h1#month").attr("cur-year"));
        var newmonth = curmonth + step;
        
        if (newmonth < 0) { newmonth = 11; curyear--; $("h1#month").attr("cur-year", curyear); }
        else if (newmonth > 11) { newmonth = 0; curyear++; $("h1#month").attr("cur-year", curyear); }


        d = new Date(curyear, newmonth);
        var day = 1;
        var days = d.daysInMonth();
        var firstday =d.getDay()-1;
        var i = 0;

        if (firstday < 0) firstday = 6;
        else if (firstday>6) firstday=0;

        $("h1#month").text(months[newmonth]);
        $("h1#month").attr("cur-month",newmonth);

        while(i != firstday)
        {
            if (i > 35) break;
            var li = "li.c-d-" + i;
            $(li).text("");
            $(li).addClass('c-d-inactive');
            i++;
        }

        while (day <= days)
        {
            if (i > 35) break;
            var li = "li.c-d-" + i;
            $(li).removeClass('c-d-inactive');
            $(li).text(day);
            day++;
            i++;
        }
        while (day > days)
        {
            if (i > 35) break;
            var li = "li.c-d-" +i;
            $(li).addClass('c-d-inactive');
            $(li).text("");
            day++;
            i++;
        }
    }

    $('button#header-order-button').click(function () {
        $('div#site-order-wrapper').removeClass('hidden');
    });
    $('button#header-order-close').click(function () {
        $('div#site-order-wrapper').addClass('hidden');
    });
});
