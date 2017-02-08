$(document).ready(function () {
    $('#forwardmonth').click(function () {
        var month = $('header').find('h1').text();
        if (month == ('January')) { $('#month').html('February'); }
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

    $('#backmonth').click(function () {
        var month = $('header').find('h1').text();
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
